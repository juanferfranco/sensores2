Unidad 4. Aplicaciones interactivas distribuidas
=====================================================

Introducción
--------------

En esta unidad vamos a integrar a la aplicación interactiva
otras aplicaciones externas mediante el protocolo de comunicación OSC

Propósito de aprendizaje
^^^^^^^^^^^^^^^^^^^^^^^^^^

Crear aplicaciones interactivas de tiempo real que integren
sensores, actuadores y aplicaciones mediante protocolos de comunicación
inalámbrica.

Temas
^^^^^^

Integración de aplicaciones mediante OSC.

Trayecto de actividades
------------------------

Ejercicio 1: RETO
^^^^^^^^^^^^^^^^^^^

Construye un aplicación interactiva en Unity que se comunique
con otra aplicación utilizando OSC. PERO HAY un RETO. Debes
implementar en Unity el protocolo tu mismo. ¿Por qué?
Como un ejercicio para comprender mejor el protocolo.

La especificación del protocolo de comunicación OSC está 
`aquí <http://opensoundcontrol.org/spec-1_0>`__.

¿Cómo es un paquete OSC?

En `este <http://opensoundcontrol.org/spec-1_0-examples>`__
enlace se pueden ver algunos ejemplos de paquetes OSC.

Para entender la estructura de los paquetes OSC ten en cuenta las siguientes consideraciones
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

Considera que queremos enviar un mensaje con el siguiente OSC ADDRESS PATTERN:
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

       "/c\x32\x00,ii\x00\x00\x00\x00\x05\x00\x00\x00\x01"

* Desactivar el beat 5 del instrumento 2:

    .. code-block:: csharp
       :lineno-start: 1

       "/c\x32\x00,ii\x00\x00\x00\x00\x05\x00\x00\x00\x00"

* Desactivar todos los beats del instrumento 1

    .. code-block:: csharp
       :lineno-start: 1

       "/c\x31\x00,ii\x00\x00\x00\x00\x11\x00\x00\x00\x00"

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

Ejercicio 2
^^^^^^^^^^^^^^^^^^^

Explorar algunas aplicaciones con las cuales podrás realizar
el proyecto de esta unidad.

Te voy a dejar dos:

`TouchOSC <https://hexler.net/products/touchosc>`__
`Node-RED <https://nodered.org/>`__


Ejercicio 3: proyecto
^^^^^^^^^^^^^^^^^^^^^^^^

Ahora piensa que quieres hacer de proyecto; sin embargo, ten presente estos
elementos mínimos:

* Debes incluir al menos dos ESP32.

* Cada ESP32 debe utilizar un sensor/actuador diferente.

* Debes usar al menos un sensor I2C y otro SPI.

* La integración entre los ESP32 y el PC la debes hacer utilizando
  OSC.

* Debes incluir en el proyecto al menos una aplicación que interactúe
  con tu aplicación interactiva usando OSC. 

* La configuración y el control de tu aplicación interactiva debe realizarse 
  mediante una interfaz de usuario gráfica.

Recuerda que antes de comenzar el proyecto debes reunirte con tu profesor para discutir
los conceptos de la unidad y obtener luz verde para comenzar a trabajar en tu proyecto.
