using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_11_04.ViewModels
{
    public class PopravniIspitOdaberiVM
    {
        public int PredmetId { get; set; }
        public string Predmet { get; set; }
        public string Razred { get; set; }
        public List<Row> Rows { get; set; }
        public class Row
        {
            public int PopravniIspitId { get; set; }
            public string Skola { get; set; }
            public string SkolskaGodina { get; set; }
            public string DatumIspita { get; set; }
            public int BrojUcenikaNaIspitu { get; set; }
            public int BrojUcenikaKojiSuPolozili { get; set; }
        }
    }
}
