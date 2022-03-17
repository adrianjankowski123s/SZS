namespace SZS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Terminarz")]
    public partial class Terminarz
    {
        public int Id { get; set; }
        [Display(Name = "Szkolenie")]

        public int IdSzkolenia { get; set; }
        [Display(Name = "Temat")]

        public int? IdTematu { get; set; }
        [Display(Name = "Liczba godzin")]

        public int? LiczbaGodzin { get; set; }
        [Display(Name = "Rozpoczêcie")]

        public DateTime TerminOd { get; set; }
        [Display(Name = "Zakoñczenie")]

        public DateTime TerminDo { get; set; }

        public virtual Szkolenie Szkolenie { get; set; }

        public virtual Temat Temat { get; set; }
    }
}
