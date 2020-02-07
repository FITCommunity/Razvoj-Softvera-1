using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2019_06_25.EF;
using RS1_2019_06_25.ViewModels;

namespace RS1_2019_06_25.Controllers
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
            var model = new AjaxStavkeIndexVM
            {
                IsZakljucan = ctx.Ispit.Find(id).Zakljucano,
                Rows = ctx.IspitStavka
                    .Where(i => i.IspitId == id)
                    .Select
                    (
                        i => new AjaxStavkeIndexVM.Row
                        {
                            StavkaIspitId = i.Id,
                            Student = i.Student.Ime + " " + i.Student.Prezime,
                            IsPristupio = i.IsPristupio,
                            Ocjena = i.Ocjena
                        }
                    )
                    .ToList()
            };
            return PartialView(model);
        }

        public IActionResult Uredi(int id)
        {
            var Stavka = ctx.IspitStavka
               .Include(i => i.Student)
               .Where(i => i.Id == id)
               .SingleOrDefault();

            var model = new AjaxStavkaUrediVM
            {
                StavkaId = id,
                Student = Stavka.Student.Ime + " " + Stavka.Student.Prezime,
                Ocjena = Stavka.Ocjena
            };

            return PartialView(model);
        }

        public IActionResult Snimi(AjaxStavkaUrediVM model)
        {
            var Stavka = ctx.IspitStavka.Find(model.StavkaId);
            Stavka.Ocjena = model.Ocjena < 5 ? 5 : model.Ocjena > 10 ? 10 : model.Ocjena;
            ctx.SaveChanges();
            return Redirect("/AjaxStavke/Index/" + Stavka.IspitId);
        }
    }
}