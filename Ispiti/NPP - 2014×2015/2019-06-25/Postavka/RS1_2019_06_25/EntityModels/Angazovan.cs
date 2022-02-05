using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2019_06_25.EntityModels
{
    public class Angazovan
    {
        public int Id { get; set; }

        [ForeignKey(nameof(NastavnikId))]
        public virtual Nastavnik Nastavnik { get; set; }
        public int NastavnikId { get; set; }

        [ForeignKey(nameof(AkademskaGodinaId))]
        public virtual AkademskaGodina AkademskaGodina{ get; set; }
        public int AkademskaGodinaId { get; set; }

        [ForeignKey(nameof(PredmetId))]
        public virtual Predmet Predmet { get; set; }
        public int PredmetId { get; set; }
    }
}
