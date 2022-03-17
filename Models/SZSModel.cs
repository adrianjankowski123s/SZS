using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using SZS.Models;

namespace SZS
{
    public partial class SZSModel : DbContext
    {
        
        public SZSModel()
            : base("name=SZSModel")
        {
        }

        public static SZSModel Create()
        {
            return new SZSModel();
        }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Finansowanie> Finansowanie { get; set; }
        public virtual DbSet<Kategoria> Kategoria { get; set; }
        public virtual DbSet<Miejscowosc> Miejscowosc { get; set; }
        public virtual DbSet<NazwaSzkolenia> NazwaSzkolenia { get; set; }
        public virtual DbSet<PracownikUmiejetnosc> PracownikUmiejetnosc { get; set; }
        public virtual DbSet<Szkolenie> Szkolenie { get; set; }
        public virtual DbSet<Temat> Temat { get; set; }
        public virtual DbSet<Terminarz> Terminarz { get; set; }
        public virtual DbSet<Uczestnik> Uczestnik { get; set; }
        public virtual DbSet<Umiejetnosci> Umiejetnosci { get; set; }
        public virtual DbSet<Wojewodztwo> Wojewodztwo { get; set; }
        public virtual DbSet<Wyksztalcenie> Wyksztalcenie { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.PracownikUmiejetnosc)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.IdUzytkownika)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Szkolenie)
                .WithOptional(e => e.AspNetUsers)
                .HasForeignKey(e => e.IdKierownika);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Szkolenie1)
                .WithOptional(e => e.AspNetUsers1)
                .HasForeignKey(e => e.IdTrenera);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Uczestnik)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.IdUczestnika)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Finansowanie>()
                .HasMany(e => e.Szkolenie)
                .WithOptional(e => e.Finansowanie)
                .HasForeignKey(e => e.IdFinansowania);

            modelBuilder.Entity<Kategoria>()
                .HasMany(e => e.NazwaSzkolenia)
                .WithOptional(e => e.Kategoria)
                .HasForeignKey(e => e.IdKategorii);

            modelBuilder.Entity<Kategoria>()
                .HasMany(e => e.Szkolenie)
                .WithRequired(e => e.Kategoria)
                .HasForeignKey(e => e.IdKategorii)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Miejscowosc>()
                .HasMany(e => e.AspNetUsers)
                .WithOptional(e => e.Miejscowosc)
                .HasForeignKey(e => e.IDMiejscowosc);

            modelBuilder.Entity<Miejscowosc>()
                .HasMany(e => e.AspNetUsers1)
                .WithOptional(e => e.Miejscowosc1)
                .HasForeignKey(e => e.IDMiejsceUrodzenia);

            modelBuilder.Entity<Miejscowosc>()
                .HasMany(e => e.Szkolenie)
                .WithRequired(e => e.Miejscowosc)
                .HasForeignKey(e => e.IdMiejscowosc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NazwaSzkolenia>()
                .HasMany(e => e.Szkolenie)
                .WithRequired(e => e.NazwaSzkolenia)
                .HasForeignKey(e => e.IdNazwaSzkolenia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Szkolenie>()
                .HasMany(e => e.Terminarz)
                .WithRequired(e => e.Szkolenie)
                .HasForeignKey(e => e.IdSzkolenia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Szkolenie>()
                .HasMany(e => e.Uczestnik)
                .WithRequired(e => e.Szkolenie)
                .HasForeignKey(e => e.IdSzkolenia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Temat>()
                .HasMany(e => e.Terminarz)
                .WithOptional(e => e.Temat)
                .HasForeignKey(e => e.IdTematu);

            modelBuilder.Entity<Umiejetnosci>()
                .HasMany(e => e.PracownikUmiejetnosc)
                .WithRequired(e => e.Umiejetnosci)
                .HasForeignKey(e => e.IdUmiejetnosc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Wojewodztwo>()
                .HasMany(e => e.Miejscowosc)
                .WithRequired(e => e.Wojewodztwo)
                .HasForeignKey(e => e.IdWojewodztwa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Wyksztalcenie>()
                .HasMany(e => e.AspNetUsers)
                .WithOptional(e => e.Wyksztalcenie)
                .HasForeignKey(e => e.IDWyksztalcenia);
        }

        public int Property
        {
            get => default;
            set
            {
            }
        }
    }
}
