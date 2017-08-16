using System;
using System.Collections.Generic;
using System.Text;

namespace LogServerUI
{
    public class DeviceData
    {

        public DeviceData()
        {
            logs = new List<DeviceLog>();
        }

        public String deviceName { get; set; }
        public List<DeviceLog> logs { get; set; }
    }

    public class DeviceLog
    {
        public String message { get; set; }
        public LogType type { get; set; }
        public DateTime date { get; set; }
    }

    public enum LogType {
        info=1,
        warning = 2,
        error = 0,
    }
}
