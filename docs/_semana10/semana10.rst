Semana 10
===========

Sesión 1
-----------
Las semanas anteriores utilizamos TCP/IP y el modelo cliente servidor para 
integrar sensores y actuadores utilizando WiFi. En ests sesión vamos a utilizar el protocolo 
UDP.

Objetivo de la sesión
----------------------

Integrar sensores y actuadores con dispositivos de cómputo utilizando WiFi y el protocolo UDP.

Ejercicio: analizar el código
------------------------------

Para explorar `UDP <https://www.arduino.cc/en/Reference/WiFi>`__ vamos a realizar un proyecto 
simple que ilustra el uso del protocolo. Se trata de un conjunto de actuadores distribuidos 
en el espacio y un coordinar central, un PC. Cada actuador enciende y 
apaga un puerto de entrada salida según lo indique el comando, recibido por UDP, que será 
enviado por el coordinador central. El coordinador cuenta con un dispositivo, que llamaremos 
bridge, quien recibirá por serial los comandos y los reenviará por UDP a los actuadores 
distribuidos.

El protocolo de comunicación serial es simple. Se trata de un protocolo ascii compuesto por 
tres caracteres. El primer carácter indica a cual actuador se enviará el comando. 
El segundo carácter el estado deseado para la salida ('1' on, '0' off). Por último, 
se envía un carácter de sincronización ('*').

El código del bridge es el siguiente:

.. code-block:: cpp
   :lineno-start: 1
   
   #include <WiFi.h>
   #include <WiFiUdp.h>
   
   const char* ssid = "?";
   const char* password = "?";
   WiFiUDP udpDevice;
   uint16_t localUdpPort = ?;
   uint16_t UDPPort = ?;
   #define MAX_LEDSERVERS 3
   const char* hosts[MAX_LEDSERVERS] = {"?.?.?.?", "?.?.?.?", "?.?.?.?"};
   #define SERIALMESSAGESIZE 3
   uint32_t previousMillis = 0;
   #define ALIVE 1000
   #define D0 5
   
   void setup() {
     pinMode(D0, OUTPUT);     // Initialize the LED_BUILTIN pin as an output
     digitalWrite(D0, HIGH);
     Serial.begin(115200);
     Serial.println();
     Serial.println();
     Serial.print("Connecting to ");
     Serial.println(ssid);
   
     WiFi.mode(WIFI_STA);
     WiFi.begin(ssid, password);
   
     while (WiFi.status() != WL_CONNECTED) {
       delay(500);
       Serial.print(".");
     }
     Serial.println("");
     Serial.println("WiFi connected");
     // Print the IP address
     Serial.println(WiFi.localIP());
     udpDevice.begin(localUdpPort);
   }
   
   void networkTask() {
     uint8_t LEDServer = 0;
     uint8_t LEDValue = 0;
     uint8_t syncChar;
   
     // Serial event:
     if (Serial.available() >= SERIALMESSAGESIZE) {
       LEDServer = Serial.read() - '0';
       LEDValue = Serial.read();
       syncChar = Serial.read();
       if ((LEDServer == 0) || (LEDServer > 3)) {
         Serial.println("Servidor inválido (seleccione 1,2,3)");
         return;
       }
       if (syncChar == '*') {
         udpDevice.beginPacket(hosts[LEDServer - 1] , UDPPort);
         udpDevice.write(LEDValue);
         udpDevice.endPacket();
       }
     }
     // UDP event:
     uint8_t packetSize = udpDevice.parsePacket();
     if (packetSize) {
       Serial.print("Data from: ");
       Serial.print(udpDevice.remoteIP());
       Serial.print(":");
       Serial.print(udpDevice.remotePort());
       Serial.print(' ');
       for (uint8_t i = 0; i < packetSize; i++) {
         Serial.write(udpDevice.read());
       }
     }
   }
   
   void aliveTask() {
     uint32_t currentMillis;
     static uint8_t ledState = 0;
     currentMillis  = millis();
     if ((currentMillis - previousMillis) >= ALIVE) {
       previousMillis = currentMillis;
       if (ledState == 0) {
         digitalWrite(D0, HIGH);
         ledState = 1;
       }
       else {
         digitalWrite(D0, LOW);
         ledState = 0;
       }
     }
   }
   
   void loop() {
     networkTask();
     aliveTask();
   }

Note que a diferencia de TCP/IP, con UDP no es necesario establecer una conexión. Los pasos 
necesario para enviar datos por UDP serán:

* Crear un objeto WiFiUDP
* Iniciar el objeto estableciendo un socket compuesto por la dirección IP y el puerto de escucha.
* Iniciar la construcción del paquete a transmitir con beginPacket(), 
* Popular el buffer de transmisión con write.
* Enviar el paquete con endPacket().

El código de los actuadores distribuidos será:

.. code-block:: cpp
   :lineno-start: 1

    #include <WiFi.h>
    #include <WiFiUdp.h>

    const char* ssid = "?";
    const char* password = "?";
    WiFiUDP udpDevice;
    uint16_t localUdpPort = ?;
    uint32_t previousMillis = 0;
    #define ALIVE 1000
    #define D0 5
    #define D8 18

    void setup() {
        pinMode(D0, OUTPUT);     // Initialize the LED_BUILTIN pin as an output
        digitalWrite(D0, HIGH);
        pinMode(D8, OUTPUT);     
        digitalWrite(D8, LOW);
        Serial.begin(115200);
        Serial.println();
        Serial.println();
        Serial.print("Connecting to ");
        Serial.println(ssid);

        WiFi.mode(WIFI_STA);
        WiFi.begin(ssid, password);

        while (WiFi.status() != WL_CONNECTED) {
            delay(500);
            Serial.print(".");
        }
        Serial.println("");
        Serial.println("WiFi connected");
        // Print the IP address
        Serial.println(WiFi.localIP());
        udpDevice.begin(localUdpPort);
    }


    void networkTask() {
        uint8_t data;
        uint8_t packetSize = udpDevice.parsePacket();
        if (packetSize) {
            data = udpDevice.read();
            if (data == '1') {
                digitalWrite(D0, HIGH);
            } else if (data == '0') {
                digitalWrite(D0, LOW);
            }
            // send back a reply, to the IP address and port we got the packet from
            udpDevice.beginPacket(udpDevice.remoteIP(), udpDevice.remotePort());
            udpDevice.write('1');
            udpDevice .endPacket();
        }
    }

    void aliveTask() {
        uint32_t currentMillis;
        static uint8_t ledState = 0;
        currentMillis  = millis();
        if ((currentMillis - previousMillis) >= ALIVE) {
            previousMillis = currentMillis;
            if (ledState == 0) digitalWrite(D8, HIGH);
            else digitalWrite(D8, LOW);
        }
    }

    void loop() {
        networkTask();
        aliveTask();
    }

Los pasos para recibir datos por UDP son:

* Crear un objeto WiFiUDP
* Iniciar el objeto estableciendo un socket compuesto por la dirección IP y el puerto de escucha.
* Procesar el siguiente paquete UDP con parsePacket(). Esta acción devolverá el tamaño del paquete en bytes.
* Luego de llamar parsePacket() será posible utilizar los métodos read() y available().
* Leer el paquete.

En el ejemplo mostrado, note que un actuador distribuido responderá al bridge con el carácter '1' cada que reciba un 
paquete. De esta manera el bridge sabrá que el dato llegó a su destino.

Ejercicio: despliegue del ejercicio
------------------------------------
Este ejercicio lo vamos a realizar en equipo.

Para desplegar el ejercicio es necesario identificar claramente las direcciones IP de cada 
uno de los actuadores remotos.

Utilice un ESP32 para cada actuador y un ESP32 para el bridge.

Utilice el programa Hercules para simular la aplicación del PC.

Sesión 2
---------
En esta sesión veremos una aplicación interesante que combina dispositivos embebidos (IoT) 
con realidad aumentada. 
La aplicación consiste en realizar tracking a una imagen y aumentarla mostrando un modelo 
3D (en este caso un simple 
cubo) sobre la imagen. Adicionalmente, el material del modelo 3D cambiará de color en base 
al valor reportado por un 
sensor. La aplicación embebida correrá en la plataforma `photon <https://docs.particle.io/photon/>`__ de la empresa 
`particle <https://www.particle.io/>`__. La aplicación de realidad aumentada funcionará en 
Unity con el SDK Vuforia. 
Ambas aplicaciones se conectarán por medio de un servidor al cual el sensor enviará datos y 
del cual Unity leerá la información para actualizar el color del material del modelo.

El siguiente video muestra un Demo corto de la aplicación:

.. raw:: html

    <div style="position: relative; padding-bottom: 56.25%; height: 0; overflow: hidden; max-width: 100%; height: auto;">
        <iframe src="https://www.youtube.com/embed/oskw30HNovk" frameborder="0" allowfullscreen style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;"></iframe>
    </div>

|

Objetivos
----------
Integrar sensores y actuadores con aplicaciones de realidad aumentada.

Procedimiento:
---------------
En el siguiente `enlace <https://drive.google.com/open?id=1R3AjLGbDifl_GxH8NB0PRxy_kFN-0ZLYxMqYboIb1Qk>`__ se encuentra una
guía que consiste en cuatro laboratorios que permiten construir paso a paso la aplicación.
