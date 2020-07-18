using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KarincaKolonisiKnapsack01
{
    public class DosyayaYazdir
    {
        public void Yaz(string dosyaAdi, double ortalamaDeger, double enIyiDeger, TimeSpan sure)
        {

            FileStream fs = new FileStream(dosyaAdi, FileMode.Create, FileAccess.Write);

            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine(ortalamaDeger + " " + enIyiDeger + " " + sure.TotalMilliseconds);

            sw.Flush();
            sw.Close();
            fs.Close();
        }
    }
}
