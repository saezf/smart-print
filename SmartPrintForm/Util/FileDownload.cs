using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PdfiumViewer;
using SmartPrintForm;

namespace SmartPrintForms.Util
{
    public class FileDownload
    {
        public static void downloadFileToSpecificPath(string strURLFile, string strPathToSave)
        {
            try
            {
                // Se valida que la URL no esté en blanco.
                if (String.IsNullOrEmpty(strURLFile))
                {
                    // Se retorna un mensaje de error al usuario.
                    throw new ArgumentNullException("La dirección URL del documento es nula o se encuentra en blanco.");
                }

                // Se valida que la ruta física no esté en blanco.
                if (String.IsNullOrEmpty(strPathToSave))
                {
                    // Se retorna un mensaje de error al usuario.
                    throw new ArgumentNullException("La ruta para almacenar el documento es nula o se encuentra en blanco.");
                }

                // Se descargar el archivo indicado en la ruta específicada.
                using (WebClient client = new WebClient())
                {
                    /*client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler
                   (delegate (object sender, System.ComponentModel.AsyncCompletedEventArgs e)
                   {
                       if (e.Error == null && !e.Cancelled)
                       {
                           Console.WriteLine("Download completed!");
                           Print();
                       }
                   });
                    client.DownloadFileAsync(new Uri(strURLFile), "test.pdf");*/

                    client.DownloadFile(strURLFile, strPathToSave);

                    /*
                    var settings = new IniFile(@"C:\Smart-Print\smart-print\ConsoleApplication2\bin\Release\Settings.ini");
                    var fileToPrint = @"C:\Smart-Print\PrintWebApi\test.pdf";
                    settings.Write("FileToPrint", fileToPrint, "Document");
                    */
                }

            }
            catch (Exception ex)
            {
                // Se retorna la excepción al cliente.
                throw ex;
            }
        }

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
                File.Delete(fileToPrint);
                settings.Write("FileToPrint", "", "Document");
            }
        }
    }
}
