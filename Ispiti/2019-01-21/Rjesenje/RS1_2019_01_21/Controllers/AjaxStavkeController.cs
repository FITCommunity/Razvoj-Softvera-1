using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2019_01_21.EF;
using RS1_2019_01_21.ViewModels;

namespace RS1_2019_01_21.Controllers
{
    public class AjaxStavkeController : Controller
    {
        private readonly MojContext ctx;

        public AjaxStavkeController(MojContext context) => ctx = context;

        public IActionResult Index(int id)
        {
            var model = new AjaxStavkeIndexVM
            {
                MaturskiIspitId = id,
                Rows = ctx.MaturskiIspitStavka
                    .Include(i => i.OdjeljenjeStavka)
                    .Where
                    (
                        i => i.MaturskiIspitId == id
                    )
                    .Select
                    (
                        i => new AjaxStavkeIndexVM.Row
                        {
                            MaturskiIspitStavkaId = i.Id,
                            Ucenik = i.OdjeljenjeStavka.Ucenik.ImePrezime,
                            Bodovi = i.Bodovi,
                            IsPristupio = i.IsPristupio,
                            Prosjek = ctx.DodjeljenPredmet
                                .Where
                                (
                                    j => j.OdjeljenjeStavkaId == i.OdjeljenjeStavkaId
                                )
                                .Select
                                (
                                    j => j.ZakljucnoKrajGodine
                                )
                                .Average()
                        }
                    )
                    .ToList()
            };
            return PartialView(model);
        }

        public IActionResult Uredi(int id)
        {
            var Stavka = ctx.MaturskiIspitStavka
                .Include(i => i.OdjeljenjeStavka.Ucenik)
                .Where(i => i.Id == id)
                .SingleOrDefault();

            var model = new AjaxStavkeUrediVM
            {
                MaturskiIspitStavkaId = id,
                Ucenik = Stavka.OdjeljenjeStavka.Ucenik.ImePrezime,
                Bodovi = Stavka.Bodovi
            };

            return PartialView(model);
        }

        public IActionResult Snimi(AjaxStavkeUrediVM model)
        {
            var Stavka = ctx.MaturskiIspitStavka.Find(model.MaturskiIspitStavkaId);
            Stavka.Bodovi = model.Bodovi > 100 ? 100 : model.Bodovi < 0 ? 0 : model.Bodovi;
            ctx.SaveChanges();
            return Redirect("/AjaxStavke/Index/" + Stavka.MaturskiIspitId);
        }
    }
}