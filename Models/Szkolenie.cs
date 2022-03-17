namespace SZS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    [Table("Szkolenie")]
    public partial class Szkolenie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Szkolenie()
        {
            Terminarz = new HashSet<Terminarz>();
            Uczestnik = new HashSet<Uczestnik>();
        }

        public int Id { get; set; }
        [Display(Name = "Nazwa szkolenia")]

        public int IdNazwaSzkolenia { get; set; }
        [Display(Name = "Kategoria")]

        public int IdKategorii { get; set; }
        [Display(Name = "Kierownik")]

        public int? IdKierownika { get; set; }
        [Display(Name = "Trener")]

        public int? IdTrenera { get; set; }
        [Display(Name = "Liczba godzin")]

        public int? LiczbaGodzin { get; set; }
        [Display(Name = "Rozpoczêcie")]

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
       
        public DateTime? DataOd { get; set; }
        [Display(Name = "Zakoñczenie")]

        [Column(TypeName = "date")]
         [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DataDo { get; set; }

        [StringLength(50)]
        [Display(Name = "Kod pocztowy")]

        public string KodPocztowy { get; set; }
        [Display(Name = "Województwo")]

        public int IdWojewodztwa { get; set; }
        [Display(Name = "Miejscowoœæ")]

        public int IdMiejscowosc { get; set; }


        [StringLength(50)]
        public string Ulica { get; set; }
        [Display(Name = "Finansowanie")]

        public int? IdFinansowania { get; set; }

        public double? Koszt { get; set; }
        [Display(Name = "Czy wydano zaœwiadczenia?")]

        public bool? CzyWydanoZaswiadczenie { get; set; }
        [Display(Name = "Czy zrealizowano?")]

        public bool? CzyZrealizowano { get; set; }
        [Display(Name = "Kod szkolenia")]

        [StringLength(50)]
        public string KodSzkolenia { get; set; }
        [Display(Name = "Czy otwarte?")]

        public bool? CzyOtwarte { get; set; }
        [Display(Name = "Czy widoczne?")]

        public bool? CzyWidoczne { get; set; }

        public string Uwagi { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual AspNetUsers AspNetUsers1 { get; set; }

        public virtual Finansowanie Finansowanie { get; set; }

        public virtual Kategoria Kategoria { get; set; }

        public virtual Wojewodztwo Wojewodztwo { get; set; }
        public virtual Miejscowosc Miejscowosc { get; set; }

        public virtual NazwaSzkolenia NazwaSzkolenia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Terminarz> Terminarz { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Uczestnik> Uczestnik { get; set; }
    }
}
