using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2019_06_25.EntityModels
{
    public class UpisGodine
    {
        public int Id { get; set; }
        public int PolozioECTS { get; set; }
        public int SlusaECTS { get; set; }
        public DateTime DatumUpisa { get; set; }
        public int GodinaStudija { get; set; }

        [ForeignKey(nameof(StudentId))]
        public virtual Student Student { get; set; }
        public int StudentId { get; set; }

        [ForeignKey(nameof(AkademskaGodinaId))]
        public virtual AkademskaGodina AkademskaGodina { get; set; }
        public int AkademskaGodinaId { get; set; }
    }
}
