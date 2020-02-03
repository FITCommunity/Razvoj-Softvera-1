using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2020_01_30.ViewModels
{
    public class AjaxRezultatiUrediDodajVM
    {
        public int TakmicenjeUcesnikId { get; set; }
        public List<SelectListItem> Ucesnici { get; set; }
        public int Bodovi { get; set; }

    }
}
