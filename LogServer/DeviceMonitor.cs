using System;
using System.Collections.Generic;
using System.Text;

namespace LogServer
{
    public class DeviceMonitor
    {
        private static System.Threading.Timer _timer;
        private static void StartDeviceMonitor()
        {
            if (_timer == null)
                _timer = new System.Threading.Timer(DeviceMonitorThread, null, 1000, 2000);
        }

        public static void Start()
        {
            StartDeviceMonitor();
        }
        public static void Stop()
        {
            if (_timer != null)
                _timer.Dispose();
        }


        private static void DeviceMonitorThread(object state)
        {
            string commandLine = "devices";
            AdbHelper.ExcuteAdbCommandAsync(commandLine, (sender, args) =>
            {
                if (args.Data != null)
                {
                    //Console.WriteLine("error msgs:" + args.Data);
                }
            }, (sender, args) =>
            {
                if (args.Data != null && args.Data.Replace("devices", "").IndexOf("device") != -1)
                {
                    string errorMsg = "";
                    string resMsg = AdbHelper.ExcuteAdbCommand("reverse --list", out errorMsg);
                    if (string.IsNullOrEmpty(resMsg.Trim()))
                        ReversePort();
                }
            });
        }

        /// <summary>
        /// revers port to enable usb connect
        /// </summary>
        private static void ReversePort()
        {
            string commandLine = @"reverse tcp:15555 tcp:15555";
            AdbHelper.ExcuteAdbCommandAsync(commandLine, (sender, args) =>
            {
                if (args.Data != null)
                {
                    //Console.WriteLine("error msgs:" + args.Data);
                }
            }, (sender, args) =>
            {
                if (args.Data != null)
                {
                    //Console.WriteLine("out put msgs:" + args.Data);
                }
            });
        }
    }
}
