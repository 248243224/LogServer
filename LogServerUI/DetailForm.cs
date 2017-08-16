using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LogServerUI
{
    public partial class DetailForm : Form
    {
        private List<LogInfo> _logs = new List<LogInfo>();
        LogInfo info;
        private bool _painted = false;
        public bool isopen = false;

        public DetailForm(String clientId)
        {
            InitializeComponent();
            this.Text = clientId;
            isopen = true;
        }

        private String setType(int type)
        {
            switch (type)
            {
                case 0:
                    return "Info";
                    break;
                case 1:
                    return "Warning";
                    break;
                case 2:
                    return "Error";
                    break;
                default: return "Unknown";
            }
        }

        private void Log_GridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (!_painted)
                SetStyle();
            _painted = true;
        }


        private void SetStyle()
        {
            int row = Log_GridView.Rows.Count;

            Log_GridView.Columns[0].Width = 60;
            Log_GridView.Columns[1].Width = 190;
            Log_GridView.Columns[2].Width = 150;
            for (int i = 0; i < row; i++)
            {
                var type = Log_GridView.Rows[i].Cells[0].Value.ToString();
                switch (type)
                {
                    case "Info":
                        {
                            Log_GridView.Rows[i].Cells[0].Style.ForeColor = Color.Green;
                            break;
                        }
                    case "Warning":
                        {
                            Log_GridView.Rows[i].Cells[0].Style.ForeColor = Color.Orange;
                            break;
                        }
                    case "Error":
                        {
                            Log_GridView.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                            break;
                        }
                    default: break;
                }
            }

            Log_GridView.FirstDisplayedScrollingRowIndex = Log_GridView.Rows[row-1].Index;
        }

        public void addNewData(object obj)
        {
            LogData logdata = (LogData)obj;
            info = new LogInfo()
            {
                date = logdata.date,
                log = logdata.log,
                type = setType(logdata.type)
            };

            try
            {
                _logs.Add(info);
                if (Log_GridView.InvokeRequired)
                {
                    Action<List<LogInfo>> actionDelegate = (x) =>
                    {
                        //rebind datasource 
                        Log_GridView.DataSource = null;
                        Log_GridView.DataSource = x;
                        SetStyle();
                    };
                    this.Log_GridView.Invoke(actionDelegate, _logs);
                }
            }
            catch (Exception e) { }
        }

        private void DetailForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            isopen = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Message_textBox.Text = null;
        }

        private void Log_GridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var gridview = (DataGridView)sender;
            for (int i = 0; i < gridview.ColumnCount; i++)
                gridview.CurrentRow.Cells[i].Selected = true;
            var value = gridview.CurrentRow.Cells[1].Value.ToString();
            var datetime = gridview.CurrentRow.Cells[2].Value.ToString();
            Message_textBox.Text = datetime + "\r\n" + value;
        }
    }
}
