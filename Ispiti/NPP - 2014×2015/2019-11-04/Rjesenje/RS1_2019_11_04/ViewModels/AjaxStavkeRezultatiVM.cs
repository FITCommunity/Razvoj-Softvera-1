using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_11_04.ViewModels
{
    public class AjaxStavkeRezultatiVM
    {
        public int PopravniIspitId { get; set; }
        public List<Row> Rows { get; set; }
        public class Row
        {
            public int StavkaPopravniIspitId { get; set; }
            public string UcenikImePrezime { get; set; }
            public string Odjeljenje { get; set; }
            public int BrojUDnevniku { get; set; }
            public bool? IsPristupio { get; set; }
            public int Bodovi { get; set; }
        }
    }
}
