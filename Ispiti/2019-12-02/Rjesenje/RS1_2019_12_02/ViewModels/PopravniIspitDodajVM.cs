using Microsoft.AspNetCore.Mvc.Rendering;
using RS1_2019_12_02.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public DateTime DatumPopravnog { get; set; }
        
        [DisplayName("Skola")]
        public int SkolaId { get; set; }
        public string Skola { get; set; }
        
        [DisplayName("Skolska godina")]
        public int SkolskaGodinaId { get; set; }
        public string SkolskaGodina { get; set; }
        
        [DisplayName("Odjeljenje")]
        public int OdjeljenjeId { get; set; }
        public string Odjeljenje { get; set; }
    }
}
