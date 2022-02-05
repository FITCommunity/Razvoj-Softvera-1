using Microsoft.EntityFrameworkCore;
using RS1_2018_01_23.EntityModels;

namespace RS1_2018_01_23.EF
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

            modelBuilder.Entity<RezultatPretrage>().HasOne(x => x.Uputnica)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

        }

        public DbSet<VrstaPretrage> VrstaPretrage { get; set; }
        public DbSet<Ljekar> Ljekar { get; set; }
        public DbSet<Pacijent> Pacijent { get; set; }
        public DbSet<RezultatPretrage> RezultatPretrage { get; set; }
        public DbSet<Uputnica> Uputnica { get; set; }
        public DbSet<LabPretraga> LabPretraga { get; set; }
        public DbSet<Modalitet> Modalitet { get; set; }
    }
}
