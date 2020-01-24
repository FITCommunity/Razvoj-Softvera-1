using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2019_12_02.EntityModels
{
   
    public class OdjeljenjeStavka
    {
        public int Id { get; set; }

        [ForeignKey(nameof(UcenikId))]
        public virtual Ucenik Ucenik { get; set; }
        public int UcenikId { get; set; }


        [ForeignKey(nameof(OdjeljenjeId))]
        public virtual Odjeljenje Odjeljenje { get; set; }
        public int OdjeljenjeId { get; set; }

        public int BrojUDnevniku { get; set; }

    }
}
