using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_01_21.ViewModels
{
    public class OdrzanaNastavaDodajVM
    {
        public int NastavnikId { get; set; }
        public string Nastavnik { get; set; }
        public int SkolaId { get; set; }
        public int SkolskaGodinaId { get; set; }
        public int PredmetId { get; set; }
        public List<SelectListItem> Skole { get; set; }
        public string SkolskaGodina { get; set; }
        public List<SelectListItem> Predmeti { get; set; }
        public DateTime Datum { get; set; }
    }
}
