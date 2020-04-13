Semana 13
===========


De la semana 12:


Esta semana vamos a conectar sensores y actuadores a una plataforma de implementación de 
aplicaciones interactivas.

Material
---------
En `este enlace <https://drive.google.com/open?id=1rkSL-DyORk19jfnax9FUph7jeXIgKb4Zl1eBfyVtQrQ>`__ 
se encuentra el material para la sesión 1

Reto 1
-------
Una vez analizado el material, se propone realizar una aplicación simple en Unity que se conecte 
con una aplicación como hercules y luego se conecte a un sensor.

Reto 2
-------
Incrementar la aplicación anterior con dos sensores y un actuador. Cada sensor y el actuador 
debe tener su propia dirección IP.

Proyecto Final
----------------
* Diseñar e implementar una experiencia interactiva que integre 2 sensores y un actuador a 
  Unity utilizando el protocolo de transporte UDP. Los sensores DEBEN tener interfaz serial,  
  I2C o SPI. Los sensores y actuador DEBEN ser 3 DISPOSITIVOS INDEPENDIENTES cada uno con 
  su propia dirección IP.

* Presentar el diseño el viernes 11 de octubre.

* Preentrega con la implementación el lunes 28 de octubre.

* Entrega Final el viernes 1 de noviembre. Realizar una presentación con explicación 
  del sistema (1 unidad en la nota), DEMO del funcionamiento (3 unidad en la nota), 
  mostrar el portafolio (1 unidad en la nota).













Esta semana veremos varias maneras de provisionar la red WiFi a los 
sensores/actuadores y diferentes maneras de asignar la dirección de IP.

Sesión 1
---------
* ¿Cómo provisionar la red WiFi?
* ¿Cómo configurar en el enrutador direcciones IP según la dirección MAC 
  de cada dispositivo?

Sesión 2
---------
En esta sesión realizaremos la presentación del diseño de cada una de las 
experiencias finales. Se presentará el concepto y las caracterísitcas 
técnicas del sistema.

Material Adicional
--------------------
Para provisionar el ESP8266 contamos con la biblioteca `WiFiManager <https://github.com/tzapu/WiFiManager>`__. Sin embargo,
para el ESP32 la biblioteca anterior aún no es compatible. Otra biblioteca 
que podemos utilizar para este último procesador es `IotWebConf <https://github.com/prampec/IotWebConf>`__.
