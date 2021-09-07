using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace testForms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            string text = "";
            Program.MainForm.Invoke(new Action(() => {
                text = Program.MainForm.textBox1.Text;
            }));
            return text;
        }

        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            Program.MainForm.Invoke(new Action(() => {
                Program.MainForm.textBox1.Text = id;
            }));
            return Ok();
        }
    }
}
