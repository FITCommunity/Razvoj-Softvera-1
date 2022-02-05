using System;

namespace RS1_2018_01_23.EntityModels
{
    public class RezultatPretrage
    {
        public int Id { get; set; }
        public virtual Uputnica Uputnica { get; set; }
        public int UputnicaId { get; set; }

        public LabPretraga LabPretraga { get; set; }
        public int LabPretragaId  { get; set; }


        public int? ModalitetId { get; set; }
        public Modalitet Modalitet { get; set; }
        public double? NumerickaVrijednost { get; set; }
    }
}
