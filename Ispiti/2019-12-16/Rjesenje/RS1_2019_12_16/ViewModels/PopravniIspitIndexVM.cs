using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_12_16.ViewModels
{
    public class PopravniIspitIndexVM
    {
        public int SkolaId { get; set; }
        public int SkolskaGodinaId { get; set; }
        public int PredmetId { get; set; }
        public List<SelectListItem> Skole { get; set; }
        public List<SelectListItem> SkoleskeGodine { get; set; }
        public List<SelectListItem> Predmeti { get; set; }
    }
}
