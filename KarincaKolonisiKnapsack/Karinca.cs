using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KarincaKolonisiKnapsack01
{
    class Karinca
    {
        List<Esya> esyalar = new List<Esya>();
        List<int> tabuListesi = new List<int>();
        Dictionary<int, double> secmeDurumu = new Dictionary<int, double>();
        public Karinca(List<Esya> esyalar)
        {
            Esyalar = esyalar;
        }

        // tabu listesinde olmayanlari dondurur
        public List<Esya> Secilmemis()
        {
            List<Esya> secilmemis = new List<Esya>();

            for (int i = 0; i < Esyalar.Count; i++)
                // esyanin indisi tabu listesinde yoksa secilmemislere ekle
                if (!TabuListesi.Contains(Esyalar[i].Indis))
                    secilmemis.Add(Esyalar[i]);

            return secilmemis;
        }
        public double CantaDegeri()
        {
            double toplam = 0;

            foreach (var esya in TabuListesi)
            {
                toplam += Esyalar[esya].Deger;
            }

            return toplam;
        }

        public double CantaAgirligi()
        {
            double toplam = 0;

            foreach (var esya in TabuListesi)
            {
                toplam += Esyalar[esya].Agirlik;
            }

            return toplam;
        }

        public List<int> TabuListesi { get => tabuListesi; set => tabuListesi = value; }
        internal List<Esya> Esyalar { get => esyalar; set => esyalar = value; }
        public Dictionary<int, double> SecmeDurumu { get => secmeDurumu; set => secmeDurumu = value; }
    }
}
