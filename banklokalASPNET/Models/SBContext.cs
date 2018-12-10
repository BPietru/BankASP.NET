using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace banklokalASPNET.Models
{
    public partial class SBContext : DbContext
    {
        public SBContext()
        {
        }

        public SBContext(DbContextOptions<SBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Klient> Klient { get; set; }
        public virtual DbSet<Konto> Konto { get; set; }
        public virtual DbSet<Pozyczki> Pozyczki { get; set; }
        public virtual DbSet<Rachunek> Rachunek { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Klient>(entity =>
            {
                entity.HasKey(e => e.KId);

                entity.ToTable("KLIENT");

                entity.Property(e => e.KId)
                    .HasColumnName("K_ID")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.DataUr)
                    .HasColumnName("DATA_UR")
                    .HasColumnType("datetime");

                entity.Property(e => e.Imie)
                    .IsRequired()
                    .HasColumnName("IMIE")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nazwisko)
                    .IsRequired()
                    .HasColumnName("NAZWISKO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Pesel)
                    .IsRequired()
                    .HasColumnName("PESEL")
                    .HasMaxLength(12)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Konto>(entity =>
            {
                entity.HasKey(e => e.KoId);

                entity.ToTable("KONTO");

                entity.HasIndex(e => e.KId)
                    .HasName("KLIENCI_KONTA_FK");

                entity.Property(e => e.KoId)
                    .HasColumnName("KO_ID")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.KId)
                    .HasColumnName("K_ID")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.TypKonta)
                    .IsRequired()
                    .HasColumnName("TYP_KONTA")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.HasOne(d => d.K)
                    .WithMany(p => p.Konto)
                    .HasForeignKey(d => d.KId)
                  //  .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_KONTO_KLIENCI_K_KLIENT");
            });

            modelBuilder.Entity<Pozyczki>(entity =>
            {
                entity.HasKey(e => e.PId);

                entity.ToTable("POZYCZKI");

                entity.HasIndex(e => e.KId)
                   .HasName("POZYCZKI_KLIENCI_FK");

                entity.Property(e => e.PId)
                    .HasColumnName("P_ID")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.DataSplaty)
                    .HasColumnName("DATA_SPLATY")
                    .HasColumnType("datetime");

                entity.Property(e => e.KId)
                    .HasColumnName("K_ID")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Wartosc)
                    .HasColumnName("WARTOSC")
                    .HasColumnType("money");

                entity.HasOne(d => d.K)
                    .WithMany(p => p.Pozyczki)
                    .HasForeignKey(d => d.KId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_POZYCZKI_POZYCZKI__KLIENT");
            });

            modelBuilder.Entity<Rachunek>(entity =>
            {
                entity.HasKey(e => e.RId);

                entity.ToTable("RACHUNEK");

                entity.HasIndex(e => e.KoId)
                    .HasName("KONTO_RACHUNEK_FK");

                entity.Property(e => e.RId)
                    .HasColumnName("R_ID")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.KoId)
                    .HasColumnName("KO_ID")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Numer)
                    .IsRequired()
                    .HasColumnName("NUMER")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Saldo)
                    .HasColumnName("SALDO")
                    .HasColumnType("money");

                entity.Property(e => e.TypRachunku)
                    .IsRequired()
                    .HasColumnName("TYP_RACHUNKU")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Ko)
                    .WithMany(p => p.Rachunek)
                    .HasForeignKey(d => d.KoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RACHUNEK_KONTO_RAC_KONTO");
            });
        }

        internal static Task ToListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
