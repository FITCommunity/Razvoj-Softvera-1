using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RS1_2019_06_25.EF;
using RS1_2019_06_25.ViewModels;

namespace RS1_2019_06_25.Controllers
{
    public class PredmetController : Controller
    {
        private readonly MojContext ctx;
        public PredmetController(MojContext context)
        {
            ctx = context;
        }
        public IActionResult Index()
        {
            var model = new PredmetIndexVM
            {
                Rows = ctx.Angazovan
                    .OrderBy(i => i.AkademskaGodinaId)
                    .Select
                    (
                        i => new PredmetIndexVM.Row
                        {
                            AngazovanId = i.Id,
                            Predmet = i.Predmet.Naziv,
                            AkademskaGodina = i.AkademskaGodina.Opis,
                            Nastavnik = i.Nastavnik.Ime + ' ' + i.Nastavnik.Prezime,
                            BrojOdrzanihCasova = ctx.OdrzaniCas.Where(j => j.AngazovaniId == i.Id).Count(),
                            BrojStudenataNaPredmetu = ctx.SlusaPredmet.Where(j => j.AngazovanId == i.Id).Count()
                        }
                    )
                    .ToList()
            };
            return View(model);
        }
    }
}