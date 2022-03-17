using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace SZS.Models
{
    // Możesz dodać dane profilu dla użytkownika, dodając więcej właściwości do klasy ApplicationUser. Odwiedź stronę https://go.microsoft.com/fwlink/?LinkID=317594, aby dowiedzieć się więcej.
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {

        public string UserRoles { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Telefon { get; set; }
        public string Plec { get; set; }
        public string Pesel { get; set; }
        public DateTime? DataUrodzenia { get; set; }
        public int? IDMiejsceUrodzenia { get; set; }
        public string KodPocztowy { get; set; }
        public string Ulica { get; set; }
        public int? IDMiejscowosc { get; set; }
        public int? IDWyksztalcenia { get; set; }
        public virtual Miejscowosc Miejscowosc { get; set; }
        public virtual Miejscowosc Miejscowosc1 { get; set; }
        public virtual Wyksztalcenie Wyksztalcenie { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Element authenticationType musi pasować do elementu zdefiniowanego w elemencie CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Dodaj tutaj niestandardowe oświadczenia użytkownika
            userIdentity.AddClaim(new Claim("IdUczestnik", this.Id.ToString()));


            return userIdentity;
        }

    }





    public class CustomUserRole : IdentityUserRole<int> {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }
 
    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole( string name) { Name = name; }
    }

    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(SZS.Models.SZSModel context)
            : base(context)
        {
        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(SZS.Models.SZSModel context)
            : base(context)
        {
        }
    }





    public class SZSModel : IdentityDbContext<ApplicationUser, CustomRole,
  int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {

        public SZSModel()
            : base("SZSModel")
        {
        }


        public static SZSModel Create()
        {
            return new SZSModel();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("MyUsers");
            modelBuilder.Entity<CustomUserRole>().ToTable("MyUserRoles");
            modelBuilder.Entity<CustomUserLogin>().ToTable("MyUserLogins");
            modelBuilder.Entity<CustomUserClaim>().ToTable("MyUserClaims");
            modelBuilder.Entity<CustomRole>().ToTable("MyRoles");
        }

        public System.Data.Entity.DbSet<SZS.NazwaSzkolenia> NazwaSzkolenias { get; set; }
    }




}


    
