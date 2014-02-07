using System;
using System.Net.Sockets;

namespace ALE.Tcp
{
	public class Net
	{
		public static WebSocketServer CreateWebSocketServer(Action<Exception, WebSocket> callback) {
			return new WebSocketServer(callback);
		}

        public static TcpSocketServer CreateTcpSocketServer(Action<Exception, TcpClient> callback) {
            return new TcpSocketServer(callback);
        }
	}
}