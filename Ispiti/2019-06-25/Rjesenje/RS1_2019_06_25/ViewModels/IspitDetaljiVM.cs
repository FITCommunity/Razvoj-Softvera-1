using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_06_25.ViewModels
{
    public class IspitDetaljiVM
    {
        public int IspitId { get; set; }
        public int PredmetId { get; set; }
        public string Predmet { get; set; }
        public string Nastavnik { get; set; }
        public string AkademskaGodina { get; set; }
        public DateTime Datum { get; set; }
        public string Opis { get; set; }
        public bool IsZakljucan { get; set; }
    }
}
