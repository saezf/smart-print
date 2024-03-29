﻿
namespace SmartPrint
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lblUrlsTitle = new System.Windows.Forms.Label();
            this.lblUrlsText = new System.Windows.Forms.Label();
            this.lblPorts = new System.Windows.Forms.Label();
            this.lblPinter = new System.Windows.Forms.Label();
            this.lblPaperBin = new System.Windows.Forms.Label();
            this.lblPaperName = new System.Windows.Forms.Label();
            this.lblFile = new System.Windows.Forms.Label();
            this.cbxPrinter = new System.Windows.Forms.ComboBox();
            this.cbxPaperBin = new System.Windows.Forms.ComboBox();
            this.cbxPaperName = new System.Windows.Forms.ComboBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnChange = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cbxPort = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUrlsTitle
            // 
            this.lblUrlsTitle.AutoSize = true;
            this.lblUrlsTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblUrlsTitle.Location = new System.Drawing.Point(13, 13);
            this.lblUrlsTitle.Name = "lblUrlsTitle";
            this.lblUrlsTitle.Size = new System.Drawing.Size(138, 13);
            this.lblUrlsTitle.TabIndex = 0;
            this.lblUrlsTitle.Text = "Web Service listening on:";
            // 
            // lblUrlsText
            // 
            this.lblUrlsText.AutoSize = true;
            this.lblUrlsText.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblUrlsText.Location = new System.Drawing.Point(167, 13);
            this.lblUrlsText.Name = "lblUrlsText";
            this.lblUrlsText.Size = new System.Drawing.Size(16, 13);
            this.lblUrlsText.TabIndex = 1;
            this.lblUrlsText.Text = "...";
            // 
            // lblPorts
            // 
            this.lblPorts.AutoSize = true;
            this.lblPorts.Location = new System.Drawing.Point(12, 41);
            this.lblPorts.Name = "lblPorts";
            this.lblPorts.Size = new System.Drawing.Size(73, 15);
            this.lblPorts.TabIndex = 2;
            this.lblPorts.Text = "Change Port";
            // 
            // lblPinter
            // 
            this.lblPinter.AutoSize = true;
            this.lblPinter.Location = new System.Drawing.Point(12, 69);
            this.lblPinter.Name = "lblPinter";
            this.lblPinter.Size = new System.Drawing.Size(86, 15);
            this.lblPinter.TabIndex = 3;
            this.lblPinter.Text = "Change Printer";
            // 
            // lblPaperBin
            // 
            this.lblPaperBin.AutoSize = true;
            this.lblPaperBin.Location = new System.Drawing.Point(12, 98);
            this.lblPaperBin.Name = "lblPaperBin";
            this.lblPaperBin.Size = new System.Drawing.Size(101, 15);
            this.lblPaperBin.TabIndex = 4;
            this.lblPaperBin.Text = "Change Paper Bin";
            // 
            // lblPaperName
            // 
            this.lblPaperName.AutoSize = true;
            this.lblPaperName.Location = new System.Drawing.Point(12, 127);
            this.lblPaperName.Name = "lblPaperName";
            this.lblPaperName.Size = new System.Drawing.Size(116, 15);
            this.lblPaperName.TabIndex = 5;
            this.lblPaperName.Text = "Change Paper Name";
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(12, 156);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(68, 15);
            this.lblFile.TabIndex = 6;
            this.lblFile.Text = "File To Print";
            // 
            // cbxPrinter
            // 
            this.cbxPrinter.FormattingEnabled = true;
            this.cbxPrinter.Location = new System.Drawing.Point(167, 67);
            this.cbxPrinter.Name = "cbxPrinter";
            this.cbxPrinter.Size = new System.Drawing.Size(181, 23);
            this.cbxPrinter.TabIndex = 8;
            this.cbxPrinter.SelectionChangeCommitted += new System.EventHandler(this.cbxPrinter_SelectionChangeCommitted);
            // 
            // cbxPaperBin
            // 
            this.cbxPaperBin.FormattingEnabled = true;
            this.cbxPaperBin.Location = new System.Drawing.Point(167, 96);
            this.cbxPaperBin.Name = "cbxPaperBin";
            this.cbxPaperBin.Size = new System.Drawing.Size(181, 23);
            this.cbxPaperBin.TabIndex = 9;
            this.cbxPaperBin.SelectionChangeCommitted += new System.EventHandler(this.cbxPaperBin_SelectionChangeCommitted);
            // 
            // cbxPaperName
            // 
            this.cbxPaperName.FormattingEnabled = true;
            this.cbxPaperName.Location = new System.Drawing.Point(167, 125);
            this.cbxPaperName.Name = "cbxPaperName";
            this.cbxPaperName.Size = new System.Drawing.Size(181, 23);
            this.cbxPaperName.TabIndex = 10;
            this.cbxPaperName.SelectionChangeCommitted += new System.EventHandler(this.cbxPaperName_SelectionChangeCommitted);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(262, 38);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(86, 23);
            this.txtPort.TabIndex = 11;
            this.txtPort.TextChanged += new System.EventHandler(this.txtPort_TextChanged);
            this.txtPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPort_KeyPress);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(167, 154);
            this.txtFile.Name = "txtFile";
            this.txtFile.ReadOnly = true;
            this.txtFile.Size = new System.Drawing.Size(181, 23);
            this.txtFile.TabIndex = 12;
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(354, 38);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(75, 23);
            this.btnChange.TabIndex = 13;
            this.btnChange.Text = "Change";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(354, 154);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 14;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(167, 202);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 15;
            this.btnPrint.Text = "Print File";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Smart Print";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restoreToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(114, 48);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.restoreToolStripMenuItem.Text = "Restore";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Archivos PDF (*.pdf)|*.pdf";
            // 
            // cbxPort
            // 
            this.cbxPort.FormattingEnabled = true;
            this.cbxPort.Items.AddRange(new object[] {
            "http",
            "https"});
            this.cbxPort.Location = new System.Drawing.Point(167, 38);
            this.cbxPort.Name = "cbxPort";
            this.cbxPort.Size = new System.Drawing.Size(89, 23);
            this.cbxPort.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 247);
            this.Controls.Add(this.cbxPort);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.cbxPaperName);
            this.Controls.Add(this.cbxPaperBin);
            this.Controls.Add(this.cbxPrinter);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.lblPaperName);
            this.Controls.Add(this.lblPaperBin);
            this.Controls.Add(this.lblPinter);
            this.Controls.Add(this.lblPorts);
            this.Controls.Add(this.lblUrlsText);
            this.Controls.Add(this.lblUrlsTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(461, 286);
            this.MinimumSize = new System.Drawing.Size(461, 286);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Smart Print";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUrlsTitle;
        private System.Windows.Forms.Label lblUrlsText;
        private System.Windows.Forms.Label lblPorts;
        private System.Windows.Forms.Label lblPinter;
        private System.Windows.Forms.Label lblPaperBin;
        private System.Windows.Forms.Label lblPaperName;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.ComboBox cbxPrinter;
        private System.Windows.Forms.ComboBox cbxPaperBin;
        private System.Windows.Forms.ComboBox cbxPaperName;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox cbxPort;
    }
}

