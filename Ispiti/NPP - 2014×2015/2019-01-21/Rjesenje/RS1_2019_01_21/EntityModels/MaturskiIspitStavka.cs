using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_01_21.EntityModels
{
    public class MaturskiIspitStavka
    {
        public int Id { get; set; }
        public bool IsPristupio { get; set; }
        public int Bodovi { get; set; }
        public int MaturskiIspitId { get; set; }
        public MaturskiIspit MaturskiIspit { get; set; }
        public int OdjeljenjeStavkaId { get; set; }
        public OdjeljenjeStavka OdjeljenjeStavka { get; set; }
    }
}
