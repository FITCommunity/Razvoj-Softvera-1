using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_11_04.ViewModels
{
    public class PopravniIspitIndexVM
    {
        public List<Row> Rows { get; set; }
        public class Row
        {
            public int PredmetId { get; set; }
            public string Razred { get; set; }
            public string Predmet { get; set; }
        }
    }
}
