using System;
using System.Collections.Generic;
using System.Text;

namespace LogServer
{
    public interface IEventListener
    {
        /// <SUMMARY>
        /// Gets executed when the server accepts a new connection.
        /// </SUMMARY>
        void OnAcceptConnection(string clienId);

        /// <SUMMARY>
        /// Gets executed when the server detects incoming data.
        /// This method is called only if OnAcceptConnection has already finished.
        /// </SUMMARY>
        void OnReceiveLog(string clientId, string log);

        /// <SUMMARY>
        /// Gets executed when the server detects connection disconnected.
        /// </SUMMARY>
        void OnDropConnection(string clientId);
    }
}
