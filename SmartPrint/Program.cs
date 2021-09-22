using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Configuration;
using System.Windows.Forms;
using System.Xml;

namespace SmartPrint
{
    static class Program
    {
        public static Form1 MainForm { get; private set; }

        static string protocolo = ConfigurationManager.AppSettings["protocolo"];
        static string puerto = ConfigurationManager.AppSettings["puerto"];

        static string url;

        static void changeUrl()
        {
            if (protocolo.Equals("http"))
            {
                url = "http://0.0.0.0:";
            }
            else if(protocolo.Equals("https"))
            {
                url = "https://0.0.0.0:";
            }
        }

        [STAThread]
        public static void Main(string[] args)
        {
            changeUrl();
            CreateWebHostBuilder(args).Build().RunAsync();
            escribirUrlsEnAppConfig(protocolo);
           
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm = new Form1();
            Application.Run(MainForm);
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().UseUrls(String.Concat(url, puerto));

        public static void escribirUrlsEnAppConfig(string protocolo)
        {
            string urlWrite="";
            if (protocolo.Equals("http"))
            {
                urlWrite = "http://localhost:";
            }
            else if (protocolo.Equals("https"))
            {
                urlWrite = "https://localhost:";
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath);

            foreach (XmlElement element in xmlDoc.DocumentElement)
            {
                if (element.Name.Equals("appSettings"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes[0].Value == "url")
                        {
                            node.Attributes[1].Value = String.Concat(urlWrite, puerto);
                        }
                    }
                }
            }

            xmlDoc.Save(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
