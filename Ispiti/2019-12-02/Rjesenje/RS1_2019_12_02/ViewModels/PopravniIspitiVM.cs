using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_12_02.ViewModels
{
    public class PopravniIspitiVM
    {
        public int OdjeljenjeId { get; set; }
        public string Odjeljenje { get; set; }
        public string Skola { get; set; }
        public string SkolskaGodina { get; set; }

        public List<Row> Rows { get; set; }
        public class Row
        {
            public int PopravniIspitId { get; set; }
            public DateTime Datum { get; set; }
            public string Predmet { get; set; }
            public int BrojUcenikaNaPopravnom { get; set; }
            public int BrojUcenikaKojiSuPolozili { get; set; }
        }
    }
}
