using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2019_06_25.EF;
using RS1_2019_06_25.EntityModels;
using RS1_2019_06_25.ViewModels;

namespace RS1_2019_06_25.Controllers
{
    public class IspitController : Controller
    {
        private readonly MojContext ctx;

        public IspitController(MojContext context)
        {
            ctx = context;
        }

        public IActionResult Odaberi(int id)
        {
            var Angazovan = ctx.Angazovan
                .Include(i => i.Predmet)
                .Include(i => i.AkademskaGodina)
                .Include(i => i.Nastavnik)
                .Where(i => i.Id == id)
                .SingleOrDefault();

            var model = new IspitOdaberiVM
            {
                AngazovanId = id,
                Predmet = Angazovan.Predmet.Naziv,
                AkademskaGodina = Angazovan.AkademskaGodina.Opis,
                Nastavnik = Angazovan.Nastavnik.Ime + " " + Angazovan.Nastavnik.Prezime,
                Rows = ctx.Ispit
                    .Select
                    (
                        i => new IspitOdaberiVM.Row
                        {
                            IspitId = i.Id,
                            Datum = i.Datum.ToString("dd/MM/yyyy"),
                            BrojPrijavljenihStudenata = ctx.IspitStavka.Where(j => j.IspitId == i.Id).Count(),
                            BrojStudenataKojiNisuPolozili = ctx.IspitStavka.Where(j => j.IspitId == i.Id && j.Ocjena < 6).Count(),
                            Zakljuceno = i.Zakljucano
                        }
                    )
                    .ToList()
            };
            return View(model);
        }

        public IActionResult Dodaj(int id)
        {
            var Angazovan = ctx.Angazovan
                .Include(i => i.Predmet)
                .Include(i => i.AkademskaGodina)
                .Include(i => i.Nastavnik)
                .Where(i => i.Id == id)
                .SingleOrDefault();

            var model = new IspitDodajVM
            {
                AngazovanId = id,
                Predmet = Angazovan.Predmet.Naziv,
                AkademskaGodina = Angazovan.AkademskaGodina.Opis,
                Nastavnik = Angazovan.Nastavnik.Ime + " " + Angazovan.Nastavnik.Prezime
            };
            return View(model);
        }

        public IActionResult Snimi(IspitDodajVM model)
        {
            var Ispit = new Ispit
            {
                AngazovanId = model.AngazovanId,
                Datum = model.Datum,
                Napomena = model.Opis,
                Zakljucano = false
            };
            ctx.Add(Ispit);
            ctx.SaveChanges();

            var Studenti = ctx.SlusaPredmet
                .Include(i => i.UpisGodine.Student)
                .Where(i => i.AngazovanId == model.AngazovanId)
                .ToList();

            foreach(var Student in Studenti)
            {
                var Stavka = new IspitStavka
                {
                    IspitId = Ispit.Id,
                    StudentId = Student.UpisGodine.Student.Id,
                    Ocjena = 5,
                    IsPristupio = false
                };

                ctx.Add(Stavka);
                ctx.SaveChanges();
            }

            
            return Redirect("/Ispit/Odaberi/" + model.AngazovanId);
        }

        public IActionResult Zakljucaj(int id)
        {
            var Ispit = ctx.Ispit
                .Include(i => i.Angazovan)
                .Where(i => i.Id == id)
                .SingleOrDefault();

            Ispit.Zakljucano = true;
            ctx.SaveChanges();
            return Redirect("/Ispit/Odaberi/" + Ispit.AngazovanId);
        }

        public IActionResult Detalji(int id)
        {

            var Ispit = ctx.Ispit
                .Include(i => i.Angazovan)
                .Include(i => i.Angazovan.Predmet)
                .Include(i => i.Angazovan.Nastavnik)
                .Include(i => i.Angazovan.AkademskaGodina)
                .Where(i => i.Id == id)
                .SingleOrDefault();

            var model = new IspitDetaljiVM
            {
                IspitId = id,
                PredmetId = Ispit.Angazovan.Predmet.Id,
                Predmet = Ispit.Angazovan.Predmet.Naziv,
                Nastavnik = Ispit.Angazovan.Nastavnik.Ime + " " + Ispit.Angazovan.Nastavnik.Prezime,
                AkademskaGodina = Ispit.Angazovan.AkademskaGodina.Opis,
                Datum = Ispit.Datum,
                Opis = Ispit.Napomena
            };
            return View(model);
        }

        public IActionResult TogglePrisustvo(int id)
        {
            var Stavka = ctx.IspitStavka.Find(id);
            Stavka.IsPristupio = !Stavka.IsPristupio;
            ctx.SaveChanges();
            return Redirect("/Ispit/Detalji/" + Stavka.IspitId);
        }
    }
}