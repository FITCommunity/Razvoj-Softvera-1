using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_12_16.ViewModels
{
    public class PopravniIspitUrediVM
    {
        public int PopravniIspitId { get; set; }
        public int SkolaId { get; set; }
        public string Skola { get; set; }
        public int SkolskaGodinaId { get; set; }
        public string SkolskaGodina { get; set; }
        public int PredmetId { get; set; }
        public string Predmet { get; set; }
        public List<string> Komisija { get; set; } = new List<string>();
        public string Datum { get; set; }
    }
}
