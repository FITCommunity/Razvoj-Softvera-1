using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_11_04.EntityModels
{
    public class PopravniIspitStavka
    {
        public int Id { get; set; }
        public int PopravniIspitId { get; set; }
        public PopravniIspit PopravniIspit { get; set; }
        public int OdjeljenjeStavkaId { get; set; }
        public OdjeljenjeStavka OdjeljenjeStavka { get; set; }
        public bool? IsPristupio { get; set; }
        public int Bodovi { get; set; }
    }
}
