using PdfiumViewer;
using System;
using System.Drawing.Printing;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace SmartPrint.Util
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
                settings.Write("FileToPrint","", "Document");
            }
        }

        public static bool URLExists(string url)
        {
            try
            {
            //Creating the HttpWebRequest
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest; 
            //Setting the Request method HEAD, you can also use GET too.
            request.Method = "HEAD"; 
            //Getting the Web Response.
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                if (response.ContentType.Equals(Application.Pdf)){
                    return (response.StatusCode == HttpStatusCode.OK);
                }
                else
                {
                    return (response.StatusCode == HttpStatusCode.BadRequest);
                }
            }
            catch (Exception)
            {
            //Any exception will returns false.
            return false;

            }
        }

        public static void downloadFileToSpecificPath(string strURLFile, string strPathToSave)
        {
            try
            {
                /*
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
                */
                // Se descargar el archivo indicado en la ruta específicada.
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(strURLFile, strPathToSave);
                }

            }
            catch (Exception)
            {
                // Se retorna la excepción al cliente.
                throw;
            }
        }

    }
}
