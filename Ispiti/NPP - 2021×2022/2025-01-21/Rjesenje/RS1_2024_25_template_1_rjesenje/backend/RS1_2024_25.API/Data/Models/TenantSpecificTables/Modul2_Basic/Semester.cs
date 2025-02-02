using RS1_2024_25.API.Data;
using RS1_2024_25.API.Data.Models.SharedTables;
using System.ComponentModel.DataAnnotations.Schema;
using RS1_2024_25.API.Data.Models.TenantSpecificTables;
using RS1_2024_25.API.Data.Models.TenantSpecificTables.Modul1_Auth;

namespace RS1_2024_25.API.Data.Models.TenantSpecificTables.Modul2_Basic
{
    public class Semester
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }
        public Student Student { get; set; }
        [ForeignKey(nameof(MyAppUser))]
        public int ProfesorId { get; set; }
        public MyAppUser Profesor { get; set; }
        public DateTime DatumUpisa { get; set; }
        public int GodinaStudija { get; set; }
        [ForeignKey(nameof(AcademicYear))]
        public int AkademskaGodinaId { get; set; }
        public AcademicYear AkademskaGodina { get; set; }
        public float CijenaSkolarine { get; set; }
        public bool Obnova { get; set; }


    }

}
