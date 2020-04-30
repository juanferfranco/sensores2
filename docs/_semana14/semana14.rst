Semana 14
===========
Durante las semanas 14, 15 y 16 vamos a realizar un sistema de aplicaciones
usando Unity versión 2019.3.10f1 y Arduino. 

Considere:

* Vamos a conectar 3 aplicaciones en 3 dispositivos diferentes así:

    * Un ESP32
    * Un dispositivo celular.
    * Un computador.

* Realice una aplicación para cada dispositivo.
* El ESP32 deberá tener un actuador y un sensor.
* Para el celular deberá hacer una aplicación en Unity o TouchOSC que
  permitan modificar un GameObject y leer alguno de los sensores del dispositivo
  o un elemento de la interfaz de usuario (sliders, toggles, etc).
* La aplicación del computador será la aplicación principal.  Su función será:

   * Activar el actuador del ESP32.
   * Modificar el GameObject de la aplicación del celular.
   * Leer el sensor del ESP32 y el sensor del celular.

* Para la comunicación de los dispostivos utilice UDP y el protocolo OSC 1.0.
* Defina los mensajes, tags y argumentos a necesidad según OSC.
* La especificación del protocolo de comunicación OSC está `aquí <http://opensoundcontrol.org/spec-1_0>`__.
* DEBE implementar el protocolo, NO UTILICE paquetes del asset store.

Ejemplos de paquetes OSC
-------------------------
En `este <http://opensoundcontrol.org/spec-1_0-examples>`__
enlace se pueden ver algunos ejemplos de paquetes OSC.

Para entender la estructura de los paquetes OSC tenga en cuenta las siguientes consideraciones
de la especificación OSC 1.0:

* La comunicación en OSC se da por intercambio de paquetes.
* La aplicación que recibe paquetes se denomina servidor o SERVER
  y quien envía los paquetes cliente o CLIENT.
* Todos los paquetes en OSC deben ser múltiplos de 4 bytes.
* Los paquetes en OSC pueden ser MENSAJES o BUNDLES. Para el reto
  usaremos solo MENSAJES.
* Los OSC-MESSAGES tienen la siguiente estructura: OSC ADDRESS PATTERN + OSC TYPE TAG STRING + 0 o MÁS OSC ARGUMENTS
* OSC ADDRESS PATTERN: son OSC-STRINGS que comienzan por este carácter: /
* OSC TYPE TAG STRING: son OSC-STRINGS que comienzan por el carácter: ,
  y luego por tags que pueden ser: i f s b. Donde i indica que el mensaje
  tendrá un argumento entero, f un argumento en punto flotante, s una
  cadena y b un blob.
* Los tipos de argumentos o ATOMIC DATA TYPES son:

  int32: entero de 32 bits signado y en big-endian

  float32: número en punto flotante de 32 bits en formato 
  `IEEE 754 <https://www.h-schmidt.net/FloatConverter/IEEE754.html>`__
  en big-endian

  osc-string: cadena de caracteres ascii terminada con el carácter NULL 
  y 0 a 3 carácter NULL adicionales para lograr que la cadena sea múltiplo
  de 4 bytes o 32 bits.
 
  osc-blob y osc-time-tag, no los trabajeremos en este reto.

* Semántica de OSC: cada mensaje recibido por un servidor es potencialmente
  un llamado a un procedimiento cuyos argumentos serán los argumentos del
  mensaje.

Supanga que queremos enviar un mensaje con el siguiente OSC ADDRESS PATTERN:
\\"/oscillator/4/frequency\\" y como argumento un número en punto flotante dado
por 440.0. El paquete será así (entre paréntesis el carácter ascii
correspondiente)

2f (/)  6f (o)  73 (s)  63 (c)

69 (i)  6c (l)  6c (l)  61 (a)
 
74 (t)  6f (o)  72 (r)  2f (/)
 
34 (4)  2f (/)  66 (f)  72 (r)
 
65 (e)  71 (q)  75 (u)  65 (e)
 
6e (n)  63 (c)  79 (y)  0 ()
 
2c (,)  66 (f)  0 ()    0 ()
 
43 (C)  dc (Ü)  0 ()    0 ()

OSC ADDRESS PATTERN: \\"/oscillator/4/frequency\\"
Será una secuencia de caracteres ASCII terminados con NULL más 0 bytes NULL
porque la cantidad de bytes sería múltiplo de 4:

2f (/)  6f (o)  73 (s)  63 (c)

69 (i)  6c (l)  6c (l)  61 (a)
 
74 (t)  6f (o)  72 (r)  2f (/)
 
34 (4)  2f (/)  66 (f)  72 (r)
 
65 (e)  71 (q)  75 (u)  65 (e)
 
6e (n)  63 (c)  79 (y)  0 ()

OSC TYPE TAG STRING: \\",f\\":
2c (,)  66 (f)  0 ()    0 ()

Como tenemos solo un argumento, tendremos solo un TAG de
tipo f. La cadena termina con un carácter NULL y solo debemos adicionar
un carácter NULL para hacer OSC TYPE TAG STRING múltiplo de 4.

Finalmente el número 440.0 en formato IEEE 754 en big-endian será:

43 (C)  dc (Ü)  0 ()    0 ()

Ejemplo 
--------

El siguiente ejemplo muestra paquetes OSC usados para comunicar dos aplicaciones
interactivas. La primera aplicación es una drump machine construida usando
MAX/MSP. La segunda aplicación es una interfaz de usuario remota que
controlará la drum machine.

Estos son los paquetes OSC que enviará la aplicación de interfaz remota
a la aplicación drum machine:

* play:

    .. code-block:: csharp
       :lineno-start: 1

       "/play\x00\x00\x00,i\x00\x00\x00\x00\x00\x01"

* stop:

    .. code-block:: csharp
       :lineno-start: 1

       "/play\x00\x00\x00,i\x00\x00\x00\x00\x00\x00"

* Activar el beat 5 del instrumento 2:

    .. code-block:: csharp
       :lineno-start: 1

       "/c\x02\x00,ii\x00\x00\x00\x00\x05\x00\x00\x00\x01"

* Desactivar el beat 5 del instrumento 2:

    .. code-block:: csharp
       :lineno-start: 1

       "/c\x02\x00,ii\x00\x00\x00\x00\x05\x00\x00\x00\x00"

* Desactivar todos los beats del instrumento 1

    .. code-block:: csharp
       :lineno-start: 1

       "/c\x01\x00,ii\x00\x00\x00\x00\x11\x00\x00\x00\x00"

* Cambiar la velocidad del beat a 100. El rango está de 100 a 300.

    .. code-block:: csharp
       :lineno-start: 1

       "/speed\x00\x00,i\x00\x00\x00\x00\x00\x64"

La drum machine enviará este paquete a la interfaz remota para indicar
el beat que está reproduciendo en ese momento:

* Trama enviada para la aplicación remota indicando que está
  reproduciendo el beat 16:

    .. code-block:: csharp
       :lineno-start: 1
       
       2F 63 6f 75 6e 74 65 72 00 00 00 00 2c 69 00 00 00 00 00 10

Programas para realizar pruebas
---------------------------------

Para realizar pruebas se recomienda el programa `Scriptcommunicator <https://sourceforge.net/projects/scriptcommunicator/>`__.

Evaluación 5
-------------

Enunciado
^^^^^^^^^^^^
Hacer el sistema previamente propuesto.

Entrega
^^^^^^^^^^^^
* Crear una carpeta e incluir allí carpetas para cada aplicación.
* Comprima la carpeta en formato .ZIP, no .RAR, no 7ZIP. SOLO .ZIP
* Incluya un archivo .pdf debe tener: su nombre y el enlace al video con las sustentación.
* El video sustentación debe tener los siguientes elementos:

    * INTRODUCCIÓN: indicar si implementó todas las características del reto y en caso contrario
      cuáles le faltaron y por qué le faltaron. 
    * DEMOSTRACIÓN: mostrar funcionando el sistema completo.
    * EXPLICACIÓN: explicar en datalle cada una de las aplicaciones realizadas así:

      * Primero explicar en un TABLERO electrónico cuál es la arquitectura de los programas, que partes tiene,
        cómo es el concepto de su solución y por qué lo solucionó de esta manera.
      * Segundo, EXPLICAR en el tablero qué mensajes OSC definió
      * Tercero, explicar el código de las aplicaciones.
      * Cuarto, explicar cómo implementó el protocolo OSC.
      * Quinto, indicar qué dificultades tuvo y cómo las superó.

* Subir el trabajo `aquí <https://www.dropbox.com/request/I0u99QqtrvnEuu56RwVu>`__
* El plazo es hasta el viernes 15 de mayo hasta las 6 p.m.
