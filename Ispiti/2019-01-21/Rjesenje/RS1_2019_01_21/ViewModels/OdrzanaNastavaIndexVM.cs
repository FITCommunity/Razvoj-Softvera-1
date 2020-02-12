using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_01_21.ViewModels
{
    public class OdrzanaNastavaIndexVM
    {

        public List<Row> Rows { get; set; }
        public class Row
        {
            public int NastavnikId { get; set; }
            public string Nastavnik { get; set; }
            public string Skola { get; set; }
        }
    }
}
