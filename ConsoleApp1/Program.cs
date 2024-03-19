using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UyumsoftEInvoiceLive.Client client = new UyumsoftEInvoiceLive.Client();

            DateTime dt1 = Convert.ToDateTime("01.01.2024");
            DateTime dt2 = Convert.ToDateTime("15.02.2024");

            var res  = client.GetInboxInvoiceList("Bomaksan_WebServis", "C8AyWmHA",dt1 , dt2);

            var result = client.GetInboxInvoiceData("Bomaksan_WebServis", "C8AyWmHA", "35d8ce3f-eff2-4a2f-bf0e-a0dce013b891");

            string base64String = Convert.ToBase64String(result.Value.Data);


            XmlDocument doc = new XmlDocument();
            string xml = Encoding.UTF8.GetString(result.Value.Data); 
            doc.LoadXml(xml);

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // Kaydedilecek dosyanın tam yolunu belirleyin
            string filePath = Path.Combine(desktopPath, "savedXmlFile.xml");

            // XML belgesini dosyaya kaydedin
            doc.Save(filePath);

        }
    }
}
