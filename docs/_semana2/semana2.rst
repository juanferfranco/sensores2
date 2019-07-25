Semana 2
===========
Durante esta semana terminaremos el reto planteado la semana pasa.
VER CRONOGRAMA. En la sesión 1 continuaremos con el trabajo en clase. Se espera 
que en la sesión de trabajo autónomo se termine el ejercicio. En la sesión 2, 
vamos a analizar una posible solución.

Material
---------
Para el reto, tenemos dos componentes a saber. El primero es el 
`código <https://github.com/juanferfranco/sensores2/blob/master/projects/ESP32_RFID/ESP32_RFID.ino>`__ de arduino que 
simulará las tramas enviadas por sensor RFID. El segundo es el código fuente del 
proyecto en Unity.

Para construir el código del lado de Unity, seguimos estos pasos:

1. Descargar de github el código fuente de `Ardity <https://github.com/dwilches/Ardity>`__.
2. Abrir la escena DemoScene_CustomDelimiter.
3. Abrir el script SampleCustomDelimiter.cs y modificarlo con este
   `código <https://github.com/juanferfranco/sensores2/blob/master/projects/RFIDReader/SampleCustomDelimiter.cs>`__. 
   Este script es un componente del GameObject SampleCustomDelimiter.

.. code-block:: csharp
   :linenos:

    // Initialization
    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialControllerCustomDelimiter>();
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

   Note que este código permite a la aplicación en Unity interactuar con el sensor, pero 
   no directamente, sino por medio de mensajes que serán enviados a colas de comunicación.
   Recuerde que se tiene una cola para enviar mensajes al sensor y otra cola para recibir 
   mensajes.

4. SerialController es otro de los GameObjects que están en la escena. Este GamObject 
   es otro MonoBehaviour que se encarga de administrar el hilo dedicado a las comunicaciones 
   seriales. Este código no será modificado.

5. Abra el script SerialThreadBinaryDelimited.cs. Modifique este script con este 
   `código <https://github.com/juanferfranco/sensores2/blob/master/projects/RFIDReader/SerialThreadCustomDelimiter.cs>`__.
   Este es el cambio más importante al código fuente de Ardity y permite soportar el 
   protocolo binario del sensor. Adicionalmente se incluye un método para verificar el 
   checksum.


