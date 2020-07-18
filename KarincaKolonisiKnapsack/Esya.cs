using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KarincaKolonisiKnapsack01
{
    class Esya
    {
        private double agirlik;
        private double deger;
        private int indis;
        private double feromon;
        private double cazibe;
        private double proportion;
        private double knapsackKapasitesi;

        public Esya(int agirlik, int deger, int indis, double knapsackKapasitesi)
        {
            Agirlik = agirlik;
            Deger = deger;
            Indis = indis;
            KnapsackKapasitesi = knapsackKapasitesi;
            Feromon = 1;
            //Cazibe = Deger / (Agirlik / knapsackKapasitesi); // AKA3
            Cazibe = Deger / (Math.Pow(Agirlik, 2)); // AKA2
        }

        public double Agirlik { get => agirlik; set => agirlik = value; }
        public double Deger { get => deger; set => deger = value; }
        public int Indis { get => indis; set => indis = value; }
        public double Feromon { get => feromon; set => feromon = value; }
        public double Proportion { get => proportion; set => proportion = value; }
        public double Cazibe { get => cazibe; set => cazibe = value; }
        public double KnapsackKapasitesi { get => knapsackKapasitesi; set => knapsackKapasitesi = value; }
    }
}
