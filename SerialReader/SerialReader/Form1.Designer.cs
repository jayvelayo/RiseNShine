namespace SerialReader
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmbPortSelect = new System.Windows.Forms.ComboBox();
            this.btnConnectDisconnect = new System.Windows.Forms.Button();
            this.tmrDataProcess = new System.Windows.Forms.Timer(this.components);
            this.serCOM = new System.IO.Ports.SerialPort(this.components);
            this.lstQueueContents = new System.Windows.Forms.ListBox();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSendData = new System.Windows.Forms.Button();
            this.txtCmd = new System.Windows.Forms.TextBox();
            this.txtData0 = new System.Windows.Forms.TextBox();
            this.txtData1 = new System.Windows.Forms.TextBox();
            this.txtData2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtData = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbPortSelect
            // 
            this.cmbPortSelect.FormattingEnabled = true;
            this.cmbPortSelect.Location = new System.Drawing.Point(107, 14);
            this.cmbPortSelect.Margin = new System.Windows.Forms.Padding(2);
            this.cmbPortSelect.Name = "cmbPortSelect";
            this.cmbPortSelect.Size = new System.Drawing.Size(117, 21);
            this.cmbPortSelect.TabIndex = 0;
            this.cmbPortSelect.Text = "<select port>";
            this.cmbPortSelect.DropDown += new System.EventHandler(this.cmbPortSelect_DropDown);
            // 
            // btnConnectDisconnect
            // 
            this.btnConnectDisconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnectDisconnect.Location = new System.Drawing.Point(242, 13);
            this.btnConnectDisconnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnConnectDisconnect.Name = "btnConnectDisconnect";
            this.btnConnectDisconnect.Size = new System.Drawing.Size(121, 46);
            this.btnConnectDisconnect.TabIndex = 1;
            this.btnConnectDisconnect.Text = "connect/disconnect";
            this.btnConnectDisconnect.UseVisualStyleBackColor = true;
            this.btnConnectDisconnect.Click += new System.EventHandler(this.btnConnectDisconnect_Click);
            // 
            // tmrDataProcess
            // 
            this.tmrDataProcess.Enabled = true;
            this.tmrDataProcess.Interval = 25;
            this.tmrDataProcess.Tick += new System.EventHandler(this.tmrDataProcess_Tick);
            // 
            // serCOM
            // 
            this.serCOM.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serCOM_DataReceived);
            // 
            // lstQueueContents
            // 
            this.lstQueueContents.FormattingEnabled = true;
            this.lstQueueContents.Location = new System.Drawing.Point(9, 179);
            this.lstQueueContents.Name = "lstQueueContents";
            this.lstQueueContents.Size = new System.Drawing.Size(96, 355);
            this.lstQueueContents.TabIndex = 2;
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Items.AddRange(new object[] {
            "9600",
            "115200"});
            this.cmbBaudRate.Location = new System.Drawing.Point(107, 38);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(117, 21);
            this.cmbBaudRate.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "COM Port:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Baud Rate:";
            // 
            // btnSendData
            // 
            this.btnSendData.Enabled = false;
            this.btnSendData.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendData.Location = new System.Drawing.Point(9, 117);
            this.btnSendData.Margin = new System.Windows.Forms.Padding(2);
            this.btnSendData.Name = "btnSendData";
            this.btnSendData.Size = new System.Drawing.Size(354, 27);
            this.btnSendData.TabIndex = 15;
            this.btnSendData.Text = "Send Packet";
            this.btnSendData.UseVisualStyleBackColor = true;
            this.btnSendData.Click += new System.EventHandler(this.btnSendData_Click);
            // 
            // txtCmd
            // 
            this.txtCmd.Location = new System.Drawing.Point(9, 92);
            this.txtCmd.Name = "txtCmd";
            this.txtCmd.Size = new System.Drawing.Size(84, 20);
            this.txtCmd.TabIndex = 16;
            // 
            // txtData0
            // 
            this.txtData0.Location = new System.Drawing.Point(99, 92);
            this.txtData0.Name = "txtData0";
            this.txtData0.Size = new System.Drawing.Size(84, 20);
            this.txtData0.TabIndex = 17;
            // 
            // txtData1
            // 
            this.txtData1.Location = new System.Drawing.Point(189, 92);
            this.txtData1.Name = "txtData1";
            this.txtData1.Size = new System.Drawing.Size(84, 20);
            this.txtData1.TabIndex = 18;
            // 
            // txtData2
            // 
            this.txtData2.Location = new System.Drawing.Point(279, 92);
            this.txtData2.Name = "txtData2";
            this.txtData2.Size = new System.Drawing.Size(84, 20);
            this.txtData2.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 18);
            this.label3.TabIndex = 20;
            this.label3.Text = "CMD Byte";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(96, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.TabIndex = 21;
            this.label4.Text = "Data0 Byte";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(186, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 22;
            this.label5.Text = "Data1 Byte";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(276, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 23;
            this.label6.Text = "Data2 Byte";
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(107, 179);
            this.txtData.Multiline = true;
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(256, 355);
            this.txtData.TabIndex = 24;
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(9, 539);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(354, 27);
            this.btnClear.TabIndex = 25;
            this.btnClear.Text = "Clear screen";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 18);
            this.label7.TabIndex = 26;
            this.label7.Text = "Raw Byte";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(104, 158);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(170, 18);
            this.label8.TabIndex = 27;
            this.label8.Text = "Byte to ASCII (for debug)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 571);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtData2);
            this.Controls.Add(this.txtData1);
            this.Controls.Add(this.txtData0);
            this.Controls.Add(this.txtCmd);
            this.Controls.Add(this.btnSendData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbBaudRate);
            this.Controls.Add(this.lstQueueContents);
            this.Controls.Add(this.btnConnectDisconnect);
            this.Controls.Add(this.cmbPortSelect);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbPortSelect;
        private System.Windows.Forms.Button btnConnectDisconnect;
        private System.Windows.Forms.Timer tmrDataProcess;
        private System.IO.Ports.SerialPort serCOM;
        private System.Windows.Forms.ListBox lstQueueContents;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSendData;
        private System.Windows.Forms.TextBox txtCmd;
        private System.Windows.Forms.TextBox txtData0;
        private System.Windows.Forms.TextBox txtData1;
        private System.Windows.Forms.TextBox txtData2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}

