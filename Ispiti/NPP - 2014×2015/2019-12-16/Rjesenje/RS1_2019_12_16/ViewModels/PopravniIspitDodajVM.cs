using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_12_16.ViewModels
{
    public class PopravniIspitDodajVM
    {
        public int SkolaId { get; set; }
        public string Skola { get; set; }
        public int SkolskaGodinaId { get; set; }
        public string SkolskaGodina { get; set; }
        public int PredmetId { get; set; }
        public string Predmet { get; set; }
        public int ClanKomisije1Id { get; set; }
        public int ClanKomisije2Id { get; set; }
        public int ClanKomisije3Id { get; set; }
        public List<SelectListItem> Nastavnici { get; set; }
        public DateTime Datum { get; set; }
    }
}
