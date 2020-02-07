/**
 * Ardity (Serial Communication for Arduino + Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */

using UnityEngine;

using System.IO.Ports;
using System.Text;

/**
 * This class contains methods that must be run from inside a thread and others
 * that must be invoked from Unity. Both types of methods are clearly marked in
 * the code, although you, the final user of this library, don't need to even
 * open this file unless you are introducing incompatibilities for upcoming
 * versions.
 * 
 * For method comments, refer to the base class.
 */
public class SerialThreadBinaryDelimited : AbstractSerialThread
{
    // Messages to/from the serial port should be delimited using this separator.
    private byte separator;
    // Buffer where a single message must fit
    private byte[] buffer = new byte[1024];
    private int bufferUsed = 0;
    private const int PRESET_VALUE = 0x0000FFFF;
    private const int POLYNOMIAL = 0x00008408;

    public SerialThreadBinaryDelimited(string portName,
                                       int baudRate,
                                       int delayBeforeReconnecting,
                                       int maxUnreadMessages,
                                       byte separator)
        : base(portName, baudRate, delayBeforeReconnecting, maxUnreadMessages, false)
    {
        this.separator = separator;
    }

    // ------------------------------------------------------------------------
    // Must include the separator already (as it shold have been passed to
    // the SendMessage method).
    // ------------------------------------------------------------------------
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

    private bool IsSeparator(byte aByte)
    {
        return aByte == separator;
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

