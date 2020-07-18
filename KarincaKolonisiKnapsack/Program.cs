using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KarincaKolonisiKnapsack01
{
    class Program
    {
        static void Main(string[] args)
        {
            int karincaSayisi = 200;
            int ADIM_SAYISI = 500;
            double ALFA = 0.5;
            double BETA = 0.5;
            double PHI = 0.2;
            int DENEME_SAYISI = 10;

            // Kullandığımız algoritmada Q sabiti olmadığı için
            // dahil edilmedi
            //double Q = 100;

            VeriOkuma veri = new VeriOkuma();
            DosyayaYazdir dosya = new DosyayaYazdir();

            for (int i = 1; i <= 10; i++)
            {
                string dosyaYolu = Path.Combine(Environment.CurrentDirectory, @"Input\", "test" + i + ".txt");
                string ciktiDosyaYolu = Path.Combine(Environment.CurrentDirectory, @"Output\", "test" + i + "_4_results.txt");

                List<Esya> esyaList = veri.elemanlarListesi(dosyaYolu);

                KarincaKolonisi karincaKolonisi = new KarincaKolonisi(karincaSayisi, ADIM_SAYISI, esyaList, veri.Kapasite, PHI, ALFA, BETA);

                Console.WriteLine("test" + i + ".txt" + " " + esyaList.Count + " " + veri.Kapasite);

                for (int j = 0; j < DENEME_SAYISI; j++)
                {
                    karincaKolonisi.Optimizasyon();

                    if (j == DENEME_SAYISI - 1)
                        karincaKolonisi.CiktiVer(karincaKolonisi.EnIyiCozumlerListesi, karincaKolonisi.ZamanFarklariListesi, ciktiDosyaYolu, dosya);
                }
            }

            Console.WriteLine("\nProgram çalışmayı durdurdu.");
            Console.ReadKey();
        }
    }
}
