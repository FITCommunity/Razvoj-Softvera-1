using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_12_16.ViewModels
{
    public class PopravniIspitOdaberiVM
    {
        public int SkolaId { get; set; }
        public string Skola { get; set; }
        public int SkolskaGodinaId { get; set; }
        public string SkolskaGodina { get; set; }
        public int PredmetId { get; set; }
        public string Predmet { get; set; }
        public List<Row> Rows { get; set; }
        public class Row
        {
            public int PopravniIspitId { get; set; }
            public string ClanKomisije1 { get; set; }
            public int BrojUcenikaNaIspitu { get; set; }
            public int BrojUcenikaPolozili { get; set; }
            public string Datum { get; set; }
        }
    }
}
