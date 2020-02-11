using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RS1_2019_12_16.ViewModels
{
    public class PopravniIspitDodajUcenikaVM
    {
        public int PopravniIspitId { get; set; }
        public int OdjeljenjeStavkaId { get; set; }
        public List<SelectListItem> Ucenici { get; set; }
    }
}