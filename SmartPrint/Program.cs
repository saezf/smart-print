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

        static string httpPort = ConfigurationManager.AppSettings["httpPort"];
        static string httpUrl = String.Concat("http://localhost:", httpPort);

        static string httpsPort = ConfigurationManager.AppSettings["httpsPort"];
        static string httpsUrl = String.Concat("https://localhost:", httpsPort);

        [STAThread]
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().RunAsync();
            escribirUrlsEnAppConfig(httpUrl, httpsUrl);

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm = new Form1();
            Application.Run(MainForm);
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls(httpUrl, httpsUrl);

        public static void escribirUrlsEnAppConfig(string http, string https)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath);

            foreach (XmlElement element in xmlDoc.DocumentElement)
            {
                if (element.Name.Equals("appSettings"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes[0].Value == "urls")
                        {
                            node.Attributes[1].Value = String.Concat(http, " | ", https);
                        }
                    }
                }
            }

            xmlDoc.Save(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
