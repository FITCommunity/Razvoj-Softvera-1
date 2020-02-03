using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2020_01_30.EntityModels
{
    public class Takmicenje
    {
        public int Id { get; set; }
        public int SkolaId { get; set; }
        public Skola Skola { get; set; }
        public int PredmetId { get; set; }
        public Predmet Predmet { get; set; }
        public int Razred { get; set; }
        public DateTime Datum { get; set; }
        public bool IsEditable { get; set; }
    }
}
