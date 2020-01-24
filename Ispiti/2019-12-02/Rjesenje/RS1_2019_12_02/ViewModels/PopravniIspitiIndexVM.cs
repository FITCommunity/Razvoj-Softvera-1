using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_12_02.ViewModels
{
    public class PopravniIspitiIndexVM
    {
        public List<Row> Rows { get; set; }
        public class Row
        {
            public int PopravniIspitId { get; set; }
            public int OdjeljenjeId { get; set; }
            public string Skola { get; set; }
            public string SkolskaGodina { get; set; }
            public string Odjeljenje { get; set; }
        }
    }
}
