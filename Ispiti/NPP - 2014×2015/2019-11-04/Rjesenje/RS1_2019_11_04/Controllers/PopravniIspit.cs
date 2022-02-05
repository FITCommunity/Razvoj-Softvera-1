using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RS1_2019_11_04.EF;
using RS1_2019_11_04.EntityModels;
using RS1_2019_11_04.ViewModels;

namespace RS1_2019_11_04.Controllers
{
    public class PopravniIspit : Controller
    {

        private readonly MojContext _db;

        public PopravniIspit(MojContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var model = new PopravniIspitIndexVM
            {
                Rows = _db.Predmet
                    .OrderBy(i => i.Razred)
                    .Select
                    (
                        i => new PopravniIspitIndexVM.Row
                        {
                            PredmetId = i.Id,
                            Razred = i.Razred.ToString(),
                            Predmet = i.Naziv
                        }
                    )
                    .ToList()
            };
            return View(model);
        }

        public IActionResult Odaberi(int id)
        {
            var Predmet = _db.Predmet.Find(id);
            var model = new PopravniIspitOdaberiVM
            {
                PredmetId = id,
                Predmet = Predmet.Naziv,
                Razred = Predmet.Razred.ToString(),
                Rows = _db.PopravniIspit
                    .Where(i => i.PredmetId == id)
                    .Select
                    (
                        i => new PopravniIspitOdaberiVM.Row
                        {
                            PopravniIspitId = i.Id,
                            Skola = i.Skola.Naziv,
                            SkolskaGodina = i.SkolskaGodina.Naziv,
                            DatumIspita = i.DatumIspita.ToString("dd/MM/yyyy"),
                            BrojUcenikaNaIspitu = _db.PopravniIspitStavka
                                .Where(j => j.PopravniIspitId == i.Id).Count(),
                            BrojUcenikaKojiSuPolozili = _db.PopravniIspitStavka
                                .Where(j => j.PopravniIspitId == i.Id && j.Bodovi > 50).Count()
                        }
                    )
                    .ToList()
            };
            return View(model);
        }

        public IActionResult Dodaj(int id)
        {
            var Predmet = _db.Predmet.Find(id);
            var model = new PopravniIspitDodajVM
            {
                PredmetId = id,
                Predmet = Predmet.Naziv,
                Razred = Predmet.Razred.ToString(),
                Skole = _db.Skola
                    .Select
                    (
                        i => new SelectListItem
                        {
                            Value = i.Id.ToString(),
                            Text = i.Naziv
                        }
                    )
                    .ToList(),
                SkolskeGodine = _db.SkolskaGodina
                    .Select
                    (
                        i => new SelectListItem
                        {
                            Value = i.Id.ToString(),
                            Text = i.Naziv
                        }
                    )
                    .ToList()
            };
            return View(model);
        }

        public IActionResult Snimi(PopravniIspitDodajVM model)
        {
            var Ispit = new EntityModels.PopravniIspit
            {
                SkolaId = model.SkolaId,
                SkolskaGodinaId = model.SkolskaGodinaId,
                DatumIspita = Convert.ToDateTime(model.DatumIspita),
                PredmetId = model.PredmetId
            };

            _db.Add(Ispit);
            _db.SaveChanges();

            var Ucenici = _db.OdjeljenjeStavka
                .Where(i => i.Odjeljenje.Skola.Id == model.SkolaId)
                .Where(i => i.Odjeljenje.Razred.ToString() == model.Razred)
                .ToList();

            foreach(var Ucenik in Ucenici)
            {
                var NegativnaOcjena = _db.DodjeljenPredmet
                    .Where
                    (
                        i => i.PredmetId == model.PredmetId &&
                             i.OdjeljenjeStavkaId == Ucenik.Id &&
                             i.ZakljucnoKrajGodine == 1
                    )
                    .ToList();
                var NegativneOcjene = _db.DodjeljenPredmet
                    .Where
                    (
                        i => i.OdjeljenjeStavkaId == Ucenik.Id &&
                             i.ZakljucnoKrajGodine == 1
                    )
                    .ToList();

                if(NegativneOcjene.Count() >= 3)
                {
                    var Stavka = new PopravniIspitStavka
                    {
                        PopravniIspitId = Ispit.Id,
                        OdjeljenjeStavkaId = Ucenik.Id,
                        Bodovi = 0,
                        IsPristupio = null
                    };

                    _db.Add(Stavka);
                    _db.SaveChanges();
                }
                else if(NegativneOcjene.Any())
                {
                    var Stavka = new PopravniIspitStavka
                    {
                        PopravniIspitId = Ispit.Id,
                        OdjeljenjeStavkaId = Ucenik.Id,
                        IsPristupio = false
                    };

                    _db.Add(Stavka);
                    _db.SaveChanges();
                }
            }


            return Redirect("/PopravniIspit/Odaberi/" + model.PredmetId);
        }

        public IActionResult Uredi(int id)
        {
            var Ispit = _db.PopravniIspit
                .Include(i => i.Predmet)
                .Include(i => i.Skola)
                .Include(i => i.SkolskaGodina)
                .Where(i => i.Id == id)
                .SingleOrDefault();

            var model = new PopravniIspitUrediVM
            {
                PopravniIspitId = Ispit.Id,
                PredmetId = Ispit.Predmet.Id,
                Predmet = Ispit.Predmet.Naziv,
                Razred = Ispit.Predmet.Razred.ToString(),
                DatumIspita = Ispit.DatumIspita,
                SkolaId = Ispit.Skola.Id,
                Skola = Ispit.Skola.Naziv,
                SkolskaGodinaId = Ispit.SkolskaGodina.Id,
                SkolskaGodina = Ispit.SkolskaGodina.Naziv
                
            };

            return View(model);
        }
        
        public IActionResult TogglePrisutan(int id)
        {
            var Stavka = _db.PopravniIspitStavka.Find(id);
            Stavka.IsPristupio = !Stavka.IsPristupio;
            _db.SaveChanges();
            return Redirect("/PopravniIspit/Uredi/" + Stavka.PopravniIspitId);
        }

        public IActionResult UcenikJeOdsutan(int id)
        {
            return TogglePrisutan(id);
        }

        public IActionResult UcenikJePrisutan(int id)
        {
            return TogglePrisutan(id);
        }
    }
}