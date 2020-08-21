using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArduinoKeyboard {
    public partial class Form1 : Form {
        SerialPort _serialPort;
        Thread readThread;
        bool SERIAL_READ_TIMEOUT = false;       //是否一直等待新消息
        bool _continueRead = true;
        bool connecting = false;
        StringBuilder messageList = new StringBuilder(10);
        bool bConnectOnLaunch = false;
        int iConnectOnLaunc_WaitTime = 1000;
        ArduinoMessageHandler _ArduinoMessageHandler = new ArduinoMessageHandler();

        public Form1() {
            InitializeComponent();

            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
        }

        private void Form1_Load(object sender, EventArgs e) {
            ConnectOnLaunch();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            if (_serialPort != null && _serialPort.IsOpen) {
                _serialPort.Close();
            }
            if (this.readThread != null) {
                this.readThread.Abort();
            }
        }


        private void button1_Click(object sender, EventArgs e) {
            if (connecting) {
                _serialPort.Close();
                connecting = false;
            }
            else {
                ConnectArduino();
            }

            if (this.checkBox1.Checked == false) {
                ShowConnectStatus();
            }
        }


        private void ConnectArduino() {
            if (this.checkBox1.Checked) {
                this.timer1.Enabled = true;
            }

            var portName = this.textBox1.Text;
            var baudRate = int.Parse(this.textBox2.Text);
            if (_serialPort == null) {
                //COM3 为Arduino使用的串口号，需根据实际情况调整
                //port = new SerialPort("COM3", 9600);
                _serialPort = new SerialPort(portName, baudRate);

                // Set the read/write timeouts
                // 设置后，readline 不会一直等待接收数据
                if (this.SERIAL_READ_TIMEOUT) {
                    _serialPort.ReadTimeout = 500;
                    _serialPort.WriteTimeout = 500;
                }
            }
            else {
                if (_serialPort.IsOpen) {
                    _serialPort.Close();
                }
                _serialPort.PortName = portName;
                _serialPort.BaudRate = baudRate;
            }

            try {
                _serialPort.Open();
                _continueRead = true;
            }
            catch (Exception) {
            }

            if (readThread == null) {
                readThread = new Thread(ReadMessage);
                readThread.Start();
            }
        }


        //向串口输出命令字符
        private void PortWrite(string message) {
            if (_serialPort != null && _serialPort.IsOpen) {
                _serialPort.Write(message);
                //port.WriteLine(message);
            }
        }


        public void ConnectOnLaunch() {
            if (this.bConnectOnLaunch) {
                this.checkBox1.Checked = false;

                this.timer2.Interval = this.iConnectOnLaunc_WaitTime;
                this.timer2.Enabled = true;
            }
        }



        private delegate void ShowMessageDelegate(string a);  //显示信息委托
        //函数形式参数必须是object格式
        public void ShowMessageFake(string message) {
            //CheckMessage();
            if (this.InvokeRequired) {
                var hander = new ShowMessageDelegate(ShowMessageFake);
                try {
                    this.Invoke(hander, message);
                }
                catch (Exception) { }
            }
            else {
                ShowMessage(message);
            }

        }

        public void ShowMessageFake2(string message) {
            this.messageList.AppendLine(message);
        }

        private void ShowMessage(string message) {
            if (this.checkBox1.Checked) {
                Console.WriteLine(message);
                this.richTextBox1.AppendText(message + "\n");
                this.richTextBox1.ScrollToCaret();
            }
        }

        private void ShowConnectStatus() {
            if (this.connecting) {
                this.BackColor = Color.Green;
                this.button1.Text = "断开";
                //this.button1.Enabled = false;
            }
            else {
                this.BackColor = SystemColors.Control;
                this.button1.Text = "连接";

                //this.button1.Enabled = true;
                this.timer1.Enabled = false;
            }
        }

        private void ReadMessage() {
            string message;
            while (true) {

                if (_serialPort != null && _serialPort.IsOpen) {
                    connecting = true;
                }
                else {
                    connecting = false;
                }
                if (_continueRead == false) {
                    Thread.Sleep(100);
                    continue;
                }
                if (connecting) {
                    try {
                        // 默认情况下，ReadLine 方法将一直阻止到接收到行时为止。
                        /*
                        message = _serialPort.ReadLine();
                        if (message != null) {
                            if (message == "quit") {
                                _continueRead = false;
                            }
                            else {
                                ShowMessageFake2(message);
                                _ArduinoMessageHandler.Handle(message);
                            }
                        }
                        */

                        int keycode = _serialPort.ReadByte();
                        ShowMessageFake2(keycode.ToString());
                        _ArduinoMessageHandler.Handle((byte)keycode);
                    }
                    catch (TimeoutException) { }
                    //catch (Exception) { }
                }

            }



        }

        private void timer1_Tick(object sender, EventArgs e) {
            ShowConnectStatus();

            if (this.messageList.Length > 0) {
                ShowMessage(this.messageList.ToString());
                this.messageList.Clear();
            }
        }

        // Hide to system tray
        private void HideToSystemTray() {
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
            this.ShowInTaskbar = false;
            this.notifyIcon1.Visible = true;
        }

        private void ShowForm() {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
            this.ShowInTaskbar = true;

            ShowConnectStatus();
        }

        private void Form1_Resize(object sender, EventArgs e) {
            if (this.WindowState == FormWindowState.Minimized) {
                HideToSystemTray();
            }
        }

        // Show from system tray
        private void notifyIcon1_DoubleClick(object sender, EventArgs e) {
            if (this.WindowState == FormWindowState.Minimized) {
                ShowForm();
            }
        }

        private void timer2_Tick(object sender, EventArgs e) {
            ConnectArduino();
            HideToSystemTray();

            this.timer2.Enabled = false;
        }
    }
}
