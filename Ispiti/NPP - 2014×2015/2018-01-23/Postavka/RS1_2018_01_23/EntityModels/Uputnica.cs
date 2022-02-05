using System;

namespace RS1_2018_01_23.EntityModels
{
    public class Uputnica
    {
        public int Id { get; set; }
        public Ljekar UputioLjekar{ get; set; }
        public int UputioLjekarId { get; set; }

        public Ljekar LaboratorijLjekar { get; set; }
        public int? LaboratorijLjekarId { get; set; }

        public Pacijent Pacijent { get; set; }
        public int PacijentId { get; set; }

        public VrstaPretrage VrstaPretrage { get; set; }
        public int VrstaPretrageId { get; set; }

        public DateTime DatumUputnice { get; set; }
        public DateTime? DatumRezultata { get; set; }
        public bool IsGotovNalaz{ get; set; }
    }
}
