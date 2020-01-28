using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2019_12_02.EF;
using RS1_2019_12_02.EntityModels;
using RS1_2019_12_02.ViewModels;

namespace RS1_2019_12_02.Controllers
{
    public class AjaxStavkeController : Controller
    {
        private readonly MojContext ctx;

        public AjaxStavkeController(MojContext context)
        {
            ctx = context;
        }
        public IActionResult Index(int Id)
        {
            var PopravniIspit = ctx.PopravniIspit
                .Include(i => i.Odjeljenje)
                .Where(i => i.Id == Id)
                .SingleOrDefault();

            var model = new AjaxStudentListaVM
            {
                PopravniIspitId = Id,
                Rows = ctx.PopravniIspitStavka
                    .Where(i => i.PopravniId == Id)
                    .Select
                    (
                        i => new AjaxStudentListaVM.Row
                        {
                            PopravniIspitStavkaId = i.Id,
                            UcenikId = i.OdjeljenjeStavka.Ucenik.Id,
                            Ucenik = i.OdjeljenjeStavka.Ucenik.ImePrezime,
                            OdjeljenjeId = i.OdjeljenjeStavka.Odjeljenje.Id,
                            Odjeljenje = i.OdjeljenjeStavka.Odjeljenje.Oznaka,
                            BrojUcenikaUDnevniku = i.OdjeljenjeStavka.BrojUDnevniku,
                            Pristupio = i.Pristupio,
                            Bodovi = i.Bodovi
                        }
                    )
                    .ToList()
            };
            return PartialView(model);
        }

        public IActionResult TogglePrisusto(int Id)
        {
            var Stavka = ctx.PopravniIspitStavka.Find(Id);
            Stavka.Pristupio = !Stavka.Pristupio;
            ctx.SaveChanges();
            return Redirect("/PopravniIspit/Uredi?Id=" + Stavka.PopravniId);
        }

        public IActionResult UcenikJePrisutan(int Id)
        {
            return TogglePrisusto(Id);
        }

        public IActionResult UcenikJeOdsutan(int Id)
        {
            return TogglePrisusto(Id);
        }

        public IActionResult Uredi(int Id)
        {
            var Stavka = ctx.PopravniIspitStavka.Find(Id);
            var model = new AjaxStavkaUrediVM
            {
                PopravniIspitStavkaId = Id,
                UcenikId = Stavka.OdjeljenjeStavka.Ucenik.Id,
                Ucenik = Stavka.OdjeljenjeStavka.Ucenik.ImePrezime,
                Bodovi = Stavka.Bodovi
            };
            return PartialView(model);
        }
    }
}