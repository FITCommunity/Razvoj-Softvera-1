using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2019_12_16.EF;
using RS1_2019_12_16.ViewModels;

namespace RS1_2019_12_16.Controllers
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
            var model = new AjaxStavkeIndexVM
            {
                PopravniIspitId = id,
                SkolaId = Ispit.SkolaId,
                SkolskaGodinaId = Ispit.SkolskaGodinaId,
                PredmetId = Ispit.PredmetId,
                Rows = ctx.PopravniIspitStavka
                    .Where(i => i.PopravniIspitId == id)
                    .Select
                    (
                        i => new AjaxStavkeIndexVM.Row
                        {
                            PopravniIspitStavkaId = i.Id,
                            Ucenik = i.OdjeljenjeStavka.Ucenik.ImePrezime,
                            Odjeljenje = i.OdjeljenjeStavka.Odjeljenje.Oznaka,
                            BrojUDnevniku = i.OdjeljenjeStavka.BrojUDnevniku,
                            IsPristupio = i.IsPristupio,
                            Bodovi = i.Bodovi
                        }
                    )
                    .ToList()
            };
            return PartialView(model);
        }

        public IActionResult Uredi(int id)
        {
            var Stavka = ctx.PopravniIspitStavka
                .Include(i => i.OdjeljenjeStavka)
                .Include(i => i.OdjeljenjeStavka.Ucenik)
                .Where(i => i.Id == id)
                .SingleOrDefault();

            var model = new AjaxStavkaUrediVM
            {
                PopravniIspitStavkaId = id,
                Ucenik = Stavka.OdjeljenjeStavka.Ucenik.ImePrezime,
                Bodovi = Stavka.Bodovi
            };

            return PartialView(model);
        }

        public IActionResult Snimi(AjaxStavkaUrediVM model)
        {
            var Stavka = ctx.PopravniIspitStavka.Find(model.PopravniIspitStavkaId);
            Stavka.Bodovi = model.Bodovi < 0 ? 0 : model.Bodovi > 100 ? 100 : model.Bodovi;
            ctx.SaveChanges();
            return Redirect("Index/" + Stavka.PopravniIspitId);
        }
    }
}