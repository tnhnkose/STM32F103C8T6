namespace c_Stm32Proje2
{
    partial class Form2
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
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.comboBoxPorts = new System.Windows.Forms.ComboBox();
            this.textBoxReceive = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.buttonReceive = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.buttonSaveSql = new System.Windows.Forms.Button();
            this.textBoxLowerLimit = new System.Windows.Forms.TextBox();
            this.textBoxVolt = new System.Windows.Forms.TextBox();
            this.textBoxUpperLimit = new System.Windows.Forms.TextBox();
            this.panelOnay = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.listBoxValues = new System.Windows.Forms.ListBox();
            this.buttonADC = new System.Windows.Forms.Button();
            this.buttonCAN = new System.Windows.Forms.Button();
            this.buttonBekle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(406, 34);
            this.buttonDisconnect.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(88, 23);
            this.buttonDisconnect.TabIndex = 1;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // comboBoxPorts
            // 
            this.comboBoxPorts.FormattingEnabled = true;
            this.comboBoxPorts.Location = new System.Drawing.Point(124, 36);
            this.comboBoxPorts.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxPorts.Name = "comboBoxPorts";
            this.comboBoxPorts.Size = new System.Drawing.Size(152, 21);
            this.comboBoxPorts.TabIndex = 2;
            this.comboBoxPorts.SelectedIndexChanged += new System.EventHandler(this.comboBoxPorts_SelectedIndexChanged);
            // 
            // textBoxReceive
            // 
            this.textBoxReceive.Location = new System.Drawing.Point(124, 235);
            this.textBoxReceive.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxReceive.Multiline = true;
            this.textBoxReceive.Name = "textBoxReceive";
            this.textBoxReceive.Size = new System.Drawing.Size(401, 97);
            this.textBoxReceive.TabIndex = 4;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(295, 34);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(88, 23);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(0, 0);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 23;
            // 
            // buttonReceive
            // 
            this.buttonReceive.Location = new System.Drawing.Point(438, 349);
            this.buttonReceive.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonReceive.Name = "buttonReceive";
            this.buttonReceive.Size = new System.Drawing.Size(88, 23);
            this.buttonReceive.TabIndex = 6;
            this.buttonReceive.Text = "Receive";
            this.buttonReceive.UseVisualStyleBackColor = true;
            this.buttonReceive.Click += new System.EventHandler(this.buttonReceive_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(58, 90);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "SEND:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(46, 238);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "RECEIVE:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(58, 34);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "PORTS:";
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // buttonSaveSql
            // 
            this.buttonSaveSql.Location = new System.Drawing.Point(589, 36);
            this.buttonSaveSql.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonSaveSql.Name = "buttonSaveSql";
            this.buttonSaveSql.Size = new System.Drawing.Size(154, 23);
            this.buttonSaveSql.TabIndex = 10;
            this.buttonSaveSql.Text = "Save to SQL";
            this.buttonSaveSql.UseVisualStyleBackColor = true;
            this.buttonSaveSql.Click += new System.EventHandler(this.buttonSaveSql_Click);
            // 
            // textBoxLowerLimit
            // 
            this.textBoxLowerLimit.Location = new System.Drawing.Point(544, 125);
            this.textBoxLowerLimit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxLowerLimit.Multiline = true;
            this.textBoxLowerLimit.Name = "textBoxLowerLimit";
            this.textBoxLowerLimit.Size = new System.Drawing.Size(78, 44);
            this.textBoxLowerLimit.TabIndex = 12;
            this.textBoxLowerLimit.TextChanged += new System.EventHandler(this.textBoxLowerLimit_TextChanged_1);
            // 
            // textBoxVolt
            // 
            this.textBoxVolt.Location = new System.Drawing.Point(663, 125);
            this.textBoxVolt.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxVolt.Multiline = true;
            this.textBoxVolt.Name = "textBoxVolt";
            this.textBoxVolt.Size = new System.Drawing.Size(80, 44);
            this.textBoxVolt.TabIndex = 13;
            this.textBoxVolt.TextChanged += new System.EventHandler(this.textBoxVolt_TextChanged);
            // 
            // textBoxUpperLimit
            // 
            this.textBoxUpperLimit.Location = new System.Drawing.Point(780, 125);
            this.textBoxUpperLimit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxUpperLimit.Multiline = true;
            this.textBoxUpperLimit.Name = "textBoxUpperLimit";
            this.textBoxUpperLimit.Size = new System.Drawing.Size(78, 44);
            this.textBoxUpperLimit.TabIndex = 14;
            // 
            // panelOnay
            // 
            this.panelOnay.Location = new System.Drawing.Point(867, 125);
            this.panelOnay.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelOnay.Name = "panelOnay";
            this.panelOnay.Size = new System.Drawing.Size(52, 44);
            this.panelOnay.TabIndex = 15;
            this.panelOnay.Paint += new System.Windows.Forms.PaintEventHandler(this.panelOnay_Paint);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox1.Location = new System.Drawing.Point(544, 87);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(78, 44);
            this.textBox1.TabIndex = 16;
            this.textBox1.Text = "Enter Lower Limit";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox2.Location = new System.Drawing.Point(665, 87);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(78, 44);
            this.textBox2.TabIndex = 17;
            this.textBox2.Text = "VALUE";
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox3.Location = new System.Drawing.Point(780, 87);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(78, 44);
            this.textBox3.TabIndex = 18;
            this.textBox3.Text = "Enter Upper Limit";
            // 
            // listBoxValues
            // 
            this.listBoxValues.FormattingEnabled = true;
            this.listBoxValues.Location = new System.Drawing.Point(556, 199);
            this.listBoxValues.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.listBoxValues.Name = "listBoxValues";
            this.listBoxValues.Size = new System.Drawing.Size(330, 173);
            this.listBoxValues.TabIndex = 19;
            this.listBoxValues.SelectedIndexChanged += new System.EventHandler(this.listBoxValues_SelectedIndexChanged);
            // 
            // buttonADC
            // 
            this.buttonADC.Location = new System.Drawing.Point(124, 87);
            this.buttonADC.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonADC.Name = "buttonADC";
            this.buttonADC.Size = new System.Drawing.Size(113, 61);
            this.buttonADC.TabIndex = 20;
            this.buttonADC.Text = "ADC TEST";
            this.buttonADC.UseVisualStyleBackColor = true;
            this.buttonADC.Click += new System.EventHandler(this.buttonADC_Click);
            // 
            // buttonCAN
            // 
            this.buttonCAN.Location = new System.Drawing.Point(261, 90);
            this.buttonCAN.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonCAN.Name = "buttonCAN";
            this.buttonCAN.Size = new System.Drawing.Size(121, 58);
            this.buttonCAN.TabIndex = 21;
            this.buttonCAN.Text = "CAN TEST";
            this.buttonCAN.UseVisualStyleBackColor = true;
            this.buttonCAN.Click += new System.EventHandler(this.buttonCAN_Click);
            // 
            // buttonBekle
            // 
            this.buttonBekle.Location = new System.Drawing.Point(406, 90);
            this.buttonBekle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonBekle.Name = "buttonBekle";
            this.buttonBekle.Size = new System.Drawing.Size(119, 58);
            this.buttonBekle.TabIndex = 22;
            this.buttonBekle.Text = "BEKLE";
            this.buttonBekle.UseVisualStyleBackColor = true;
            this.buttonBekle.Click += new System.EventHandler(this.buttonBekle_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 450);
            this.Controls.Add(this.buttonBekle);
            this.Controls.Add(this.buttonCAN);
            this.Controls.Add(this.buttonADC);
            this.Controls.Add(this.listBoxValues);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panelOnay);
            this.Controls.Add(this.textBoxUpperLimit);
            this.Controls.Add(this.textBoxVolt);
            this.Controls.Add(this.textBoxLowerLimit);
            this.Controls.Add(this.buttonSaveSql);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonReceive);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textBoxReceive);
            this.Controls.Add(this.comboBoxPorts);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.buttonConnect);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.ComboBox comboBoxPorts;
        private System.Windows.Forms.TextBox textBoxReceive;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Button buttonReceive;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button buttonSaveSql;
        private System.Windows.Forms.TextBox textBoxLowerLimit;
        private System.Windows.Forms.TextBox textBoxVolt;
        private System.Windows.Forms.TextBox textBoxUpperLimit;
        private System.Windows.Forms.Panel panelOnay;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ListBox listBoxValues;
        private System.Windows.Forms.Button buttonADC;
        private System.Windows.Forms.Button buttonCAN;
        private System.Windows.Forms.Button buttonBekle;
    }
}