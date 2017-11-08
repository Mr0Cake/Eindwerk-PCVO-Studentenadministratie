using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMessage
{
    public static class clsSendMessage
    {
        public static void SendMessage(string strMessage)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
                    ProtocolType.Udp);
            sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            IPEndPoint iep2 = new IPEndPoint(IPAddress.Parse("192.168.1.255"), 9050);
            IPEndPoint iep = new IPEndPoint(IPAddress.Broadcast, 9050);
            byte[] data = Encoding.ASCII.GetBytes(strMessage);
            sock.SendTo(data, iep);
            sock.SendTo(data, iep2);
            sock.Close();
        }
    }
}
