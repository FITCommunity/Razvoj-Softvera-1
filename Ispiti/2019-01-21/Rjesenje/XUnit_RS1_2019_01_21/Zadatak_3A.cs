using Microsoft.EntityFrameworkCore;
using RS1_2019_01_21.Controllers;
using RS1_2019_01_21.ViewModels;
using RS1_2019_01_21.EF;
using System;
using Xunit;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace XUnit_RS1_2019_01_21
{
    public class Zadatak_3A
    {
        private readonly MojContext ctx;
        
        public Zadatak_3A()
        {
            var builder = new DbContextOptionsBuilder<MojContext>()
                .UseSqlServer("Server=.;Database=RS1_2019_01_21;Trusted_Connection=true;MultipleActiveResultSets=true;");
            ctx = new MojContext(builder.Options);
        }

        [Fact]
        public async Task Test_Zadatak_3A()
        {
            var odrzanaNastava = new OdrzanaNastavaController(ctx);

            OdrzanaNastavaDodajVM model = new OdrzanaNastavaDodajVM
            {
                Datum = DateTime.Now,
                NastavnikId = 1,
                PredmetId = 1,
                SkolaId = 2,
                SkolskaGodinaId = ctx.SkolskaGodina.Where(i => i.Aktuelna == true).Select(i => i.Id).FirstOrDefault()
            };

            odrzanaNastava.Snimi(model);

            //Pronaci ID ucenika kojeg ne treba dodati na ovaj Ispit
            Assert.False(await ctx.MaturskiIspitStavka.AnyAsync(m => m.OdjeljenjeStavka.UcenikId == 1));

            //Pronaci ID ucenika kojeg treba dodati na ovaj Ispit
            //Assert.True(await ctx.MaturskiIspitStavka.AnyAsync(m => m.OdjeljenjeStavka.UcenikId == 4));
        }
    }
}
