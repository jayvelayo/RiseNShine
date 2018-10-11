using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Concurrent;
using System.IO.Ports;


namespace SerialReader
{
    public partial class Form1 : Form
    {
        double dequeueVariable;
        ConcurrentQueue<byte> dataQueue = new ConcurrentQueue<byte>();
        List<char> listData = new List<char>();
        bool clearStr = false;

        public Form1()
        {
            InitializeComponent();
            txtData.ScrollBars = ScrollBars.Vertical;
            txtData.WordWrap = false;
        }

        private void cmbPortSelect_DropDown(object sender, EventArgs e)
        {
            cmbPortSelect.Items.Clear();
            cmbPortSelect.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames().ToArray());
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            serCOM.Close();
        }

        private void btnConnectDisconnect_Click(object sender, EventArgs e)
        {
            //remember: open = port is active; close = port is inactive
            if (serCOM.IsOpen)
            {
                serCOM.Close();
                if (!serCOM.IsOpen) {
                    cmbPortSelect.Enabled = true;
                    cmbBaudRate.Enabled = true;
                    btnSendData.Enabled = false;
                }
            }
            else
            {
                serCOM.PortName = cmbPortSelect.SelectedItem.ToString();
                serCOM.BaudRate = int.Parse(cmbBaudRate.SelectedItem.ToString());
                serCOM.Parity = Parity.None;
                serCOM.DataBits = 8;
                serCOM.StopBits = StopBits.One;


                serCOM.Open();
                if (serCOM.IsOpen) {
                    cmbPortSelect.Enabled = false;
                    cmbBaudRate.Enabled = false;
                    btnSendData.Enabled = true;
                }

            }
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            byte[] txBuffer = new byte[9];
            int data0 = Convert.ToInt16(txtData0.Text);
            int data1 = Convert.ToInt16(txtData1.Text);
            int data2 = Convert.ToInt16(txtData2.Text);

            string cmdText = txtCmd.Text;
            char cmdTextChar = cmdText[0];

            txBuffer[0] = 0xff;
            txBuffer[1] = Convert.ToByte(cmdTextChar);
            txBuffer[2] = (byte)((data0 >> 8) & 0xff);
            txBuffer[3] = (byte)(data0 & 0xff);
            txBuffer[4] = (byte)((data1 >> 8) & 0xff);
            txBuffer[5] = (byte)(data1 & 0xff);
            txBuffer[6] = (byte)((data2 >> 8) & 0xff);
            txBuffer[7] = (byte)(data2 & 0xff);
            txBuffer[8] = (byte)((data0 + data1 + data2) & 0xff);

            serCOM.Write(txBuffer, 0, 9);

        }

        
        private void tmrDataProcess_Tick(object sender, EventArgs e)
        {
            if (serCOM.IsOpen) { btnConnectDisconnect.Text = "Disconnect"; }
            else { btnConnectDisconnect.Text = "Connect"; }

           
            byte result;
            string str = "";
            if (dataQueue.ToArray().Length >= 1)
            {
                dataQueue.TryDequeue(out result);
                lstQueueContents.Items.Add(result);
                str += ((char)result);
            }
            /*if (clearStr)
            {
                listData.Clear();
                clearStr = false;
            }*/
            txtData.AppendText(str);
            lstQueueContents.TopIndex = lstQueueContents.Items.Count - 1;

        }

        private void serCOM_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int bytes = serCOM.BytesToRead;
            byte[] buffer = new byte[bytes];
            serCOM.Read(buffer, 0, bytes);

            for (int i = 0; i < bytes; i++)
            {
                dataQueue.Enqueue(buffer[i]);
            }
            /*
            string strData = serCOM.ReadExisting();
            AppendText(txtData, strData);*/
        }

        private void AppendText(TextBox textBox, string text)
        {
            // If Invoke is required, i.e. we're not running on the UI thread, then
            // we need to invoke it so that this function gets run again but on the UI
            // thread.
            if (textBox.InvokeRequired)
            {
                textBox.BeginInvoke(new Action(() => AppendText(textBox, text)));
            }
            // We're on the UI thread, we can append the new text.
            else
            {
                textBox.AppendText(text);
            }
        }

        private void genericTextBoxEventHandler(object sender, EventArgs e)
        {
            TextBox currentTextBox = sender as TextBox;
            int parseResult;
            if (Int32.TryParse((currentTextBox.Text), out parseResult))
            {
                if (parseResult > 65535)
                    parseResult = 65535;
                if (parseResult <= 0)
                    parseResult = 0;
                currentTextBox.Text = parseResult.ToString();
            }
            else
                currentTextBox.Text = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtData.Clear();
            lstQueueContents.Items.Clear();
        }
    }
}
