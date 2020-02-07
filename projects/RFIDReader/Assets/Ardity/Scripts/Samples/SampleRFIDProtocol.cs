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
