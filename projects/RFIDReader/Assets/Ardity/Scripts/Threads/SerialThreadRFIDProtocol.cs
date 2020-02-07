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
        int LSBCkecksum = uiCrcValue & 0x000000FF;
        int MSBCkecksum = (uiCrcValue & 0x0000FF00) >> 8 ;

        if ((packet[len - 2] == LSBCkecksum) && (packet[len - 1] == MSBCkecksum)) checksumOK = true;
        return checksumOK;
    }


}
