using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2019_11_04.EF;
using RS1_2019_11_04.ViewModels;

namespace RS1_2019_11_04.Controllers
{
    public class AjaxStavkeController : Controller
    {
        private readonly MojContext ctx;

        public AjaxStavkeController(MojContext context)
        {
            ctx = context;
        }

        public IActionResult Index(int id)
        {
            var Ispit = ctx.PopravniIspit.Find(id);
            var model = new AjaxStavkeRezultatiVM
            {
                PopravniIspitId = id,
                Rows = ctx.PopravniIspitStavka
                    .Where
                    (
                        i => i.PopravniIspitId == id
                    )
                    .Select
                    (
                        i => new AjaxStavkeRezultatiVM.Row
                        {
                            StavkaPopravniIspitId = i.Id,
                            UcenikImePrezime = i.OdjeljenjeStavka.Ucenik.ImePrezime,
                            BrojUDnevniku = i.OdjeljenjeStavka.BrojUDnevniku,
                            Odjeljenje = i.OdjeljenjeStavka.Odjeljenje.Oznaka,
                            IsPristupio = i.IsPristupio,
                            Bodovi = i.Bodovi
                        }
                    )
                    .ToList()
            };
            return PartialView(model);
        }

        public IActionResult UpdateBodovi(int id, int bodovi)
        {
            var Stavka = ctx.PopravniIspitStavka.Find(id);
            Stavka.Bodovi = bodovi;
            ctx.SaveChanges();
            return Redirect("/PopravniIspit/Uredi/" + Stavka.PopravniIspitId);
        }

        public IActionResult Uredi(int id)
        {
            var Stavka = ctx.PopravniIspitStavka
                .Include(i => i.OdjeljenjeStavka)
                .Include(i => i.OdjeljenjeStavka.Ucenik)
                .Where(i => i.Id == id)
                .SingleOrDefault();

            var model = new AjaxStavkeUrediVM
            {
                PopravniIspitStavkaId = id,
                ImePrezime = Stavka.OdjeljenjeStavka.Ucenik.ImePrezime,
                Bodovi = Stavka.Bodovi
            };
            return PartialView(model);
        }

        public IActionResult Snimi(AjaxStavkeUrediVM model)
        {
            var Stavka = ctx.PopravniIspitStavka.Find(model.PopravniIspitStavkaId);
            Stavka.Bodovi = model.Bodovi;
            ctx.SaveChanges();
            return Redirect("/AjaxStavke/Index/" + Stavka.PopravniIspitId);
        }
        
    }
}