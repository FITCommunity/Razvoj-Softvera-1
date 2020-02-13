using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RS1_2019_01_21.EF;
using RS1_2019_01_21.EntityModels;
using RS1_2019_01_21.ViewModels;

namespace RS1_2019_01_21.Controllers
{
    public class OdrzanaNastavaController : Controller
    {
        private readonly MojContext ctx;

        public OdrzanaNastavaController(MojContext context) => ctx = context;

        public IActionResult Index()
        {
            var model = new OdrzanaNastavaIndexVM
            {
                Rows = ctx.Nastavnik
                    .Select
                    (
                        i => new OdrzanaNastavaIndexVM.Row
                        {
                            NastavnikId = i.Id,
                            Nastavnik = i.Ime + " " + i.Prezime,
                            Skola = ctx.PredajePredmet.Where(j => j.Id == i.Id).Select(j => j.Odjeljenje.Skola.Naziv).FirstOrDefault()
                        }
                    )
                    .ToList()
            };
            return View(model);
        }

        public IActionResult Odaberi(int id)
        {
            var Nastavnik = ctx.Nastavnik.Find(id);
            var model = new OdrzanaNastavaOdaberiVM
            {
                NastavnikId = id,
                Nastavnik = Nastavnik.Ime + " " + Nastavnik.Prezime,
                Rows = ctx.MaturskiIspit
                    .Where(i => i.NastavnikId == id)
                    .Select
                    (
                        i => new OdrzanaNastavaOdaberiVM.Row
                        {
                            MaturskiIspitId = i.Id,
                            Datum = i.Datum.ToString("dd/MM/yyyy"),
                            Predmet = i.Predmet.Naziv,
                            Skola = i.Skola.Naziv,
                            Ucenici = ctx.MaturskiIspitStavka
                                .Where
                                (
                                    j => j.MaturskiIspitId == i.Id &&
                                         j.IsPristupio == false
                                )
                                .Select
                                (
                                    j => j.OdjeljenjeStavka.Ucenik.ImePrezime
                                )
                                .ToList()
                        }
                    )
                    .ToList()
            };
            return View(model);
        }

        public IActionResult Dodaj(int id)
        {
            var Nastavnik = ctx.Nastavnik.Find(id);
            var model = new OdrzanaNastavaDodajVM
            {
                NastavnikId = id,
                Nastavnik = Nastavnik.Ime + " " + Nastavnik.Prezime,
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
                SkolskaGodina = ctx.SkolskaGodina.Where(i => i.Aktuelna == true).Select(i => i.Naziv).FirstOrDefault(),
                SkolskaGodinaId = ctx.SkolskaGodina.Where(i => i.Aktuelna == true).Select(i => i.Id).FirstOrDefault(),
                Predmeti = ctx.PredajePredmet
                    .Where(i => i.NastavnikID == id)
                    .Select
                    (
                        i => new SelectListItem
                        {
                            Value = i.Predmet.Id.ToString(),
                            Text = i.Predmet.Naziv
                        }
                    )
                    .ToList()
            };
            return View(model);
        }

        public IActionResult Snimi(OdrzanaNastavaDodajVM model)
        {
            var Ispit = new MaturskiIspit
            {
                Datum = model.Datum,
                PredmetId = model.PredmetId,
                SkolaId = model.SkolaId,
                NastavnikId = model.NastavnikId,
                SkolskaGodinaId = model.SkolskaGodinaId
            };
            ctx.Add(Ispit);
            ctx.SaveChanges();

            var Ucenici = ctx.OdjeljenjeStavka
                .Where
                (
                    i => i.Odjeljenje.Razred == 4 &&
                         i.Odjeljenje.Skola.Id == model.SkolaId
                )
                .ToList();
            var Polozili = ctx.MaturskiIspitStavka
                .Where(i => i.Bodovi > 55)
                .Select(i => i.Id)
                .ToList();

            foreach(var Ucenik in Ucenici)
            {
                bool flag = ctx.DodjeljenPredmet
                    .Where
                    (
                        i => i.OdjeljenjeStavkaId == Ucenik.Id &&
                             i.PredmetId == model.PredmetId &&
                             i.ZakljucnoKrajGodine != 1 &&
                             !Polozili.Contains(i.OdjeljenjeStavkaId)
                    )
                    .Any();

                if(flag)
                {
                    var Stavka = new MaturskiIspitStavka
                    {
                        MaturskiIspitId = Ispit.Id,
                        OdjeljenjeStavkaId = Ucenik.OdjeljenjeId,
                        IsPristupio = false,
                        Bodovi = 0
                    };

                    ctx.Add(Stavka);
                }
            }

            ctx.SaveChanges();
            return Redirect("Odaberi/" + model.NastavnikId);
        }

        public IActionResult Uredi(int id)
        {
            var Ispit = ctx.MaturskiIspit
                .Include(i => i.Predmet)
                .Where(i => i.Id == id)
                .SingleOrDefault();

            var model = new OdrzanaNastavaUrediVM
            {
                MaturskiIspitId = id,
                Datum = Ispit.Datum.ToString("dd/MM/yyyy"),
                Predmet = Ispit.Predmet.Naziv,
                Napomena = Ispit.Napomena
            };
            return View(model);
        }

        public IActionResult ToggleIsPristupio(int id)
        {
            var Stavka = ctx.MaturskiIspitStavka.Find(id);
            Stavka.IsPristupio = !Stavka.IsPristupio;
            ctx.SaveChanges();
            return Redirect("/OdrzanaNastava/Uredi/" + Stavka.MaturskiIspitId);
        }

        public IActionResult UcenikJePristupio(int id) => ToggleIsPristupio(id);
        public IActionResult UcenikNijePristupio(int id) => ToggleIsPristupio(id);
    }
}