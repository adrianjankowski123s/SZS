namespace SZS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Miejscowosc")]
    public partial class Miejscowosc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Miejscowosc()
        {
            AspNetUsers = new HashSet<AspNetUsers>();
            AspNetUsers1 = new HashSet<AspNetUsers>();
            Szkolenie = new HashSet<Szkolenie>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nazwa { get; set; }
        [Display(Name = "Województwo")]

        public int IdWojewodztwa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUsers> AspNetUsers1 { get; set; }

        public virtual Wojewodztwo Wojewodztwo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Szkolenie> Szkolenie { get; set; }
    }
}
