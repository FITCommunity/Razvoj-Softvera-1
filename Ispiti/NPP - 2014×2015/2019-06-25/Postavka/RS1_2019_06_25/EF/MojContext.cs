using Microsoft.EntityFrameworkCore;
using RS1_2019_06_25.EntityModels;

namespace RS1_2019_06_25.EF
{
    public class MojContext : DbContext
    {
        public MojContext(DbContextOptions<MojContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SlusaPredmet>().HasOne(x => x.UpisGodine)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OdrzaniCasDetalji>().HasOne(x => x.SlusaPredmete)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

        }

        public DbSet<AkademskaGodina> AkademskaGodina { get; set; }
        public DbSet<Nastavnik> Nastavnik { get; set; }
        public DbSet<Angazovan> Angazovan { get; set; }
        public DbSet<Predmet> Predmet { get; set; }
        public DbSet<SlusaPredmet> SlusaPredmet { get; set; }
        public DbSet<UpisGodine> UpisGodine { get; set; }
        public DbSet<Student> Student { get; set; }

        public DbSet<OdrzaniCas> OdrzaniCas { get; set; }
        public DbSet<OdrzaniCasDetalji> OdrzaniCasDetalji { get; set; }
    }
}
