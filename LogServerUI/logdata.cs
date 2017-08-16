using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace LogServerUI
{
    [XmlRoot("logdata")]
    public class LogData
    {
        public String devicename { get; set; }
        public String log { get; set; }
        public int type { get; set; }
        public String date { get; set; }
    }


}
