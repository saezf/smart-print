using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace SmartPrintForm
{
    static class Program
    {
        public static Form1 MainForm { get; private set; }

        static string httpPort = ConfigurationManager.AppSettings["httpPort"];
        static string httpUrl = String.Concat("http://0.0.0.0:", httpPort) ?? "8080";

        static string httpsPort = ConfigurationManager.AppSettings["httpsPort"];
        static string httpsUrl = String.Concat("http://0.0.0.0:", httpsPort) ?? "8081";

        [STAThread]
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().RunAsync();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm = new Form1();
            Application.Run(MainForm);
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls(httpUrl, httpsUrl);
    }
}
