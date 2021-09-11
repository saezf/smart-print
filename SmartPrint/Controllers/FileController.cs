using Microsoft.AspNetCore.Mvc;
using SmartPrint.Util;
using System;

namespace SmartPrint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetPdfFromUrl(string url)
        {
            if (Functions.URLExists(url))
            {
                var fileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var fileToPrint = fileDirectory + @"\tmp.pdf";
                Functions.downloadFileToSpecificPath(url, fileToPrint);
                Functions.Print(fileToPrint);
                Functions.deleteFile(fileToPrint);
                return Ok("File sent to printer");
            }
            else
            {
                return BadRequest("Invalid url");
            }

        }
    }
}
