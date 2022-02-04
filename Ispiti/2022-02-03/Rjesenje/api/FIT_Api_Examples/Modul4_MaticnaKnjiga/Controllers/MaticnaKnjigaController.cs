using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.Modul2.Models;
using FIT_Api_Examples.Modul3.Models;
using FIT_Api_Examples.Modul4_MaticnaKnjiga.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIT_Api_Examples.Modul4_MaticnaKnjiga.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class MaticnaKnjigaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public MaticnaKnjigaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public class MaticnaKnjigaGetVM
        {
            public int id { get; set; }
            public string ime { get; set; }
            public string prezime { get; set; }
            public string broj_indeksa { get; set; }
            public List<UpisUAkGodinu> upisaneAkGodine { get; set; }
            public List<CmbStavke> cmbStavkeAkademskeGodine { get; set; }
        }

        [HttpGet("{id}")]
        public ActionResult<MaticnaKnjigaGetVM> GetByStudent(int id)
        {

            if (!HttpContext.GetLoginInfo().isLogiran)
               return BadRequest("Not logged in!");

            var student = _dbContext.Student.FirstOrDefault(x => x.id == id);
            if (student == null) return BadRequest("No such student!");

            var maticna = new MaticnaKnjigaGetVM
            {
                id = student.id,
                ime = student.ime,
                prezime = student.prezime,
                broj_indeksa = student.broj_indeksa,
                upisaneAkGodine = _dbContext.UpisUAkGodinu.Include(x => x.evidentiraoKorisnik)
                .Include(x => x.akademskaGodina).Where(x => x.studentId == student.id).ToList(),
                cmbStavkeAkademskeGodine = _dbContext.AkademskaGodina.Select(x => new CmbStavke { id = x.id, opis = x.opis }).ToList()
            };

            return maticna;
        }



        public class MaticnaKnjigaAkGodinuZimskiUpisVM
        {
            public DateTime datum { get; set; }
            public int studentId { get; set; }
            public int godinaStudija { get; set; }
            public int akademskaGodinaId { get; set; }
            public float? cijenaSkolarine { get; set; }
            public bool obnovaGodine { get; set; }
        }

        [HttpPost]
        public IActionResult UpisiZimski(MaticnaKnjigaAkGodinuZimskiUpisVM maticnaKnjiga)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("Not logged in!");

            var upis = new UpisUAkGodinu
            {
                datum1_ZimskiUpis = maticnaKnjiga.datum,
                studentId = maticnaKnjiga.studentId,
                godinaStudija = maticnaKnjiga.godinaStudija,
                akademskaGodinaId = maticnaKnjiga.akademskaGodinaId,
                obnovaGodine = maticnaKnjiga.obnovaGodine,
                cijenaSkolarine = maticnaKnjiga.cijenaSkolarine,
                evidentiraoKorisnikId = HttpContext.GetLoginInfo().autentifikacijaToken.id
            };

            if (_dbContext.UpisUAkGodinu.Any(x => x.godinaStudija == upis.godinaStudija && x.studentId == upis.studentId) &&
                upis.obnovaGodine == false)
                return BadRequest("Ne mozete dva puta istu godinu bez obnove!");

            _dbContext.UpisUAkGodinu.Add(upis);
            return Ok(_dbContext.SaveChanges());
        }

        [HttpPost("{id}")]
        public IActionResult OvjeriZimski(int id)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("Not logged in!");

            var x = _dbContext.UpisUAkGodinu.FirstOrDefault(x => x.id == id);

            

            x.datum2_ZimskiOvjera = DateTime.Now;

            _dbContext.UpisUAkGodinu.Update(x);
            return Ok(_dbContext.SaveChanges());
        }

    }
}
