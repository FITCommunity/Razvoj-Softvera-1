using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RS1_2024_25.API.Data;
using RS1_2024_25.API.Data.Models.SharedTables;
using RS1_2024_25.API.Data.Models.TenantSpecificTables.Modul1_Auth;
using RS1_2024_25.API.Data.Models.TenantSpecificTables.Modul2_Basic;
using RS1_2024_25.API.Services;

namespace RS1_2024_25.API.Endpoints.SemesterEndpoints
{
    [Route("semesters/getByStudent")]
    public class SemesterGetEndpoint(ApplicationDbContext db,IHttpContextAccessor httpContextAccessor) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSemstersByStudent(int id, CancellationToken cancellationToken = default)
        {
            var semesters = await db.SemestersAll.Include(x => x.Student).Include(x => x.Profesor).Include(x => x.AkademskaGodina).Where(x => x.StudentId == id)
                 .Select(x => new SemesterResponse
                 {
                     Id = x.Id,
                     StudentId = x.StudentId,
                     Student = x.Student,
                     ProfesorId = x.ProfesorId,
                     ProfesorName=x.Profesor.FirstName,
                     DatumUpisa = x.DatumUpisa.ToString("yyyy-MM-dd"),
                     GodinaStudija = x.GodinaStudija,
                     AkademskaGodinaId = x.AkademskaGodinaId,
                     AkademskaGodinaDescription = x.AkademskaGodina.Description.Substring(x.AkademskaGodina.Description.LastIndexOf(' ') + 1),
                     CijenaSkolarine = x.CijenaSkolarine,
                     Obnova = x.Obnova,
                 }).ToListAsync(cancellationToken);

            return Ok(semesters);
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateSemester([FromBody] SemesterRequest r,CancellationToken cancellationToken = default)
        {
            MyAuthInfo myAuthInfo = MyAuthServiceHelper.GetAuthInfoFromRequest(db, httpContextAccessor);
            if (!myAuthInfo.IsLoggedIn)
            {
                return Unauthorized();
            }



            int profId = myAuthInfo.UserId;
            Semester? semester = new Semester();

            if (r == null) return BadRequest();

            semester.StudentId = r.StudentId;
            semester.ProfesorId = profId;
            semester.DatumUpisa = r.DatumUpisa;
            semester.GodinaStudija= r.GodinaStudija;
            semester.AkademskaGodinaId = r.AkademskaGodinaId;
            semester.CijenaSkolarine = r.CijenaSkolarine;
            semester.Obnova = r.Obnova;

            db.SemestersAll.Add(semester);
            await db.SaveChangesAsync(cancellationToken);

            return StatusCode(201, new { message = "Uspjesno ste dodali semestar", semesterId = semester.Id });

        }

    }

    public class SemesterResponse
    {
        public required int Id { get; set; }
        public required int StudentId { get; set; }
        public Student? Student { get; set; }
        public required int ProfesorId { get; set; }
        public MyAppUser? Profesor { get; set; }
        public string ProfesorName { get; set; }
        public required string DatumUpisa { get; set; }
        public required int GodinaStudija { get; set; }
        public required int AkademskaGodinaId { get; set; }
        public AcademicYear? AkademskaGodina { get; set; }
        public string AkademskaGodinaDescription { get; set; }
        public required float CijenaSkolarine { get; set; }
        public required bool Obnova { get; set; }
    }

    public class SemesterRequest
    {
        public required int StudentId { get; set; }
        public required DateTime DatumUpisa { get; set; }
        public required int GodinaStudija { get; set; }
        public required int AkademskaGodinaId { get; set; }
        public required float CijenaSkolarine { get; set; }
        public required bool Obnova { get; set; }
    }
}
