using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartPrintForms.Util;
using SmartPrintForm;

namespace SmartPrintForms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {

        [HttpGet]
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
      
    }
}
