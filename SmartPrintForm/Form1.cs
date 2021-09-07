using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using testForms.Util;

namespace testForms
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
            printerSettings = new PrinterSettings();

            printerSettings.PrinterName = settings.Read("printerName", "Printer");

            // Cargar combobox printers

            List<string> printer = new List<string>();
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                printer.Add(PrinterSettings.InstalledPrinters[i]);
            }
            comboBox1.DataSource = printer;
            comboBox1.Text = settings.Read("printerName", "Printer");

            // cargar combo box papersource
            List<string> paperbin = new List<string>();

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
                selectedPaperBin = comboBox2.Text;
            }
            comboBox2.DataSource = paperbin;
            comboBox2.Text = settings.Read("PaperBin", "Printer");

            // cargar combobox paper name

            List<string> paper = new List<string>();
            for (int i = 0; i < printerSettings.PaperSizes.Count; i++)
            {
                paper.Add(printerSettings.PaperSizes[i].PaperName.ToUpper());
            }
            comboBox3.DataSource = paper;
            comboBox3.Text = settings.Read("PaperName", "Printer");

            //cargar archivo

            txtFileToPrint.Text = settings.Read("FileToPrint", "Document");
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int selectedOption = comboBox1.SelectedIndex;

            selectedPrinter = PrinterSettings.InstalledPrinters[selectedOption];
            printerSettings.PrinterName = selectedPrinter;

            settings.Write("printerName", selectedPrinter, "Printer");
            comboBox1.Text = selectedPrinter;

            //cargar de nuevo paper bin
            List<string> paperbin = new List<string>();
            for (int i = 0; i < printerSettings.PaperSources.Count; i++)
            {
                paperbin.Add(printerSettings.PaperSources[i].SourceName.ToUpper());
            }
            comboBox2.DataSource = paperbin;

            //evaluar paper bin == 0
            bool skipPaperSourceP = printerSettings.PaperSources.Count == 0;
            if (skipPaperSourceP)
            {
                selectedPaperBin = "DEFAULT";
                settings.Write("PaperBin", selectedPaperBin, "Printer");
                comboBox2.Text = selectedPaperBin;
            }

            selectedPaperBin = comboBox2.Text;
            settings.Write("PaperBin", selectedPaperBin, "Printer");

            // cargar de nuevo paper name
            List<string> paper = new List<string>();
            for (int i = 0; i < printerSettings.PaperSizes.Count; i++)
            {
                paper.Add(printerSettings.PaperSizes[i].PaperName.ToUpper());
            }
            comboBox3.DataSource = paper;
            comboBox3.Text = settings.Read("PaperName", "Printer");
            selectedPaperName = comboBox3.Text;

        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int selectedOption = comboBox2.SelectedIndex;

            bool skipPaperSource = printerSettings.PaperSources.Count == 0;
            if (skipPaperSource)
            {
                selectedPaperBin = "DEFAULT";
            }
            else
            {
                selectedPaperBin = printerSettings.PaperSources[selectedOption].SourceName.ToUpper();
                settings.Write("PaperBin", selectedPaperBin, "Printer");
                comboBox2.Text = selectedPaperBin;
            }

        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {

            int selectedOption = comboBox3.SelectedIndex;
            
            selectedPaperName = printerSettings.PaperSizes[selectedOption].PaperName.ToUpper();
            settings.Write("PaperName", selectedPaperName, "Printer");
            comboBox3.Text = selectedPaperName;
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

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            //notifyIcon1.BalloonTipText = "Restored Form";
            //notifyIcon1.ShowBalloonTip(1000);
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFileToPrint.Text = openFileDialog1.FileName;
                fileToPrint = openFileDialog1.FileName;
                settings.Write("FileToPrint", fileToPrint, "Document");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult dialogResult = MessageBox.Show("The application will close", "Close", buttons, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Functions.Print();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }
    }
}
