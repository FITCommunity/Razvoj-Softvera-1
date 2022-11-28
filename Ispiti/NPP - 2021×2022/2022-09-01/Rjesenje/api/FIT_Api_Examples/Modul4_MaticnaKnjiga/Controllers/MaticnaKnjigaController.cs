using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.Modul4_MaticnaKnjiga.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

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
            _dbContext = dbContext;
        }

        public class MaticnaKnjigaVM
        {
            public int id { get; set; }
            public string ime { get; set; }
            public string prezime { get; set; }
            public List<UpisUAkGodinu> godinaStudija { get; set; }
            public List<CmbStavke> akGodina { get; set; }
        }

        [HttpGet]
        public ActionResult<MaticnaKnjigaVM> GetMaticna(int id)
        {
            //if (!HttpContext.GetLoginInfo().isLogiran)
            //    return BadRequest("Niste logirani.");

            var student = _dbContext.Student.Find(id);

            if (student == null)
                return BadRequest("Student ne postoji!");

            var maticna = new MaticnaKnjigaVM()
            {
                id = student.id,
                ime = student.ime,
                prezime = student.prezime,
                akGodina = _dbContext.AkademskaGodina.Select(x => new CmbStavke() { id = x.id, opis = x.opis }).ToList(),
                godinaStudija = _dbContext.UpisUAkGodinu.Include(x => x.student)
                                                        .Include(x => x.akademskaGodina)
                                                        .Include(x => x.evidentiraoKorisnik)
                                                        .ToList()
            };

            return Ok(maticna);
        }

        public class ZimskiSemestar
        {
            public DateTime datum { get; set; }
            public int godinaStudija { get; set; }
            public int akGodina { get; set; }
            public float cijenaSkolarine { get; set; }
            public bool obnova { get; set; }
        }

        [HttpGet]
        public ActionResult GetGodine()
        {
            var godine = _dbContext.AkademskaGodina.Select(x => new CmbStavke() { id = x.id, opis = x.opis }).ToList();

            return Ok(godine);
        }

        [HttpPost("{id}")]
        public ActionResult UpisiZimski(int id, [FromBody] ZimskiSemestar semestar)
        {
            //if (!HttpContext.GetLoginInfo().isLogiran)
            //    return BadRequest("Niste logirani.");

            var student = _dbContext.Student.Find(id);

            if (student == null)
                return BadRequest("Student ne postoji");

            var novi = new UpisUAkGodinu();
            _dbContext.Add(novi);

            novi.studentId = student.id;
            novi.evidentiraoKorisnik = HttpContext.GetLoginInfo()?.korisnickiNalog;
            novi.evidentiraoKorisnikId = 1;
            novi.datum1_ZimskiUpis = semestar.datum;
            novi.obnovaGodine = semestar.obnova;
            novi.cijenaSkolarine = semestar.cijenaSkolarine;
            novi.akademskaGodinaId = semestar.akGodina;
            novi.godinaStudija = semestar.godinaStudija;

            _dbContext.SaveChanges();

            return Ok(novi);
        }

        [HttpPost ("{id}")]
        public ActionResult OvjeriZimski(int id)
        {
            //if (!HttpContext.GetLoginInfo().isLogiran)
            //    return BadRequest("Niste logirani.");

            var godina = _dbContext.UpisUAkGodinu.Find(id);

            if (godina == null)
                return BadRequest("Ne postoji zimski semestar za ovjeru");

            godina.datum2_ZimskiOvjera = DateTime.Now;

            _dbContext.SaveChanges();

            return Ok(godina);
        }
    }
}
