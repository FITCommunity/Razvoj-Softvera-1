using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2020_01_30.ViewModels
{
    public class RezultatiAjaxIndexVM
    {
        public int TakmicenjeId { get; set; }

        public List<Row> Rows { get; set; }
        public class Row
        {
            public int TakmicenjeUcesnikId { get; set; }
            public int UcenikId { get; set; }
            public string Odjeljenje { get; set; }
            public int BrojUDnevniku { get; set; }
            public bool IsPristupio { get; set; }
            public int? Bodovi { get; set; }

        }
    }
}
