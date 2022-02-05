using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using RS1_2019_12_02.EF;
using RS1_2019_12_02.EntityModels;
using RS1_2019_12_02.ViewModels;

namespace RS1_2019_12_02.Controllers
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
            var model = new OdjeljenjaVM
            {
                Rows = ctx.Odjeljenje
                    .OrderBy(i => i.SkolskaGodina.Naziv)
                    .ThenBy(i => i.Skola.Naziv)
                    .ThenBy(i => i.Oznaka)
                    .Select
                    (
                        i => new OdjeljenjaVM.Row
                        {
                            OdjeljenjeId = i.Id,
                            Odjeljenje = i.Oznaka,
                            Skola = i.Skola.Naziv,
                            SkolskaGodina = i.SkolskaGodina.Naziv
                        }
                    )
                    .ToList()
                    
            };
            return View(model);
        }

        public IActionResult PopravniPrikaz(int Id)
        {
            var Odjeljenje = ctx.Odjeljenje
                .Include(i => i.Skola)
                .Include(i => i.SkolskaGodina)
                .Where(i => i.Id == Id)
                .SingleOrDefault();
            
            var model = new PopravniIspitiVM
            {
                OdjeljenjeId = Id,
                Odjeljenje = Odjeljenje.Oznaka,
                Skola = Odjeljenje.Skola.Naziv,
                SkolskaGodina = Odjeljenje.SkolskaGodina.Naziv,
                Rows = ctx.PopravniIspit
                    .Where(i => i.OdjeljenjeId == Id)
                    .Select
                    (
                        i => new PopravniIspitiVM.Row
                        {
                            PopravniIspitId = i.Id,
                            Datum = i.Datum,
                            Predmet = i.Predmet.Naziv,
                            BrojUcenikaNaPopravnom = ctx.PopravniIspitStavka.Where(j => j.PopravniId == i.Id).Count(),
                            BrojUcenikaKojiSuPolozili = ctx.PopravniIspitStavka.Where(j => j.PopravniId == i.Id && j.Bodovi >= 50).Count()
                        }
                    )
                    .ToList()
            };
            return View(model);
        }

        public IActionResult Dodaj(int Id)
        {
            var Odjeljenje = ctx.Odjeljenje
                .Include(i => i.Skola)
                .Include(i => i.SkolskaGodina)
                .Where(i => i.Id == Id)
                .SingleOrDefault();

            var model = new PopravniIspitDodajVM
            {
                OdjeljenjeId = Id,
                Odjeljenje = Odjeljenje.Oznaka,
                SkolaId = Odjeljenje.Skola.Id,
                Skola = Odjeljenje.Skola.Naziv,
                SkolskaGodinaId = Odjeljenje.SkolskaGodina.Id,
                SkolskaGodina = Odjeljenje.SkolskaGodina.Naziv,
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

        public IActionResult Snimi(PopravniIspitDodajVM model)
        {
            var PopravniIspit = new PopravniIspit
            {
                OdjeljenjeId = model.OdjeljenjeId,
                PredmetId = model.PredmetId,
                Datum = model.Datum
            };

            ctx.Add(PopravniIspit);
            ctx.SaveChanges();

            var Ucenici = ctx.OdjeljenjeStavka
                .Where(i => i.OdjeljenjeId == model.OdjeljenjeId)
                .ToList();

            foreach(var Ucenik in Ucenici)
            {
                var negativnaOcjenaPredmet = ctx.DodjeljenPredmet
                    .Where
                    (
                        i => i.OdjeljenjeStavkaId == Ucenik.Id &&
                             i.Predmet.Id == model.PredmetId &&
                             i.ZakljucnoKrajGodine == 1
                    );
                var triPlusNegativneOcjene = ctx.DodjeljenPredmet
                    .Where
                    (
                        i => i.OdjeljenjeStavkaId == Ucenik.Id &&
                             i.ZakljucnoKrajGodine == 1
                    );

                if(triPlusNegativneOcjene.Count() >= 3)
                {
                    var novaStavka = new PopravniIspitStavka
                    {
                        PopravniId = PopravniIspit.Id,
                        Pristupio = false,
                        Bodovi = null,
                        OdjeljenjeStavkaId = Ucenik.Id
                    };
                    ctx.Add(novaStavka);
                }
                else if(negativnaOcjenaPredmet.Any())
                {
                    var novaStavka = new PopravniIspitStavka
                    {
                        PopravniId = PopravniIspit.Id,
                        Pristupio = false,
                        Bodovi = 0,
                        OdjeljenjeStavkaId = Ucenik.Id
                    };
                    ctx.Add(novaStavka);
                }
            }

            ctx.SaveChanges();
            return Redirect("/PopravniIspit/PopravniPrikaz/" + model.OdjeljenjeId);
        }

        public IActionResult Uredi(int Id)
        {
            var PopravniIspit = ctx.PopravniIspit
                .Include(i => i.Predmet)
                .Include(i => i.Odjeljenje)
                .Include(i => i.Odjeljenje.Skola)
                .Include(i => i.Odjeljenje.SkolskaGodina)
                .Where(i => i.Id == Id)
                .SingleOrDefault();

            var model = new PopravniIspitDodajVM
            {
                PopravniIspitId = Id,
                OdjeljenjeId = PopravniIspit.Odjeljenje.Id,
                Odjeljenje = PopravniIspit.Odjeljenje.Oznaka,
                SkolaId = PopravniIspit.Odjeljenje.Skola.Id,
                Skola = PopravniIspit.Odjeljenje.Skola.Naziv,
                SkolskaGodinaId = PopravniIspit.Odjeljenje.SkolskaGodina.Id,
                SkolskaGodina = PopravniIspit.Odjeljenje.SkolskaGodina.Naziv,
                PredmetId = PopravniIspit.Predmet.Id,
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
    }
}