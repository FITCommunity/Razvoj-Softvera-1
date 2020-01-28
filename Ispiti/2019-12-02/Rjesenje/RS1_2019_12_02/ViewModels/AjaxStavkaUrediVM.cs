using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_12_02.ViewModels
{
    public class AjaxStavkaUrediVM
    {
        public int PopravniIspitStavkaId { get; set; }
        public int UcenikId { get; set; }
        public string Ucenik { get; set; }
        [Max(100)]
        [Min(0)]
        public int? Bodovi { get; set; }
    }
}
