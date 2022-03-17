namespace SZS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NazwaSzkolenia")]
    public partial class NazwaSzkolenia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NazwaSzkolenia()
        {
            Szkolenie = new HashSet<Szkolenie>();
        }

        public int Id { get; set; }

        [Display(Name = "Nazwa szkolenia")]
        [Required]
        [StringLength(50)]
        public string Nazwa { get; set; }
        [Display(Name = "Opis szkolenia")]

        public string OpisSzkolenia { get; set; }
        [Display(Name = "Kategoria")]

        public int? IdKategorii { get; set; }

        public virtual Kategoria Kategoria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Szkolenie> Szkolenie { get; set; }
    }
}
