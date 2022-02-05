using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_12_16.ViewModels
{
    public class AjaxStavkeIndexVM
    {
        public int PopravniIspitId { get; set; }
        public int SkolaId { get; set;}
        public int SkolskaGodinaId { get; set;}
        public int PredmetId { get; set;}
        public List<Row> Rows { get; set; }

        public class Row
        {
            public int PopravniIspitStavkaId { get; set; }
            public string Odjeljenje { get; set; }
            public int BrojUDnevniku { get; set; }
            public string Ucenik { get; set; }
            public bool? IsPristupio { get; set; }
            public int Bodovi { get; set; }
        }
    }
}
