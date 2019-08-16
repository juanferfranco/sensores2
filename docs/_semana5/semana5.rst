Semana 5
===========
Esta semana vamos a realizar algunos retos relacionados con los buses I2C y SPI.
Para el reto de esta primera sesión vamos a conectar un controlador a un sensor I2C. 
El reto consiste en implementar, sólo con el API de arduino, la comunicación I2C con 
el sensor. No vamos a buscar, ni utilizar bibliotecas para el sensor.

El material que se encuentra 
`aquí <https://drive.google.com/open?id=1Hg5zy4VJLpjAjO-xdBMAljGYHGpOJRmjZoIGko7Xldo>`__ 
tiene algunos ejemplos de comunicación entre dispostivos utilizando el bus I2C. Algunos 
ejemplos muestran el uso del framework de arduino para la implementación de un maestro y 
un esclavo. Para el reto de esta sesión, sólo haremos uso del API I2C para el maestro.

Ejercicios
-----------

Reto:
^^^^^^^^
En este reto vamos a conectar un sensor a un controlador mediante el bus I2C. 
Este reto tendrá las siguientes consideraciones:

* Lea todas las consideraciones hasta el final.

* Reto personal: NO BUSCAR EN INTERNET la solución, NO BUSCAR EN INTERNET soluciones 
  similares para basarse en ellas, SE PUEDE CONSULTAR la documentación de I2C de Arduino, 
  es decir, el API, y las hojas de datos del sensor.

* Se requiere construir un programa interactivo que reciba comandos enviados desde la 
  terminal serial de arduino para interactuar con un reloj de tiempo real o RTC. Tener 
  presente las siguientes consideraciones para este sensor:

    * Detectar si el sensor está conectado al sistema de cómputo.
    * Configurar la hora, minutos, segundos y el formato 12H o 24H.
    * Configurar el día, mes, año y día de la semana. 
    * Leer la hora completa (horas, minutos, segundos).
    * Leer la fecha completa (día, mes, año y día de la semana). 
    * Almacenar información en la RAM interna del dispositivo.
    * Leer información de la RAM interna del dispositivo.

* `Hoja de datos <https://datasheets.maximintegrated.com/en/ds/DS1307.pdf>`__ 
  del circuito integrado del sensor: 

* Documentación oficial de arduino: https://www.arduino.cc/en/Reference/Wire

* Información del `sensor <http://robotdyn.com/wifi-d1-mini-shield-rtc-ds1307-real-time-clock-with-battery.html>`__

* `Planos <http://robotdyn.com/pub/media/0G-00005695==D1mini-SHLD-RTCDS1307/DOCS/Schematic==0G-00005695==D1mini-SHLD-RTCDS1307.pdf>`__ 
  del sensor.

* En los planos se puede ver un circuito convertidor bidireccionar de 3.3V a 5V similar a 
  `este <https://cdn.sparkfun.com/datasheets/BreakoutBoards/Logic_Level_Bidirectional.pdf>`__

* Tenga presente los niveles de alimentación del sensor: 5V, 3.3V, GND.

* La interfaz I2C será a 3.3V. Las resistencias de pullup ya están en el sensor como puede
  observar en los planos.


Evaluación sumativa número 1
-----------------------------

Descripción: construir una aplicación interactiva utilizando Unity, un sensor I2C, un sensor SPI y 
controlador y el framework de arduino.

Requisitos
------------

* El sensor I2C será un RTC, el sensor SPI será un sensor de temperatura, presión y altitud. Estas son 
  las variables a medir.Los sensores SPI pueden ser BMP280 o BME280.
* Diseñar una interfaz de usuario en Unity agradable y fácil de usar para un usuario sin 
  cononocimientos técnicos.
* La aplicación presentará el valor de las variables.
* La aplicación debe contar con un mecanismo de entrada en la interfaz de usuario que solicite el valor
  de cada una de las variables de manera independiente. Solo debe actualizar el valor de la variable 
  solicitada.
* El valor de la varible deberá ser enriquecido con el instante de tiempo y la fecha de la última lectura.
* Al iniciar la aplicación se debe presentar el valor de las variables y la hora y fecha de adquisición de 
  cada una.
* La aplicación deberá tener una opción para configurar el RTC a la hora y fecha correcta.
* Diseñar un protocolo de integración entre la aplicación en Unity y el controlador.

Valoración
------------

* Funcionamiento según los requisitos del cliente (40%).
* Portafolio en internet con: video del funcionamiento con explicación (30%) y la 
  documentación (informe escrito) (30%).
* Informe escrito: debe explicar detalladamente cómo funciona la aplicación. 
* Informe escrito: debe explicar detalladamente cómo está construida la aplicación. No olvide mencionar 
  cada aspecto, es decir, programa en Unity, sensores, programa de arduino y PROTOCOLO de integración.

Sustentación
-------------
La sustentación se realizará el 23 de agosto en la sesión de clase. 
Deberá traer todos los materiales necesarios para reproducir la aplicación solicitada 
(Unity, arduino, sensores) y computador con las herramientas instaladas y configuradas.

La sustentación consiste en realizar una modificación, cambio, adición, mejora a la aplicación solicitada.

Valoración Final
-----------------
Nota final = (LO SOLICITADO ) * sustentación. Donde la sustentación tendrá un valor de 0 a 1 
y será un factor multiplicativo de lo solicitado.

Plazos
-------

* Enviar al correo del profesor el enlace al portafolio indicando claramente donde está todo lo solicitado.
* El plazo máximo es el jueves 22 a las 12 de la noche para enviar el enlace con toda la información completa. 
  Luego del plazo se penalizará la entrega 1 unidad hastas las 6 a.m. del 23 de agosto. Luego de esta hora, no se recibirán trabajos.
* La sustentación es presencial. En caso de inasistencia debe solicitar autorización del director de la facultad para presentar 
  supletorio.
* La sustentación deberá ser entregada el 23 de agosto antes de las 7:40 a.m. a un enlace suministrado por el profesor.
