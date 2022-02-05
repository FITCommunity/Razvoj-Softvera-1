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
    public class TakmicenjeController : Controller
    {
        private readonly MojContext ctx;
        public TakmicenjeController(MojContext context)
        {
            ctx = context;
        }

        public IActionResult Index()
        {
            var Razredi = new List<string> { "", "1", "2", "3", "4" };
            var model = new TakmicenjeIndexVM
            {
                Skole = ctx.Skola
                    .Select
                    (
                       i => new SelectListItem
                       {
                           Value = i.Id.ToString(),
                           Text = i.Naziv
                       }
                    )
                    .OrderBy(i => i.Text)
                    .ToList(),
                Razredi = Razredi
                    .ConvertAll
                    (
                        i =>
                        {
                            return new SelectListItem()
                            {
                                Text = i,
                                Value = i,
                                Selected = false
                            };
                        }
                    )

            };
            return View(model);
        }

        public IActionResult Odaberi(TakmicenjeIndexVM model)
        {
            var skola = ctx.Skola.Find(model.SkolaId);

            var modelOdaberi = new TakmicenjeOdaberiVM
            {
                SkolaId = skola.Id,
                Skola = skola.Naziv,
                Razred = model.Razred,
                Rows = ctx.Takmicenje
                    .Where
                    (
                        i => i.Skola.Id == model.SkolaId                   
                    )
                    .Select
                    (
                        i => new TakmicenjeOdaberiVM.Row
                        {
                            TakmicenjeId = i.Id,
                            PredmetId = i.Predmet.Id,
                            Predmet = i.Predmet.Naziv,
                            Razred = i.Razred,
                            Datum = i.Datum.ToString("dd/MM/yyyy"),
                            BrojUcesnikaKojiNisuPristupili = ctx.TakmicenjeUcesnik
                                .Where
                                (
                                    t => t.TakmicenjeId == i.Id &&
                                         t.IsPristupio == false
                                )
                                .Count()
                        }
                    )
                    .ToList()
            };

            if(model.Razred != null)
            {
                modelOdaberi.Rows = modelOdaberi.Rows.Where(i => i.Razred.ToString() == model.Razred).ToList();
            }

            foreach (var Row in modelOdaberi.Rows)
            {
                var Najbolji = ctx.TakmicenjeUcesnik
                    .Include(i => i.OdjeljenjeStavka.Ucenik)
                    .Include(i => i.OdjeljenjeStavka.Odjeljenje)
                    .Include(i => i.OdjeljenjeStavka.Odjeljenje.Skola)
                    .OrderByDescending(i => i.Bodovi)
                    .Where(i => i.TakmicenjeId == Row.TakmicenjeId && i.IsPristupio == true)
                    .FirstOrDefault();

                if(Najbolji != null)
                {
                    Row.NajboljiUcesnikId = Najbolji.OdjeljenjeStavka.Ucenik.Id;
                    Row.NajboljiUcesnikImePrezime = Najbolji.OdjeljenjeStavka.Ucenik.ImePrezime;
                    Row.NajboljiUcesnikOdjeljenje = Najbolji.OdjeljenjeStavka.Odjeljenje.Oznaka;
                    Row.NajboljiUcesnikSkola = Najbolji.OdjeljenjeStavka.Odjeljenje.Skola.Naziv;
                }

            }

            return View(modelOdaberi);
        }

        public IActionResult Dodaj(int Id)
        {
            var skola = ctx.Skola.Find(Id);
            var Razredi = new List<int> { 1, 2, 3, 4 };
            var model = new TakmicenjeDodajVM
            {
                SkolaId = Id,
                Skola = skola.Naziv,
                Predmeti = ctx.Predmet
                    .GroupBy(i => i.Naziv)
                    .Select(i => i.First())
                    .Select
                    (
                        i => new SelectListItem
                        {
                            Value = i.Naziv,
                            Text = i.Naziv
                        }
                    )
                    .ToList(),
                Razredi = Razredi
                    .ConvertAll
                    (
                        i =>
                        {
                            return new SelectListItem()
                            {
                                Text = i.ToString(),
                                Value = i.ToString(),
                                Selected = false
                            };
                        }
                    )

            };
            return View(model);
        }

        public IActionResult Snimi(TakmicenjeDodajVM model)
        {
            var Predmet = ctx.Predmet
                .Where(i => i.Naziv.Equals(model.Predmet) && i.Razred == model.Razred)
                .FirstOrDefault();

            var Takmicenje = new Takmicenje
            {
                SkolaId = model.SkolaId,
                PredmetId = Predmet.Id,
                Razred = model.Razred,
                Datum = model.Datum,
                IsZakljucano = false
            };

            ctx.Add(Takmicenje);
            ctx.SaveChanges();

            var Takmicari = ctx.DodjeljenPredmet
                .Where
                (
                    i => i.Predmet.Id == Predmet.Id &&
                         i.ZakljucnoKrajGodine == 5
                )
                .Select
                (
                    i => new TakmicenjeUcesnik
                    {
                        OdjeljenjeStavkaId = i.OdjeljenjeStavkaId,
                        IsPristupio = false,
                        Bodovi = 0
                    }
                )
                .ToList();

            foreach(var Takmicar in Takmicari)
            {
                bool flag = ctx.DodjeljenPredmet
                    .Where
                    (
                        i => i.OdjeljenjeStavkaId == Takmicar.OdjeljenjeStavkaId &&
                             i.OdjeljenjeStavka.Odjeljenje.Razred == Takmicenje.Razred
                    )
                    .Select
                    (
                        i => i.ZakljucnoKrajGodine
                    )
                    .Average() > 4;

                if(flag)
                {
                    var NoviTakmicar = new TakmicenjeUcesnik()
                    {
                        TakmicenjeId = Takmicenje.Id,
                        OdjeljenjeStavkaId = Takmicar.OdjeljenjeStavkaId,
                        IsPristupio = false,
                        Bodovi = 0
                    };

                    ctx.Add(NoviTakmicar);
                    ctx.SaveChanges();
                }
                    
            }



            return Redirect("/Takmicenje/Index");
        }

        public IActionResult Rezultati(int id)
        {
            var Takmicenje = ctx.Takmicenje
                .Include(i => i.Skola)
                .Include(i => i.Predmet)
                .Where(i => i.Id == id)
                .SingleOrDefault();
                
            var model = new TakmicenjeRezultatiVM
            { 
                TakmicenjeId = id,
                SkolaId = Takmicenje.Skola.Id,
                Skola = Takmicenje.Skola.Naziv,
                PredmetId = Takmicenje.Predmet.Id,
                Predmet = Takmicenje.Predmet.Naziv,
                Razred = Takmicenje.Razred,
                Datum = Takmicenje.Datum.ToString("dd/MM/yyyy")
                
            };

            return View(model);
        }

        public IActionResult Zakljucaj(int id)
        {
            var Takmicenje = ctx.Takmicenje.Find(id);
            if(!Takmicenje.IsZakljucano)
            {
                Takmicenje.IsZakljucano = true;
                ctx.SaveChanges();
            }
            return Redirect("/Takmicenje/Rezultati/" + id);
        }

        public IActionResult UcenikNijePristupio(int id)
        {
            return TogglePristupio(id);
        }

        public IActionResult UcenikJePristupio(int id)
        {
            return TogglePristupio(id);
        }

        public IActionResult TogglePristupio(int id)
        {
            var TakmicenjeUcesnik = ctx.TakmicenjeUcesnik.Find(id);
            TakmicenjeUcesnik.IsPristupio = !TakmicenjeUcesnik.IsPristupio;
            ctx.SaveChanges();
            return Redirect("/Takmicenje/Rezultati/" + TakmicenjeUcesnik.TakmicenjeId);
        }

    }
}