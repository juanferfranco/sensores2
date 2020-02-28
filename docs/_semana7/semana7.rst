Semana 7
===========

Sesión 1
----------
En esta sesión vamos a revisar el el reto del viernes.

Sesión 2
----------

En esta sesión vamos a introducir el bus I2C (``Inter-Integrated Circuit``).

Introducción a I2C
^^^^^^^^^^^^^^^^^^^
Para realizar la introducción al bus I2C vamos a utilizar el siguiente
material de referencia:

* `Presentación teórica <https://drive.google.com/open?id=1koxaaKxT7FhGBK2CITGljjGEOfgs1aYpfE1OZ70SmZ4>`__.

Ejercicio
^^^^^^^^^^
En el material que se encuentra `aquí <https://docs.google.com/presentation/d/1Z5BEncGpW4RSQBqeRl1i-axLXDreKpHjHKo-QgXcKPY/edit?usp=sharing>`__
encontrará algunos ejemplos de comunicación entre dispostivos
utilizando el bus I2C. Algunos ejemplos muestran el uso del framework de
arduino para la implementación de un maestro y un esclavo.

Reto: evaluación formativa
^^^^^^^^^^^^^^^^^^^^^^^^^^^

En este reto vamos a conectar un sensor a un controlador mediante el bus I2C.
Este reto tendrá las siguientes consideraciones:

* Lea todas las consideraciones hasta el final.

* Reto personal: NO BUSCAR EN INTERNET la solución, NO BUSCAR EN
  INTERNET soluciones   similares para basarse en ellas, SE PUEDE
  CONSULTAR la documentación de I2C de Arduino,
  es decir, el API, y las hojas de datos del sensor.

* Se requiere construir un programa interactivo en el ESP32 que reciba comandos
  enviados desde una aplicación hecha con Unity. Al ESP32 vamos a conectar
  un reloj de tiempo real o RTC, considerando:

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

* En los planos se puede ver un circuito convertidor bidireccional
  de 3.3V a 5V similar a `este <https://cdn.sparkfun.com/datasheets/BreakoutBoards/Logic_Level_Bidirectional.pdf>`__

* Tenga presente los niveles de alimentación del sensor: 5V, 3.3V, GND.

* La interfaz I2C será a 3.3V. Las resistencias de pullup ya están en el sensor
  como puede observar en los planos.

* Las comunicaciones entre Unity y el ESP32 deben realizarse siguiendo
  un protocolo binario. Proponga el protocolo.


