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
    }
}