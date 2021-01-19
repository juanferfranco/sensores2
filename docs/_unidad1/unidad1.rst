Unidad 1. Comunicaciones seriales
==============================================

Introducción
--------------

En esta primera unidad vamos a repasar algunos aspectos
importantes de sensores 1 que necesitarás de nuevo en este curso.
En especial:

* Comunicaciones seriales mediante protocolos ascii y binarios
* Técnicas de programación concurrente y programación orientada
  a objetos.

Adicionalmente, en esta unidad vamos a introducir herramientas
de trabajo en equipo bajo control de versión y de productividad.

Propósito de aprendizaje
^^^^^^^^^^^^^^^^^^^^^^^^^^

Crear aplicaciones interactivas de tiempo real que integren
sensores y actuadores mediante puertos seriales utilizando protocolos
de comunicación binarios y ascii.

Aplicar herramientas de control de versión y productividad al
desarrollo del proyecto.

Temas
^^^^^^

* Protocolos de comunicación seriales ascii y binarios.
* Arquitecturas de software concurrentes
* Control de versión

Trayecto de actividades
------------------------

Ejercicio 1: control de versión
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

* Crea una cuenta en GitHub con tu correo institucional de la UPB. Si
  tienes la tienes en hora buena.
* Explora `este <https://www.gitkraken.com/student-resources>`__ sitio y 
  solicita, por medio de tu cuenta de GitHub, el paquete de herramientas
  para estudiantes. Este proceso requiere aprobación, así que te recomiendo
  que lo hagas YA.
* Ingresa a `este <https://www.gitkraken.com/learn/git/tutorials>`__ sitio 
  para que aprendas los conceptos básicos de git.
* Finalmente, observa el siguiente `video <https://www.youtube.com/watch?v=lYAHmthUO1M>`__
  para que analices un posible flujo de trabajo en equipo utilizando herramientas
  de control de versión. Ten en cuenta que puedes seguir exactamente el mismo
  flujo para trabajar tu solo. Esto te ayudará a organizarte mucho.

Ejercicio 2: practica
^^^^^^^^^^^^^^^^^^^^^^^^

* Crear un proyecto en Unity y lo colocarás bajo control de versión.
* Luego vas a escribir 3 features que vas a implementar. Para cada feature
  sigue las estrategias del flujo de trabajo que aprendiste. No te compliques,
  la idea es que practiques control de versión, pero sobre una plataforma
  real de trabajo.

¿Para qué te pido que hagas esto? Trabajar bajo control de versión es un
estándar en la industria y es una habilidad que deberás dominar.

¿Por qué Unity? Porque es una herramienta muy versátil con la que puedes hacer
muchos tipos de productos interactivos. Te recomiendo además que aprendas
todo lo que puedas de esta herramienta. No solo es útil para desarrollar sino
también para hacer prototipos, pruebas de conceptos, maquetas digitales, entre
otros.

Te pido entonces que de ahora en adelante todos los ejercicios que vamos
a realizar los coloques bajo control de versión. ¿Para qué? para que
practiques MUCHO!

Como ANEXO a este ejercicio:

Te voy a pedir que mires de nuevo `este <https://youtu.be/S03EEusFxoI>`__. 
¿Por qué es importante para nosotros?
Por que como tu sabrás cuando hacemos aplicaciones interactivas generamos
varios archivos binarios. Por tanto git lfs será necesario en nuestros 
proyectos. 

Lo primero entonces que tendrás que hacer es instalar el soporte para Git LFS. 
Mira `aquí <https://docs.github.com/en/github/managing-large-files/installing-git-large-file-storage>`__ 
cómo lo harías para tu sistema operativo.

Ahora observa `este <https://youtu.be/LS1VI1Y8WTM>`__ video y repasa con 
`este <https://youtu.be/09McJ2NL7YM>`__ otro. Te explicarán cómo usar Unity con git lfs.

Ten en cuenta que PUEDES usar lo que has aprendido con otras herramientas, no solo Unity.

Por último te recomiendo que sigas las instrucciones del último video y practica la 
creación de un proyecto en Unity con control de versión y lfs.

Ejercicio 3: ESP32 
^^^^^^^^^^^^^^^^^^^^^

En este curso vamos a trabajar con el controlador `ESP32 <https://www.didacticaselectronicas.com/index.php/comunicaciones/bluetooth/tarjeta-de-desarrollo-esp32-wroom-32d-tarjetas-modulos-de-desarrollo-con-de-wifi-y-bluetooth-esp32u-con-conector-u-fl-tarjeta-comunicaci%C3%B3n-wi-fi-bluetooth-esp32u-iot-esp32-nodemcu-d0wd-detail>`__ 
y con el framework de arduino.

Para trabajar con el ESP32 debes instalar el soporte para esta plataforma en el IDE de
arduino como indica 
`este <https://github.com/espressif/arduino-esp32/blob/master/docs/arduino-ide/boards_manager.md>`__ 
sitio.

Ejercicio 4: protocolos ASCII
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

Vamos a recordar cómo integrar un sensor a Unity mediante el uso
de protocolos ASCII. ¿Recuerdas Ardity?

Lo primero que debes hacer es asegurarte que Ardity funciona.
Te dejo de nuevo una guía con la cual puedes recordar cómo hacerlo.
`Aquí <https://docs.google.com/presentation/d/1yNiycicVK9W4Fbeb-A8wFh6kP7vpAAaL_5aoCfUVWaU/edit?usp=sharing>`__ 
está la guía

Analicemos en detalle el DEMO. Primero, vamos a analizar el código de arduino:

.. code-block:: cpp
   :lineno-start: 1

    uint32_t last_time = 0;
    
    void setup()
    {
        Serial.begin(9600);
    }
    
    void loop()
    {
        // Print a heartbeat
        if (millis() > last_time + 2000)
        {
            Serial.println("Arduino is alive!!");
            last_time = millis();
        }
    
        // Send some message when I receive an 'A' or a 'Z'.
        switch (Serial.read())
        {
            case 'A':
                Serial.println("That's the first letter of the abecedarium.");
                break;
            case 'Z':
                Serial.println("That's the last letter of the abecedarium.");
                break;
        }
    }

Consideraciones a tener presentes con este código:

* La velocidad de comunicación es de 9600. Esa misma velocidad se tendrá que configurar
  del lado de Unity para que ambas partes se puedan entender.
* Nota que nos estamos usando la función delay(). Estamos usando millis para medir tiempos
  relativos. Noten que cada dos segundos estamos enviando un mensaje indicando que el
  arduino está activo:  ""Arduino is alive!!""
* Observa que el buffer del serial se lee constantemente. NO estamos usando
  el método available() que usualmente utilizamos, ¿Recuerda? Con available() nos aseguramos
  que el buffer de recepción tiene al menos un byte para leer; sin embargo, cuando usamos
  Serial.read() sin verificar antes que tengamos datos en el buffer, es muy posible que
  el método devuelva un -1 indicando que no había nada en el buffer de recepción.
* Por último nota que todos los mensajes enviados por arduino usan el método println.
  ¿Y esto por qué es importante? porque println enviará la información que le pasemos
  como argumento codificada en ASCII y adicionará al final 2 bytes: 0x0D y 0x0A. Estos
  bytes serán utilizados por Ardity para detectar que la cadena enviada por Arduino está completa.

Ahora analicemos la parte de Unity con Ardity. Para ello, carguemos una de las escenas ejemplo:
DemoScene_UserPoll_ReadWrite

.. image:: ../_static/scenes.jpg
   :scale: 100%
   :align: center

Nota que la escena tiene 3 gameObjects: Main Camera, SerialController y SampleUserPolling_ReadWrite.

Veamos el gameObject SampleUserPolling_ReadWrite. Este gameObject tiene dos components, un transform
y un script. El script tiene el código como tal de la aplicación del usuario.

.. image:: ../_static/user_code.jpg
   :scale: 100%
   :align: center

Nota que el script expone una variable pública: serialController. Esta variable es del tipo SerialController.

.. image:: ../_static/serialControllerVarCode.jpg
   :scale: 100%
   :align: center

Esa variable nos permite almacenar la referencia a un objeto tipo SerialController. ¿Donde estaría ese
objeto? Pues cuando el gameObject SerialController es creado note que uno de sus componentes es un objeto
de tipo SerialController:

.. image:: ../_static/serialControllerGO_Components.jpg
   :scale: 100%
   :align: center

Entonces desde el editor de Unity podemos arrastrar el gameObject SerialController al campo SerialController
del gameObject SampleUserPolling_ReadWrite y cuando se despligue la escena, automáticamente se inicializará
la variable serialController con la referencia en memoria al objeto SerialController:

.. image:: ../_static/serialControllerUnityEditor.jpg
   :scale: 100%
   :align: center

De esta manera logramos que el objeto SampleUserPolling_ReadWrite tenga acceso a la información
del objeto SerialController.

Observemos ahora qué datos y qué comportamientos tendría un objeto de tipo SampleUserPolling_ReadWrite:

.. code-block:: csharp
   :lineno-start: 1

    /**
     * Ardity (Serial Communication for Arduino + Unity)
     * Author: Daniel Wilches <dwilches@gmail.com>
     *
     * This work is released under the Creative Commons Attributions license.
     * https://creativecommons.org/licenses/by/2.0/
     */

    using UnityEngine;
    using System.Collections;

    /**
     * Sample for reading using polling by yourself, and writing too.
     */
    public class SampleUserPolling_ReadWrite : MonoBehaviour
    {
        public SerialController serialController;

        // Initialization
        void Start()
        {
            serialController = GameObject.Find("SerialController").GetComponent<SerialController>();

            Debug.Log("Press A or Z to execute some actions");
        }

        // Executed each frame
        void Update()
        {
            //---------------------------------------------------------------------
            // Send data
            //---------------------------------------------------------------------

            // If you press one of these keys send it to the serial device. A
            // sample serial device that accepts this input is given in the README.
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("Sending A");
                serialController.SendSerialMessage("A");
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("Sending Z");
                serialController.SendSerialMessage("Z");
            }


            //---------------------------------------------------------------------
            // Receive data
            //---------------------------------------------------------------------

            string message = serialController.ReadSerialMessage();

            if (message == null)
                return;

            // Check if the message is plain data or a connect/disconnect event.
            if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
                Debug.Log("Connection established");
            else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
                Debug.Log("Connection attempt failed or disconnection detected");
            else
                Debug.Log("Message arrived: " + message);
        }
    }

Vamos a realizar una prueba. Pero antes configuremos el puerto serial en el cual está conectado
el arduino. El arduino ya debe estar corriendo el código de muestra del sitio web del plugin.

.. image:: ../_static/serialControllerCOM.jpg
   :scale: 100%
   :align: center

En este caso el puerto es COM4.

Corre el programa, abre la consola y seleccione la ventana Game del Unitor de Unity. Con la ventana
seleccionada (click izquierdo del mouse), escriba las letras A y Z. Notarás los mensajes que aparecen
en la consola:

.. image:: ../_static/unityConsole.jpg
   :scale: 100%
   :align: center

Una vez la aplicación funcione note algo en el código de SampleUserPolling_ReadWrite:

.. code-block:: csharp
   :lineno-start: 1

    serialController = GameObject.Find("SerialController").GetComponent<SerialController>();

Comenta esta línea y corre la aplicación de nuevo. Funciona?

Ahora, descomenta la línea y luego borre la referencia al SerialController en el editor de Unity:

.. image:: ../_static/removeSerialControllerUnityEditor.jpg
   :scale: 100%
   :align: center

Corre de nuevo la aplicación.

* ¿Qué podemos concluir?
* ¿Para qué incluyó esta línea el autor del plugin?

Ahora analicemos el código del método Update de SampleUserPolling_ReadWrite:

.. code-block:: csharp
   :lineno-start: 1

    // Executed each frame
    void Update()
    {
      .
      .
      .
      serialController.SendSerialMessage("A");
      .
      .
      .
      string message = serialController.ReadSerialMessage();
      .
      .
      .
    }

¿Recuerda cada cuánto se llama el método Update? Ese método se llama en cada frame de la
aplicación. Lo llama automáticamente el motor de Unity

Nota los dos métodos que se resaltan:

.. code-block:: csharp
   :lineno-start: 1

    serialController.SendSerialMessage("A");
    string message = serialController.ReadSerialMessage();

Ambos métodos se llaman sobre el objeto cuya dirección en memoria está guardada en
la variable serialController.

El primer método permite enviar la letra A y el segundo permite recibir una cadena
de caracteres.

* ¿Cada cuánto se envía la letra A o la Z?
* ¿Cada cuánto leemos si nos llegaron mensajes desde el arduino?

Ahora vamos a analizar cómo transita la letra A desde el SampleUserPolling_ReadWrite hasta
el arduino.

Para enviar la letra usamos el método SendSerialMessage de la clase SerialController. Observe
que la clase tiene dos variables protegidas importantes:

.. image:: ../_static/serialControllerUMLClass.jpg
   :scale: 35%
   :align: center

.. code-block:: csharp
   :lineno-start: 1

   protected Thread thread;
   protected SerialThreadLines serialThread;

Con esas variables vamos a administrar un nuevo hilo y vamos a crear referenciar un objeto
de tipo SerialThreadLines.

En el método onEnable de SerialController tenemos:

.. code-block:: csharp
   :lineno-start: 1

   serialThread = new SerialThreadLines(portName, baudRate, reconnectionDelay, maxUnreadMessages);
   thread = new Thread(new ThreadStart(serialThread.RunForever));
   thread.Start();

Aquí vemos algo muy interesante, el código del nuevo hilo que estamos creando será RunForever y
ese código actuará sobre los datos del objeto cuya referencia está almacenada en serialThread.

Vamos a concentrarnos ahora en serialThread que es un objeto de la clase SerialThreadLines:

.. code-block:: csharp
   :lineno-start: 1

    public class SerialThreadLines : AbstractSerialThread
    {
        public SerialThreadLines(string portName,
                                 int baudRate,
                                 int delayBeforeReconnecting,
                                 int maxUnreadMessages)
            : base(portName, baudRate, delayBeforeReconnecting, maxUnreadMessages, true)
        {
        }

        protected override void SendToWire(object message, SerialPort serialPort)
        {
            serialPort.WriteLine((string) message);
        }

        protected override object ReadFromWire(SerialPort serialPort)
        {
            return serialPort.ReadLine();
        }
    }

Al ver este código no se observa por ningún lado el método RunForever (este es el código
que ejecutará nuestro hilo). ¿Dónde está? Observa que SerialThreadLines también es un
AbstractSerialThread. Entonces es de esperar que el método RunForever esté en la clase
AbstractSerialThread.

Por otro lado nota que para enviar la letra A usamos el método SendSerialMessage también
sobre los datos del objeto referenciado por serialThread del cual ya sabemos que es un
SerialThreadLines y un AbstractSerialThread

.. code-block:: csharp
   :lineno-start: 1

    public void SendSerialMessage(string message)
    {
        serialThread.SendMessage(message);
    }

Al igual que RunForever, el método SendMessage también está definido en AbstractSerialThread.

Veamos entonces ahora qué hacemos con la letra A:

.. code-block:: csharp
   :lineno-start: 1

    public void SendMessage(object message)
    {
        outputQueue.Enqueue(message);
    }

Este código nos da la clave. Lo que estamos haciendo es guardar la letra A 
que queremos transmitir en una COLA, una estructura de datos que nos ofrece el
sistema operativo para PASAR información de un HILO a otro HILO.

¿Cuáles hilos?

Pues tenemos en este momento dos hilos: el hilo del motor y el nuevo hilo que creamos antes.
El hilo que ejecutará el código RunForever sobre los datos del objeto de tipo
SerialThreadLines-AbstractSerialThread. Por tanto, observe que la letra A la estamos
guardando en la COLA del SerialThreadLines-AbstractSerialThread

Si observamos el código de RunForever:

.. code-block:: csharp
   :lineno-start: 1

    public void RunForever()
    {
        try
        {
            while (!IsStopRequested())
            {
                ...
                try
                {
                    AttemptConnection();
                    while (!IsStopRequested())
                        RunOnce();
                }
                catch (Exception ioe)
                {
                ...
                }
            }
        }
        catch (Exception e)
        {
        ...
        }
    }

Los detalles están en RunOnce():

.. code-block:: csharp
   :lineno-start: 1

    private void RunOnce()
    {
        try
        {
            // Send a message.
            if (outputQueue.Count != 0)
            {
                SendToWire(outputQueue.Dequeue(), serialPort);
            }
            object inputMessage = ReadFromWire(serialPort);
            if (inputMessage != null)
            {
                if (inputQueue.Count < maxUnreadMessages)
                {
                    inputQueue.Enqueue(inputMessage);
                }
            }
        }
        catch (TimeoutException)
        {
        }
    }

Y en este punto vemos finalmente qué es lo que pasa: para enviar la letra
A, el código del hilo pregunta si hay mensajes en la cola. Si los hay,
note que el mensaje se saca de la cola y se envía:

.. code-block:: csharp
   :lineno-start: 1

   SendToWire(outputQueue.Dequeue(), serialPort);

Si buscamos el método SendToWire en AbstractSerialThread vemos:

.. code-block:: csharp
   :lineno-start: 1
   
   protected abstract void SendToWire(object message, SerialPort serialPort);

Y aquí es donde se conectan las clases SerialThreadLines con AbstractSerialThread, ya
que el método SendToWire es abstracto, SerialThreadLines tendrá que implementarlo

.. code-block:: csharp
   :lineno-start: 1

    public class SerialThreadLines : AbstractSerialThread
    {
        ...
        protected override void SendToWire(object message, SerialPort serialPort)
        {
            serialPort.WriteLine((string) message);
        }
        ...
    }

Aquí vemos finalmente el uso de la clase SerialPort de C# con el método
`WriteLine <https://docs.microsoft.com/en-us/dotnet/api/system.io.ports.serialport.writeline?view=netframework-4.8>`__ 

Finalmente, para recibir datos desde el serial, ocurre el proceso contrario:

.. code-block:: csharp
   :lineno-start: 1


    public class SerialThreadLines : AbstractSerialThread
    {
        ...
        protected override object ReadFromWire(SerialPort serialPort)
        {
            return serialPort.ReadLine();
        }
    }

`ReadLine <https://docs.microsoft.com/en-us/dotnet/api/system.io.ports.serialport.readline?view=netframework-4.8>`__
también es la clase SerialPort. Si leemos cómo funciona ReadLine queda completamente claro la razón de usar otro
hilo:

.. warning::

  Remarks
  Note that while this method does not return the NewLine value, the NewLine value is removed from the input buffer.

  By default, the ReadLine method will block until a line is received. If this behavior is undesirable, set the
  ReadTimeout property to any non-zero value to force the ReadLine method to throw a TimeoutException if
  a line is not available on the port.

Por tanto, volviendo a RunOnce:

.. code-block:: csharp
   :lineno-start: 1

    private void RunOnce()
    {
        try
        {
            if (outputQueue.Count != 0)
            {
                SendToWire(outputQueue.Dequeue(), serialPort);
            }

           object inputMessage = ReadFromWire(serialPort);
            if (inputMessage != null)
            {
                if (inputQueue.Count < maxUnreadMessages)
                {
                    inputQueue.Enqueue(inputMessage);
                }
                else
                {
                    Debug.LogWarning("Queue is full. Dropping message: " + inputMessage);
                }
            }
        }
        catch (TimeoutException)
        {
            // This is normal, not everytime we have a report from the serial device
        }
    }

Vemos que se envía el mensaje: 

.. code-block:: csharp
   :lineno-start: 1

    SendToWire(outputQueue.Dequeue(), serialPort);

Y luego el hilo se bloquea esperando por una respuesta:

.. code-block:: csharp
   :lineno-start: 1

    object inputMessage = ReadFromWire(serialPort);

En este caso no hay respuesta, simplemente luego de enviar la letra A, el hilo
se bloquea hasta que llegue el mensaje ""Arduino is alive!!""


TEN MUY PRESENTE ESTO:

.. code-block:: csharp
   :lineno-start: 1

    private void RunOnce()
    {
        try
        {
            // Send a message.
            if (outputQueue.Count != 0)
            {
                SendToWire(outputQueue.Dequeue(), serialPort);
            }

            // Read a message.
            // If a line was read, and we have not filled our queue, enqueue
            // this line so it eventually reaches the Message Listener.
            // Otherwise, discard the line.
            object inputMessage = ReadFromWire(serialPort);
            if (inputMessage != null)
            {
                if (inputQueue.Count < maxUnreadMessages)
                {
                    inputQueue.Enqueue(inputMessage);
                }
                else
                {
                    Debug.LogWarning("Queue is full. Dropping message: " + inputMessage);
                }
            }
        }
        catch (TimeoutException)
        {
            // This is normal, not everytime we have a report from the serial device
        }
    }

Nota que primero se envía (SendToWire) y luego el hilo se bloquea (ReadFromWire). 
NO SE DESBLOQUEARÁ HASTA que no envíen una respuesta desde Arduino o pasen 100 ms 
que es el tiempo que dura bloqueada la función antes de generar una excepción de 
timeout de lectura.

.. code-block:: csharp
   :lineno-start: 1

    // Amount of milliseconds alloted to a single read or connect. An
    // exception is thrown when such operations take more than this time
    // to complete.
    private const int readTimeout = 100;

.. warning::

   SIEMPRE QUE SE ENVIÉ DESDE UNITY, EL HILO SE BLOQUEA ESPERANDO UNA RESPUESTA DEL ARDUINO. SI 
   ARDUINO NO RESPONDE DURANTE 100 MS, READLINE GENERA UNA EXCEPCIÓN DE TIMEOUT Y LUEGO 
   SE BLOQUEARÁ POR 100 MS MÁS, Y ASÍ SUCESIVAMENTE.


Ejercicio 5: protocolos binarios
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

Ahora vamos a recordar el proyecto de sensores 1 en
el cual simulamos un sensor RFID.

`Este sensor <http://www.chafon.com/productdetails.aspx?pid=382>`__ era el sensor y 
`aquí <https://drive.google.com/open?id=1uDtgNkUCknkj3iTkykwhthjLoTGJCcea>`__ está
el manual del fabricante. Finalmente, en `este <https://drive.google.com/open?id=1iVr2Fiv8wXLqNyShr_EOplSvOJBIPqJP>`__ 
archivo encuentras de nuevo la secuencia de comandos.

La solución al proyecto se compone del código de Arduino
para simular el sensor y el código de Unity para leer los
datos del sensor.

El código de Arduino de lo puedes encontrar
`aquí <https://github.com/juanferfranco/sensores2/tree/master/projects/ESP32_RFID>`__

El código de Unity está `aquí <https://github.com/juanferfranco/sensores2/tree/master/projects/RFIDReader>`__ 

La escena donde está la implementación se llama DemoScene_RFIDProtocol.

Ejercicio 6: proyecto
^^^^^^^^^^^^^^^^^^^^^^^^

Ahora piensa que quieres hacer de proyecto; sin embargo, ten presente estos
elementos mínimos:

* Debes incluir dos dispositivos seriales, es decir, tu aplicación
  interactiva debe manejar simultáneamente dos puertos.

* En un puerto implementarás un protocolo ASCII y en el otro un protocolo binario.

* La configuración (puerto serial, velocidad, etc) y el control de tu aplicación
  interactiva debe realizarse mediante una interfaz de usuario gráfica.

Recuerda que antes de comenzar el proyecto debes reunirte con tu profesor para discutir
los conceptos de la unidad y obtener luz verde para comenzar a trabajar en tu proyecto.
