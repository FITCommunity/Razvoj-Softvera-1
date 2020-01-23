using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2018_02_13.Data.EntityModels
{
    public class Student
    {
        [Key]
        public int ID { get; set; }
        public string ImePrezime { get; set; }


        [ForeignKey(nameof(KorisnickiNalog))]
        public int KorisnickiNalogId { get; set; }
        public KorisnickiNalog KorisnickiNalog { get; set; }
    }
}
