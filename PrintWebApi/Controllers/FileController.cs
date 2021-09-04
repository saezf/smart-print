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
            var fileToPrint = fileDirectory + @"\tmp.pdf";
            FileDownload.downloadFileToSpecificPath(url, fileToPrint);
            
            var settings = new IniFile("Settings.ini");
            settings.Write("FileToPrint", fileToPrint, "Document");

            FileDownload.Print();
            return Ok("File sent to printer");
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
