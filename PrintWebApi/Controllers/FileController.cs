using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrintWebApi.Context;
using PrintWebApi.Models;
using PrintWebApi.Util;
using SmartPrint;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PrintWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly AppDbContext context;

        public FileController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet("fileList")]
        public ActionResult Get()
        {
            try
            {
                return Ok(context.Archivo.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("url")]
        public ActionResult GetPdfFromUrl(string url)
        {
            var fileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            FileDownload.downloadFileToSpecificPath(url, fileDirectory + @"\" + Path.GetFileName(url).ToString());
            
            var settings = new IniFile(@"C:\Smart-Print\smart-print\ConsoleApplication2\bin\Release\Settings.ini");
            var fileToPrint = fileDirectory + @"\" + Path.GetFileName(url);
            settings.Write("FileToPrint", fileToPrint, "Document");

            //var selectedPrinter = settings.Read("printerName", "Printer");
            //var selectedPaperBin = settings.Read("PaperBin", "Printer");
            //var selectedPaperName = settings.Read("PaperName", "Printer");
            
            FileDownload.Print();

            /*string[] files = Directory.GetFiles(fileDirectory);
            foreach (string file in files.Where(
                        file => file.ToUpper().Contains(".PDF")))
            {
                Pdf.PrintPDFs(file);
            }*/

            //Pdf.PrintPDF(selectedPrinter, selectedPaperName, fileToPrint, 1);

            return Ok("Archivo descargado con exito");
        }

        [HttpPost]
        public ActionResult Post([FromForm] List<IFormFile> files)
        {
            List<Archivo> archivos = new List<Archivo>();

            try
            {
                if (files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        //guardar archivo en directorio
                        var filePath = "C:\\Users\\Gonzalo\\source\\repos\\ApiPrintPdf\\ApiPrintPdf\\DescargaPdf\\" + file.FileName;
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            file.CopyToAsync(stream);
                        }
                        //guardar en la base de datos
                        double size = file.Length;
                        size = size / 1000000; //tamaño en mb
                        size = Math.Round(size, 2);
                        Archivo archivo = new Archivo();
                        archivo.Size = size;
                        archivo.Extension = Path.GetExtension(file.FileName).Substring(1);
                        archivo.Name = Path.GetFileNameWithoutExtension(file.FileName);
                        archivo.Location = filePath;

                        archivos.Add(archivo);
                    }
                    context.Archivo.AddRange(archivos);
                    context.SaveChanges();
                    //FileDownload.Print();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}
