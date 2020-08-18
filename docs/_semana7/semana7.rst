Semana 7: Unidad 3 - sensores y actuadores SPI
=================================================

En esta unidad vamos a estudiar el bus de comunicaciones SPI. Este bus
nos permitirá conectar a los controladores sensores y actuadores
complejos

.. warning::
   ES MUY IMPORTANTE QUE LEAS CON DETENIMIENTO LA RÚBRICA DE EVALUACIÓN DE ESTA
   UNIDAD ANTES DE COMENZAR.

Propósitos de aprendizaje
-----------------------------
Comprender el funcionamiento del bus de comunicaciones SPI, el cual permite
acceder a cientos de sensores y actuadores que brindan funciones complejas.


Actividad de aprendizaje
-----------------------------

Se realizará las SEMANAS 7,8,9

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

Debes construir un sistema de aplicaciones interactivas (aplicación
en el PC y aplicación móvil) que te permitan
leer las variables de tu sensor SPI. Dependiendo del sensor que tengas las
variables podrían ser: temperatura, humedad, presión, altitud. El sistema
de aplicaciones interactivas tendrá las siguientes características:

* Utiliza Unity, processing, p5.js o TouchDesigner (puedes sugerir otra opción)
  para la aplicación del PC
* Utiliza kodular, processing for android, android studio 
  (puedes sugerir otra opción) para la aplicación móvil.
* El sensor será controlado por un ESP32 quien deberá conectarse por serial
  a la aplicación del PC y por Bluetooth clásico a la aplicación móvil.
* Debes diseñar una interfaz de usuario para ambas aplicaciones (PC, móvil).
* Diseña un PROTOCOLO BINARIO de comunicación entre el ESP32 y cada aplicación.
* Las variables se deben refrescar mínimo cada 100 ms y máximo cada 1000 ms. Las
  aplicaciones deben ofrecerle al usuario la posibilidad de programar, DESDE
  LA INTERFAZ de usuario, este valor.

¿Qué debes entregar?
-----------------------------

* Crea una carpeta, la llamaras principal. 
* En la carpeta principal guarda una copia de la `rúbrica <https://docs.google.com/spreadsheets/d/1IkWJJ3p9pqewzsf_Trvhb4n89E2ZJrKCh0zhco3g47k/edit?usp=sharing>`__
  con tu autoevaluación.
* Guarda los tres proyecto en la carpeta.
* Comprime la carpeta principal en formato .ZIP
* Entrega el archivo .ZIP `aquí <https://auladigital.upb.edu.co/mod/assign/view.php?id=621965>`__

Para la sustentación
-----------------------------
Vamos a realizar la sustentación del RETO en la última
sesión de la semana 9. Para ello prepara:

* Una demostración de tu trabajo funcionando
* Estudia muy bien tus proyectos y prepárate para responder
  algunas preguntas relacionadas con los puntos de la rúbrica,
  es decir, preguntas relacionadas con el modelo (¿Cómo funciona?), con la
  implementación y con las pruebas que hiciste para comprobar el funcionamiento
  del RETO. TEN TODA ESTA DOCUMENTACIÓN A LA MANO para que la puedas mostrar
  en caso de ser solicitada.

Trayecto de acciones, tiempos y formas de trabajo
---------------------------------------------------

Actividad 1
######################
* Fecha: agosto 19 de 2020 - 10 a.m.
* Descripción: asiste al encuentro sincrónico donde se introducirá
  el reto de la unidad y los fundamentos teóricos.
* Recursos: ingresa al grupo de Teams
* Duración de la actividad: 1 hora 40 minutos.
* Forma de trabajo: grupal

Material
^^^^^^^^^^^^^^^^
En `este enlace <https://docs.google.com/presentation/d/1RgrSLVdjDvEZWuj5TfYLl-81asZI_k0HFqVpN23Nkgs/edit?usp=sharing>`__
puedes encontrar los fundamentos teóricos del bus SPI.

Actividad 2 
############################

* Fecha: agosto 19 a agosto 21 de 2020
* Descripción: realiza los siguientes ejercicios
* Recursos: ejercicios
* Duración de la actividad: 5 horas
* Forma de trabajo: individual con solución de dudas en tiempo real

Ejercicio SPI
^^^^^^^^^^^^^^^^
Realizaremos un ejercicio práctico para conectar un sensor a un
controlador utilizando el puerto SPI. El sensor a utilizar será
el `BME280 <https://www.bosch-sensortec.com/bst/products/all_products/bme280>`__
de la empresa Bosh. El BME280 es un sensor ambiental que permite
medir humedad relativa, presión y temperatura. Como  controlador,
vamos a utilizar el ESP32 y el `framework de arduino <https://github.com/espressif/arduino-esp32>`__.

Para realizar el ejercicio utilizaremos el siguiente material:

* API de `arduino <https://www.arduino.cc/en/Reference/SPI>`__.
* Código fuente del módulo SPI del `ESP32 Arduino Core <https://github.com/espressif/arduino-esp32/tree/master/libraries/SPI/src>`__.
* Información general del sensor `BME280 <https://www.bosch-sensortec.com/bst/products/all_products/bme280>`__.
* Hoja de datos del sensor `BME280 <https://ae-bst.resource.bosch.com/media/_tech/media/datasheets/BST-BME280-DS002.pdf>`__.
* Tutorial del sensor `BME280 <https://learn.adafruit.com/adafruit-bme280-humidity-barometric-pressure-temperature-sensor-breakout/overview>`__.

Pinouts
*********

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

Los controladores se puede comprar aquí:

`ESP32 pico <https://www.didacticaselectronicas.com/index.php/comunicaciones/bluetooth/tarjeta-de-desarrollo-wifi-y-bluetooth-esp32-pico-kit-esp32-pico-kit-v4-comunicaci%C3%B3n-iot-detail>`__.

`DevKit32 <https://www.didacticaselectronicas.com/index.php/comunicaciones/wi-fi/wifi,-wi-fi,-bluetooth-internet-iot-tarjeta-desarrollo-esp32-detail>`__.

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
**********
Para realizar la prueba del sensor es necesario instalar estas dos bibliotecas:

* `Adafruit Sensor <https://github.com/adafruit/Adafruit_Sensor>`__
* `Adafruit BME280 Library <https://github.com/adafruit/Adafruit_BME280_Library>`__

Programa de prueba
********************
Una vez instalada la biblioteca Adafruit BME280, se debe abrir el ejemplo
BME280test.ino. Y realizar las siguiente modificaciones:

Comentar la el archivo de cabeceras Wire.h. Este archivo corresponde al API I2C.
Modificar el pinout del SPI:

.. code-block:: c 
   :lineno-start: 24

    #include <SPI.h>
    #define BME_SCK 18
    #define BME_MISO 19
    #define BME_MOSI 23
    #define BME_CS 5

Comentar la línea que declara el objeto I2C y descomentar la
correspondiente a SPI:

.. code-block:: c 
   :lineno-start: 33

    //Adafruit_BME280 bme; // I2C
    Adafruit_BME280 bme(BME_CS); // hardware SPI
    //Adafruit_BME280 bme(BME_CS, BME_MOSI, BME_MISO, BME_SCK); // software SPI

A continuación se observa el código completo:

.. code-block:: cpp
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
    Humidity = 51.67 %S

    Temperature = 25.43 *C
    Pressure = 850.43 hPa
    Approx. Altitude = 1453.42 m
    Humidity = 51.67 %

    Temperature = 25.43 *C
    Pressure = 850.47 hPa
    Approx. Altitude = 1453.03 m
    Humidity = 51.67 %

La temperatura se reporta como un número en punto flotante en
grados centígrados. La presión se reporta como un número en punto
flotante en Pascales. Note que el valor de presión se divide por
el literal 100.0F (constante en punto flotante) para convertir
a hecto Pascales el resultado. Para el cálculo de la altitud
aproximada, es necesario pasar la presión sobre el nivel del mar
de la ciudad al día y hora de la prueba en unidades de hecto
Pascales. Finalmente se reporta la humada relativa en punto flotante.

Análisis de la biblioteca SPI y la hoja de datos del sensor
*************************************************************

Abre el `código fuente <https://github.com/adafruit/Adafruit_BME280_Library/blob/master/Adafruit_BME280.cpp>`__
de la biblioteca del sensor.

* Analiza el código del constructor de la clase. ¿Qué estrategia
  utilizan para diferenciar el SPI por hardware al SPI por software?
* ¿En qué parte del código se inicializa el objeto SPI?
* Haciendo la lectura del código fuente, ¿Qué bit se envía primero,
  el de mayor peso o el de menor peso?
* ¿Cuál modo de SPI utiliza el sensor?
* ¿Cuál es la velocidad de comunicación?
* El sensor soporta dos modos SPI. Leyendo la información en la hoja
  de datos, cómo sería posible configurar el modo?
* ¿Cómo es el protocolo para escribir información en el sensor?
* ¿Cómo es el protocolo para leer información del sensor?
* Busque en el código fuente de la biblioteca,  ¿Dónde se lee
  el chip-ID del sensor BME280?
* Muestra y explica detalladamente los pasos y el código para identificar
  el chip-ID. No olvide apoyarse de la hoja de datos
* ¿Qué otros pasos se requieren para inicializar el sensor?

Actividad 3
######################
* Fecha: agosto 21 de 2020 - 10 a.m.
* Descripción: asiste al encuentro sincrónico para resolver dudas de la actividad anterior
* Recursos: ingresa al grupo de Teams
* Duración de la actividad: 1 hora 40 minutos.
* Forma de trabajo: grupal
