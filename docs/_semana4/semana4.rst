Semana 4
===========

Unidad 2: plataforma ESP32
-----------------------------
En esta unidad vamos a estudiar la plataforma ESP32. Con esta
plataforma podremos hacer prototipos usando lo que hemos
aprendido con otros microcontroladores que soportan el framework
de arduino, pero además tendremos la posibilidad de conectarnos
a redes WiFi y bluetooth. Esto abre las puertas a la construcción de aplicaciones
interactivas con sensores y actuadores distribuidos y al mundo del
Internet de las cosas.

.. warning::
   ES MUY IMPORTANTE QUE LEAS CON DETENIMIENTO LA RÚBRICA DE EVALUACIÓN DE ESTA
   UNIDAD ANTES DE COMENZAR.

Propósitos de aprendizaje
^^^^^^^^^^^^^^^^^^^^^^^^^^
Familiarizarse con la plataforma ESP32 a nivel de hardware como a nivel de
software.

Repasar los conceptos de máquinas de estado estudiados en sensores 1.

Actividad de aprendizaje
^^^^^^^^^^^^^^^^^^^^^^^^^^

Se realizará las SEMANAS 4,5,6

Lee con detenimiento el código de honor y luego los pasos que
debes seguir para evidenciar esta actividad.

Código de honor
###################
Para realizar este reto se espera que hagas lo siguiente:

* Colabora con tus compañeros cuando así se indique.
* Trabaja de manera individual cuando la actividad así te lo
  proponga.
* Usa solo la documentación oficial del framework del controlador
  y de la plataforma interactiva.
* NO DEBES utilizar sitios en Internet con soluciones o ideas para
  abordar el problema.
* NO DEBES hacer uso de foros.
* ¿Entonces qué hacer si no me funciona algo? Te propongo que
  experimentes, crea hipótesis, experimenta de nuevo, observa y concluye.
* NO OLVIDES, este curso se trata de pensar y experimentar NO de
  BUSCAR soluciones en Internet.

Enunciado
##########

El siguiente ejercicio ya lo hiciste en sensores 1. Ahora vamos
a aumentar la complejidad y a migrar el código a plataforma ESP32.

Se requiere construir una aplicación para controlar una bomba temporizada.
sin embargo, a diferencia del ejercicio en sensores 1, la interfaz de usuario
de la bomba estará implementada en un celular.

Para controlador la bomba debes implementar una aplicación móvil con:

* Tres botones, denominados UP, DOWN, ARM.
* Muestra en el celular toda la información relacionada con la programación
  del tiempo y con el conteo regresivo.
* Cuando la bomba se active has que el celular vibre y la interfaz de usuario
  cambie.

Ten cuenta los siguientes ítems de funcionamiento:

* Inicia en modo de configuración, es decir, no cuenta aún, la bomba está
  ``desarmada``. El valor inicial del conteo regresivo es de 20 segundos.
* En el modo de configuración, los botones UP y DOWN permiten
  aumentar o disminuir el tiempo inicial de la bomba.
* El tiempo se puede programar entre 10 y 60 segundos con cambios de 1 segundo.
* El tiempo de configuración se debe visualizar en el celular.
* El botón ARM arma la bomba.
* Una vez armada la bomba, comienza la cuenta regresiva que será visualizada
  en el celular.
* La bomba explotará cuando el tiempo llegue a cero. Implementa un mecanismo
  en la aplicación móvil para reiniciar la bomba al modo de configuración.
* Una vez la bomba esté armada es posible desactivarla ingresando un código
  de seguridad. El código será la siguiente secuencia de pulsadores
  presionados uno después de otro:  UP, DOWN, DOWN, UP, UP, ARM.
* Si la secuencia se ingresa correctamente la bomba pasará de nuevo
  al modo de configuración de lo contrario continuará la fatal cuenta
  regresiva.

¿Qué debes entregar?
######################

* Crea una carpeta, la llamaras principal. 
* En la carpeta principal guarda una copia de la `rúbrica <https://docs.google.com/spreadsheets/d/1_SnplUHVGTYiaCIoOhpE8pIeao14Ch0AjKFVGRYM0W4/edit?usp=sharing>`__
  con tu autoevaluación.
* En la carpeta principal guarda un archivo .pdf donde colocarás dos cosas:
  
  * UN ENLACE a tu ONE DRIVE donde estará alojado el video de sustentación.
  * Una tabla de contenidos que indique el instante de tiempo en el cual se
    pueden encontrar cada una de las secciones solicitadas en el video.

* Comprime la carpeta principal en formato .ZIP
* Entrega el archivo .ZIP `aquí <https://upbeduco-my.sharepoint.com/:f:/g/personal/juanf_franco_upb_edu_co/ElJszzl9uABAnqoyRfiRRSoBP0j9wj9Cnlu6gs-C793pKA`__
  antes de la sesión del 14 de agosto a las 10 a.m. En esta sesión realizaremos la retroalimentación final.

¿Qué deberá tener el video de sustentación?
#############################################

* Máximo 20 minutos: debes planear el video tal como aprendiste en segundo 
  semestre en tu curso de narrativa audiovisual.
* Cuida la calidad del audio y del video.
* Sección 1: introducción, donde dirás tu nombre y si realizaste el RETO
  completo. Si no terminaste indica claramente qué te faltó y por qué.
* Sección 2: explica la arquitectura de tu solución. Cómo funciona la app móvil,
  cómo funciona la plataforma ESP32. Realiza las máquinas de estado que modelan
  la solución en el ESP32.
* Sección 3: explica el código de tus soluciones. 
* Sección 4: realiza una demostración de funcionamiento de tu solución.
* Sección 5: explica cómo vas a probar cada programa por separado y en conjunto 
* Sección 6: muestra que tu programa funciona según los escenarios de prueba
  identificados.
* Tus explicaciones deben ser claras, precisas y completas. No olvides planear 
  bien tu video de sustentación.

Trayecto de acciones, tiempos y formas de trabajo
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

Fase 1 (motivación)
######################

* Fecha: julio 29 de 2020 - 10 a.m.
* Descripción: asiste al encuentro sincrónico donde se introducirá
  la actividad y las plataformas de trabajo.
* Recursos: ingresa al grupo de Teams
* Duración de la actividad: 1 hora 40 minutos.
* Forma de trabajo: grupal

Fase 2 (diagnóstico-repaso)
############################

* Fecha: julio 29 a julio 31 de 2020
* Descripción: revisa el ejercicio de la bomba realizado en sensores 1, realiza
  el modelo de máquina de estados e implementa la solución.
* Recursos: semanas 4 y 5 del curso `sensores 1 2020-10 <https://sensores1.readthedocs.io/es/v2020.10/_semana4/semana4.html>`__
* Duración de la actividad: 5 horas
* Forma de trabajo: individual con solución de dudas en tiempo real

Fase 3 (fundamentación)
#############################

* Fecha: julio 31  de 2020
* Descripción: muestra el proyecto de sensores 1 funcionando 
  y soluciona tus dudas en tiempo real.
* Recursos: ingresa al grupo en Teams 
* Duración de la actividad: 1 hora 40 minutos.
* Forma de trabajo: individual

Material y enlaces
^^^^^^^^^^^^^^^^^^^

* Para trabajar con el ESP32 vas a instalar el soporte
  para esta plataforma en el IDE de arduino como indica 
  `este <https://github.com/espressif/arduino-esp32>`__ sitio.
* Para realizar la aplicación móvil te recomiendo `kodular <https://www.kodular.io/creator>`__
* Lista de materiales para este reto y los que siguen.

    * Dos `ESP32 <https://www.didacticaselectronicas.com/index.php/comunicaciones/bluetooth/tarjeta-de-desarrollo-esp32-wroom-32d-modulo-wifi-y-bluetooth-esp32u-con-conector-u-fl-tarjeta-comunicaci%C3%B3n-wi-fi-bluetooth-esp32u-iot-esp32-nodemcu-d0wd-9368-9386-detail>`__
      (para este reto solo necesitas uno). SI PUEDES comprar Y PEDIR
      que te suelden `ESTE <https://www.didacticaselectronicas.com/index.php/comunicaciones/wi-fi/wifi,-wi-fi,-bluetooth-internet-iot-tarjeta-desarrollo-esp32-detail>`__
      mejor.
    * LEDs
    * Potenciómetros
    * Protoboard
    * Resistencias
    * Pulsadores
    * `BME280 <https://www.didacticaselectronicas.com/index.php/sensores/presion-atm/sensor-de-presion-atmosferica-bmp280-sensores-de-presion-relativa-atmosferica-barometros-bmp180-detail>`__
    * `Reloj de tiempo real <https://www.didacticaselectronicas.com/index.php/semiconductores/reloj-de-tiempo-real/shield-ds1307-rtc-para-wemos-d1-mini-wemos-sh-rtc-reloj-tiempo-real-relojes-de-tiempo-real-rtcs-wemos-detail>`__

Si compras los dispositivos en didácticas electrónicas pide que te suelden
las partes.