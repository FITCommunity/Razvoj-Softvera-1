using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data;

namespace RS1_2024_25.API.Endpoints.AcademicYearEndpoints
{
    [Route("academicYearGetAll")]
    public class AcademicYearGetAllEndpoint(ApplicationDbContext db) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<ActionResult> GetAllAcademicYears(CancellationToken cancellationToken = default)
        {
            var academicYears = await db.AcademicYears.Select(x => new AcademicYearResponse
            {
                Id = x.ID,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Description = x.Description.Substring(x.Description.LastIndexOf(' ') + 1),
            }).ToListAsync(cancellationToken);

            return Ok(academicYears);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAcademicYearById(int id,CancellationToken cancellationToken = default)
        {
            var year = await db.AcademicYears.Where(x => x.ID == id).Select(x => new AcademicYearResponse
            {
                Id = x.ID,
                Description = x.Description.Substring(x.Description.LastIndexOf(' ') + 1),
            }).ToListAsync(cancellationToken);

            return Ok(year);
        }
    }

    public class AcademicYearResponse
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Description { get; set; }
    }
}
