Semana 16 Unidad 5: sensores y actuadores por red WiFi
========================================================

Trayecto de acciones, tiempos y formas de trabajo
---------------------------------------------------

Actividad 1
^^^^^^^^^^^^^^^^^
* Fecha: octubre 21 de 2020 - 10 a.m.
* Descripción: construcción de la aplicación en el PC.
* Recursos: ingresa al grupo de Teams
* Duración de la actividad: 1 hora 40 minutos.
* Forma de trabajo: grupal

Esta semana vamos a construir finalmente la aplicación
del PC que nos permita realizar la comunicación con los dispositivos.
Vamos a emplear UDP como protocolo de transporte y Unity como
plataforma interactiva.


Material de referencia
########################

Vamos a leer juntos `este <https://docs.google.com/presentation/d/1DEIDuHbXSiDWhJrAWZwONOC7wpsmyV-baHFjp-jsL_E/edit?usp=sharing>`__
material.

Ejercicio 1
##############
¿Recuerdas Ardity? te recomiendo qué repases cómo se creaba el hilo de serial y cómo hacíamos
la comunicación entre el hilo del motor y el hilo del serial.

Ejercicio 2
##############

Analiza con detenimiento este ejemplo. Puede ser de mucha, pero mucha utilidad para el reto
de esta semana:

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

Ejercicio 3
#############
Pon a funcionar el ejercicio anterior.

RETO 
#######
¿Recuerdas el reto de la semana pasada?

Se trata de un programa en el PC que se comunica con un controlador ESP32. El controlador
tiene conectados un sensor y un actuador.

* Ahora usa Unity para leer el sensor y modificar el actuador.
* Utiliza una tecla para preguntar por el estado del sensor. Utiliza
  otras dos teclas para encender y apagar el pulsador.
* El ESP32 tendrá conectado un pulsador y un LED (actuador).
* Define un protocolo binario para comunicar Unity y el ESP32 por UDP.

Actividad 2
^^^^^^^^^^^^^^^^^
* Fecha: octubre 21 a octubre 23 de 2020 - 10 a.m.
* Descripción: termina el RETO.
* Recursos: estudia el material de referencia
* Duración de la actividad: 5 horas.
* Forma de trabajo: individual.

Actividad 3
^^^^^^^^^^^^^^^^^
* Fecha: octubre 23 de 2020 - 10 a.m.
* Descripción: sustentación de los retos de la unidad 5: TCP, UDP, Unity y
  de la unidad 4.
* Recursos: ingresa al grupo de Teams
* Duración de la actividad: 1 hora 40 minutos.
* Forma de trabajo: sustentación individual.
