using System;
using System.Net;
using System.Net.Sockets;

namespace ALE.Tcp
{
    public class TcpSocketServer
    {
        public readonly Action<Exception, TcpClient> Callback;
        public string IP;
        public string Origin;
        public int Port;

        public TcpSocketServer(Action<Exception, TcpClient> callback)
        {
            if (callback == null) throw new ArgumentNullException("callback");
            Callback = callback;
        }

        public void Listen(string ip, int port, string origin)
        {
            var address = IPAddress.Parse(ip);
            var listener = new TcpListener(address, port);
            IP = ip;
            Port = port;
            Origin = origin;
            listener.Start();
            listener.BeginAcceptTcpClient(AcceptTcpClientCallback, listener);
        }

        private void AcceptTcpClientCallback(IAsyncResult result)
        {
            var listener = (TcpListener)result.AsyncState;

            //get the tcpClient and create a new WebSocket object.
            var tcpClient = listener.EndAcceptTcpClient(result);
            Callback(null, tcpClient);

            listener.BeginAcceptTcpClient(AcceptTcpClientCallback, listener);
        }
    }
}