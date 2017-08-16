using LogServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using TcpLib;
using System.Configuration;

namespace LogServerUI
{
    public partial class Form1 : Form
    {
        private String _ipAddress;
        private String _port;
        private ITcpServer _server;
        private ITcpServer _usbServer;

        public Form1()
        {
            InitializeComponent();
        }

        private void Start_button_Click(object sender, EventArgs e)
        {
            _ipAddress = IPAddress_textBox.Text;
            _port = Port_textBox.Text;
            IEventListener listener = new EventListener();
            EchoServiceProvider Provider = new EchoServiceProvider(listener);
            int port;

            if (int.TryParse(Port_textBox.Text, out port))
                _server = new TcpServer(Provider, IPAddress_textBox.Text, port);
            else
            {
                MessageBox.Show("invalid port!");
                return;
            }
            try
            {
                if (!_server.Start())
                {
                    MessageBox.Show("Start failed");
                    return;
                }   
                if (USB_checkBox.Checked)
                {
                    _usbServer = new TcpServer(Provider, "127.0.0.1", 15555);
                    if (!_usbServer.Start())
                       MessageBox.Show("Start USB Support failed");
                }
                MessageBox.Show("Server Started");
                Status_label.ForeColor = Color.Green;
                Status_label.Text = "Start";
                Stop_button.Enabled = true;
                Start_button.Enabled = false;         
            }
            catch (Exception ex)
            {
                MessageBox.Show("Start failed:" + ex.Message);
            }
        }

        private void Stop_button_Click(object sender, EventArgs e)
        {
            _server.Stop();
            if(_usbServer != null)
                _usbServer.Stop();
            MessageBox.Show("Server Stopped");
            Status_label.ForeColor = Color.Red;
            Status_label.Text = "Stop";
            Start_button.Enabled = true;
            Stop_button.Enabled = false;
        }

        private void CheckUSB()
        {
            string adbFilePath = ConfigurationManager.AppSettings["AdbPath"];
            if (!File.Exists(adbFilePath))
            {
                MessageBox.Show("The adb.exe does not exist,You Must set the AdbPath!");
                browser();
            }
        }

        private void browser()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "package|*.exe";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ConfigurationManager.AppSettings.Set("AdbPath", dialog.FileName);
            }
        }

        private void USB_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox)sender;
            if (checkbox.Checked)
            {
                CheckUSB();
            }
        }
    }
}
