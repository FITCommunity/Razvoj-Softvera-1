using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_11_04.ViewModels
{
    public class PopravniIspitDodajVM
    {
        public int PredmetId { get; set; }
        public string Predmet { get; set; }
        public string Razred { get; set; }
        public string DatumIspita { get; set; }
        public int SkolaId { get; set; }
        public List<SelectListItem> Skole { get; set; }
        public int SkolskaGodinaId { get; set; }
        public List<SelectListItem> SkolskeGodine { get; set; }
    }
}
