using LogServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Serialization;

namespace LogServerUI
{
    public class EventListener : IEventListener
    {
        public static Dictionary<string, Queue<string>> _logQueue;
        private List<DetailForm> _detailForms = new List<DetailForm>();

        public void OnAcceptConnection(string clientId)
        {

        }

        public void openform(object detailform)
        {
            var form = (DetailForm)detailform;
            form.ShowDialog();
        }


        public void OnReceiveLog(string clientId, string log)
        {
            String[] logs = Regex.Split(log, "<data_split>");
            for (int i = 0; i < logs.Length - 1; i++)
            {
                LogData data = xmlToData(logs[i]);
                var form = _detailForms.Find(t => t.Text == data.devicename);
                if (form != null && !form.isopen)
                {
                    _detailForms.Remove(form);
                    form = null;
                }
                if (form == null)
                {
                    form = new DetailForm(data.devicename);
                    _detailForms.Add(form);
                    
                    Thread t = new Thread(openform);
                    t.Start(form);
                }

                Thread thread1 = new Thread(new ParameterizedThreadStart(form.addNewData));
                thread1.Start(data);
            }
        }

        public void OnDropConnection(string clientId)
        {
            Console.WriteLine("\n connection disconnected, clientId:" + clientId);
        }

        public LogData xmlToData(String xmlStr)
        {
            using (StringReader rdr = new StringReader(xmlStr))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(LogData));
                    LogData data = (LogData)serializer.Deserialize(rdr);
                    return data;
                }
                catch (Exception e) {
                    return new LogData();
                }
            }
        }

        void ConsoleLogs(object obj)
        {
            String endPoint = obj.ToString();
            Thread.Sleep(10);
            while (true)
            {
                if (_logQueue[endPoint].Count > 0)
                {
                    String xmlstr = _logQueue[endPoint].Dequeue();
                    String[] logs = Regex.Split(xmlstr, "<data_split>");
                    for (int i = 0; i < logs.Length-1; i++)
                    {
                        LogData data = xmlToData(logs[i]);
                        var form = _detailForms.Find(t => t.Text == endPoint);
                        if (form != null)
                        {
                            Thread thread1 = new Thread(new ParameterizedThreadStart(form.addNewData));
                            thread1.Start(data);
                        }
                    }
                }
            }
        }
    }
}

