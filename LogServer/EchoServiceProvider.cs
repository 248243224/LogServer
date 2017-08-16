using System;
using System.Collections.Generic;
using System.Text;
using TcpLib;
using System.Threading;

namespace LogServer
{
    /// <SUMMARY>
    /// EchoServiceProvider. Just replies messages received from the clients.
    /// </SUMMARY>
    public class EchoServiceProvider : TcpServiceProvider
    {
        IEventListener _enventListener;
        public override object Clone()
        {
            return new EchoServiceProvider(_enventListener);
        }
        public EchoServiceProvider(IEventListener listener = null)
        {
            _enventListener = listener;
        }

        public override void OnAcceptConnection(ConnectionState state)
        {
            string clientId = state.RemoteEndPoint.ToString();
            _enventListener?.OnAcceptConnection(clientId);
            if (!state.Write(Encoding.UTF8.GetBytes("Connect Succeed!\r\n"), 0, 14))
            {
                state.EndConnection(); //if write fails... then close connection
                _enventListener?.OnDropConnection(clientId);
            }
        }


        public override void OnReceiveData(ConnectionState state)
        {
            string receivedStr = "";
            string clientId = state.RemoteEndPoint.ToString();
            byte[] buffer = new byte[1024];
            while (state.AvailableData > 0)
            {
                int readBytes = state.Read(buffer, 0, 1024);
                if (readBytes > 0)
                {
                    receivedStr += Encoding.UTF8.GetString(buffer, 0, readBytes);
                    if (receivedStr.IndexOf("[&client_disconnected]") >= 0)
                    {
                        _enventListener?.OnDropConnection(clientId);
                    }
                    else if (receivedStr.IndexOf("<EOF>") >= 0)
                    {
                        //to do
                    }
                    else
                    {
                        _enventListener?.OnReceiveLog(clientId, receivedStr);
                    }
                }
                else
                {
                    state.EndConnection(); //If read fails then close connection
                    _enventListener?.OnDropConnection(clientId);
                }
            }
        }


        public override void OnDropConnection(string connectionId)
        {
            //Nothing to clean here
            _enventListener?.OnDropConnection(connectionId);
        }
    }
}
