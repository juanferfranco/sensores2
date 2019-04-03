Semana 11
===========
Esta semana vamos a estudiar el API que ofrece Arduino para interactuar con dispositivos sensores y actuadores por I2C. 

Objetivos
----------

1. Estudiar el API I2C de Arduino.
2. Realizar ejercicios de integración entre controladores y sensores mediante I2C.

API I2C Arduino
-------------------

El material de referencia se encuentra `aquí <https://drive.google.com/open?id=1Hg5zy4VJLpjAjO-xdBMAljGYHGpOJRmjZoIGko7Xldo>`__.

Ejercicios
-----------

Reto 1:
^^^^^^^^
Se propone realizar un chat entre dos terminales seriales ASCII. Las terminales están conectadas por medio de dos arduinos, 
que a su vez se conectan utilizando I2C.

Reto 2:
^^^^^^^^
En este reto vamos a conectar un sensor a un controlador mediante el bus I2C. Este reto tendrá las siguientes 
consideraciones:

* Reto personal: NO BUSCAR EN INTERNET la solución, NO BUSCAR EN INTERNET soluciones similares para basarse en ellas, 
  SE PUEDE CONSULTAR la documentación de I2C de Arduino, es decir, el API, y las hojas de datos del sensor.

* Se requiere construir un programa interactivo que reciba comandos enviados desde la terminal serial de arduino para:

    * Detectar si el sensor está conectado al sistema de cómputo.
    * Configurar la hora, minutos, segundos y el formato 12H o 24H.
    * Configurar el día, mes, año y día de la semana. 
    * Leer la hora completa (horas, minutos, segundos).
    * Leer la fecha completa (día, mes, año y día de la semana). 
    * Almacenar información en la RAM interna del dispositivo.
    * Leer información de la RAM interna del dispositivo.

* `Hoja de datos <https://www.maximintegrated.com/en/products/digital/real-time-clocks/DS1307.html>`__ 
  del circuito integrado del sensor: 

* Documentación oficial de arduino: https://www.arduino.cc/en/Reference/Wire

* Información del `sensor <http://robotdyn.com/wifi-d1-mini-shield-rtc-ds1307-real-time-clock-with-battery.html>`__

* `Planos <http://robotdyn.com/pub/media/0G-00005695==D1mini-SHLD-RTCDS1307/DOCS/Schematic==0G-00005695==D1mini-SHLD-RTCDS1307.pdf>`__ 
  del sensor.

* En los planos se puede ver un circuito convertidor bidireccionar de 3.3V a 5V similar a 
  `este <https://cdn.sparkfun.com/datasheets/BreakoutBoards/Logic_Level_Bidirectional.pdf>`__