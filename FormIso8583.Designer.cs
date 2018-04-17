using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Iso8583Simu
{
    public partial class FormIso8583 : Form
    {
        private IContainer components = (IContainer)null;
        private DataGridView grvIso8583;
        private Panel panel1;
        private Button OK;
        private Button btnCancel;
        private Panel panel2;
        private Label label1;
        private Label label2;
        private TextBox txtMTI;
        private TextBox txtTPDU;
        private DataGridViewTextBoxColumn FieldColumn;
        private DataGridViewTextBoxColumn ValueColumn;
        private CheckBox cbkSmarlinkTemplate;
        private static FormIso8583 m_StaticForm;
        public Iso8583Data iso8583;
        private BitTemplate.Bit_Specific[] template;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.grvIso8583 = new System.Windows.Forms.DataGridView();
            this.FieldColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.OK = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbkSmarlinkTemplate = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMTI = new System.Windows.Forms.TextBox();
            this.txtTPDU = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grvIso8583)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grvIso8583
            // 
            this.grvIso8583.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvIso8583.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FieldColumn,
            this.ValueColumn});
            this.grvIso8583.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grvIso8583.Location = new System.Drawing.Point(0, 31);
            this.grvIso8583.Name = "grvIso8583";
            this.grvIso8583.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvIso8583.Size = new System.Drawing.Size(479, 342);
            this.grvIso8583.TabIndex = 0;
            // 
            // FieldColumn
            // 
            this.FieldColumn.Frozen = true;
            this.FieldColumn.HeaderText = "Field Number";
            this.FieldColumn.MaxInputLength = 3;
            this.FieldColumn.Name = "FieldColumn";
            this.FieldColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FieldColumn.Width = 50;
            // 
            // ValueColumn
            // 
            this.ValueColumn.HeaderText = "Value";
            this.ValueColumn.MaxInputLength = 900;
            this.ValueColumn.Name = "ValueColumn";
            this.ValueColumn.Width = 300;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.OK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 373);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(479, 49);
            this.panel1.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(152, 14);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(30, 14);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 0;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbkSmarlinkTemplate);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtMTI);
            this.panel2.Controls.Add(this.txtTPDU);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(479, 31);
            this.panel2.TabIndex = 2;
            // 
            // cbkSmarlinkTemplate
            // 
            this.cbkSmarlinkTemplate.AutoSize = true;
            this.cbkSmarlinkTemplate.Location = new System.Drawing.Point(347, 5);
            this.cbkSmarlinkTemplate.Name = "cbkSmarlinkTemplate";
            this.cbkSmarlinkTemplate.Size = new System.Drawing.Size(110, 17);
            this.cbkSmarlinkTemplate.TabIndex = 3;
            this.cbkSmarlinkTemplate.Text = "smartlink template";
            this.cbkSmarlinkTemplate.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(160, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Message Type";
            // 
            // txtMTI
            // 
            this.txtMTI.Location = new System.Drawing.Point(243, 6);
            this.txtMTI.MaxLength = 4;
            this.txtMTI.Name = "txtMTI";
            this.txtMTI.Size = new System.Drawing.Size(34, 20);
            this.txtMTI.TabIndex = 1;
            this.txtMTI.Text = "0200";
            // 
            // txtTPDU
            // 
            this.txtTPDU.Location = new System.Drawing.Point(55, 6);
            this.txtTPDU.MaxLength = 10;
            this.txtTPDU.Name = "txtTPDU";
            this.txtTPDU.Size = new System.Drawing.Size(75, 20);
            this.txtTPDU.TabIndex = 1;
            this.txtTPDU.Text = "6000000000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "TPDU";
            // 
            // FormIso8583
            // 
            this.ClientSize = new System.Drawing.Size(479, 422);
            this.ControlBox = false;
            this.Controls.Add(this.grvIso8583);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FormIso8583";
            this.ShowInTaskbar = false;
            this.Text = "Create Iso8583 message";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormIso8583_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grvIso8583)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        public static FormIso8583 StaticForm
        {
            get
            {
                if (FormIso8583.m_StaticForm == null)
                    FormIso8583.m_StaticForm = new FormIso8583();
                return FormIso8583.m_StaticForm;
            }
        }

        public FormIso8583()
        {
            this.InitializeComponent();
            this.grvIso8583.Rows.Add((object[])new string[2]
            {
        "3",
        "000000"
            });
            this.grvIso8583.Rows.Add((object[])new string[2]
            {
        "4",
        "000009999000"
            });
            this.grvIso8583.Rows.Add((object[])new string[2]
            {
        "11",
        "000077"
            });
            this.grvIso8583.Rows.Add((object[])new string[2]
            {
        "12",
        DateTime.Now.ToString("hhmmss")
            });
            this.grvIso8583.Rows.Add((object[])new string[2]
            {
        "13",
        DateTime.Now.ToString("MMdd")
            });
            this.grvIso8583.Rows.Add((object[])new string[2]
            {
        "22",
        "0022"
            });
            this.grvIso8583.Rows.Add((object[])new string[2]
            {
        "25",
        "00"
            });
            this.grvIso8583.Rows.Add((object[])new string[2]
            {
        "35",
        "4541822000289640D100220120389421000000"
            });
            this.grvIso8583.Rows.Add((object[])new string[2]
            {
        "41",
        "11111111"
            });
            this.grvIso8583.Rows.Add((object[])new string[2]
            {
        "42",
        "111111112345678"
            });
            this.grvIso8583.Rows.Add((object[])new string[2]
            {
        "60",
        "000003"
            });
            this.grvIso8583.Rows.Add((object[])new string[2]
            {
        "62",
        "000023"
            });
        }

        private void FormIso8583_Load(object sender, EventArgs e)
        {
        }

        private void PackData()
        {
            if (this.cbkSmarlinkTemplate.Checked)
            {
                this.iso8583 = new Iso8583Data(BitTemplate.GetSmartlinkTemplate());
                this.iso8583.HasHeader = false;
                this.iso8583.LengthInAsc = true;
            }
            else
            {
                this.iso8583 = new Iso8583Data(this.template);
                this.iso8583.TPDUHeader.UnPack(IsoUltil.StringToBCD(this.txtTPDU.Text, 10));
            }
            this.iso8583.MessageType = int.Parse(this.txtMTI.Text);
            foreach (DataGridViewRow row in (IEnumerable)this.grvIso8583.Rows)
            {
                if (row.IsNewRow)
                    break;
                this.iso8583.PackBit(int.Parse(row.Cells[0].Value.ToString()), row.Cells[1].Value.ToString());
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            try
            {
                this.PackData();
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.PackData();
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.ToString());
            }
        }

        public DialogResult ShowDialog(BitTemplate.Bit_Specific[] SpecificTemplate)
        {
            this.template = SpecificTemplate;
            return this.ShowDialog();
        }
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    //private void InitializeComponent()
    //    {
    //        this.components = new System.ComponentModel.Container();
    //        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    //        this.ClientSize = new System.Drawing.Size(800, 450);
    //        this.Text = "FormIso8583";
    //    }

        #endregion
}