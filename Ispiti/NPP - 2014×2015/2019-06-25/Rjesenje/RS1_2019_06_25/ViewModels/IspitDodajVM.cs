using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_06_25.ViewModels
{
    public class IspitDodajVM
    {
        public int AngazovanId { get; set; }
        public string Predmet { get; set; }
        public string Nastavnik { get; set; }
        public string AkademskaGodina { get; set; }
        public DateTime Datum { get; set; }
        public string Opis { get; set; }
    }
}
