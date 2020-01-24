using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_12_02.EntityModels
{
    public class PopravniIspitStavke
    {
        public int Id { get; set; }

        [ForeignKey(nameof(PopravniIspitId))]
        public PopravniIspit PopravniIspit { get; set; }
        
        public int PopravniIspitId { get; set; }

        [ForeignKey(nameof(OdjeljenjeStavkaId))]
        public OdjeljenjeStavka OdjeljenjeStavka { get; set; }
        
        public int OdjeljenjeStavkaId { get; set; }
        public int? Bodovi { get; set; }
        public bool Pristupio { get; set; }
    }
}
