Semana 12
===========

Evaluación 3
-------------

Enunciado
^^^^^^^^^^^^
Realizar los siguientes proyectos:

* De la semana 10 el ejercicio 11, reto A y reto B
* De la semana 11 el reto de la sesión 2

Entrega
^^^^^^^^^^^^
* Crear una carpeta. En esa carpeta crear 4 más así: semana10_ej11, semanana10_retoA, semana10_retoB, semana11_reto.
* En cada carpeta guardar los proyectos de Arduino.
* Incluir un archivo .pdf que contenga un enlace a un video-sustentación.
* El video sustentación debe tener los siguientes elementos por cada proyecto:

    * Hacer un introducción donde diga su nombre y cuántos retos pudo realizar correctamente y cuáles no.
    * Mostrar cómo se compila cada código.
    * Mostar cómo se lanza cada programa.
    * Hacer un demo de funcionamiento de cada proyecto.
    * Explicar detalladamente cada programa. OJO: no se trata de mostar solo las partes del programa,
      se trata de explicar detalladamente cómo funciona así:

      * Primero explicar en un TABLERO cuál es la arquitectura de los programas, que partes tiene,
        cómo es el concepto de su solución y por qué lo solucionó, de esta manera y cómo es el esquema de direccionamiento
        donde indique direcciones IP y puertos.
      * Segundo, explicar el código.
      * Tercero, indicar qué dificultades tuvo solucionando los retos y cómo las superó.

* Subir el trabajo `aquí <https://www.dropbox.com/request/ZxUAJ0pdUo1ZIGxkRT0m>`__
* El plazo es hasta el sábado 18 de abril hasta las 11:59 p.m.

Nota Final
^^^^^^^^^^^^
La nota final será = La suma de cada reto ponderada con su sustentación (Funcionamiento * sustentación) y 
luego dividida por 4. 

El funcionamiento se califica como 0 o 5. Es decir, funciona o no funciona. La sustentación es un factor que
va de 0 a 1. 

El factor será:

* 1 si incluye todos los elementos solicitados en el video.
* 0.8 se incluye todo lo solicitado pero hay uno o dos errores conceptuales leves que no afectan el planteamiento
* 0.6 se incluye todo lo solicitado pero hay vacíos en las explicaciones
* 0.4 se incluye todo lo solicitado pero hay errores y vacíos conceptuales.
* 0 No se incluye todo lo pedido o hay errores conceptuales graves.

Sesión 1
---------
En esta sesión vamos a trabajar con `este <https://docs.google.com/presentation/d/1DEIDuHbXSiDWhJrAWZwONOC7wpsmyV-baHFjp-jsL_E/edit?usp=sharing>`__
material.

ES FUNDAMENTAL, repasar de nuevo a fondo cómo fuciona ardity.

Sesión 2
---------

Reto 
^^^^^^
Una vez analizado el material, se propone realizar una aplicación simple en Unity que se conecte 
con una aplicación como hercules. (Y si te animas a conectarlo con el celular en vez de hércules?)

Reto 
^^^^^^
Repita el reto anterior, pero esta vez conecte Unity con un ESP32. Recuerde, haga que el sensor
envíe datos y Unity los reciba. 


Ayuda para el reto
-------------------

.. code-block:: csharp
   :lineno-start: 1

    using System.Collections;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using UnityEngine;

    public class comm : MonoBehaviour
    {

        private static comm instance;
        private Thread receiveThread;
        private UdpClient receiveClient;
        private IPEndPoint receiveEndPoint;
        public string ip = "127.0.0.1";
        public int receivePort = 32002;
        private bool isInitialized;
        private Queue receiveQueue;
        public GameObject cube;
        private Material m_Material;

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            m_Material = cube.GetComponent<Renderer>().material;
        }

        private void Initialize()
        {
            instance = this;
            receiveEndPoint = new IPEndPoint(IPAddress.Parse(ip), receivePort);
            receiveClient = new UdpClient(receivePort);
            receiveQueue = Queue.Synchronized(new Queue());
            receiveThread = new Thread(new ThreadStart(ReceiveDataListener));
            receiveThread.IsBackground = true;
            receiveThread.Start();
            isInitialized = true;
        }

        private void ReceiveDataListener()
        {
            while (true)
            {
                try
                {
                    byte[] data = receiveClient.Receive(ref receiveEndPoint);
                    string text = Encoding.UTF8.GetString(data);
                    SerializeMessage(text);
                }
                catch (System.Exception ex)
                {
                    Debug.Log(ex.ToString());
                }
            }
        }

        private void SerializeMessage(string message)
        {
            try
            {
                string[] chain = message.Split(' ');
                string key = chain[0];
                float value = 0;
                if (float.TryParse(chain[1], out value))
                {
                    receiveQueue.Enqueue(value);
                }
            }
            catch (System.Exception e)
            {
                Debug.Log(e.ToString());
            }
        }

        private void OnDestroy()
        {
            TryKillThread();
        }

        private void OnApplicationQuit()
        {
            TryKillThread();
        }

        private void TryKillThread()
        {
            if (isInitialized)
            {
                receiveThread.Abort();
                receiveThread = null;
                receiveClient.Close();
                receiveClient = null;
                Debug.Log("Thread killed");
                isInitialized = false;
            }
        }

        void Update()
        {
            if (receiveQueue.Count != 0)
            {
                float counter = (float)receiveQueue.Dequeue();

                if(counter == 1F) m_Material.color = Color.black;
                if(counter == 2F) m_Material.color = Color.red;
            }

        }

    }
