using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_12_02.EntityModels
{
    public class PopravniIspit
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public int OdjeljenjeId { get; set; }
        public Odjeljenje Odjeljenje { get; set; }
        public int PredmetId { get; set; }
        public Predmet Predmet { get; set; }
    }
}
