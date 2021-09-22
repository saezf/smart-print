using SmartPrint.Util;
using System;
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

        IniFile settings = new IniFile("Settings.ini");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string url = ConfigurationManager.AppSettings["url"];
            lblUrlsText.Text = url;

            string protocolo = ConfigurationManager.AppSettings["protocolo"];
            cbxPort.Text = protocolo;

            string port = ConfigurationManager.AppSettings["puerto"];
            txtPort.Text = port;

            printerSettings = new PrinterSettings();

            printerSettings.PrinterName = settings.Read("printerName", "Printer");

            // Cargar combobox printers
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                cbxPrinter.Items.Add(PrinterSettings.InstalledPrinters[i]);
            }
            cbxPrinter.Text = settings.Read("printerName", "Printer");

            // cargar combo box papersource
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
                    cbxPaperBin.Items.Add(printerSettings.PaperSources[i].SourceName.ToUpper());
                }
                selectedPaperBin = cbxPaperBin.Text;
            }
            cbxPaperBin.Text = settings.Read("PaperBin", "Printer");

            //cargar combobox paper name
            for (int i = 0; i < printerSettings.PaperSizes.Count; i++)
            {
                cbxPaperName.Items.Add(printerSettings.PaperSizes[i].PaperName.ToUpper());
            }
            cbxPaperName.Text = settings.Read("PaperName", "Printer");

            //cargar archivo
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
                cbxPaperBin.Items.Clear();
                //cbxPaperBin.Items.Add("DEFAULT");
                cbxPaperBin.Text = "DEFAULT";
                selectedPaperBin = cbxPaperBin.Text;
                settings.Write("PaperBin", selectedPaperBin, "Printer");
            }
            else
            {
                //cargar de nuevo paper bin
                cbxPaperBin.Items.Clear();
                for (int i = 0; i < printerSettings.PaperSources.Count; i++)
                {
                    cbxPaperBin.Items.Add(printerSettings.PaperSources[i].SourceName.ToUpper());
                }
                selectedPaperBin = cbxPaperBin.Text;
                settings.Write("PaperBin", selectedPaperBin, "Printer");
            }

            // cargar de nuevo paper name
            cbxPaperName.Items.Clear();
            for (int i = 0; i < printerSettings.PaperSizes.Count; i++)
            {
                cbxPaperName.Items.Add(printerSettings.PaperSizes[i].PaperName.ToUpper());
            }
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
            DialogResult dialogResult = MessageBox.Show("¿Desea detener el web service?", "Confirmar", buttons, MessageBoxIcon.Information);

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
            var fileToPrint = settings.Read("FileToPrint", "Document");
            if (Functions.existFileLocal(fileToPrint))
            {
                Functions.Print(fileToPrint);
                txtFile.Text = settings.Read("FileToPrint", "Document");
            }
            else
            {
                MessageBox.Show("Archivo no encontrado");
                settings.Write("FileToPrint", "", "Document");
                txtFile.Text = settings.Read("FileToPrint", "Document");
            }

        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            string protocolo = cbxPort.Text;
            string newPort = txtPort.Text.Trim();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath);

            foreach (XmlElement element in xmlDoc.DocumentElement)
            {
                if (element.Name.Equals("appSettings"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes[0].Value == "protocolo")
                        {
                            node.Attributes[1].Value = protocolo;
                        }
                        if (node.Attributes[0].Value == "puerto")
                        {
                            node.Attributes[1].Value = newPort;
                        }
                    }
                }
            }
            //xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            xmlDoc.Save(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath);
            ConfigurationManager.RefreshSection("appSettings");
            MessageBox.Show("Se cambió el puerto. Los cambios surtirán efecto la próxima vez que inicie el servicio");
        }

        private void txtPort_TextChanged(object sender, EventArgs e)
        {
            if (txtPort.Text.Length == 1)
            {
                if (txtPort.Text[0] == '0')
                {
                    txtPort.Clear();
                }
            }
            else if (txtPort.Text.Length > 1)
            {
                if (Convert.ToInt32(txtPort.Text) > 65535)
                {
                    MessageBox.Show("Puerto fuera de rango");
                    txtPort.Clear();
                }
            }
        }

        private void txtPort_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
