using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_12_02.ViewModels
{
    public class PopravniIspitDetaljiVM
    {
        public int OdjeljenjeId { get; set; }
        public string Skola { get; set; }
        public string SkolskaGodina { get; set; }
        public string Odjeljenje { get; set; }
        public List<Row> Rows { get; set; }
        public class Row
        {
            public int PopravniIspitId { get; set; }
            public DateTime DatumPopravnog { get; set; }
            public string Predmet { get; set; }
            public int BrojUcenikaNaPopravnom { get; set; }
            public int BrojUcenikaPolozili { get; set; }
        }
    }
}
