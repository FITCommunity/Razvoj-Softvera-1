using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2019_12_02.EF;
using RS1_2019_12_02.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using RS1_2019_12_02.EntityModels;

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
            var model = new PopravniIspitiIndexVM()
            {
                Rows = ctx.Odjeljenje
                    .Select
                    (
                        i => new PopravniIspitiIndexVM.Row
                        {
                            OdjeljenjeId = i.Id,
                            Skola = i.Skola.Naziv,
                            SkolskaGodina = i.SkolskaGodina.Naziv,
                            Odjeljenje = i.Oznaka
                        }
                    )
                    .ToList()
            };
            return View(model);
        }

        public IActionResult Detalji(int Id)
        {
            var odjeljenje = ctx.Odjeljenje
                .Include(i => i.Skola)
                .Include(i => i.SkolskaGodina)
                .Where(i => i.Id == Id)
                .SingleOrDefault();

            PopravniIspitDetaljiVM model = new PopravniIspitDetaljiVM
            {
                OdjeljenjeId = Id,
                Odjeljenje = odjeljenje.Oznaka,
                Skola = odjeljenje.Skola.Naziv,
                SkolskaGodina = odjeljenje.SkolskaGodina.Naziv,
                Rows = ctx.PopravniIspit
                    .Where(i => i.OdjeljenjeId == Id)
                    .Select
                    (
                        i => new PopravniIspitDetaljiVM.Row
                        {
                            PopravniIspitId = i.Id,
                            DatumPopravnog = i.DatumPopravnog,
                            Predmet = i.Predmet.Naziv,
                            BrojUcenikaNaPopravnom = ctx.PopravniIspitStavke.Where(s => s.PopravniIspitId == i.Id).Count(),
                            BrojUcenikaPolozili = ctx.PopravniIspitStavke.Where(s => s.PopravniIspitId == i.Id && s.Bodovi > 49).Count()
                        }
                    )
                    .ToList()
            };
            return View(model);
        }

        public IActionResult Dodaj(int Id)
        {
            var odjeljenje = ctx.Odjeljenje
                .Include(i => i.Skola)
                .Include(i => i.SkolskaGodina)
                .Where(i => i.Id == Id)
                .SingleOrDefault();

            var model = new PopravniIspitDodajVM
            {
                OdjeljenjeId = odjeljenje.Id,
                Odjeljenje = odjeljenje.Oznaka,
                SkolaId = odjeljenje.SkolaID,
                Skola = odjeljenje.Skola.Naziv,
                SkolskaGodinaId = odjeljenje.SkolskaGodinaID,
                SkolskaGodina = odjeljenje.SkolskaGodina.Naziv,
                Predmeti = ctx.PredajePredmet
                    .Where(i => i.OdjeljenjeID == Id)
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

        public IActionResult Uredi(int Id)
        {
            var ispit = ctx.PopravniIspit
                .Include(i => i.Odjeljenje.Skola)
                .Include(i => i.Odjeljenje.SkolskaGodina)
                .Include(i => i.Predmet)
                .Where(i => i.Id == Id)
                .SingleOrDefault();

            var model = new PopravniIspitDodajVM
            {
                PopravniIspitId = ispit.Id,
                DatumPopravnog = ispit.DatumPopravnog,
                OdjeljenjeId = ispit.OdjeljenjeId,
                Odjeljenje = ispit.Odjeljenje.Oznaka,
                SkolaId = ispit.Odjeljenje.SkolaID,
                Skola = ispit.Odjeljenje.Skola.Naziv,
                SkolskaGodinaId = ispit.Odjeljenje.SkolskaGodinaID,
                SkolskaGodina = ispit.Odjeljenje.SkolskaGodina.Naziv,
                PredmetId = ispit.PredmetId,
                Predmeti = ctx.PredajePredmet
                    .Where(i => i.OdjeljenjeID == Id)
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

        public IActionResult Snimi(PopravniIspitDodajVM model)
        {
            PopravniIspit ispit;
            if(model.PopravniIspitId == 0)
            {
                ispit = new PopravniIspit();
                ctx.Add(ispit);
            }
            else
            {
                ispit = ctx.PopravniIspit
                    .Where(i => i.Id == model.PopravniIspitId)
                    .SingleOrDefault();
            }

            ispit.OdjeljenjeId = model.OdjeljenjeId;
            ispit.PredmetId = model.PredmetId;
            ispit.DatumPopravnog = model.DatumPopravnog;

            if(ispit != null)
            {
                var uceniciOdjeljenja = ctx.OdjeljenjeStavka
                    .Where(i => i.OdjeljenjeId == model.OdjeljenjeId)
                    .ToList();

                foreach(var ucenik in uceniciOdjeljenja)
                {
                    var uceniciNegativnaOcjenaJedanPredmet = ctx.DodjeljenPredmet
                        .Where
                        (
                            i => i.OdjeljenjeStavkaId == ucenik.Id && 
                                 i.ZakljucnoKrajGodine == 1 && 
                                 i.PredmetId == model.PredmetId
                        );
                    var uceniciNegativnaOcjenaVisePredmeta = ctx.DodjeljenPredmet
                        .Where
                        (
                            i => i.OdjeljenjeStavkaId == ucenik.Id &&
                                 i.ZakljucnoKrajGodine == 1
                        );

                    if(uceniciNegativnaOcjenaVisePredmeta.Count() >= 3)
                    {
                        var stavka = new PopravniIspitStavke
                        {
                            OdjeljenjeStavkaId = ucenik.Id,
                            PopravniIspitId = model.PopravniIspitId,
                            Pristupio = false,
                            Bodovi = 0
                        };
                        ctx.Add(stavka);
                    }
                    else if(uceniciNegativnaOcjenaJedanPredmet.Any())
                    {
                        var stavka = new PopravniIspitStavke
                        {
                            OdjeljenjeStavkaId = ucenik.Id,
                            PopravniIspitId = model.PopravniIspitId,
                            Pristupio = false,
                            Bodovi = null
                        };
                        ctx.Add(stavka);
                    }

                }
            }

            ctx.SaveChanges();
            ctx.Dispose();
            return Redirect("/PopravniIspit/Detalji?Id=" + model.OdjeljenjeId);
        }

    }
}