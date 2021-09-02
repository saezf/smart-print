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
            printerSettings = new PrinterSettings();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          var selected = settings.Read("printerName", "Printer");

            // Cargar combobox printers
            
            lblPrinterSelected.Text = settings.Read("printerName", "Printer");
                List<string> printer = new List<string>();
                printer.Add("CHANGE PRINTER");
                for (int i = 1; i < PrinterSettings.InstalledPrinters.Count; i++)
                {
                    printer.Add(PrinterSettings.InstalledPrinters[i]);
                }
                comboBox1.DataSource = printer;

            // cargar combo box papersource
            lblPaperBinSelected.Text = settings.Read("PaperBin", "Printer");
            List<string> paperbin = new List<string>();

            bool skipPaperSource = printerSettings.PaperSources.Count == 0;
            if (skipPaperSource)
            {
                paperbin.Add("DEFAULT");
                selectedPaperBin = "DEFAULT";
            }
            else
            {
                paperbin.Add("CHANGE PAPER BIN");

                for (int i = 1; i < printerSettings.PaperSources.Count; i++)
                {
                    paperbin.Add(printerSettings.PaperSources[i].SourceName.ToUpper());
                }
            }
            comboBox2.DataSource = paperbin;

            // cargar combobox paper name

            lblPaperNameSelected.Text = settings.Read("PaperName", "Printer");
            List<string> paper = new List<string>();
            paper.Add("CHANGE PAPER NAME");
            for (int i = 1; i < printerSettings.PaperSizes.Count; i++)
            {
                paper.Add(printerSettings.PaperSizes[i].PaperName.ToUpper());
            }
            comboBox3.DataSource = paper;
        }
        
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {

            int selectedOption = comboBox1.SelectedIndex;
            selectedPrinter = PrinterSettings.InstalledPrinters[selectedOption];
            settings.Write("printerName", selectedPrinter, "Printer");
            
            lblPrinterSelected.Text = selectedPrinter;

            printerSettings.PrinterName = selectedPrinter;
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
            }
            settings.Write("PaperBin", selectedPaperBin, "Printer");
            lblPaperBinSelected.Text = selectedPaperBin;
        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {

            int selectedOption = comboBox3.SelectedIndex;
            selectedPaperName = printerSettings.PaperSizes[selectedOption].PaperName.ToUpper();
            
            settings.Write("PaperName", selectedPaperName, "Printer");
            lblPaperNameSelected.Text = selectedPaperName;
        }


        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
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
            notifyIcon1.BalloonTipText = "Formulario restaurado";
            notifyIcon1.ShowBalloonTip(1000);
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = openFileDialog1.FileName;
                fileToPrint = openFileDialog1.FileName;
                settings.Write("FileToPrint", fileToPrint, "Document");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Functions.Print();
        }
    }
}
