Semana 4
===========
Esta semana analizaremos una posible solución al ejercicio
planteado y realizaremos la primera evaluación sumativa.


Una posible solución al ejercicio
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
La solución al ejercicio se compone del código de Arduino
para simular el sensor y el código de Unity para leer los
datos del sensor.

Código de Arduino
-------------------

.. code-block:: cpp
    :linenos:

    #include <Arduino.h>


    //#define DEBUG
    #ifdef DEBUG
    #define DEBUG_PRINT(msg,value) Serial.print(msg); Serial.println(value)
    #else
    #define DEBUG_PRINT(msg,value)
    #endif

    void TaskReadCommand();
    unsigned int uiCrc16Cal(unsigned char const *, unsigned char);
    void parseCommad(uint8_t *);

    void setup()
    {
    Serial.begin(57600);
    }

    void loop()
    {
    TaskReadCommand();
    }

    void TaskReadCommand()
    {
    enum class serialStates{
        waitLen,
        waitData
    };
    static auto state = serialStates::waitLen;
    static uint8_t buffer[32] = {0};
    static uint8_t dataCounter = 0;

    switch (state)
    {
        case serialStates::waitLen: // wait for the first byte: len

        if (Serial.available())
        {
            buffer[dataCounter] = Serial.read();
            dataCounter++;
            state = serialStates::waitData;
            DEBUG_PRINT("Go to rx data", "");
        }
        break;

        case serialStates::waitData: // read data
        while (Serial.available())
        {
            buffer[dataCounter] = Serial.read();
            dataCounter++;

            if (dataCounter == (buffer[0] + 1))
            { // if all bytes arrived
            // verify the checksum
            DEBUG_PRINT("Verify the checksum", "");
            DEBUG_PRINT("dataCount: ", dataCounter);
            if (dataCounter >= 5)
            {
                unsigned int checksum = uiCrc16Cal(buffer, dataCounter - 2);
                uint8_t lsBChecksum = (uint8_t)(checksum & 0x000000FF);
                uint8_t msBChecksum = (uint8_t)((checksum & 0x0000FF00) >> 8);
                if ((lsBChecksum == buffer[dataCounter - 2]) && (msBChecksum == buffer[dataCounter - 1]))
                {
                DEBUG_PRINT("ChecksumOK", "");
                parseCommad(buffer);
                }
            }
            dataCounter = 0;
            state = serialStates::waitLen;
            DEBUG_PRINT("Go to rx len", "");
            }
        }
        break;
    }
    }



    void parseCommad(uint8_t *pdata)
    {
    uint8_t command = pdata[2];
    static uint8_t command21[] = {0x0D, 0x00, 0x21, 0x00, 0x02, 0x44, 0x09, 0x03, 0x4E, 0x00, 0x1E, 0x0A, 0xF2, 0x16};
    static uint8_t command24[] = {0x05, 0x00, 0x24, 0x00, 0x25, 0x29};
    static uint8_t command2F[] = {0x05, 0x00, 0x2F, 0x00, 0x8D, 0xCD};
    static uint8_t command22[] = {0x05, 0x00, 0x22, 0x00, 0xF5, 0x7D};
    static uint8_t command28[] = {0x05, 0x00, 0x28, 0x00, 0x85, 0x80};
    static uint8_t command25[] = {0x05, 0x00, 0x25, 0x00, 0xFD, 0x30};


    switch (command)
    {
        case 0x21:
        Serial.write(command21, sizeof(command21));
        break;
        case 0x24:
        Serial.write(command24, sizeof(command24));
        break;

        case 0x2F:
        Serial.write(command2F, sizeof(command2F));
        break;

        case 0x22:
        Serial.write(command22, sizeof(command22));
        break;

        case 0x28:
        Serial.write(command28, sizeof(command28));
        break;

        case 0x25:
        Serial.write(command25, sizeof(command25));
        break;
    }
    }

    unsigned int uiCrc16Cal(unsigned char const *pucY, unsigned char ucX)
    {
    const uint16_t PRESET_VALUE = 0xFFFF;
    const uint16_t POLYNOMIAL = 0x8408;


    unsigned char ucI, ucJ;
    unsigned short int uiCrcValue = PRESET_VALUE;

    for (ucI = 0; ucI < ucX; ucI++)
    {
        uiCrcValue = uiCrcValue ^ *(pucY + ucI);
        for (ucJ = 0; ucJ < 8; ucJ++)
        {
        if (uiCrcValue & 0x0001)
        {
            uiCrcValue = (uiCrcValue >> 1) ^ POLYNOMIAL;
        }
        else
        {
            uiCrcValue = (uiCrcValue >> 1);
        }
        }
    }
    return uiCrcValue;
    }

Código de Unity
-----------------
El código de Unity está compuesto de tres clases. La primera
clase implementa el protocolo, la segunda el controlador
y a tercera es la lógica de la aplicación como tal.

Código que implementa el protocolo, SerialThreadRFIDProtocol.cs:

.. code-block:: csharp
    :linenos:

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.IO.Ports;

    using System.Text;

    public class SerialThreadRFIDProtocol : AbstractSerialThread
    {
        // Buffer where a single message must fit
        private byte[] buffer = new byte[1024];
        private int bufferUsed = 0;
        private const int PRESET_VALUE = 0x0000FFFF;
        private const int POLYNOMIAL = 0x00008408;


        public SerialThreadRFIDProtocol(string portName,
                                        int baudRate,
                                        int delayBeforeReconnecting,
                                        int maxUnreadMessages)                                       
            : base(portName, baudRate, delayBeforeReconnecting, maxUnreadMessages, false)
        {

        }

        protected override void SendToWire(object message, SerialPort serialPort)
        {
            byte[] binaryMessage = (byte[])message;
            serialPort.Write(binaryMessage, 0, binaryMessage.Length);
        }

        protected override object ReadFromWire(SerialPort serialPort)
        {
            if(serialPort.BytesToRead > 0)
            {
                serialPort.Read(buffer, 0, 1);
                bufferUsed = 1;
                // wait for the rest of data
                while ( bufferUsed < (buffer[0] + 1) )
                {
                    bufferUsed = bufferUsed + serialPort.Read(buffer, bufferUsed, buffer[0]);
                }

                // Verify Checksum and
                if(verifyChecksum(buffer) == true)
                {
                    // send the package to the application
                    byte[] returnBuffer = new byte[bufferUsed];
                    System.Array.Copy(buffer, returnBuffer, bufferUsed);
                    bufferUsed = 0;
                    return returnBuffer;
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Packet: ");
                    foreach (byte data in buffer)
                    {
                        sb.Append(data.ToString("X2") + " ");
                    }
                    sb.Append("Checksum fails");
                    Debug.Log(sb);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private bool verifyChecksum(byte[] packet)
        {
            bool checksumOK = false;
            byte ucI, ucJ;
            int uiCrcValue = PRESET_VALUE;
            int len = packet[0] + 1;

            for (ucI = 0; ucI < (len - 2); ucI++)
            {
                uiCrcValue = uiCrcValue ^ packet[ucI];
                for (ucJ = 0; ucJ < 8; ucJ++)
                {
                    if ( (uiCrcValue & 0x00000001) == 0x00000001)
                    {
                        uiCrcValue = (uiCrcValue >> 1) ^ POLYNOMIAL;
                    }
                    else
                    {
                        uiCrcValue = (uiCrcValue >> 1);
                    }
                }
            }

            if ((packet[len - 2] == LSBCkecksum) && (packet[len - 1] == MSBCkecksum)) checksumOK = true;
            return checksumOK;
        }
    }

Código del controlador, SerialControllerRFIDProtocol.cs:

.. code-block:: csharp
    :linenos:

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    using System.Threading;

    public class SerialControllerRFIDProtocol : MonoBehaviour
    {
        [Tooltip("Port name with which the SerialPort object will be created.")]
        public string portName = "/dev/ttyUSB0";

        [Tooltip("Baud rate that the serial device is using to transmit data.")]
        public int baudRate = 57600;

        [Tooltip("Reference to an scene object that will receive the events of connection, " +
                "disconnection and the messages from the serial device.")]
        public GameObject messageListener;

        [Tooltip("After an error in the serial communication, or an unsuccessful " +
                "connect, how many milliseconds we should wait.")]
        public int reconnectionDelay = 1000;

        [Tooltip("Maximum number of unread data messages in the queue. " +
                "New messages will be discarded.")]
        public int maxUnreadMessages = 1;

        [Tooltip("Maximum number of unread data messages in the queue. " +
                "New messages will be discarded.")]

        // Internal reference to the Thread and the object that runs in it.
        protected Thread thread;
        protected SerialThreadRFIDProtocol serialThread;


        // ------------------------------------------------------------------------
        // Invoked whenever the SerialController gameobject is activated.
        // It creates a new thread that tries to connect to the serial device
        // and start reading from it.
        // ------------------------------------------------------------------------
        void OnEnable()
        {
            serialThread = new SerialThreadRFIDProtocol(portName,
                                                        baudRate,
                                                        reconnectionDelay,
                                                        maxUnreadMessages);
            thread = new Thread(new ThreadStart(serialThread.RunForever));
            thread.Start();
        }

        // ------------------------------------------------------------------------
        // Invoked whenever the SerialController gameobject is deactivated.
        // It stops and destroys the thread that was reading from the serial device.
        // ------------------------------------------------------------------------
        void OnDisable()
        {
            // If there is a user-defined tear-down function, execute it before
            // closing the underlying COM port.
            if (userDefinedTearDownFunction != null)
                userDefinedTearDownFunction();

            // The serialThread reference should never be null at this point,
            // unless an Exception happened in the OnEnable(), in which case I've
            // no idea what face Unity will make.
            if (serialThread != null)
            {
                serialThread.RequestStop();
                serialThread = null;
            }

            // This reference shouldn't be null at this point anyway.
            if (thread != null)
            {
                thread.Join();
                thread = null;
            }
        }

        // ------------------------------------------------------------------------
        // Polls messages from the queue that the SerialThread object keeps. Once a
        // message has been polled it is removed from the queue. There are some
        // special messages that mark the start/end of the communication with the
        // device.
        // ------------------------------------------------------------------------
        void Update()
        {
            // If the user prefers to poll the messages instead of receiving them
            // via SendMessage, then the message listener should be null.
            if (messageListener == null)
                return;

            // Read the next message from the queue
            byte[] message = ReadSerialMessage();
            if (message == null)
                return;

            // Check if the message is plain data or a connect/disconnect event.
            messageListener.SendMessage("OnMessageArrived", message);
        }

        // ------------------------------------------------------------------------
        // Returns a new unread message from the serial device. You only need to
        // call this if you don't provide a message listener.
        // ------------------------------------------------------------------------
        public byte[] ReadSerialMessage()
        {
            // Read the next message from the queue
            return (byte[]) serialThread.ReadMessage();
        }

        // ------------------------------------------------------------------------
        // Puts a message in the outgoing queue. The thread object will send the
        // message to the serial device when it considers it's appropriate.
        // ------------------------------------------------------------------------
        public void SendSerialMessage(byte[] message)
        {
            serialThread.SendMessage(message);
        }

        // ------------------------------------------------------------------------
        // Executes a user-defined function before Unity closes the COM port, so
        // the user can send some tear-down message to the hardware reliably.
        // ------------------------------------------------------------------------
        public delegate void TearDownFunction();
        private TearDownFunction userDefinedTearDownFunction;
        public void SetTearDownFunction(TearDownFunction userFunction)
        {
            this.userDefinedTearDownFunction = userFunction;
        }

    }

Código de la aplicación como tal, SampleRFIDProtocol.cs:

.. code-block:: csharp
    :linenos:

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Text;

    public class SampleRFIDProtocol : MonoBehaviour
    {
        public SerialControllerRFIDProtocol serialController;

        // Initialization
        void Start()
        {
            serialController = GameObject.Find("SerialController").GetComponent<SerialControllerRFIDProtocol>();
            Debug.Log("Q: 0x21, W: 0x24, E: 0x2F, R: 0x22, T: 0x28, Y: 0x25");
        }

        // Executed each frame
        void Update()
        {
        //---------------------------------------------------------------------
            // Send data
            //---------------------------------------------------------------------
            if (Input.GetKeyUp(KeyCode.Q))
            {
                Debug.Log("Command 04 FF 21 19 95 ");
                serialController.SendSerialMessage(new byte[] { 0x04, 0xFF, 0x21, 0x19, 0x95 });
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                Debug.Log("Command 05 00 24 00 25 29");
                serialController.SendSerialMessage(new byte[] { 0x05, 0x00, 0x24, 0x00, 0x25, 0x29 });
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                Debug.Log("Command 05 00 2F 1E 72 34 ");
                serialController.SendSerialMessage(new byte[] { 0x05, 0x00, 0x2F, 0x1E, 0x72, 0x34 });
            }
            if (Input.GetKeyUp(KeyCode.R))
            {
                Debug.Log("Command 06 00 22 31 80 E1 96 ");
                serialController.SendSerialMessage(new byte[] { 0x06, 0x00, 0x22, 0x31, 0x80, 0xE1, 0x96 });
            }
            if (Input.GetKeyUp(KeyCode.T))
            {
                Debug.Log("Command 05 00 28 05 28 D7 ");
                serialController.SendSerialMessage(new byte[] { 0x05, 0x00, 0x28, 0x05, 0x28, 0xD7 });
            }
            if (Input.GetKeyUp(KeyCode.Y))
            {
                Debug.Log("Command 05 00 25 0A A7 9F ");
                serialController.SendSerialMessage(new byte[] { 0x05, 0x00, 0x25, 0x00, 0xFD, 0x30 });
            }

            //---------------------------------------------------------------------
            // Receive data
            //---------------------------------------------------------------------

            byte[] message = serialController.ReadSerialMessage();

            if (message == null)
                return;
            StringBuilder sb = new StringBuilder();
            sb.Append("Packet: ");
            foreach (byte data in message)
            {
                sb.Append(data.ToString("X2") + " ");
            }
            Debug.Log(sb);
        }
    }

Evaluación sumativa 1
^^^^^^^^^^^^^^^^^^^^^^
En `este enlace <https://docs.google.com/document/d/172U4YEml8EKyqhClaGw_GJk5HVwZVbzA3dCIGc-1W-Q/edit?usp=sharing>`__
se puede descargar el enunciado de la evaluación sumativa 1.
