using System;
using System.Collections.Generic;
using System.Text;

namespace LogServer
{
    public interface ITcpServer
    {
        bool Start();
        void Stop();
    }
}
