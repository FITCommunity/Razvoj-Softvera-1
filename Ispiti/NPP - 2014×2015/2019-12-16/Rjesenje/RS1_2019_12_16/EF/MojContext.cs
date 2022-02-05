using Microsoft.EntityFrameworkCore;
using RS1_2019_12_16.EntityModels;

namespace RS1_2019_12_16.EF
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

            modelBuilder.Entity<Odjeljenje>().HasOne(x => x.SkolskaGodina)
               .WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Odjeljenje>().HasOne(x => x.Skola)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PredajePredmet>().HasOne(x => x.Odjeljenje)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Komisija>()
                .HasKey(x => new { x.PopravniIspitId, x.NastavnikId });
            modelBuilder.Entity<Komisija>()
                .HasOne(x => x.PopravniIspit)
                .WithMany(x => x.Komisija)
                .HasForeignKey(x => x.PopravniIspitId);
            modelBuilder.Entity<Komisija>()
                .HasOne(x => x.Nastavnik)
                .WithMany(x => x.Komisija)
                .HasForeignKey(x => x.NastavnikId);

        }


        public DbSet<Predmet> Predmet { get; set; }
        public DbSet<DodjeljenPredmet> DodjeljenPredmet { get; set; }
        public DbSet<Ucenik> Ucenik { get; set; }
        public DbSet<OdjeljenjeStavka> OdjeljenjeStavka { get; set; }
        public DbSet<Odjeljenje> Odjeljenje { get; set; }
   
        public DbSet<Nastavnik> Nastavnik { get; set; }
        public DbSet<PredajePredmet> PredajePredmet { get; set; }
        public DbSet<Skola> Skola { get; set; }
        public DbSet<SkolskaGodina> SkolskaGodina { get; set; }
        public DbSet<PopravniIspit> PopravniIspit { get; set; }
        public DbSet<PopravniIspitStavka> PopravniIspitStavka { get; set; }
        public DbSet<Komisija> Komisija { get; set; }
    }
}
