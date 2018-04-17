using System;
using System.ComponentModel;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Iso8583Simu
{
    public partial class FormClientSimulator : Form
    {
        private byte[] buffer = new byte[100000];
        private IContainer components = (IContainer)null;
        private Thread thrSending;
        private BitTemplate.Bit_Specific[] SpecificTemplate;
        //private RS232Handler rs232;
        private TextBox txtRawMessage;
        private TextBox txtParsedText;
        private Button btnUnpack;
        private GroupBox groupBox1;
        private Button btnSendTCPIP;
        private TextBox txtPort;
        private Label label2;
        private TextBox txtServer;
        private Label label1;
        private GroupBox groupBox2;
        private Button btnSendRS232;
        private GroupBox groupBox3;
        private Button button4;
        private TextBox textBox7;
        private Label label5;
        private ComboBox cboLengthType;
        private TextBox txtResponse;
        private TextBox txtRawResponse;
        private Label label4;
        private Label label6;
        private Button btnClear;
        private Label label7;
        private NumericUpDown numericUpDown1;
        private Button btnLoadTemplate;
        private OpenFileDialog openFileDialog1;
        private NumericUpDown numSendTimes;
        private Label label8;
        private Label label9;
        private ComboBox cboBaudIn;
        private ComboBox cboCOMPortIn;
        private Label label15;
        private Label label3;
        private CheckBox cbkStopWhenError;
        private CheckBox ckbOneConnection;
        private CheckBox ckbSslEnable;
        private Label label11;
        private Label label10;
        private NumericUpDown numInterval;
        private CheckBox cbkUseSTX_ETX;
        private Button btnCreateIso8583;
        private CheckBox cbkANSI;
        private CheckBox cbkSmartlinkTemplate;

        public FormClientSimulator()
        {
            this.InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = this.txtRawMessage.Text.Replace(" ", "").Replace("\r\n", ";");
            Iso8583Data iso8583Data = new Iso8583Data();
            string[] strArray = str.Split(';');
            for (int index = 0; index < strArray.Length; ++index)
                iso8583Data.PackBit(int.Parse(strArray[index].Split(':')[0]), strArray[index].Split(':')[1]);
            this.txtParsedText.Text = IsoUltil.BytesToHexString(iso8583Data.Pack(), 16, true);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private byte[] DataConverted(TextBox tbox, bool isToANSI)
        {
            byte[] numArray;
            if (isToANSI)
            {
                numArray = Encoding.Default.GetBytes(tbox.Text);
            }
            else
            {
                string _strInput = tbox.Text.Replace(" ", "").Replace("\r\n", "");
                numArray = IsoUltil.StringToBCD(_strInput, (_strInput.Length + 1) / 2);
            }
            return numArray;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DataConverted(this.txtRawMessage, this.cbkANSI.Checked);
            this.ParseData();
        }

        private void FormUnpackIso8583_Load(object sender, EventArgs e)
        {
            this.cboLengthType.SelectedIndex = 0;
            this.SpecificTemplate = BitTemplate.GetTemplate_Standard();
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void ParseData()
        {
            if (this.cbkANSI.Checked)
                return;
            Iso8583Data iso8583Data = (Iso8583Data)null;
            try
            {
                if (this.cbkSmartlinkTemplate.Checked)
                {
                    iso8583Data = new Iso8583Data(BitTemplate.GetSmartlinkTemplate());
                    iso8583Data.HasHeader = false;
                    iso8583Data.LengthInAsc = true;
                }
                else
                    iso8583Data = new Iso8583Data(this.SpecificTemplate);
                iso8583Data.EMVShowOptions = E_EMVShowOption.Len | E_EMVShowOption.VALUE | E_EMVShowOption.NAME | E_EMVShowOption.DESCRIPTION | E_EMVShowOption.BITS;
                iso8583Data.Unpack(this.DataConverted(this.txtRawMessage, this.cbkANSI.Checked));
                this.txtParsedText.Text = iso8583Data.LogFormat();
            }
            catch (Exception ex)
            {
                this.txtParsedText.Text = ex.ToString() + "\r\n" + iso8583Data.LogFormat(iso8583Data.LastBitError);
            }
        }

        public void DoSendTCPIP()
        {
            Iso8583Data iso8583Data = (Iso8583Data)null;
            this.ParseData();
            TransmittedData transmittedData = new TransmittedData((EMessageLengthType)this.cboLengthType.SelectedIndex);
            this.btnSendTCPIP.Enabled = false;
            try
            {
                TcpClient client = (TcpClient)null;
                iso8583Data = new Iso8583Data();
                if (this.ckbOneConnection.Checked)
                    client = new TcpClient(this.txtServer.Text, int.Parse(this.txtPort.Text));
                for (int index = 0; (Decimal)index < this.numSendTimes.Value; ++index)
                {
                    try
                    {
                        if (!this.ckbOneConnection.Checked)
                            client = new TcpClient(this.txtServer.Text, int.Parse(this.txtPort.Text));
                        this.txtRawResponse.Text = "";
                        client.GetStream().ReadTimeout = 100000;
                        transmittedData.Write(client.GetStream(), this.DataConverted(this.txtRawMessage, this.cbkANSI.Checked));
                        client.GetStream().Flush();
                        if (this.cbkANSI.Checked)
                        {
                            this.txtRawResponse.Text = Encoding.Default.GetString(this.buffer, 0, client.Client.Receive(this.buffer));
                        }
                        else
                        {
                            transmittedData.Read(client, (int)this.numericUpDown1.Value, false);
                            this.txtRawResponse.Text = IsoUltil.BytesToHexString(transmittedData.ReadMessage, 20, false);
                            iso8583Data.Unpack(transmittedData.ReadMessage);
                            this.txtResponse.Text = iso8583Data.LogFormat();
                        }
                        if (!this.ckbOneConnection.Checked)
                        {
                            client.Client.Shutdown(SocketShutdown.Both);
                            client.Client.Disconnect(false);
                            client.Client.Close();
                        }
                        if (this.numInterval.Value > new Decimal(0))
                            Thread.Sleep((int)this.numInterval.Value);
                    }
                    catch (Exception ex)
                    {
                        this.txtResponse.Text = ex.ToString() + "\r\n";
                        if (iso8583Data != null)
                            this.txtResponse.Text += iso8583Data.LogFormat(iso8583Data.LastBitError);
                        if (this.cbkStopWhenError.Checked)
                            throw ex;
                    }
                }
                if (this.ckbOneConnection.Checked)
                {
                    client.Client.Shutdown(SocketShutdown.Both);
                    client.Client.Disconnect(false);
                    client.Client.Close();
                }
            }
            catch (Exception ex)
            {
                this.txtResponse.Text = ex.ToString() + "\r\n";
                if (iso8583Data != null)
                    this.txtResponse.Text += iso8583Data.LogFormat(iso8583Data.LastBitError);
            }
            this.btnSendTCPIP.Enabled = true;
            this.thrSending = (Thread)null;
        }

        private void btnSendTCPIP_Click(object sender, EventArgs e)
        {
            if (this.thrSending != null)
                return;
            this.thrSending = new Thread(new ThreadStart(this.DoSendTCPIP));
            this.thrSending.Start();
        }

        public static string AboutUs
        {
            get
            {
                return "Thuocnv, ETC-Solution";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtResponse.Text = this.txtRawResponse.Text = "";
        }

        //private void btnLoadTemplate_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
        //            this.SpecificTemplate = BitTemplate.GetBINARYpecificArray(this.openFileDialog1.FileName);
        //        int num = (int)MessageBox.Show("Bits Template is changed");
        //    }
        //    catch (Exception ex)
        //    {
        //        int num = (int)MessageBox.Show(ex.ToString(), "ERROR");
        //    }
        //}

        //private void DoSendRS232()
        //{
        //    Iso8583Data iso8583Data = new Iso8583Data(this.SpecificTemplate);
        //    iso8583Data.EMVShowOptions = E_EMVShowOption.Len | E_EMVShowOption.VALUE | E_EMVShowOption.NAME | E_EMVShowOption.DESCRIPTION | E_EMVShowOption.BITS;
        //    this.btnSendRS232.Enabled = false;
        //    this.ParseData();
        //    if (this.rs232 == null)
        //        this.rs232 = new RS232Handler();
        //    try
        //    {
        //        this.rs232.MessageLengthType = (EMessageLengthType)this.cboLengthType.SelectedIndex;
        //        this.rs232.BaudRate = int.Parse(this.cboBaudIn.SelectedItem as string);
        //        this.rs232.PortName = this.cboCOMPortIn.SelectedItem as string;
        //        this.rs232.STX_ETXRule = this.cbkUseSTX_ETX.Checked;
        //        this.rs232.SendMessage(this.DataConverted(this.txtRawMessage, this.cbkANSI.Checked), (EMessageLengthType)this.cboLengthType.SelectedIndex);
        //        this.rs232.ReadMessageTimeOut = (int)this.numericUpDown1.Value;
        //        byte[] message = this.rs232.ReceiveMessage();
        //        if (message != null)
        //        {
        //            this.txtRawResponse.Text = IsoUltil.BytesToHexString(message, 20, false);
        //            iso8583Data.Unpack(message, 2, message.Length - 2);
        //            this.txtResponse.Text = iso8583Data.LogFormat();
        //        }
        //        else
        //            this.txtRawResponse.Text = "NO RESPONSE";
        //        this.rs232.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        this.txtResponse.Text = ex.ToString() + "\r\n";
        //        if (iso8583Data != null)
        //            this.txtResponse.Text += iso8583Data.LogFormat(iso8583Data.LastBitError);
        //        if (this.rs232.IsOpen)
        //            this.rs232.Close();
        //    }
        //    this.btnSendRS232.Enabled = true;
        //    this.thrSending = (Thread)null;
        //}

        //private void btnSendRS232_Click(object sender, EventArgs e)
        //{
        //    this.DoSendRS232();
        //}

        private void cboLengthType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnCreateIso8583_Click(object sender, EventArgs e)
        {
            if (FormIso8583.StaticForm.ShowDialog(this.SpecificTemplate) != DialogResult.OK)
                return;
            this.txtRawMessage.Text = IsoUltil.BytesToHexString(FormIso8583.StaticForm.iso8583.Pack(), 20, false);
        }

        private void cbkUseSTX_ETX_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void cbkANSI_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cbkANSI.Checked)
                    this.txtRawMessage.Text = Encoding.Default.GetString(this.DataConverted(this.txtRawMessage, false));
                else
                    this.txtRawMessage.Text = IsoUltil.BytesToHexString(this.DataConverted(this.txtRawMessage, true));
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("Input is incorrect");
                this.txtRawMessage.Text = "";
            }
        }

        private void cbkANSI_reponse_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cbkANSI.Checked)
                    this.txtRawMessage.Text = Encoding.Default.GetString(this.DataConverted(this.txtRawMessage, false));
                else
                    this.txtRawMessage.Text = IsoUltil.BytesToHexString(this.DataConverted(this.txtRawMessage, true));
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("Input is incorrect");
                this.txtRawMessage.Text = "";
            }
        }
        private void InitializeComponent()
        {
            this.txtRawMessage = new TextBox();
            this.txtParsedText = new TextBox();
            this.btnUnpack = new Button();
            this.groupBox1 = new GroupBox();
            this.label11 = new Label();
            this.label10 = new Label();
            this.numInterval = new NumericUpDown();
            this.cbkStopWhenError = new CheckBox();
            this.ckbOneConnection = new CheckBox();
            this.ckbSslEnable = new CheckBox();
            this.numSendTimes = new NumericUpDown();
            this.label8 = new Label();
            this.btnSendTCPIP = new Button();
            this.txtPort = new TextBox();
            this.label2 = new Label();
            this.txtServer = new TextBox();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.cbkUseSTX_ETX = new CheckBox();
            this.label9 = new Label();
            this.cboBaudIn = new ComboBox();
            this.cboCOMPortIn = new ComboBox();
            this.label15 = new Label();
            this.btnSendRS232 = new Button();
            this.groupBox3 = new GroupBox();
            this.button4 = new Button();
            this.textBox7 = new TextBox();
            this.label5 = new Label();
            this.cboLengthType = new ComboBox();
            this.txtResponse = new TextBox();
            this.txtRawResponse = new TextBox();
            this.label4 = new Label();
            this.label6 = new Label();
            this.btnClear = new Button();
            this.label7 = new Label();
            this.numericUpDown1 = new NumericUpDown();
            this.btnLoadTemplate = new Button();
            this.openFileDialog1 = new OpenFileDialog();
            this.label3 = new Label();
            this.btnCreateIso8583 = new Button();
            this.cbkANSI = new CheckBox();
            this.cbkSmartlinkTemplate = new CheckBox();
            this.groupBox1.SuspendLayout();
            this.numInterval.BeginInit();
            this.numSendTimes.BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.numericUpDown1.BeginInit();
            this.SuspendLayout();
            this.txtRawMessage.Location = new Point(12, 40);
            this.txtRawMessage.Multiline = true;
            this.txtRawMessage.Name = "txtRawMessage";
            this.txtRawMessage.Size = new Size(331, 140);
            this.txtRawMessage.TabIndex = 0;
            this.txtRawMessage.TextChanged += new EventHandler(this.textBox1_TextChanged);
            this.txtParsedText.BackColor = Color.White;
            this.txtParsedText.Location = new Point(12, 242);
            this.txtParsedText.Multiline = true;
            this.txtParsedText.Name = "txtParsedText";
            this.txtParsedText.ReadOnly = true;
            this.txtParsedText.Size = new Size(331, 214);
            this.txtParsedText.TabIndex = 2;
            this.btnUnpack.Location = new Point(12, 184);
            this.btnUnpack.Name = "btnUnpack";
            this.btnUnpack.Size = new Size(63, 23);
            this.btnUnpack.TabIndex = 3;
            this.btnUnpack.Text = "Unpack ";
            this.btnUnpack.UseVisualStyleBackColor = true;
            this.btnUnpack.Click += new EventHandler(this.button2_Click);
            this.groupBox1.Controls.Add((Control)this.label11);
            this.groupBox1.Controls.Add((Control)this.label10);
            this.groupBox1.Controls.Add((Control)this.numInterval);
            this.groupBox1.Controls.Add((Control)this.cbkStopWhenError);
            this.groupBox1.Controls.Add((Control)this.ckbOneConnection);
            this.groupBox1.Controls.Add((Control)this.ckbSslEnable);
            this.groupBox1.Controls.Add((Control)this.numSendTimes);
            this.groupBox1.Controls.Add((Control)this.label8);
            this.groupBox1.Controls.Add((Control)this.btnSendTCPIP);
            this.groupBox1.Controls.Add((Control)this.txtPort);
            this.groupBox1.Controls.Add((Control)this.label2);
            this.groupBox1.Controls.Add((Control)this.txtServer);
            this.groupBox1.Controls.Add((Control)this.label1);
            this.groupBox1.Location = new Point(349, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(197, 172);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TCPIP";
            this.label11.AutoSize = true;
            this.label11.Location = new Point(8, 120);
            this.label11.Name = "label11";
            this.label11.Size = new Size(42, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "Interval";
            this.label10.AutoSize = true;
            this.label10.Location = new Point(110, 118);
            this.label10.Name = "label10";
            this.label10.Size = new Size(20, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "ms";
            this.numInterval.Location = new Point(51, 115);
            this.numInterval.Maximum = new Decimal(new int[4]
            {
        10000,
        0,
        0,
        0
            });
            this.numInterval.Name = "numInterval";
            this.numInterval.Size = new Size(55, 20);
            this.numInterval.TabIndex = 13;
            this.cbkStopWhenError.AutoSize = true;
            this.cbkStopWhenError.Location = new Point(9, 100);
            this.cbkStopWhenError.Name = "cbkStopWhenError";
            this.cbkStopWhenError.Size = new Size(148, 17);
            this.cbkStopWhenError.TabIndex = 12;
            this.cbkStopWhenError.Text = "Stop when occurring error";
            this.cbkStopWhenError.UseVisualStyleBackColor = true;
            this.ckbOneConnection.AutoSize = true;
            this.ckbOneConnection.Checked = true;
            this.ckbOneConnection.CheckState = CheckState.Checked;
            this.ckbOneConnection.Location = new Point(9, 81);
            this.ckbOneConnection.Name = "ckbOneConnection";
            this.ckbOneConnection.Size = new Size(90, 17);
            this.ckbOneConnection.TabIndex = 11;
            this.ckbOneConnection.Text = "One Connection";
            this.ckbOneConnection.UseVisualStyleBackColor = true;
            this.ckbSslEnable.AutoSize = true;
            this.ckbSslEnable.Checked = true;
            this.ckbSslEnable.CheckState = CheckState.Checked;
            this.ckbSslEnable.Location = new Point(114, 81);
            this.ckbSslEnable.Name = "ckbSslEnable";
            this.ckbSslEnable.Size = new Size(80, 17);
            this.ckbSslEnable.TabIndex = 11;
            this.ckbSslEnable.Text = "SSL Enable";
            this.ckbSslEnable.UseVisualStyleBackColor = true;
            this.numSendTimes.Location = new Point(50, 59);
            this.numSendTimes.Maximum = new Decimal(new int[4]
            {
        1000000,
        0,
        0,
        0
            });
            this.numSendTimes.Minimum = new Decimal(new int[4]
            {
        1,
        0,
        0,
        0
            });
            this.numSendTimes.Name = "numSendTimes";
            this.numSendTimes.Size = new Size(58, 20);
            this.numSendTimes.TabIndex = 10;
            this.numSendTimes.Value = new Decimal(new int[4]
            {
        1,
        0,
        0,
        0
            });
            this.label8.AutoSize = true;
            this.label8.Location = new Point(9, 63);
            this.label8.Name = "label8";
            this.label8.Size = new Size(35, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Times";
            this.btnSendTCPIP.Location = new Point(6, 139);
            this.btnSendTCPIP.Name = "btnSendTCPIP";
            this.btnSendTCPIP.Size = new Size(75, 23);
            this.btnSendTCPIP.TabIndex = 8;
            this.btnSendTCPIP.Text = "Send";
            this.btnSendTCPIP.UseVisualStyleBackColor = true;
            this.btnSendTCPIP.Click += new EventHandler(this.btnSendTCPIP_Click);
            this.txtPort.Location = new Point(50, 36);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new Size(41, 20);
            this.txtPort.TabIndex = 7;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(6, 39);
            this.label2.Name = "label2";
            this.label2.Size = new Size(26, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Port";
            this.txtServer.Location = new Point(50, 13);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new Size(126, 20);
            this.txtServer.TabIndex = 5;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(38, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Server";
            this.groupBox2.Controls.Add((Control)this.cbkUseSTX_ETX);
            this.groupBox2.Controls.Add((Control)this.label9);
            this.groupBox2.Controls.Add((Control)this.cboBaudIn);
            this.groupBox2.Controls.Add((Control)this.cboCOMPortIn);
            this.groupBox2.Controls.Add((Control)this.label15);
            this.groupBox2.Controls.Add((Control)this.btnSendRS232);
            this.groupBox2.Location = new Point(349, 252);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(200, 122);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "RS232";
            this.cbkUseSTX_ETX.AutoSize = true;
            this.cbkUseSTX_ETX.Checked = true;
            this.cbkUseSTX_ETX.CheckState = CheckState.Checked;
            this.cbkUseSTX_ETX.Location = new Point(32, 71);
            this.cbkUseSTX_ETX.Name = "cbkUseSTX_ETX";
            this.cbkUseSTX_ETX.Size = new Size(118, 17);
            this.cbkUseSTX_ETX.TabIndex = 14;
            this.cbkUseSTX_ETX.Text = "Use STX ENX LRC";
            this.cbkUseSTX_ETX.UseVisualStyleBackColor = true;
            this.cbkUseSTX_ETX.CheckedChanged += new EventHandler(this.cbkUseSTX_ETX_CheckedChanged);
            this.label9.AutoSize = true;
            this.label9.Location = new Point(10, 47);
            this.label9.Name = "label9";
            this.label9.Size = new Size(53, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Baud rate";
            this.cboBaudIn.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboBaudIn.FormattingEnabled = true;
            this.cboBaudIn.Items.AddRange(new object[7]
            {
        (object) "115200",
        (object) "9600",
        (object) "14400",
        (object) "19200",
        (object) "28800",
        (object) "38400",
        (object) "57600"
            });
            this.cboBaudIn.Location = new Point(83, 44);
            this.cboBaudIn.Name = "cboBaudIn";
            this.cboBaudIn.Size = new Size(86, 21);
            this.cboBaudIn.TabIndex = 12;
            this.cboCOMPortIn.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboCOMPortIn.FormattingEnabled = true;
            this.cboCOMPortIn.Items.AddRange(new object[5]
            {
        (object) "COM1",
        (object) "COM2",
        (object) "COM3",
        (object) "COM4",
        (object) "COM5"
            });
            this.cboCOMPortIn.Location = new Point(83, 17);
            this.cboCOMPortIn.Name = "cboCOMPortIn";
            this.cboCOMPortIn.Size = new Size(86, 21);
            this.cboCOMPortIn.TabIndex = 11;
            this.label15.AutoSize = true;
            this.label15.Location = new Point(10, 20);
            this.label15.Name = "label15";
            this.label15.Size = new Size(61, 13);
            this.label15.TabIndex = 10;
            this.label15.Text = "COMPORT";
            this.btnSendRS232.Location = new Point(9, 93);
            this.btnSendRS232.Name = "btnSendRS232";
            this.btnSendRS232.Size = new Size(75, 23);
            this.btnSendRS232.TabIndex = 8;
            this.btnSendRS232.Text = "Send";
            this.btnSendRS232.UseVisualStyleBackColor = true;
            //this.btnSendRS232.Click += new EventHandler(this.btnSendRS232_Click);
            this.groupBox3.Controls.Add((Control)this.button4);
            this.groupBox3.Controls.Add((Control)this.textBox7);
            this.groupBox3.Controls.Add((Control)this.label5);
            this.groupBox3.Location = new Point(349, 380);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(200, 74);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Modem";
            this.button4.Location = new Point(6, 47);
            this.button4.Name = "button4";
            this.button4.Size = new Size(75, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "Send";
            this.button4.UseVisualStyleBackColor = true;
            this.textBox7.Location = new Point(74, 13);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Size(99, 20);
            this.textBox7.TabIndex = 5;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(3, 16);
            this.label5.Name = "label5";
            this.label5.Size = new Size(65, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Dial Number";
            this.label5.Click += new EventHandler(this.label5_Click);
            this.cboLengthType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboLengthType.FormattingEnabled = true;
            this.cboLengthType.Items.AddRange(new object[4]
            {
        (object) "BCD",
        (object) "High Low",
        (object) "Low High",
        (object) "None"
            });
            this.cboLengthType.Location = new Point(260, 186);
            this.cboLengthType.Name = "cboLengthType";
            this.cboLengthType.Size = new Size(83, 21);
            this.cboLengthType.TabIndex = 11;
            this.cboLengthType.SelectedIndexChanged += new EventHandler(this.cboLengthType_SelectedIndexChanged);
            this.txtResponse.BackColor = Color.White;
            this.txtResponse.Location = new Point(555, 213);
            this.txtResponse.Multiline = true;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.ReadOnly = true;
            this.txtResponse.Size = new Size(331, 243);
            this.txtResponse.TabIndex = 13;
            this.txtRawResponse.BackColor = Color.White;
            this.txtRawResponse.Location = new Point(555, 40);
            this.txtRawResponse.Multiline = true;
            this.txtRawResponse.Name = "txtRawResponse";
            this.txtRawResponse.ReadOnly = true;
            this.txtRawResponse.Size = new Size(331, 140);
            this.txtRawResponse.TabIndex = 12;
            this.label4.AutoSize = true;
            this.label4.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.label4.Location = new Point(126, 9);
            this.label4.Name = "label4";
            this.label4.Size = new Size(77, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Request";
            this.label6.AutoSize = true;
            this.label6.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.label6.Location = new Point(683, 9);
            this.label6.Name = "label6";
            this.label6.Size = new Size(90, 20);
            this.label6.TabIndex = 14;
            this.label6.Text = "Response";
            this.btnClear.Location = new Point(811, 184);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(75, 23);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            this.label7.AutoSize = true;
            this.label7.Location = new Point(566, 191);
            this.label7.Name = "label7";
            this.label7.Size = new Size(45, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Timeout";
            this.numericUpDown1.Location = new Point(618, 187);
            this.numericUpDown1.Minimum = new Decimal(new int[4]
            {
        1,
        0,
        0,
        0
            });
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new Size(42, 20);
            this.numericUpDown1.TabIndex = 16;
            this.numericUpDown1.Value = new Decimal(new int[4]
            {
        1,
        0,
        0,
        0
            });
            this.btnLoadTemplate.Location = new Point(349, 40);
            this.btnLoadTemplate.Name = "btnLoadTemplate";
            this.btnLoadTemplate.Size = new Size(188, 23);
            this.btnLoadTemplate.TabIndex = 17;
            this.btnLoadTemplate.Text = "Load specific Iso8583 template";
            this.btnLoadTemplate.UseVisualStyleBackColor = true;
            //this.btnLoadTemplate.Click += new EventHandler(this.btnLoadTemplate_Click);
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "*.xml|*.xml";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(141, 189);
            this.label3.Name = "label3";
            this.label3.Size = new Size(113, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Message Length Type";
            this.btnCreateIso8583.Location = new Point(209, 9);
            this.btnCreateIso8583.Name = "btnCreateIso8583";
            this.btnCreateIso8583.Size = new Size(134, 23);
            this.btnCreateIso8583.TabIndex = 19;
            this.btnCreateIso8583.Text = "Create Iso8583 message";
            this.btnCreateIso8583.UseVisualStyleBackColor = true;
            this.btnCreateIso8583.Click += new EventHandler(this.btnCreateIso8583_Click);
            this.cbkANSI.AutoSize = true;
            this.cbkANSI.Location = new Point(81, 188);
            this.cbkANSI.Name = "cbkANSI";
            this.cbkANSI.Size = new Size(51, 17);
            this.cbkANSI.TabIndex = 20;
            this.cbkANSI.Text = "ANSI";
            this.cbkANSI.UseVisualStyleBackColor = true;
            this.cbkANSI.CheckedChanged += new EventHandler(this.cbkANSI_CheckedChanged);
            this.cbkSmartlinkTemplate.AutoSize = true;
            this.cbkSmartlinkTemplate.Location = new Point(13, 218);
            this.cbkSmartlinkTemplate.Name = "cbkSmartlinkTemplate";
            this.cbkSmartlinkTemplate.Size = new Size(112, 17);
            this.cbkSmartlinkTemplate.TabIndex = 21;
            this.cbkSmartlinkTemplate.Text = "SmartlinkMessage";
            this.cbkSmartlinkTemplate.UseVisualStyleBackColor = true;
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(896, 468);
            this.Controls.Add((Control)this.cbkSmartlinkTemplate);
            this.Controls.Add((Control)this.cbkANSI);
            this.Controls.Add((Control)this.btnCreateIso8583);
            this.Controls.Add((Control)this.label3);
            this.Controls.Add((Control)this.btnLoadTemplate);
            this.Controls.Add((Control)this.numericUpDown1);
            this.Controls.Add((Control)this.label7);
            this.Controls.Add((Control)this.label6);
            this.Controls.Add((Control)this.label4);
            this.Controls.Add((Control)this.txtResponse);
            this.Controls.Add((Control)this.txtRawResponse);
            this.Controls.Add((Control)this.cboLengthType);
            this.Controls.Add((Control)this.groupBox3);
            this.Controls.Add((Control)this.groupBox2);
            this.Controls.Add((Control)this.groupBox1);
            this.Controls.Add((Control)this.btnClear);
            this.Controls.Add((Control)this.btnUnpack);
            this.Controls.Add((Control)this.txtParsedText);
            this.Controls.Add((Control)this.txtRawMessage);
            this.Name = nameof(FormClientSimulator);
            this.Text = "Terminal Simulation Tools - ETC SOLUTION";
            this.Load += new EventHandler(this.FormUnpackIso8583_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.numInterval.EndInit();
            this.numSendTimes.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.numericUpDown1.EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
