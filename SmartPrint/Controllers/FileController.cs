using Microsoft.AspNetCore.Mvc;
using SmartPrint.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrint.Controllers
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
            Functions.downloadFileToSpecificPath(url, fileToPrint);

            var settings = new IniFile("Settings.ini");
            settings.Write("FileToPrint", fileToPrint, "Document");

            Functions.Print();
            return Ok("File sent to printer");
        }

    }
}
