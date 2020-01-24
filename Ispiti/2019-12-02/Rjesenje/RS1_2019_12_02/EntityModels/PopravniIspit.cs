using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_12_02.EntityModels
{
    public class PopravniIspit
    {
        public int Id { get; set; }
        public DateTime DatumPopravnog { get; set; }

        [ForeignKey(nameof(OdjeljenjeId))]
        public Odjeljenje Odjeljenje { get; set; }
        public int OdjeljenjeId { get; set; }
        [ForeignKey(nameof(PredmetId))]
        public Predmet Predmet { get; set; }
        
        public int PredmetId { get; set; }
    }
}
