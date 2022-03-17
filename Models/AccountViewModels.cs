using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SZS.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Kod")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Pamiętasz tę przeglądarkę?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętać Cię?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {

        [Display(Name = "Role użytkownika")]
        public string UserRoles { get; set; }

        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi zawierać co najmniej następującą liczbę znaków: {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasło i jego potwierdzenie są niezgodne.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} musi zawierać co najmniej następującą liczbę znaków: {2}.", MinimumLength = 2)]
        [Display(Name = "Imię")]
        public string Imie { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} musi zawierać co najmniej następującą liczbę znaków: {2}.", MinimumLength = 2)]
        [Display(Name = "Nazwisko")]
        public string Nazwisko { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefon")]
        public string Telefon { get; set; }

        [Display(Name = "Płeć")]
        [StringLength(50)]
        public string Plec { get; set; }

        public string Pesel { get; set; }

        [Display(Name = "Data urodzenia")]
        [DataType(DataType.Date)]
        public DateTime? DataUrodzenia { get; set; }

        [Display(Name = "Miejsce urodzenia")]
        public int? IDMiejsceUrodzenia { get; set; }

        [Display(Name = "Kod pocztowy")]
        [StringLength(50)]
        public string KodPocztowy { get; set; }

        [StringLength(50)]
        public string Ulica { get; set; }

        [Display(Name = "Miejscowość")]
        public int? IDMiejscowosc { get; set; }


        [Display(Name = "Wykształcenie")]
        public int? IDWyksztalcenia { get; set; }


        [Display(Name = "Miejscowość")]
        public virtual Miejscowosc Miejscowosc { get; set; }


        [Display(Name = "Miejsce urodzenia")]
        public virtual Miejscowosc Miejscowosc1 { get; set; }

        public virtual Wyksztalcenie Wyksztalcenie { get; set; }

    }

    public class UserEdit
    {
        
        [Required]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} musi zawierać co najmniej następującą liczbę znaków: {2}.", MinimumLength = 2)]
        [Display(Name = "Imię")]
        public string Imie { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} musi zawierać co najmniej następującą liczbę znaków: {2}.", MinimumLength = 2)]
        [Display(Name = "Nazwisko")]
        public string Nazwisko { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefon")]
        public string Telefon { get; set; }

        [Display(Name = "Płeć")]
        [StringLength(50)]
        public string Plec { get; set; }

        public string Pesel { get; set; }

        [Display(Name = "Data urodzenia")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime? DataUrodzenia { get; set; }

        [Display(Name = "Miejsce urodzenia")]
        public int? IDMiejsceUrodzenia { get; set; }

        [Display(Name = "Kod pocztowy")]
        [StringLength(50)]
        public string KodPocztowy { get; set; }

        [StringLength(50)]
        public string Ulica { get; set; }

        [Display(Name = "Miejscowość")]
        public int? IDMiejscowosc { get; set; }


        [Display(Name = "Wykształcenie")]
        public int? IDWyksztalcenia { get; set; }


        [Display(Name = "Miejscowość")]
        public virtual Miejscowosc Miejscowosc { get; set; }


        [Display(Name = "Miejsce urodzenia")]
        public virtual Miejscowosc Miejscowosc1 { get; set; }

        public virtual Wyksztalcenie Wyksztalcenie { get; set; }
    }


        public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi zawierać co najmniej następującą liczbę znaków: {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasło i jego potwierdzenie są niezgodne.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }
    }
}
