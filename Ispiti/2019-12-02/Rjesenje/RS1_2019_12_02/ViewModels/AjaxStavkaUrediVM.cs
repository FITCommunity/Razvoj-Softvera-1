using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_12_02.ViewModels
{
    public class AjaxStavkaUrediVM
    {
        public int PopravniIspitStavkaId { get; set; }
        public int UcenikId { get; set; }
        public string Ucenik { get; set; }
        [Range(0, 100)]
        public int? Bodovi { get; set; }
    }
}
