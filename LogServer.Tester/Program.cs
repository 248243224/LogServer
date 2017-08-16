using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpLib;

namespace LogServer.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            IEventListener listener = new EventListener();
            EchoServiceProvider Provider = new EchoServiceProvider(listener);
            //
            //this is for wifi connect 
            //
            ITcpServer WifiServer = new TcpServer(Provider, "10.229.18.134", 15556);
            WifiServer.Start();
            //
            //this is for usb connect 
            //
            //ITcpServer UsbServer = new TcpServer(Provider, "127.0.0.1", 15555);
            //UsbServer.Start();

            Console.WriteLine("\n Server Started...");
            Console.WriteLine("\n Press Enter to Stop...");
            Console.WriteLine("\n Begin to console logs:");

            Console.Read();
            WifiServer.Stop();
            //UsbServer.Stop();
            Console.WriteLine("\n Server Stopped...");
        }
    }
}
