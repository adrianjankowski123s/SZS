namespace SZS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Wojewodztwo")]
    public partial class Wojewodztwo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Wojewodztwo()
        {
            Miejscowosc = new HashSet<Miejscowosc>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nazwa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Miejscowosc> Miejscowosc { get; set; }
    }
}
