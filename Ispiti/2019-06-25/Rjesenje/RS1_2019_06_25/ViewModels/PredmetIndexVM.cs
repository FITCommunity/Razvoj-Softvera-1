using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_06_25.ViewModels
{
    public class PredmetIndexVM
    {
        public List<Row> Rows { get; set; }
        public class Row
        {
            public int AngazovanId { get; set; }
            public string Predmet { get; set; }
            public string AkademskaGodina { get; set; }
            public string Nastavnik { get; set; }
            public int BrojOdrzanihCasova { get; set; }
            public int BrojStudenataNaPredmetu { get; set; }

        }
    }
}
