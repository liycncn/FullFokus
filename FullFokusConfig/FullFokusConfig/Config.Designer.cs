namespace FullFokusConfig
{
  partial class Config
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
      this.cmdOK = new System.Windows.Forms.Button();
      this.cmdCancel = new System.Windows.Forms.Button();
      this.gbxTime = new System.Windows.Forms.GroupBox();
      this.lblMinute = new System.Windows.Forms.Label();
      this.lblTime = new System.Windows.Forms.Label();
      this.mskTime = new System.Windows.Forms.MaskedTextBox();
      this.chkTimed = new System.Windows.Forms.CheckBox();
      this.gbxInstalled = new System.Windows.Forms.GroupBox();
      this.chkInstalled = new System.Windows.Forms.CheckBox();
      this.cmbInstalled = new System.Windows.Forms.ComboBox();
      this.gbxManual = new System.Windows.Forms.GroupBox();
      this.btnDir = new System.Windows.Forms.Button();
      this.txtDir = new System.Windows.Forms.TextBox();
      this.ofdDir = new System.Windows.Forms.OpenFileDialog();
      this.chkManual = new System.Windows.Forms.CheckBox();
      this.gbxTime.SuspendLayout();
      this.gbxInstalled.SuspendLayout();
      this.gbxManual.SuspendLayout();
      this.SuspendLayout();
      // 
      // cmdOK
      // 
      this.cmdOK.Location = new System.Drawing.Point(336, 43);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.Size = new System.Drawing.Size(75, 23);
      this.cmdOK.TabIndex = 3;
      this.cmdOK.Text = "确定";
      this.cmdOK.UseVisualStyleBackColor = true;
      this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
      // 
      // cmdCancel
      // 
      this.cmdCancel.Location = new System.Drawing.Point(336, 99);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new System.Drawing.Size(75, 23);
      this.cmdCancel.TabIndex = 4;
      this.cmdCancel.Text = "取消";
      this.cmdCancel.UseVisualStyleBackColor = true;
      this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
      // 
      // gbxTime
      // 
      this.gbxTime.Controls.Add(this.lblMinute);
      this.gbxTime.Controls.Add(this.lblTime);
      this.gbxTime.Controls.Add(this.mskTime);
      this.gbxTime.Controls.Add(this.chkTimed);
      this.gbxTime.Location = new System.Drawing.Point(12, 12);
      this.gbxTime.Name = "gbxTime";
      this.gbxTime.Size = new System.Drawing.Size(249, 153);
      this.gbxTime.TabIndex = 7;
      this.gbxTime.TabStop = false;
      this.gbxTime.Text = "时间";
      // 
      // lblMinute
      // 
      this.lblMinute.AutoSize = true;
      this.lblMinute.Location = new System.Drawing.Point(197, 112);
      this.lblMinute.Name = "lblMinute";
      this.lblMinute.Size = new System.Drawing.Size(31, 13);
      this.lblMinute.TabIndex = 9;
      this.lblMinute.Text = "分钟";
      // 
      // lblTime
      // 
      this.lblTime.AutoSize = true;
      this.lblTime.Location = new System.Drawing.Point(22, 70);
      this.lblTime.Name = "lblTime";
      this.lblTime.Size = new System.Drawing.Size(67, 13);
      this.lblTime.TabIndex = 8;
      this.lblTime.Text = "指定时间：";
      // 
      // mskTime
      // 
      this.mskTime.Location = new System.Drawing.Point(25, 109);
      this.mskTime.Mask = "00000";
      this.mskTime.Name = "mskTime";
      this.mskTime.Size = new System.Drawing.Size(156, 20);
      this.mskTime.TabIndex = 7;
      this.mskTime.ValidatingType = typeof(int);
      // 
      // chkTimed
      // 
      this.chkTimed.AutoSize = true;
      this.chkTimed.Location = new System.Drawing.Point(25, 27);
      this.chkTimed.Name = "chkTimed";
      this.chkTimed.Size = new System.Drawing.Size(74, 17);
      this.chkTimed.TabIndex = 6;
      this.chkTimed.Text = "强制模式";
      this.chkTimed.UseVisualStyleBackColor = true;
      this.chkTimed.CheckedChanged += new System.EventHandler(this.chkTimed_CheckedChanged);
      // 
      // gbxInstalled
      // 
      this.gbxInstalled.Controls.Add(this.chkInstalled);
      this.gbxInstalled.Controls.Add(this.cmbInstalled);
      this.gbxInstalled.Location = new System.Drawing.Point(12, 187);
      this.gbxInstalled.Name = "gbxInstalled";
      this.gbxInstalled.Size = new System.Drawing.Size(503, 123);
      this.gbxInstalled.TabIndex = 8;
      this.gbxInstalled.TabStop = false;
      this.gbxInstalled.Text = "从已安装程序列表里选择";
      // 
      // chkInstalled
      // 
      this.chkInstalled.AutoSize = true;
      this.chkInstalled.Location = new System.Drawing.Point(25, 44);
      this.chkInstalled.Name = "chkInstalled";
      this.chkInstalled.Size = new System.Drawing.Size(98, 17);
      this.chkInstalled.TabIndex = 9;
      this.chkInstalled.Text = "从列表里选择";
      this.chkInstalled.UseVisualStyleBackColor = true;
      this.chkInstalled.CheckedChanged += new System.EventHandler(this.chkInstalled_CheckedChanged);
      // 
      // cmbInstalled
      // 
      this.cmbInstalled.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbInstalled.FormattingEnabled = true;
      this.cmbInstalled.Location = new System.Drawing.Point(15, 86);
      this.cmbInstalled.Name = "cmbInstalled";
      this.cmbInstalled.Size = new System.Drawing.Size(482, 21);
      this.cmbInstalled.Sorted = true;
      this.cmbInstalled.TabIndex = 7;
      // 
      // gbxManual
      // 
      this.gbxManual.Controls.Add(this.chkManual);
      this.gbxManual.Controls.Add(this.btnDir);
      this.gbxManual.Controls.Add(this.txtDir);
      this.gbxManual.Location = new System.Drawing.Point(12, 329);
      this.gbxManual.Name = "gbxManual";
      this.gbxManual.Size = new System.Drawing.Size(503, 122);
      this.gbxManual.TabIndex = 9;
      this.gbxManual.TabStop = false;
      this.gbxManual.Text = "手动指定(任何程序)";
      // 
      // btnDir
      // 
      this.btnDir.Location = new System.Drawing.Point(456, 82);
      this.btnDir.Name = "btnDir";
      this.btnDir.Size = new System.Drawing.Size(28, 23);
      this.btnDir.TabIndex = 11;
      this.btnDir.Text = "...";
      this.btnDir.UseVisualStyleBackColor = true;
      this.btnDir.Click += new System.EventHandler(this.btnDir_Click);
      // 
      // txtDir
      // 
      this.txtDir.Location = new System.Drawing.Point(15, 84);
      this.txtDir.Name = "txtDir";
      this.txtDir.Size = new System.Drawing.Size(426, 20);
      this.txtDir.TabIndex = 10;
      // 
      // ofdDir
      // 
      this.ofdDir.DefaultExt = "exe";
      this.ofdDir.Filter = "Executable files (.exe)|*.exe|All Files (*.*)|*.*";
      this.ofdDir.FileOk += new System.ComponentModel.CancelEventHandler(this.ofdDir_FileOk);
      // 
      // chkManual
      // 
      this.chkManual.AutoSize = true;
      this.chkManual.Location = new System.Drawing.Point(27, 41);
      this.chkManual.Name = "chkManual";
      this.chkManual.Size = new System.Drawing.Size(74, 17);
      this.chkManual.TabIndex = 13;
      this.chkManual.Text = "手动选择";
      this.chkManual.UseVisualStyleBackColor = true;
      this.chkManual.CheckedChanged += new System.EventHandler(this.chkManual_CheckedChanged);
      // 
      // Config
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(535, 471);
      this.Controls.Add(this.gbxManual);
      this.Controls.Add(this.gbxInstalled);
      this.Controls.Add(this.gbxTime);
      this.Controls.Add(this.cmdCancel);
      this.Controls.Add(this.cmdOK);
      this.Name = "Config";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "程序配置";
      this.gbxTime.ResumeLayout(false);
      this.gbxTime.PerformLayout();
      this.gbxInstalled.ResumeLayout(false);
      this.gbxInstalled.PerformLayout();
      this.gbxManual.ResumeLayout(false);
      this.gbxManual.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button cmdOK;
    private System.Windows.Forms.Button cmdCancel;
    private System.Windows.Forms.GroupBox gbxTime;
    private System.Windows.Forms.Label lblMinute;
    private System.Windows.Forms.Label lblTime;
    private System.Windows.Forms.MaskedTextBox mskTime;
    private System.Windows.Forms.CheckBox chkTimed;
    private System.Windows.Forms.GroupBox gbxInstalled;
    private System.Windows.Forms.ComboBox cmbInstalled;
    private System.Windows.Forms.GroupBox gbxManual;
    private System.Windows.Forms.Button btnDir;
    private System.Windows.Forms.TextBox txtDir;
    private System.Windows.Forms.OpenFileDialog ofdDir;
    private System.Windows.Forms.CheckBox chkInstalled;
    private System.Windows.Forms.CheckBox chkManual;
  }
}