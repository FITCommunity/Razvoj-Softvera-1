using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2020_01_30.ViewModels
{
    public class TakmicenjeRezultatiVM
    {
        public int TakmicenjeId { get; set; }
        public int SkolaId { get; set; }
        public string Skola { get; set; }
        public int PredmetId { get; set; }
        public string Predmet { get; set; }
        public int Razred { get; set; }
        public string Datum { get; set; }
        public bool Zakljucan { get; set; }
    }
}
