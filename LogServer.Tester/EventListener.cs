using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogServer.Tester
{
    public class EventListener : IEventListener
    {
        public static Dictionary<string, ConcurrentQueue<string>> _logQueue;

        public void OnAcceptConnection(string clientId)
        {
            Console.WriteLine("\n a new client connected, clientId:" + clientId);
            if (_logQueue == null)
                _logQueue = new Dictionary<string, ConcurrentQueue<string>>();//use concurrentQueue to ensure thread safe
            if (!_logQueue.ContainsKey(clientId))
            {
                _logQueue.Add(clientId, new ConcurrentQueue<string>());
            }
            //console logs
            ConsoleLogs(clientId);
        }

        public void OnReceiveLog(string clientId, string log)
        {
            ConcurrentQueue<string> currentStateLogQueue = new ConcurrentQueue<string>();
            if (_logQueue.TryGetValue(clientId, out currentStateLogQueue))
            {
                currentStateLogQueue.Enqueue(log);
                _logQueue[clientId] = currentStateLogQueue;
            }
        }

        public void OnDropConnection(string clientId)
        {
            Console.WriteLine("\n connection disconnected, clientId:" + clientId);
        }


        static async void ConsoleLogs(string endPoint)
        {
            await Task.Delay(10);
            while (true)
            {
                string logMsg = "";
                if (_logQueue[endPoint].Count > 0 && _logQueue[endPoint].TryDequeue(out logMsg))
                {
                    Console.WriteLine(string.Format("\n client id: {0},Log Msg:{1}", endPoint, logMsg));
                }
            }
        }
    }
}
