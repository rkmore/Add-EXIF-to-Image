using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddEXIFexample
{
    class Program
    {
        static void Main(string[] args)
        {
            Image img = new Bitmap(100, 100);
            Graphics.FromImage(img).FillRectangle(Brushes.Blue, 0, 0, 100, 100);
            
            string fileName = Path.Combine(Path.GetTempPath(), "I_" + Guid.NewGuid().ToString("N") + ".jpg");

            var dateTime = (PropertyItem)FormatterServices.GetUninitializedObject(typeof(PropertyItem));
            dateTime.Id = 36867;
            dateTime.Type = 2;
            dateTime.Value = System.Text.ASCIIEncoding.ASCII.GetBytes(DateTime.Now.ToString("yyyy:MM:dd HH:mm:ss"));
            dateTime.Len = dateTime.Value.Length;
            img.SetPropertyItem(dateTime);

            var make = (PropertyItem)FormatterServices.GetUninitializedObject(typeof(PropertyItem));
            make.Id = 0x010f;
            make.Type = 2;
            make.Value = System.Text.ASCIIEncoding.ASCII.GetBytes("DummyMake");
            make.Len = make.Value.Length;
            img.SetPropertyItem(make);

            var model = (PropertyItem)FormatterServices.GetUninitializedObject(typeof(PropertyItem));
            model.Id = 0x0110;
            model.Type = 2;
            model.Value = System.Text.ASCIIEncoding.ASCII.GetBytes("Clipboard");
            model.Len = model.Value.Length;
            img.SetPropertyItem(model);

            img.Save(fileName, ImageFormat.Jpeg);
            Console.WriteLine("Image with EXIF meta data saved to " + fileName);
            Console.WriteLine("Press a key to exit....");
            Console.ReadKey();
        }
    }
}
