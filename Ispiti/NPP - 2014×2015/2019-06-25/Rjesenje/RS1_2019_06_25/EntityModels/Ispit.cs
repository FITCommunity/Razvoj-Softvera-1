using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_06_25.EntityModels
{
    public class Ispit
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public int AngazovanId { get; set; }
        public Angazovan Angazovan { get; set; }
        public bool Zakljucano { get; set; }
        public string Napomena { get; set; }
    }
}
