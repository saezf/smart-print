using SmartPrint.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Xml;

namespace SmartPrint
{
    public partial class Form1 : Form
    {
        private static string selectedPrinter;
        private static string selectedPaperBin;
        private static string selectedPaperName;
        private static string fileToPrint;
        private static PrinterSettings printerSettings;
        //private static PageSettings pageSettings;

        IniFile settings = new IniFile("Settings.ini");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string urls = ConfigurationManager.AppSettings["urls"];
            lblUrlsText.Text = urls;

            string httpPort = ConfigurationManager.AppSettings["httpPort"];
            txtHttpPort.Text = httpPort;

            string httpsPort = ConfigurationManager.AppSettings["httpsPort"];
            txtHttpsPort.Text = httpsPort;

            printerSettings = new PrinterSettings();

            printerSettings.PrinterName = settings.Read("printerName", "Printer");

            // Cargar combobox printers
            List<string> printer = new List<string>();
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                printer.Add(PrinterSettings.InstalledPrinters[i]);
            }
            cbxPrinter.DataSource = printer;
            cbxPrinter.Text = settings.Read("printerName", "Printer");

            // cargar combo box papersource
            List<string> paperbin = new List<string>();
            selectedPaperBin = cbxPaperBin.Text;
            bool skipPaperSource = printerSettings.PaperSources.Count == 0;
            if (skipPaperSource)
            {
                selectedPaperBin = "DEFAULT";
            }
            else
            {
                for (int i = 0; i < printerSettings.PaperSources.Count; i++)
                {
                    paperbin.Add(printerSettings.PaperSources[i].SourceName.ToUpper());
                }
                selectedPaperBin = cbxPaperBin.Text;
            }
            cbxPaperBin.DataSource = paperbin;
            cbxPaperBin.Text = settings.Read("PaperBin", "Printer");

            // cargar combobox paper name
            List<string> paper = new List<string>();
            for (int i = 0; i < printerSettings.PaperSizes.Count; i++)
            {
                paper.Add(printerSettings.PaperSizes[i].PaperName.ToUpper());
            }
            cbxPaperName.DataSource = paper;
            cbxPaperName.Text = settings.Read("PaperName", "Printer");

            ////cargar archivo
            txtFile.Text = settings.Read("FileToPrint", "Document");
        }

        private void cbxPrinter_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int selectedOption = cbxPrinter.SelectedIndex;

            selectedPrinter = PrinterSettings.InstalledPrinters[selectedOption];
            printerSettings.PrinterName = selectedPrinter;
            cbxPrinter.Text = selectedPrinter;
            settings.Write("printerName", selectedPrinter, "Printer");

            //evaluar paper bin == 0
            bool skipPaperSourceP = printerSettings.PaperSources.Count == 0;
            if (skipPaperSourceP)
            {
                selectedPaperBin = "DEFAULT";
                cbxPaperBin.Text = selectedPaperBin;
                settings.Write("PaperBin", selectedPaperBin, "Printer");
            }

            //cargar de nuevo paper bin
            List<string> paperbin = new List<string>();
            for (int i = 0; i < printerSettings.PaperSources.Count; i++)
            {
                paperbin.Add(printerSettings.PaperSources[i].SourceName.ToUpper());
            }
            cbxPaperBin.DataSource = paperbin;
            selectedPaperBin = cbxPaperBin.Text;
            settings.Write("PaperBin", selectedPaperBin, "Printer");

            // cargar de nuevo paper name
            List<string> paper = new List<string>();
            for (int i = 0; i < printerSettings.PaperSizes.Count; i++)
            {
                paper.Add(printerSettings.PaperSizes[i].PaperName.ToUpper());
            }
            cbxPaperName.DataSource = paper;
            selectedPaperName = cbxPaperName.Text;
            settings.Write("PaperName", selectedPaperName, "Printer");
            //cbxPaperName.Text = settings.Read("PaperName", "Printer");
        }

        private void cbxPaperBin_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int selectedOption = cbxPaperBin.SelectedIndex;
            selectedPaperBin = printerSettings.PaperSources[selectedOption].SourceName.ToUpper();
            settings.Write("PaperBin", selectedPaperBin, "Printer");
            cbxPaperBin.Text = selectedPaperBin;
        }

        private void cbxPaperName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int selectedOption = cbxPaperName.SelectedIndex;
            selectedPaperName = printerSettings.PaperSizes[selectedOption].PaperName.ToUpper();
            settings.Write("PaperName", selectedPaperName, "Printer");
            cbxPaperName.Text = selectedPaperName;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                //notifyIcon1.Icon = SystemIcons.Application;
                //notifyIcon1.BalloonTipText = "Corriendo en segundo plano";
                //notifyIcon1.ShowBalloonTip(1000);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult dialogResult = MessageBox.Show("¿Desea cerrar el web service?", "Close", buttons, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            //notifyIcon1.BalloonTipText = "Restored Form";
            //notifyIcon1.ShowBalloonTip(1000);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = openFileDialog1.FileName;
                fileToPrint = openFileDialog1.FileName;
                settings.Write("FileToPrint", fileToPrint, "Document");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Functions.Print();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            string newHttpPort = txtHttpPort.Text.Trim();
            string newHttpsPort = txtHttpsPort.Text.Trim();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath);

                foreach (XmlElement element in xmlDoc.DocumentElement)
                {
                    if (element.Name.Equals("appSettings"))
                    {
                        foreach (XmlNode node in element.ChildNodes)
                        {
                            if (node.Attributes[0].Value == "httpPort")
                            {
                                node.Attributes[1].Value = newHttpPort;
                            }
                            if (node.Attributes[0].Value == "httpsPort")
                            {
                                node.Attributes[1].Value = newHttpsPort;
                            }
                        }
                    }
                }
                //xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                xmlDoc.Save(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath);
                ConfigurationManager.RefreshSection("appSettings");
                MessageBox.Show("Se cambió el puerto. Los cambios surtirán efecto la próxima vez que inicie el servicio");
        }

        private void txtHttpPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtHttpsPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtHttpPort_TextChanged(object sender, EventArgs e)
        {
            if (txtHttpPort.Text.Length == 1)            {
                if (txtHttpPort.Text[0] == '0')
                {
                    txtHttpPort.Clear();
                }
            }
            else if (txtHttpPort.Text.Length > 1)
            {
                if (Convert.ToInt32( txtHttpPort.Text) > 65535)
                {
                    MessageBox.Show("Puerto fuera de rango");
                    txtHttpPort.Clear();
                }
            }
        }

        private void txtHttpsPort_TextChanged(object sender, EventArgs e)
        {
            if (txtHttpsPort.Text.Length == 1)
            {
                if (txtHttpsPort.Text[0] == '0')
                {
                    txtHttpsPort.Clear();
                }
            }
            else if (txtHttpsPort.Text.Length > 1)
            {
                if (Convert.ToInt32(txtHttpsPort.Text) > 65535)
                {
                    MessageBox.Show("Puerto fuera de rango");
                    txtHttpsPort.Clear();
                }
            }
        }
    }
}
