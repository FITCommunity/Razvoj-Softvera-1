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
                    .OrderBy(i => i.Id)
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
            var Komisija = ctx.Komisija
                .Include(i => i.Nastavnik)
                .Where(i => i.PopravniIspitId == id)
                .FirstOrDefault();

            return Komisija != null ? Komisija.Nastavnik.Ime + " " + Komisija.Nastavnik.Prezime : "N/A";
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
                            BrojUcenikaNaIspitu = ctx.PopravniIspitStavka.Where(j => j.PopravniIspitId == i.Id).Count(),
                            BrojUcenikaPolozili = ctx.PopravniIspitStavka.Where(j => j.PopravniIspitId == i.Id && j.Bodovi > 50).Count()
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
                    NastavnikId = model.ClanKomisije1Id,
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
                    NastavnikId = model.ClanKomisije2Id,
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
                    NastavnikId = model.ClanKomisije3Id,
                    PopravniIspitId = Ispit.Id
                };
                ctx.Add(Clan3);
                ctx.SaveChanges();
            }
            catch
            {

            }

            var Ucenici = ctx.OdjeljenjeStavka
                .Where
                (
                    i => i.Odjeljenje.Skola.Id == model.SkolaId &&
                         i.Odjeljenje.SkolskaGodina.Id == model.SkolskaGodinaId
                )
                .ToList();

            foreach(var Ucenik in Ucenici)
            {
                var NegativnaOcjena = ctx.DodjeljenPredmet
                    .Where
                    (
                        i => i.PredmetId == model.PredmetId &&
                             i.OdjeljenjeStavkaId == Ucenik.Id &&
                             i.ZakljucnoKrajGodine == 1
                    )
                    .ToList();
                    

                var TriNegativneOcjene = ctx.DodjeljenPredmet
                    .Where
                    (
                        i => i.OdjeljenjeStavkaId == Ucenik.Id &&
                             i.ZakljucnoKrajGodine == 1
                    )
                    .ToList();

                if(TriNegativneOcjene.Count() >= 3)
                {
                    var Stavka = new PopravniIspitStavka
                    {
                        OdjeljenjeStavkaId = Ucenik.Id,
                        PopravniIspitId = Ispit.Id,
                        IsPristupio = null,
                        Bodovi = 0
                    };

                    ctx.Add(Stavka);
                    ctx.SaveChanges();
                }
                else if(NegativnaOcjena.Any())
                {
                    var Stavka = new PopravniIspitStavka
                    {
                        OdjeljenjeStavkaId = Ucenik.Id,
                        PopravniIspitId = Ispit.Id,
                        IsPristupio = false
                    };

                    ctx.Add(Stavka);
                    ctx.SaveChanges();
                }
            }

            return Redirect("/PopravniIspit/Index");
        }

        public IActionResult Uredi(int id)
        {
            var Ispit = ctx.PopravniIspit
                .Include(i => i.Skola)
                .Include(i => i.SkolskaGodina)
                .Include(i => i.Predmet)
                .Where(i => i.Id == id)
                .SingleOrDefault();

            var Komisija = ctx.Komisija
                .Include(i => i.Nastavnik)
                .Where(i => i.PopravniIspitId == id)
                .ToList();

            var model = new PopravniIspitUrediVM
            {
                PopravniIspitId = id,
                SkolaId = Ispit.SkolaId,
                Skola = Ispit.Skola.Naziv,
                SkolskaGodinaId = Ispit.SkolskaGodinaId,
                SkolskaGodina = Ispit.SkolskaGodina.Naziv,
                PredmetId = Ispit.PredmetId,
                Predmet = Ispit.Predmet.Naziv,
                Datum = Ispit.Datum.ToString("dd/MM/yyyy")
            };

            
            foreach (var Clan in Komisija)
                model.Komisija.Add(Clan.Nastavnik.Ime + " " + Clan.Nastavnik.Prezime);

            return View(model);
        }

        public IActionResult TogglePrisustvo(int id)
        {
            var Stavka = ctx.PopravniIspitStavka.Find(id);
            Stavka.IsPristupio = !Stavka.IsPristupio;
            ctx.SaveChanges();
            return Redirect("/PopravniIspit/Uredi?id=" + Stavka.PopravniIspitId);
        }

        public IActionResult UcenikJePristupio(int id)
        {
            return TogglePrisustvo(id);
        }

        public IActionResult UcenikNijePristupio(int id)
        {
            return TogglePrisustvo(id);
        }

        public IActionResult DodajUcenika(int id)
        {
            var naIspitu = ctx.PopravniIspitStavka
                .Include(i => i.OdjeljenjeStavka)
                .Where(i => i.PopravniIspitId == id)
                .ToList();

            var model = new PopravniIspitDodajUcenikaVM
            {
                PopravniIspitId = id,
                Ucenici = ctx.OdjeljenjeStavka
                    .Include(i => i.Ucenik)
                    .Where(i => !naIspitu.Any(j => j.OdjeljenjeStavkaId == i.Id))
                    .Select
                    (
                        i => new SelectListItem
                        {
                            Value = i.Id.ToString(),
                            Text = i.Ucenik.ImePrezime
                        }
                    )
                    .ToList()
            };
            return View(model);
        }

        public IActionResult SnimiUcenika(PopravniIspitDodajUcenikaVM model)
        {
            var Stavka = new PopravniIspitStavka
            {
                PopravniIspitId = model.PopravniIspitId,
                OdjeljenjeStavkaId = model.OdjeljenjeStavkaId,
                IsPristupio = false,
                Bodovi = 0
            };
            ctx.Add(Stavka);
            ctx.SaveChanges();
            return Redirect("Uredi/" + Stavka.PopravniIspitId);
        }
    }
}