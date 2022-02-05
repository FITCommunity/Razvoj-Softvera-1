using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RS1_2020_01_30.EF;
using RS1_2020_01_30.EntityModels;
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
            var Takmicenje = ctx.Takmicenje.Find(id);
            var model = new RezultatiAjaxIndexVM
            {
                TakmicenjeId = id,
                IsZakljucano = Takmicenje.IsZakljucano,
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
            var Takmicar = ctx.TakmicenjeUcesnik.Find(id);
            var model = new AjaxRezultatiUrediDodajVM
            {
                TakmicenjeId = Takmicar.TakmicenjeId,
                UcesnikId = id,
                Ucesnici = ctx.TakmicenjeUcesnik
                    .Where(i => i.Id == id)
                    .Select
                    (
                        i => new SelectListItem
                        {
                            Value = i.Id.ToString(),
                            Text = i.OdjeljenjeStavka.Odjeljenje.Oznaka + " " + i.OdjeljenjeStavka.Ucenik.ImePrezime
                        }
                    )
                    .ToList(),
                Bodovi = ctx.TakmicenjeUcesnik
                    .Where(i => i.Id == id)
                    .Select(i => i.Bodovi)
                    .SingleOrDefault()
            };
            return PartialView("UrediDodaj", model);
        }

        public IActionResult Dodaj(int id)
        {
            var Takmicenje = ctx.Takmicenje.Find(id);

            var Takmicari = ctx.TakmicenjeUcesnik
                .Where(i => i.TakmicenjeId == id)
                .Select(i => i.OdjeljenjeStavkaId)
                .ToList();

            var model = new AjaxRezultatiUrediDodajVM
            {
                TakmicenjeId = id,
                Bodovi = 0,
                Ucesnici = ctx.OdjeljenjeStavka
                    .Where
                    (
                        i => i.Odjeljenje.Razred == Takmicenje.Razred &&
                             Takmicari.Contains(i.Id) == false
                    )
                    .Select
                    (
                        i => new SelectListItem
                        {
                            Value = i.Id.ToString(),
                            Text = i.Odjeljenje.Oznaka + " " + i.Ucenik.ImePrezime
                        }
                    )
                    .ToList()
            };
            return PartialView("UrediDodaj", model);
        }

        public IActionResult Snimi(AjaxRezultatiUrediDodajVM model)
        {
            var TakmicenjeUcesnik = ctx.TakmicenjeUcesnik
                .Where(i => i.Id == model.UcesnikId)
                .SingleOrDefault();

            if (TakmicenjeUcesnik != null)
            {
                TakmicenjeUcesnik.Bodovi = model.Bodovi <= 100 ? model.Bodovi : 100;
                ctx.SaveChanges();
            }
            else if(model.UcesnikId != 0)
            {
                var noviUcesnik = new TakmicenjeUcesnik
                {
                    TakmicenjeId = model.TakmicenjeId,
                    Bodovi = model.Bodovi <= 100 ? model.Bodovi : 100,
                    IsPristupio = true,
                    OdjeljenjeStavkaId = model.UcesnikId
                };
                ctx.Add(noviUcesnik);
                ctx.SaveChanges();
                
            }
            return Redirect("/Takmicenje/Rezultati/" + model.TakmicenjeId);
        }
    }
}