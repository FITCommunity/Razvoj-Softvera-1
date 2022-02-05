using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_11_04.EntityModels
{
    public class PopravniIspit
    {
        public int Id { get; set; }
        public DateTime DatumIspita { get; set; }
        public int SkolaId { get; set; }
        public Skola Skola { get; set; }
        public int SkolskaGodinaId { get; set; }
        public SkolskaGodina SkolskaGodina { get; set; }
        public int PredmetId { get; set; }
        public Predmet Predmet { get; set; }
    }
}
