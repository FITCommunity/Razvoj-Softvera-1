using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_06_25.ViewModels
{
    public class AjaxStavkeIndexVM
    {
        public bool IsZakljucan { get; set; }
        public List<Row> Rows { get; set; }
        public class Row
        {
            public int StavkaIspitId { get; set; }
            public string Student { get; set; }
            public bool IsPristupio { get; set; }
            public int Ocjena { get; set; }
        }
    }
}
