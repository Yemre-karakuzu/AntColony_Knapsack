using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KarincaKolonisiKnapsack01
{
    class KarincaKolonisi
    {
        private double alfa;
        private double beta;
        private double phi;
        private double globalMax = 0;
        private int iterasyonSayisi;
        private int karincaSayisi;
        private int kapasite;
        private List<Esya> esyalar = new List<Esya>();
        List<Karinca> karincalar = new List<Karinca>();
        RastgeleSayi rastgeleSayi = new RastgeleSayi();
        List<double> enIyiCozumlerListesi = new List<double>();
        List<TimeSpan> zamanFarklariListesi = new List<TimeSpan>();

        public KarincaKolonisi(int karincaSayisi, int iterasyonSayisi, List<Esya> esyalar, int kapasite, double phi, double alfa, double beta)
        {
            KarincaSayisi = karincaSayisi;
            IterasyonSayisi = iterasyonSayisi;
            Esyalar = esyalar;
            Kapasite = kapasite;
            Phi = phi;
            Alfa = alfa;
            Beta = beta;

            for (int i = 0; i < karincaSayisi; i++)
                Karincalar.Add(new Karinca(esyalar));
        }

        public void IlkAtama()
        {
            int sayi;

            // karinca ilk esyayi secti
            for (int i = 0; i < Karincalar.Count; i++)
            {
                sayi = RastgeleSayi.Between(0, Esyalar.Count);
                Karincalar[i].TabuListesi.Add(sayi);
            }
        }

        public void Optimizasyon()
        {
            //sure baslangic
            DateTime sureBas = DateTime.Now;

            IlkAtama();

            double globalEnIyiDeger = 0;
            double localEnIyiDeger = 0;

            for (int step = 0; step < IterasyonSayisi; step++)
            {
                localEnIyiDeger = 0;
                for (int i = 0; i < Karincalar.Count; i++)
                {
                    // karincanin cantasi boşsa
                    while (Kapasite - Karincalar[i].CantaAgirligi() >= 0)
                    {
                        double pToplam = 0;
                        Dictionary<int, double> indisVeProportion = new Dictionary<int, double>();

                        foreach (var secilmemis in Karincalar[i].Secilmemis())
                        {
                            indisVeProportion.Add(secilmemis.Indis, Math.Pow(secilmemis.Feromon, Alfa) * Math.Pow(secilmemis.Cazibe, Beta));
                            pToplam += indisVeProportion[secilmemis.Indis];
                        }

                        foreach (var element in indisVeProportion.ToList())
                        {
                            indisVeProportion[element.Key] = element.Value / pToplam;
                        }

                        int secilecek = RuletIleSecim(indisVeProportion);
                        Karincalar[i].TabuListesi.Add(secilecek);
                    }

                    if (localEnIyiDeger < Karincalar[i].CantaDegeri())
                        localEnIyiDeger = Karincalar[i].CantaDegeri();
                }

                if (globalEnIyiDeger < localEnIyiDeger)
                    globalEnIyiDeger = localEnIyiDeger;

                FeromonGuncelle();
            }

            //sure bitis
            TimeSpan zamanFarki = DateTime.Now - sureBas;

            EnIyiCozumlerListesi.Add(globalEnIyiDeger);
            ZamanFarklariListesi.Add(zamanFarki);

            Console.WriteLine(" " + globalEnIyiDeger + " " + zamanFarki.TotalMilliseconds);
        }

        // karincanin secebilecegi esyalari rulete atip ruletten hangisini sececegine karar ver
        public int RuletIleSecim(Dictionary<int, double> indisProp)
        {
            // dictionary'deki degerleri artan sirada siraladik
            var siraliIndisProp = indisProp.ToList();
            siraliIndisProp.Sort((x, y) => x.Value.CompareTo(y.Value));

            double toplam = 0;
            Dictionary<int, double> toplamList = new Dictionary<int, double>();

            for (int i = 0; i < siraliIndisProp.Count; i++)
            {
                for (int j = i; j <= i; j++)
                    toplam += siraliIndisProp[j].Value;
                //dictionary'nin indisiyle beraber ekliyoruz
                toplamList.Add(siraliIndisProp[i].Key, toplam);
            }

            // 0 ile 1 arasinda sayi tuttuk
            double sayi = RastgeleSayi.BetweenDouble(0, 2);

            // tutulan sayi, hangi rulet araliginda kaliyorsa o indisi tutuyoruz
            int secilecekEsya = toplamList.Aggregate((x, y) => x.Value < sayi && y.Value > sayi ? y : x).Key;

            return secilecekEsya;
        }

        public void FeromonGuncelle()
        {
            double feromonDegisim = 0;
            int guncellenecekEsya = 0;

            // enIyiEsyaninDegeri = bestZ
            double enIyiEsyaninDegeri = Karincalar[0].Esyalar.Aggregate((x, y) => x.Deger > y.Deger ? x : y).Deger;

            for (int i = 0; i < Karincalar.Count; i++)
            {
                for (int j = 0; j < Karincalar[i].TabuListesi.Count; j++)
                {
                    guncellenecekEsya = Karincalar[i].TabuListesi[j];
                    feromonDegisim = 1 / (1 + (enIyiEsyaninDegeri - Esyalar[guncellenecekEsya].Deger) / enIyiEsyaninDegeri);
                    Esyalar[guncellenecekEsya].Feromon = (1 - Phi) * Esyalar[guncellenecekEsya].Feromon + feromonDegisim;
                }
            }
        }

        public void CiktiVer(List<double> enIyiCozum, List<TimeSpan> zamanFarki, string dosyaAdi, DosyayaYazdir dosya)
        {
            // dosya yazma islemleri
            Dictionary<double, TimeSpan> ciktilar = new Dictionary<double, TimeSpan>();
            double toplamDeger = 0, ortalamaDeger;

            for (int i = 0; i < enIyiCozum.Count; i++)
            {
                ciktilar.Add(enIyiCozum[i], zamanFarki[i]);
            }

            for (int i = 0; i < ciktilar.Count; i++)
                toplamDeger += ciktilar.ElementAt(i).Key;

            ortalamaDeger = toplamDeger / ciktilar.Count;

            double enIyiCiktiDegeri = ciktilar.Keys.Max();
            TimeSpan enIyiCiktiSuresi = ciktilar[enIyiCiktiDegeri];

            dosya.Yaz(dosyaAdi, ortalamaDeger, enIyiCiktiDegeri, enIyiCiktiSuresi);
        }

        public int IterasyonSayisi { get => iterasyonSayisi; set => iterasyonSayisi = value; }
        internal List<Esya> Esyalar { get => esyalar; set => esyalar = value; }
        internal List<Karinca> Karincalar { get => karincalar; set => karincalar = value; }
        public int KarincaSayisi { get => karincaSayisi; set => karincaSayisi = value; }
        public RastgeleSayi RastgeleSayi { get => rastgeleSayi; set => rastgeleSayi = value; }
        public int Kapasite { get => kapasite; set => kapasite = value; }
        public double GlobalMax { get => globalMax; set => globalMax = value; }
        public double Phi { get => phi; set => phi = value; }
        public double Alfa { get => alfa; set => alfa = value; }
        public double Beta { get => beta; set => beta = value; }
        public List<double> EnIyiCozumlerListesi { get => enIyiCozumlerListesi; set => enIyiCozumlerListesi = value; }
        public List<TimeSpan> ZamanFarklariListesi { get => zamanFarklariListesi; set => zamanFarklariListesi = value; }
    }
}
