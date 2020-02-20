Semana 6
===========




Esta semana comenzaremos a explorar las posibilidades de conectividad entre el 
microcontrolador y periféricos externos. Dichos periféricos externos usualmente son 
sensores, actuadores o dispositivos de comunicación. Estos dispositivos poseen alguna 
capacidad de cómputo. Comenzaremos estudiando SPI o ``Serial Peripheral Interface``. 

Objetivos
----------

1. Comprender el funcionamiento del bus SPI.

2. Realizar ejercicios de comunicación entre un microcontrolador y un dispositivo 
   mediante el uso de SPI.

3. Practicar el uso del bus SPI mediante la comunicación entre un sensor y un 
   microcontrolador.


Material de Referencia
-----------------------

`Aquí <https://drive.google.com/open?id=1A5mUIMiL8_nxpgoeCZLFX_T_KP2Rf2Lur32tZGQTD6s>`__ esta el 
enlace con el material introductorio.

El `framework de arduino <https://www.arduino.cc/en/Reference/SPI>`__ 
soporte el bus SPI.


Ejercicio SPI
---------------
Realizaremos un ejercicio práctico para conectar un sensor a un controlador utilizando 
el puerto SPI. El sensor a utilizar será el `BME280 <https://www.bosch-sensortec.com/bst/products/all_products/bme280>`__ de la 
empresa Bosh. El BME280 es un sensor ambiental que permite medir humedad relativa, 
presión y temperatura. Como  controlador, vamos a utilizar el ESP32 y el `framework de 
arduino <https://github.com/espressif/arduino-esp32>`__.

Para realizar el ejericicio utilizaremos el siguiente material:

* API de `arduino <https://www.arduino.cc/en/Reference/SPI>`__.
* Código fuente del módulo SPI del `ESP32 Arduino Core <https://github.com/espressif/arduino-esp32/tree/master/libraries/SPI/src>`__.
* Información general del sensor `BME280 <https://www.bosch-sensortec.com/bst/products/all_products/bme280>`__.
* Hoja de datos del sensor `BME280 <https://ae-bst.resource.bosch.com/media/_tech/media/datasheets/BST-BME280-DS002.pdf>`__.
* Tutorial del sensor `BME280 <https://learn.adafruit.com/adafruit-bme280-humidity-barometric-pressure-temperature-sensor-breakout/overview>`__.

Pinouts
^^^^^^^^^^
La siguiente figura muestra los pines del sensor a utilizar:

.. image:: ../_static/BME280Pinout.jpeg
   :scale: 40 %

Las señales tienen la siguiente función:

* VCC: alimentación a 3.3V.
* GND: 0V.
* SCL: Clock SPI.
* SDA: MOSI SPI.
* CSB: CS o SS (Chip Select) SPI.
* SDO: MISO SPI.

En relación al controlador a utilizar, hay varias opciones:

`Aquí <https://docs.espressif.com/projects/esp-idf/en/latest/get-started/get-started-pico-kit.html>`__ 
se puede ver información de los pines SPI para el ESP32 pico:

* Clock SPI: pin 18
* MISO: pin 19
* MOSI: pin 23
* CS: pin 5

En `este <https://github.com/espressif/arduino-esp32/raw/master/docs/esp32_pinmap.png>`__ 
enlace se puede ver otro controlador. 

Y en `este <https://es.aliexpress.com/item/32887251214.html>`__ para otro  
módulo basado en el ESP32 y el módulo `ESP32-WROOM-32D <https://www.espressif.com/sites/default/files/documentation/esp32-wroom-32d_esp32-wroom-32u_datasheet_en.pdf>`__.

Los controladores se puede comprar aquí:

`ESP32 pico <https://www.didacticaselectronicas.com/index.php/comunicaciones/bluetooth/tarjeta-de-desarrollo-wifi-y-bluetooth-esp32-pico-kit-esp32-pico-kit-v4-comunicaci%C3%B3n-iot-detail>`__.

`DevKit32 <https://www.didacticaselectronicas.com/index.php/comunicaciones/wi-fi/wifi,-wi-fi,-bluetooth-internet-iot-tarjeta-desarrollo-esp32-detail>`__.

`ESP32-OLED-18650-BAT <https://www.didacticaselectronicas.com/index.php/comunicaciones/wi-fi/m%C3%B3dulo-wifi-esp32-con-soporte-para-bater%C3%ADa-18650-wi-fi-bluetooth-esp32-bater%C3%ADa-18650-pantalla-oled-detail>`__.

Para conectar el sensor con el controlador se procede así:

========== ========== ======== =======
ESP32 pico  DevKit32   BME280   SPI
========== ========== ======== =======
3V3        3V3         VCC      ---
GND        GND         GND      ---
pin 18     SCK/18      SCL      CLOCK
pin 23     MOSI/23     SDA      MOSI
pin 5      SS/5/LED    CSB      SS
pin 19     MISO/19     SDO      MISO 
========== ========== ======== =======

Software
^^^^^^^^^^
Para realizar la prueba del sensor es necesario instalar estas dos bibliotecas:

* `Adafruit Sensor <https://github.com/adafruit/Adafruit_Sensor>`__
* `Adafruit BME280 Library <https://github.com/adafruit/Adafruit_BME280_Library>`__

Programa de prueba
^^^^^^^^^^^^^^^^^^^^
Una vez instalada la biblioteca Adafruit BME280. Se debe abrir el ejemplo BME280test.ino. Y realizar las siguiente 
modificaciones:

Comentar la el archivo de cabeceras Wire.h. Este archivo corresponde al API I2C. Modificar el pinout del SPI:

.. code-block:: c 
   :lineno-start: 24

    #include <SPI.h>
    #define BME_SCK 18
    #define BME_MISO 19
    #define BME_MOSI 23
    #define BME_CS 5

Comentar la línea que declara el objeto I2C y descomentar la correspondiente a SPI:

.. code-block:: c 
   :lineno-start: 33

    //Adafruit_BME280 bme; // I2C
    Adafruit_BME280 bme(BME_CS); // hardware SPI
    //Adafruit_BME280 bme(BME_CS, BME_MOSI, BME_MISO, BME_SCK); // software SPI

A continuación se observa el código completo:

.. code-block:: c 
   :lineno-start: 1

    /***************************************************************************
    This is a library for the BME280 humidity, temperature & pressure sensor

    Designed specifically to work with the Adafruit BME280 Breakout
    ----> http://www.adafruit.com/products/2650

    These sensors use I2C or SPI to communicate, 2 or 4 pins are required
    to interface. The device's I2C address is either 0x76 or 0x77.

    Adafruit invests time and resources providing this open source code,
    please support Adafruit andopen-source hardware by purchasing products
    from Adafruit!

    Written by Limor Fried & Kevin Townsend for Adafruit Industries.
    BSD license, all text above must be included in any redistribution
    ***************************************************************************/

    //#include <Wire.h>

    #include <Adafruit_Sensor.h>
    #include <Adafruit_BME280.h>


    #include <SPI.h>
    #define BME_SCK 18
    #define BME_MISO 19
    #define BME_MOSI 23
    #define BME_CS 5


    #define SEALEVELPRESSURE_HPA (1013.25)

    //Adafruit_BME280 bme; // I2C
    Adafruit_BME280 bme(BME_CS); // hardware SPI
    //Adafruit_BME280 bme(BME_CS, BME_MOSI, BME_MISO, BME_SCK); // software SPI

    unsigned long delayTime;

    void setup() {
    Serial.begin(9600);
    Serial.println(F("BME280 test"));

    bool status;

    // default settings
    // (you can also pass in a Wire library object like &Wire2)
    //status = bme.begin(0x76);ç
    status = bme.begin();
    if (!status) {
        Serial.println("Could not find a valid BME280 sensor, check wiring!");
        while (1);
    }

    Serial.println("-- Default Test --");
    delayTime = 1000;

    Serial.println();
    }


    void loop() {
    printValues();
    delay(delayTime);
    }


    void printValues() {
    Serial.print("Temperature = ");
    Serial.print(bme.readTemperature());
    Serial.println(" *C");

    Serial.print("Pressure = ");

    Serial.print(bme.readPressure() / 100.0F);
    Serial.println(" hPa");

    Serial.print("Approx. Altitude = ");
    Serial.print(bme.readAltitude(SEALEVELPRESSURE_HPA));
    Serial.println(" m");

    Serial.print("Humidity = ");
    Serial.print(bme.readHumidity());
    Serial.println(" %");

    Serial.println();
    }

Al ejecutar el código el resultado será algo similar a esto::

    Temperature = 25.44 *C
    Pressure = 850.51 hPa
    Approx. Altitude = 1452.61 m
    Humidity = 51.67 %

    Temperature = 25.43 *C
    Pressure = 850.43 hPa
    Approx. Altitude = 1453.42 m
    Humidity = 51.67 %

    Temperature = 25.43 *C
    Pressure = 850.47 hPa
    Approx. Altitude = 1453.03 m
    Humidity = 51.67 %

La temperatura se reporta como un número en punto flotante en grados centígrados. La presión se reporta como un número 
en punto flotante en Pascales. Note que el valor de presión se divide por el literal 100.0F (constante en punto flotante) 
para convertir a hecto Pascales el resultado. Para el cálculo de la altitud aproximada, es necesario pasar la presión 
sobre el nivel del mar de la ciudad al día y hora de la prueba en unidades de hecto Pascales. Finalmente se reporta la humdad 
relativa en punto flotante.

Análisis de la biblioteca SPI y la hoja de datos del sensor
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
Abra el `código fuente <https://github.com/adafruit/Adafruit_BME280_Library/blob/master/Adafruit_BME280.cpp>`__ 
de la biblioteca del sensor.

* Analice el código del constructor de la clase. ¿Qué estrategia utilizan para diferenciar el SPI por hardware al SPI 
  por software?
* ¿En qué parte del código se inicializa el objeto SPI?
* Haciendo la lectura del código fuente, ¿Qué bit se envía primero, el de mayor peso o el de menor peso?
* ¿Cuál modo de SPI utiliza el sensor?
* ¿Cuál es la velocidad de comunicación?
* El sensor soporta dos modos SPI. Leyendo la información en la hoja de datos, cómo sería posible configurar el modo?
* ¿Cuál es el protocolo para escribir información en el sensor?
* ¿Cuál es el protocolo para leer informacion del sensor?
* Busque en el código fuente de la biblioteca,  ¿Dónde se lee el chip-ID del sensore BME280?
* Muestre y explique detalladamente los pasos y el código para identificar el chip-ID. No olvide apoyarse de la hoja de datos
* ¿Qué otros pasos se requieren para inicializar el sensor?

/// SEMANA 4
En esta sesión vamos a introducir el bus I2C (``Inter-Integrated Circuit``).

ObjetivoEn esta sesión vamos a introducir el bus I2C (``Inter-Integrated Circuit``).

----------
Introducir de manera teórica y con ejemplos el bus I2C.

Introducción a I2C
-------------------
Para realizar la introducción al bus I2C vamos a utilizar el siguiente material de 
referencia:

* `Presentación teórica <https://drive.google.com/open?id=1koxaaKxT7FhGBK2CITGljjGEOfgs1aYpfE1OZ70SmZ4>`__.







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
* El valor de la variable deberá ser enriquecido con el instante de tiempo y la fecha de la última lectura.
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
* El plazo máximo es el jueves 29 de agosto de 2019 a las 12 de la noche para enviar el enlace con toda la información completa. 
  Luego del plazo se penalizará la entrega 1 unidad hastas las 6 a.m. del 30 de agosto. Luego de esta hora, no se recibirán trabajos.
* La sustentación es presencial. En caso de inasistencia debe solicitar autorización del director de la facultad para presentar 
  supletorio.
* La sustentación deberá ser entregada el 30 de agosto de 2019 antes de las 7:40 a.m. a un enlace suministrado por el profesor.


