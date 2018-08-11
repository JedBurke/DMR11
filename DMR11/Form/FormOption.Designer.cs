namespace DMR11
{
    partial class FormOption
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ProxyGroup = new System.Windows.Forms.GroupBox();
            this.ProxyPort = new System.Windows.Forms.NumericUpDown();
            this.EnableProxyCheckBox = new System.Windows.Forms.CheckBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.ProxyAddress = new System.Windows.Forms.TextBox();
            this.grpSaveDestination = new System.Windows.Forms.GroupBox();
            this.btnSaveBrowse = new System.Windows.Forms.Button();
            this.txtSaveDestination = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ProxyGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProxyPort)).BeginInit();
            this.grpSaveDestination.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Host";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "UserName";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Password";
            // 
            // ProxyGroup
            // 
            this.ProxyGroup.Controls.Add(this.ProxyPort);
            this.ProxyGroup.Controls.Add(this.EnableProxyCheckBox);
            this.ProxyGroup.Controls.Add(this.label1);
            this.ProxyGroup.Controls.Add(this.textBox4);
            this.ProxyGroup.Controls.Add(this.textBox3);
            this.ProxyGroup.Controls.Add(this.label3);
            this.ProxyGroup.Controls.Add(this.label4);
            this.ProxyGroup.Controls.Add(this.ProxyAddress);
            this.ProxyGroup.Location = new System.Drawing.Point(12, 12);
            this.ProxyGroup.Name = "ProxyGroup";
            this.ProxyGroup.Size = new System.Drawing.Size(283, 170);
            this.ProxyGroup.TabIndex = 0;
            this.ProxyGroup.TabStop = false;
            this.ProxyGroup.Text = "Proxy";
            // 
            // ProxyPort
            // 
            this.ProxyPort.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::DMR11.Properties.Settings.Default, "ProxyPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ProxyPort.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", global::DMR11.Properties.Settings.Default, "ProxyEnable", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ProxyPort.Enabled = global::DMR11.Properties.Settings.Default.ProxyEnable;
            this.ProxyPort.Location = new System.Drawing.Point(223, 43);
            this.ProxyPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.ProxyPort.Name = "ProxyPort";
            this.ProxyPort.Size = new System.Drawing.Size(54, 22);
            this.ProxyPort.TabIndex = 4;
            this.ProxyPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ProxyPort.Value = global::DMR11.Properties.Settings.Default.ProxyPort;
            // 
            // EnableProxyCheckBox
            // 
            this.EnableProxyCheckBox.AutoSize = true;
            this.EnableProxyCheckBox.Checked = global::DMR11.Properties.Settings.Default.ProxyEnable;
            this.EnableProxyCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::DMR11.Properties.Settings.Default, "ProxyEnable", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.EnableProxyCheckBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnableProxyCheckBox.Location = new System.Drawing.Point(6, 0);
            this.EnableProxyCheckBox.Name = "EnableProxyCheckBox";
            this.EnableProxyCheckBox.Size = new System.Drawing.Size(93, 19);
            this.EnableProxyCheckBox.TabIndex = 0;
            this.EnableProxyCheckBox.Text = "Enable Proxy";
            this.EnableProxyCheckBox.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            this.textBox4.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::DMR11.Properties.Settings.Default, "ProxyPassword", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox4.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", global::DMR11.Properties.Settings.Default, "ProxyEnable", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox4.Enabled = global::DMR11.Properties.Settings.Default.ProxyEnable;
            this.textBox4.Location = new System.Drawing.Point(92, 106);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(185, 22);
            this.textBox4.TabIndex = 8;
            this.textBox4.Text = global::DMR11.Properties.Settings.Default.ProxyPassword;
            this.textBox4.UseSystemPasswordChar = true;
            // 
            // textBox3
            // 
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::DMR11.Properties.Settings.Default, "ProxyUserName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", global::DMR11.Properties.Settings.Default, "ProxyEnable", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox3.Enabled = global::DMR11.Properties.Settings.Default.ProxyEnable;
            this.textBox3.Location = new System.Drawing.Point(92, 78);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(185, 22);
            this.textBox3.TabIndex = 6;
            this.textBox3.Text = global::DMR11.Properties.Settings.Default.ProxyUserName;
            // 
            // ProxyAddress
            // 
            this.ProxyAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::DMR11.Properties.Settings.Default, "ProxyHost", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ProxyAddress.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", global::DMR11.Properties.Settings.Default, "ProxyEnable", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ProxyAddress.Enabled = global::DMR11.Properties.Settings.Default.ProxyEnable;
            this.ProxyAddress.Location = new System.Drawing.Point(92, 42);
            this.ProxyAddress.Name = "ProxyAddress";
            this.ProxyAddress.Size = new System.Drawing.Size(125, 22);
            this.ProxyAddress.TabIndex = 2;
            this.ProxyAddress.Text = global::DMR11.Properties.Settings.Default.ProxyHost;
            // 
            // grpSaveDestination
            // 
            this.grpSaveDestination.Controls.Add(this.btnSaveBrowse);
            this.grpSaveDestination.Controls.Add(this.txtSaveDestination);
            this.grpSaveDestination.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSaveDestination.Location = new System.Drawing.Point(12, 188);
            this.grpSaveDestination.Name = "grpSaveDestination";
            this.grpSaveDestination.Size = new System.Drawing.Size(283, 88);
            this.grpSaveDestination.TabIndex = 1;
            this.grpSaveDestination.TabStop = false;
            this.grpSaveDestination.Text = "Save Destination";
            // 
            // btnSaveBrowse
            // 
            this.btnSaveBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSaveBrowse.Location = new System.Drawing.Point(202, 49);
            this.btnSaveBrowse.Name = "btnSaveBrowse";
            this.btnSaveBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnSaveBrowse.TabIndex = 1;
            this.btnSaveBrowse.Text = "Browse";
            this.btnSaveBrowse.UseVisualStyleBackColor = true;
            this.btnSaveBrowse.Click += new System.EventHandler(this.btnSaveBrowse_Click);
            // 
            // txtSaveDestination
            // 
            this.txtSaveDestination.Location = new System.Drawing.Point(6, 21);
            this.txtSaveDestination.Name = "txtSaveDestination";
            this.txtSaveDestination.ReadOnly = true;
            this.txtSaveDestination.Size = new System.Drawing.Size(271, 23);
            this.txtSaveDestination.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(220, 291);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(139, 291);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // FormOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ClientSize = new System.Drawing.Size(307, 326);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.grpSaveDestination);
            this.Controls.Add(this.ProxyGroup);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormOption";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.ProxyGroup.ResumeLayout(false);
            this.ProxyGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProxyPort)).EndInit();
            this.grpSaveDestination.ResumeLayout(false);
            this.grpSaveDestination.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ProxyAddress;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.CheckBox EnableProxyCheckBox;
        private System.Windows.Forms.GroupBox ProxyGroup;
        private System.Windows.Forms.NumericUpDown ProxyPort;
        private System.Windows.Forms.GroupBox grpSaveDestination;
        private System.Windows.Forms.Button btnSaveBrowse;
        private System.Windows.Forms.TextBox txtSaveDestination;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
    }
}