using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RS1_2019_12_16.EF;
using RS1_2019_12_16.EntityModels;
using RS1_2019_12_16.ViewModels;

namespace RS1_2019_12_16.Controllers
{
    public class PopravniIspitController : Controller
    {
        private readonly MojContext ctx;

        public PopravniIspitController(MojContext context)
        {
            ctx = context;
        }
        public IActionResult Index()
        {
            var model = new PopravniIspitIndexVM
            {
                Skole = ctx.Skola
                    .Select
                    (
                        i => new SelectListItem
                        {
                            Value = i.Id.ToString(),
                            Text = i.Naziv
                        }
                    )
                    .ToList(),
                SkoleskeGodine = ctx.SkolskaGodina
                    .Select
                    (
                        i => new SelectListItem
                        {
                            Value = i.Id.ToString(),
                            Text = i.Naziv
                        }
                    )
                    .ToList(),
                Predmeti = ctx.Predmet
                    .Select
                    (
                        i => new SelectListItem
                        {
                            Value = i.Id.ToString(),
                            Text = i.Naziv
                        }
                    )
                    .ToList()
            };
            return View(model);
        }

        string GetClanKomisije(int id)
        {
            //var Nastavnik = ctx.Komisija
            //    .Include(i => i.Nastavnik)
            //    .Where(i => i.PopravniIspitId == id)
            //    .FirstOrDefault();

            //return Nastavnik != null ? Nastavnik.Nastavnik.Ime + " " + Nastavnik.Nastavnik.Prezime : "N/A";
            return "N/A";
        }

        public IActionResult Odaberi(PopravniIspitIndexVM model)
        {
            var modelDodaj = new PopravniIspitOdaberiVM
            {
                SkolaId = model.SkolaId,
                Skola = ctx.Skola.Find(model.SkolaId).Naziv,
                SkolskaGodinaId = model.SkolskaGodinaId,
                SkolskaGodina = ctx.SkolskaGodina.Find(model.SkolskaGodinaId).Naziv,
                PredmetId = model.PredmetId,
                Predmet = ctx.Predmet.Find(model.PredmetId).Naziv,
                Rows = ctx.PopravniIspit
                    .Where
                    (
                        i => i.SkolaId == model.SkolaId &&
                             i.SkolskaGodinaId == model.SkolskaGodinaId &&
                             i.PredmetId == model.PredmetId
                    )
                    .Select
                    (
                        i => new PopravniIspitOdaberiVM.Row
                        {
                            PopravniIspitId = i.Id,
                            Datum = i.Datum.ToString("dd/MM/yyyy"),
                            BrojUcenikaNaIspitu = ctx.PopravniIspitStavka.Where(j => j.Id == i.Id).Count(),
                            BrojUcenikaPolozili = ctx.PopravniIspitStavka.Where(j => j.Id == i.Id && j.Bodovi > 50).Count()

                        }
                    )
                    .ToList()
            };

            foreach (var Ispit in modelDodaj.Rows)
                Ispit.ClanKomisije1 = GetClanKomisije(Ispit.PopravniIspitId);
            return View(modelDodaj);
        }

        public IActionResult Dodaj(int skolaId, int skolskaGodinaId, int predmetId)
        {

            var model = new PopravniIspitDodajVM
            {
                SkolaId = skolaId,
                Skola = ctx.Skola.Find(skolaId).Naziv,
                SkolskaGodinaId = skolskaGodinaId,
                SkolskaGodina = ctx.SkolskaGodina.Find(skolskaGodinaId).Naziv,
                PredmetId = predmetId,
                Predmet = ctx.Predmet.Find(predmetId).Naziv,
                Nastavnici = ctx.Nastavnik
                    .Select
                    (
                        i => new SelectListItem
                        {
                            Value = i.Id.ToString(),
                            Text = i.Ime + " " + i.Prezime
                        }
                    )
                    .ToList()
            };
            return View(model);
        }

        public IActionResult Snimi(PopravniIspitDodajVM model)
        {
            var Ispit = new PopravniIspit
            {
                SkolaId = model.SkolaId,
                SkolskaGodinaId = model.SkolskaGodinaId,
                PredmetId = model.PredmetId,
                Datum = model.Datum
            };

            ctx.Add(Ispit);
            ctx.SaveChanges();

            try
            {
                var Clan1 = new Komisija
                {
                    NastanikId = model.ClanKomisije1Id,
                    PopravniIspitId = Ispit.Id
                };
                ctx.Add(Clan1);
                ctx.SaveChanges();
            }
            catch
            {

            }

            try
            {
                var Clan2 = new Komisija
                {
                    NastanikId = model.ClanKomisije2Id,
                    PopravniIspitId = Ispit.Id
                };
                ctx.Add(Clan2);
                ctx.SaveChanges();
            }
            catch
            {

            }

            try
            {
                var Clan3 = new Komisija
                {
                    NastanikId = model.ClanKomisije3Id,
                    PopravniIspitId = Ispit.Id
                };
                ctx.Add(Clan3);
                ctx.SaveChanges();
            }
            catch
            {

            }

            return Redirect("/PopravniIspit/Index");
        }
    }
}