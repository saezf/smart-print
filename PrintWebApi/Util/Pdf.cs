using PdfiumViewer;
using SmartPrint;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;

namespace PrintWebApi.Util
{
    public class Pdf
    {
        public static Boolean PrintPDFs(string pdfFileName)
        {
            try
            {
                var settings = new IniFile(@"C:\Smart-Print\smart-print\ConsoleApplication2\bin\Release\Settings.ini");
                var selectedPrinter = settings.Read("printerName", "Printer");
                var selectedPaperBin = settings.Read("PaperBin", "Printer");
                var selectedPaperName = settings.Read("PaperName", "Printer");
                var fileToPrint = settings.Read("FileToPrint", "Document");

                Process proc = new Process();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.Verb = "print";

                //Define location of adobe reader/command line
                //switches to launch adobe in "print" mode
                proc.StartInfo.FileName =
                  @"C:\Program Files (x86)\Adobe\Acrobat Reader DC\Reader\AcroRd32.exe";
                proc.StartInfo.Arguments = String.Format("/t \"{0}\" \"{1}\"", pdfFileName, selectedPrinter);
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;

                proc.Start();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                if (proc.HasExited == false)
                {
                    proc.WaitForExit(10000);
                }

                proc.EnableRaisingEvents = true;

                proc.Close();
                KillAdobe("AcroRd32");
                return true;
            }
            catch
            {
                return false;
            }
        }

        //For whatever reason, sometimes adobe likes to be a stage 5 clinger.
        //So here we kill it with fire.
        private static bool KillAdobe(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses().Where(
                         clsProcess => clsProcess.ProcessName.StartsWith(name)))
            {
                clsProcess.Kill();
                return true;
            }
            return false;
        }

        //------

        public static void PrintPDF(string printer, string paperName, string filename, int copies)
        {

            // Create the printer settings for our printer
            var printerSettings = new PrinterSettings
            {
                PrinterName = printer,
                Copies = (short)copies,
            };

            // Create our page settings for the paper size selected
            var pageSettings = new PageSettings(printerSettings)
            {
                Margins = new Margins(0, 0, 0, 0),
            };
            foreach (PaperSize paperSize in printerSettings.PaperSizes)
            {
                if (paperSize.PaperName == paperName)
                {
                    pageSettings.PaperSize = paperSize;
                    break;
                }
            }

            // Now print the PDF document
            using (var document = PdfDocument.Load(filename))
            {
                using (var printDocument = document.CreatePrintDocument())
                {
                    printDocument.PrinterSettings = printerSettings;
                    printDocument.DefaultPageSettings = pageSettings;
                    printDocument.PrintController = new StandardPrintController();
                    printDocument.Print();
                }
            }

        }

        //---

        public static void pdfTest(string pdfFileName)
        {
            string processFilename = Microsoft.Win32.Registry.LocalMachine
                 .OpenSubKey("Software")
                 .OpenSubKey("Microsoft")
                 .OpenSubKey("Windows")
                 .OpenSubKey("CurrentVersion")
                 .OpenSubKey("App Paths")
                 .OpenSubKey("AcroRd32.exe")
                 .GetValue(String.Empty).ToString();

            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";
            info.FileName = processFilename;
            info.Arguments = String.Format("/p /h {0}", pdfFileName);
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            //(It won't be hidden anyway... thanks Adobe!)
            info.UseShellExecute = false;

            Process p = Process.Start(info);
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            int counter = 0;
            while (!p.HasExited)
            {
                System.Threading.Thread.Sleep(1000);
                counter += 1;
                if (counter == 5) break;
            }
            if (!p.HasExited)
            {
                p.CloseMainWindow();
                p.Kill();
            }
        }
    }
}
