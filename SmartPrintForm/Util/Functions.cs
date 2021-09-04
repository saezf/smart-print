using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfiumViewer;

namespace testForms.Util
{
    class Functions
    {
        public static void Print()
        {
            PrinterSettings printerSettings = new PrinterSettings();
            PageSettings pageSettings = new PageSettings();

            var settings = new IniFile("Settings.ini");
            var selectedPrinter = settings.Read("printerName", "Printer");
            var selectedPaperBin = settings.Read("PaperBin", "Printer");
            var selectedPaperName = settings.Read("PaperName", "Printer");
            var fileToPrint = settings.Read("FileToPrint", "Document");

            bool canPrint = true;

            // Validate selectedPrinter
            if (String.IsNullOrEmpty(selectedPrinter))
            {
                canPrint = false;
            }

            // Validate selectedPaperBin
            if (String.IsNullOrEmpty(selectedPaperBin))
            {
                canPrint = false;
            }

            // Validate selectedPaperName
            if (String.IsNullOrEmpty(selectedPaperName))
            {
                canPrint = false;
            }

            // Validate fileToPrint
            if (String.IsNullOrEmpty(fileToPrint))
            {
                canPrint = false;
            }

            if (canPrint)
            {
                printerSettings.PrinterName = selectedPrinter;

                foreach (PaperSource _pSource in printerSettings.PaperSources)
                {
                    if (_pSource.SourceName.ToUpper() == selectedPaperBin.ToUpper())
                    {
                        pageSettings.PaperSource = _pSource;
                        break;
                    }
                }

                foreach (PaperSize _pSize in printerSettings.PaperSizes)
                {
                    if (_pSize.PaperName.ToUpper() == selectedPaperName.ToUpper())
                    {
                        pageSettings.PaperSize = _pSize;
                        break;
                    }
                }
                using (PdfDocument pdfDocument = PdfDocument.Load(fileToPrint))
                {
                    using (PrintDocument printDocument = pdfDocument.CreatePrintDocument())
                    {
                        printDocument.PrinterSettings = printerSettings;
                        printDocument.DefaultPageSettings = pageSettings;
                        printDocument.PrintController = new StandardPrintController();
                        printDocument.Print();
                    }
                }
                //File.Delete(fileToPrint);
                settings.Write("FileToPrint", "", "Document");
            }
        }
    }
}
