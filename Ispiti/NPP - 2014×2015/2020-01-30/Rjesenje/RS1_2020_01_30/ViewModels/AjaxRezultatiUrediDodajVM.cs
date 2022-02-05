using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2020_01_30.ViewModels
{
    public class AjaxRezultatiUrediDodajVM
    {
        public int TakmicenjeId { get; set; }
        public int UcesnikId { get; set; }
        public List<SelectListItem> Ucesnici { get; set; }
        public int Bodovi { get; set; }
    }
}
