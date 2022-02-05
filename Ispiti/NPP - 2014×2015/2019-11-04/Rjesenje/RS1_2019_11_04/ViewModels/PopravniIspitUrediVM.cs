using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_11_04.ViewModels
{
    public class PopravniIspitUrediVM
    {
        public int PopravniIspitId { get; set; }
        public int PredmetId { get; set; }
        public string Predmet { get; set; }
        public string Razred { get; set; }
        public DateTime DatumIspita { get; set; }
        public int SkolaId { get; set; }
        public string Skola { get; set; }
        public int SkolskaGodinaId { get; set; }
        public string SkolskaGodina { get; set; }
    }
}
