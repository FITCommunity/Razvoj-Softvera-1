using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RS1_2017_09_11.EntityModels
{
    public class Odjeljenje
    {
        public int Id { get; set; }
        public string SkolskaGodina { get; set; }
        public int Razred { get; set; }
        public string Oznaka { get; set; }
        public bool IsPrebacenuViseOdjeljenje { get; set; }
        public int? NastavnikID { get; set; }
        [ForeignKey(nameof(NastavnikID))]
        public Nastavnik Nastavnik { get; set; }
    }
}
