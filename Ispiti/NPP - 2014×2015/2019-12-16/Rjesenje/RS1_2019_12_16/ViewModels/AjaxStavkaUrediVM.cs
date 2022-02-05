using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RS1_2019_12_16.ViewModels
{
    public class AjaxStavkaUrediVM
    {
        public int PopravniIspitStavkaId { get; set; }
        public string Ucenik { get; set; }
        public int Bodovi { get; set; }
    }
}