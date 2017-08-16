using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Diagnostics;

namespace LogServer
{
    public class AdbHelper
    {
        private static string adbFilePath = ConfigurationManager.AppSettings["AdbPath"];

        /// <summary>
        /// run adb command
        /// </summary>
        /// <param name="commandLine"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static string ExcuteAdbCommand(string commandLine, out string errorMsg)
        {
            ProcessStartInfo sti = new ProcessStartInfo(adbFilePath);
            sti.Arguments = commandLine;
            sti.CreateNoWindow = true;
            sti.RedirectStandardOutput = true;
            sti.RedirectStandardInput = true;
            sti.RedirectStandardError = true;
            sti.UseShellExecute = false;
            using (Process process = Process.Start(sti))
            {
                errorMsg = "Error:" + process.StandardError.ReadToEnd();
                string res = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                return res;
            }
        }
        /// <summary>
        /// run adb command async
        /// </summary>
        /// <param name="commandLine"></param>
        /// <param name="onErrorDataReceived"></param>
        /// <param name="onOutputDataReceived"></param>
        public static void ExcuteAdbCommandAsync(string commandLine, DataReceivedEventHandler onErrorDataReceived, DataReceivedEventHandler onOutputDataReceived)
        {
            ProcessStartInfo sti = new ProcessStartInfo(adbFilePath);
            sti.Arguments = commandLine;
            sti.CreateNoWindow = true;
            sti.RedirectStandardOutput = true;
            sti.RedirectStandardInput = true;
            sti.RedirectStandardError = true;
            sti.UseShellExecute = false;
            using (Process process = Process.Start(sti))
            {
                process.ErrorDataReceived += onErrorDataReceived;
                process.OutputDataReceived += onOutputDataReceived;
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
            }
        }
        /// <summary>
        /// push script file to mobile sdcard
        /// </summary>
        /// <param name="scriptPath"></param>
        /// <returns></returns>
        public static string PushScript(string scriptPath)
        {
            string errorMsg = string.Empty;
            StringBuilder commandLine = new StringBuilder();
            commandLine.Append("push ");
            commandLine.Append(scriptPath);
            commandLine.Append(" sdcard/");
            string resMsg = ExcuteAdbCommand(commandLine.ToString(), out errorMsg);
            if (errorMsg.Contains("No such file or directory"))
                return errorMsg;
            return string.Empty;
        }
    }
}
