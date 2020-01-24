using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_12_02.ViewModels
{
    public class PopravniIspitUcenici
    {
        public int PopravniIspitId { get; set; }
        public List<Row> Rows { get; set; }
        public class Row
        {
            public int PopravniIspitStavkeId { get; set; }
            public string Ucenik { get; set; }
            public int UcenikId { get; set; }
            public string Odjeljenje { get; set; }
            public int BrojUDnevniku { get; set; }
            public bool Pristupio { get; set; }
            public int? Bodovi { get; set; }

        }
    }
}
