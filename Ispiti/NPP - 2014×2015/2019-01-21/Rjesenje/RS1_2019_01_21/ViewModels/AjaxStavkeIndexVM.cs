using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_01_21.ViewModels
{
    public class AjaxStavkeIndexVM
    {
        public int MaturskiIspitId { get; set; }

        public List<Row> Rows { get; set; }

        public class Row
        {
            public int MaturskiIspitStavkaId { get; set; }
            public string Ucenik { get; set; }
            public double Prosjek { get; set; }
            public bool IsPristupio { get; set; }
            public int Bodovi { get; set; }
        }
    }
}
