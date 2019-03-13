Semana 8
===========
Para esta semana en la sesión 1 terminaremos la implementación de la máquina de estados y resolveremos 
las dudas en el proceso.

En la segunda sesión vamos a realizar una actvidad evaluativa para cerrar el tema de máquinas de
estados.

Enunciado para la sesión 2
------------------------------
Se requiere construir una aplicación para controlar una bomba de tiempo que al explotar libera 
felicidad y paz en el mundo. La siguiente figura ilustra la interfaz de la bomba compuesta por 
tres sensores (botones) y un actuador (LCD). Los sensores son digitales y el 
actuador es un display con interfaz de comunicación serial (lo simularemos con el PC). El controlador 
funciona así: 

.. image:: ../_static/bomb.png

* Inicia con un valor preconfigurado de 20 segundos.
* Debe iniciar en modo de configuración, es decir, inicialmente desarmado.
* En el modo de configuración, los botones UP y DOWN permiten aumentar o disminuir el timpo inicial de la bomba.
* El tiempo se puede programar entre 1 segundo y 60 segundos.
* Cada que se modifica el tiempo, se debe visualizar el valor en el LCD (enviamos el valor al PC).
* El botón ARM arma la bomba.
* Una vez armada la bomba comienza la cuenta regresiva que será visualizada en el LCD.
* La bomba explotará cuando el tiempo llegue a cero y la aplicación terminará.
* Es posible desactivar la bomba ingresando un código de seguridad. El código será: UP, DOWN, DOWN, UP, UP y ARM.
* Si la secuencia se ingresa correctamente el controlador pasará de nuevo al modo de configuración de lo contrario continuará
  la fatal cuenta regresiva.