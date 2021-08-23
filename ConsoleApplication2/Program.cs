using PdfiumViewer;
using System;
using System.Drawing.Printing;
using System.IO;

namespace SmartPrint
{
    class Program
    {
        private static string selectedPrinter;
        private static string selectedPaperBin;
        private static string selectedPaperName;
        private static string fileToPrint;
        private static StreamReader streamToPrint;
        private static PrinterSettings printerSettings;
        private static PageSettings pageSettings;

        static void Main(string[] args)
        {
            FetchPrintingOptions();
            printerSettings = new PrinterSettings();
            pageSettings = new PageSettings()
            {
                Margins = new Margins(0, 0, 0, 0)
            };
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }
        
        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Available options:");
            Console.WriteLine("0) Show current printing configurations");
            Console.WriteLine("1) Change printer");
            Console.WriteLine("2) Change paper bin");
            Console.WriteLine("3) Change paper name");
            Console.WriteLine("4) Change file to print");
            Console.WriteLine("5) Print");
            Console.WriteLine("6) Exit");
 
            switch (CaptureInput())
            {
                case "0":
                    ShowPrintingOptions();
                    return true;
                case "1":
                    AvailablePrinters();
                    return true;
                case "2":
                    AvailablePaperBin();
                    return true;
                case "3":
                    AvailablePaperName();
                    return true;
                case "4":
                    SelectFileToPrint();
                    return true;
                case "5":
                    Print();
                    return true;
                case "6":
                    return false;
                default:
                    return true;
            }
        }
 
        private static string CaptureInput()
        {
            Console.Write("\r\nSelect an option: ");
            return Console.ReadLine();
        }
 
        private static void FetchPrintingOptions()
        {
            var settings = new IniFile("Settings.ini");
            selectedPrinter = settings.Read("printerName", "Printer");
            selectedPaperBin = settings.Read("PaperBin", "Printer");
            selectedPaperName = settings.Read("PaperName", "Printer");
            fileToPrint = settings.Read("FileToPrint", "Document");
        }

        private static void ShowPrintingOptions()
        {
            Console.Clear();
            Console.WriteLine("The print job will be send using the following settings...");
            Console.WriteLine("PrinterName:\t{0}", selectedPrinter);
            Console.WriteLine("PaperBin:\t{0}", selectedPaperBin);
            Console.WriteLine("PaperName:\t{0}", selectedPaperName);
            Console.WriteLine("FileToPrint:\t{0}", fileToPrint);
            Console.WriteLine("\r\nPress any key to continue...");
            Console.ReadKey();
        }

        private static void AvailablePrinters()
        {
            Console.Clear();
            Console.WriteLine("Available printers:");

            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                Console.WriteLine("{0}) {1}", i, PrinterSettings.InstalledPrinters[i]);
            }

            int selectedOption;
            do
            {
                Int32.TryParse(CaptureInput(), out selectedOption);
            } while (selectedOption < 0 || selectedOption >= PrinterSettings.InstalledPrinters.Count);

            var settings = new IniFile("Settings.ini");
            selectedPrinter = PrinterSettings.InstalledPrinters[selectedOption];
            settings.Write("printerName", selectedPrinter, "Printer");
            printerSettings.PrinterName = selectedPrinter;
        }
 
        private static void AvailablePaperBin()
        {
            Console.Clear();
            Console.WriteLine("Available paper bin:");

            bool skipPaperSource = printerSettings.PaperSources.Count == 0;
            if (skipPaperSource)
            {
                Console.WriteLine("0) DEFAULT");
            }
            else
            {
                for (int i = 0; i < printerSettings.PaperSources.Count; i++)
                {
                    Console.WriteLine("{0}) {1}", i, printerSettings.PaperSources[i].SourceName.ToUpper());
                }
            }

            int selectedOption;
            do
            {
                Int32.TryParse(CaptureInput(), out selectedOption);
            } while (selectedOption < 0 || (!skipPaperSource && selectedOption >= printerSettings.PaperSources.Count));

            var settings = new IniFile("Settings.ini");
            if (skipPaperSource)
            {
                selectedPaperBin = "DEFAULT";
            }
            else
            {
                selectedPaperBin = printerSettings.PaperSources[selectedOption].SourceName.ToUpper();
            }
            settings.Write("PaperBin", selectedPaperBin, "Printer");
        }
 
        private static void AvailablePaperName()
        {
            Console.Clear();
            Console.WriteLine("Available paper names:");

            for (int i = 0; i < printerSettings.PaperSizes.Count; i++)
            {
                Console.WriteLine("{0}) {1}", i, printerSettings.PaperSizes[i].PaperName.ToUpper());
            }

            int selectedOption;
            do
            {
                Int32.TryParse(CaptureInput(), out selectedOption);

            } while (selectedOption < 0 || selectedOption >= printerSettings.PaperSizes.Count);

            var settings = new IniFile("Settings.ini");
            selectedPaperName = printerSettings.PaperSizes[selectedOption].PaperName.ToUpper();
            settings.Write("PaperName", selectedPaperName, "Printer");
        }

        private static void SelectFileToPrint()
        {
            Console.Clear();
            Console.Write("File to print: ");

            var settings = new IniFile("Settings.ini");
            fileToPrint = Console.ReadLine();
            settings.Write("FileToPrint", fileToPrint, "Document");
        }
        private static void Print()
        {
            bool canPrint = true;
            
            // Validate selectedPrinter
            if (String.IsNullOrEmpty(selectedPrinter))
            {
                Console.WriteLine("Need to select a printer before sending a print job");
                canPrint = false;
            }

            // Validate selectedPaperBin
            if (String.IsNullOrEmpty(selectedPaperBin))
            {
                Console.WriteLine("Need to select a paper bin before sending a print job");
                canPrint = false;
            }
            
            // Validate selectedPaperName
            if (String.IsNullOrEmpty(selectedPaperName))
            {
                Console.WriteLine("Need to select a paper name before sending a print job");
                canPrint = false;
            }
            
            // Validate fileToPrint
            if (String.IsNullOrEmpty(fileToPrint))
            {
                Console.WriteLine("Need to select a file before sending a print job");
                canPrint = false;
            }

            if (canPrint)
            {
                printerSettings.PrinterName = selectedPrinter;
                streamToPrint = new StreamReader(fileToPrint);

                try
                {


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


                    //----------



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


                    //----------


                    //-----

                    // Create an instance of the Printer
                    //IPrinter printer = new Printer();

                    // Print the file
                    //printer.PrintRawFile(selectedPrinter, fileToPrint, Path.GetFileNameWithoutExtension(fileToPrint));

                    //-----

                    Console.WriteLine("File sent to the printer");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    if (streamToPrint != null)
                        streamToPrint.Close();
                }
            }

            Console.WriteLine("\r\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
