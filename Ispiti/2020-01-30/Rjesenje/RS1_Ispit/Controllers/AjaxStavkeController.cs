using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RS1_2020_01_30.EF;
using RS1_2020_01_30.ViewModels;

namespace RS1_2020_01_30.Controllers
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
            var model = new RezultatiAjaxIndexVM
            {
                TakmicenjeId = id,
                Rows = ctx.TakmicenjeUcesnik
                    .Where
                    (
                        i => i.TakmicenjeId == id
                    )
                    .Select
                    (
                        i => new RezultatiAjaxIndexVM.Row
                        {
                            TakmicenjeUcesnikId = i.Id,
                            UcenikId = i.OdjeljenjeStavka.Ucenik.Id,
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

        public IActionResult UpdateBodovi(int id, int bodovi)
        {
            var TakmicenjeUcesnik = ctx.TakmicenjeUcesnik.Find(id);
            TakmicenjeUcesnik.Bodovi = bodovi;
            ctx.SaveChanges();
            return Redirect("/Takmicenje/Rezultati/" + TakmicenjeUcesnik.TakmicenjeId);
        }

        public IActionResult Uredi(int id)
        {

            return View();
        }
    }
}