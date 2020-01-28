using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_12_02.ViewModels
{
    public class PopravniIspitDodajVM
    {
        public int PopravniIspitId { get; set; }
        public int PredmetId { get; set; }
        public List<SelectListItem> Predmeti { get; set; }
        
        public DateTime Datum { get; set; }
        public int SkolaId { get; set; }
        public string Skola { get; set; }
        public int SkolskaGodinaId { get; set; }
        public string SkolskaGodina { get; set; }
        public int OdjeljenjeId { get; set; }
        public string Odjeljenje { get; set; }
    }
}
