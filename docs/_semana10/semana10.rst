Semana 10: Unidad 4 - sensores y actuadores I2C
=================================================

En esta unidad vamos a estudiar el bus de comunicaciones I2C. Este bus
nos permitirá conectar a los controladores sensores y actuadores
complejos.

.. warning::
   ES MUY IMPORTANTE QUE LEAS CON DETENIMIENTO LA RÚBRICA DE EVALUACIÓN DE ESTA
   UNIDAD ANTES DE COMENZAR.

Propósitos de aprendizaje
-----------------------------
Comprender el funcionamiento del bus de comunicaciones I2C, el cual permite
acceder a cientos de sensores y actuadores que brindan funciones complejas.


Actividad de aprendizaje
-----------------------------

Se realizará las SEMANAS 10,11,12

Lee con detenimiento el código de honor y luego los pasos que
debes seguir para evidenciar esta actividad.

Código de honor
-----------------------------
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
-----------------------------

Lea todas las consideraciones hasta el final.

Se requiere construir un programa interactivo en el ESP32 que reciba comandos enviados 
desde una aplicación en el PC (por serial) y desde otra aplicación en el celular
(bluetooth clásico). Al ESP32 vamos a conectar un reloj de tiempo real o RTC.

Implementa las siguientes características:

* Detectar si el sensor está conectado al sistema de cómputo.
* Detectar si el sensor se desconecta del sistema de cómputo.
* Configurar la hora, minutos, segundos y el formato 12H o 24H.
* Configurar el día, mes, año y día de la semana.
* Leer la hora completa (horas, minutos, segundos).
* Leer la fecha completa (día, mes, año y día de la semana).
* Almacenar información en la RAM interna del dispositivo.
* Leer información de la RAM interna del dispositivo.
* Una vez tengas todas las funcionalidades, crea una biblioteca arduino para el RTC
  que vamos a utilizar y realiza un refactoring a la aplicación de modo
  que uses tu biblioteca. `Aquí <https://www.arduino.cc/en/Hacking/libraryTutorial>`__
  encontrarás un tutorial que te muestra cómo realizar la biblioteca.

* `Hoja de datos <https://datasheets.maximintegrated.com/en/ds/DS1307.pdf>`__
  del circuito integrado del sensor:

* Documentación oficial de arduino: `Wire <https://www.arduino.cc/en/Reference/Wire>`__

* Información del `sensor <http://robotdyn.com/wifi-d1-mini-shield-rtc-ds1307-real-time-clock-with-battery.html>`__

* `Planos <http://robotdyn.com/pub/media/0G-00005695==D1mini-SHLD-RTCDS1307/DOCS/Schematic==0G-00005695==D1mini-SHLD-RTCDS1307.pdf>`__
  del sensor.

* En los planos se puede ver un circuito convertidor bidireccional
  de 3.3V a 5V similar a `este <https://cdn.sparkfun.com/datasheets/BreakoutBoards/Logic_Level_Bidirectional.pdf>`__

* Tenga presente los niveles de alimentación del sensor: 5V, 3.3V, GND.

* La interfaz I2C será a 3.3V. Las resistencias de pullup ya están en el sensor
  como puede observar en los planos.

* Las comunicaciones entre todos los dispositivos se realizarán mediante
  protocolos binarios.

¿Qué debes entregar?
-----------------------------

* Crea una carpeta, la llamaras principal. 
* En la carpeta principal guarda una copia de la `rúbrica <https://docs.google.com/spreadsheets/d/1mJzpjBrAb2o8IS4re10KoOewia5oDX7WItJNSpzai9s/edit?usp=sharing>`__
  con tu autoevaluación. Cuando descargues la rúbrica bájala en forma Microsoft Excel.
* Guarda SOLO los códigos fuentes de todos los sistemas en la carpeta principal.
* Comprime la carpeta principal en formato .ZIP
* Entrega el archivo .ZIP `aquí <https://auladigital.upb.edu.co/mod/assign/view.php?id=621966>`__

Para la sustentación
-----------------------------
Vamos a realizar la sustentación del RETO en la última
sesión de la unidad. Para ello prepara:

* Una demostración de tu trabajo funcionando
* Estudia muy bien tu trabajo y prepárate para responder
  algunas preguntas relacionadas con la Unidad.

Trayecto de acciones, tiempos y formas de trabajo
---------------------------------------------------

Actividad 1
######################
* Fecha: septiembre 9 de 2020 - 10 a.m.
* Descripción: asiste al encuentro sincrónico donde se introducirá
  el reto de la unidad y los fundamentos teóricos.
* Recursos: ingresa al grupo de Teams
* Duración de la actividad: 1 hora 40 minutos.
* Forma de trabajo: grupal

Material
^^^^^^^^^^^^^^^^
En `este enlace <https://drive.google.com/open?id=1koxaaKxT7FhGBK2CITGljjGEOfgs1aYpfE1OZ70SmZ4>`__
puedes encontrar los fundamentos teóricos del bus I2C.

Actividad 2 
############################

* Fecha: septiembre 9 a septiembre 11 de 2020
* Descripción: lee el material y el ejemplos propuestos en el ejercicio
* Recursos: ejercicios
* Duración de la actividad: 5 horas
* Forma de trabajo: individual

Ejercicio
^^^^^^^^^^
En el material que se encuentra `aquí <https://docs.google.com/presentation/d/1Z5BEncGpW4RSQBqeRl1i-axLXDreKpHjHKo-QgXcKPY/edit?usp=sharing>`__
encontrará algunos ejemplos de comunicación entre dispostivos
utilizando el bus I2C. Algunos ejemplos muestran el uso del framework de
arduino para la implementación de un maestro y un esclavo

Actividad 3
######################
* Fecha: septiembre 11 de 2020 - 10 a.m.
* Descripción: asiste al encuentro sincrónico para resolver dudas de la actividad anterior
* Recursos: ingresa al grupo de Teams
* Duración de la actividad: 1 hora 40 minutos.
* Forma de trabajo: grupal
