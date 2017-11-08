using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMessage
{
    /// <summary>
    /// http://stackoverflow.com/questions/12316406/thread-safe-c-sharp-singleton-pattern
    /// </summary>
    public sealed class Singleton
    {
        private static volatile Singleton instance;
        private static object syncRoot = new Object();
        private static volatile Socket sock;

        private static volatile IPEndPoint iep;

        private static volatile EndPoint ep;

        private static volatile bool SocketBound = false;
        private Singleton() { }

        public static Singleton Instance
        {

            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Singleton();
                    }
                }

                return instance;
            }
        }

        public EndPoint getEndpoint()
        {
            getSocket();
            return ep;
        }

        public Socket getSocket()
        {
            if(sock == null)
            {
                lock (syncRoot)
                {
                    if(sock == null)
                    {
                        sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                        if(iep == null)
                        {
                            iep = new IPEndPoint(IPAddress.Any, 9050);
                        }
                        if(ep == null)
                        {
                            ep = (EndPoint)iep;
                        }
                    }
                    if (!SocketBound)
                    {
                        sock.Bind(iep);
                        SocketBound = true;
                    }
                }
                sock.ReceiveTimeout = 10000;
                return sock;
            }else
            {
                if (!SocketBound)
                {
                    try
                    {
                        sock.Bind(iep);
                    }catch(SocketException s)
                    {
                        Console.WriteLine(s.Message);
                    }
                    SocketBound = true;
                }
                sock.ReceiveTimeout = 10000;
                return sock;

            }
            
        }
    }

    /// <summary>
    /// http://stackoverflow.com/questions/870328/receiving-a-broadcast-message-in-c-sharp
    /// class to handle receiving and sending of broadcast network messages
    /// </summary>
    public static class clsReceiveMessage
    {

        public static volatile bool StopReceive = false;


        public static void GetString<T>(T target, Expression<Func<T, string>> outExpr)
        {
            var expr = (MemberExpression)outExpr.Body;
            var prop = (PropertyInfo)expr.Member;
            prop.SetValue(target, ReceiveMessage(), null);
            //Loop, receiving is done start receiving again
            GetString<T>(target, outExpr);
        }

        public static string ReceiveMessage()
        {
            Socket sock = new Socket(AddressFamily.InterNetwork,
            SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 9050);
            sock.Bind(iep);
            EndPoint ep = (EndPoint)iep;
            Console.WriteLine("Ready to receive…");
            byte[] data = new byte[1024];
            int recv = sock.ReceiveFrom(data, ref ep);
            string stringData = Encoding.ASCII.GetString(data, 0, recv);
            Console.WriteLine("received: {0} from: {1}",
                       stringData, ep.ToString());

            sock.Close();
            return stringData;
        }

        


        /// <summary>
        /// Receives updates broadcasted inside the network.
        /// Requires a method to handle the incoming bytes.
        /// As soon as the bytes are received, start a task and start receiving again, we don't want to miss the data
        /// </summary>
        /// <param name="ParseMessageAction"></param>
        public static void ReceiveMessage(Action<byte[], int> ParseMessageAction)
        {
            //Socket sock = new Socket(AddressFamily.InterNetwork,
            //SocketType.Dgram, ProtocolType.Udp);
            //IPEndPoint iep = new IPEndPoint(IPAddress.Any, 9050);
            
            
            
            do
            {
                byte[] data = new byte[1024];
                try
                {
                    EndPoint e = Singleton.Instance.getEndpoint();
                    int recv = Singleton.Instance.getSocket().ReceiveFrom(data, ref e);
                    Task.Factory.StartNew(() => ParseMessageAction(data, recv));
                }catch(SocketException e)
                {
                    if (e.ErrorCode == 10038 || e.ErrorCode == 10060)
                        Console.WriteLine("Timeout");
                    else
                        throw;
                }

            } while (!StopReceive); 



            Singleton.Instance.getSocket().Close();
            
        }
    }
}
