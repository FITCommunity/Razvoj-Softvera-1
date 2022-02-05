using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2020_01_30.ViewModels
{
    public class TakmicenjeOdaberiVM
    {
        public int SkolaId { get; set; }
        public string Skola { get; set; }
        public string Razred { get; set; }
        public List<Row> Rows { get; set; }
        public class Row
        {
            public int TakmicenjeId { get; set; }
            public int PredmetId { get; set; }
            public string Predmet { get; set; }
            public int Razred { get; set; }
            public string Datum { get; set; }
            public int BrojUcesnikaKojiNisuPristupili { get; set; }
            public int NajboljiUcesnikId { get; set; }
            public string NajboljiUcesnikSkola { get; set; }
            public string NajboljiUcesnikOdjeljenje { get; set; }
            public string NajboljiUcesnikImePrezime { get; set; }
        }
    }
}
