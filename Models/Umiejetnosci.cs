namespace SZS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Umiejetnosci")]
    public partial class Umiejetnosci
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Umiejetnosci()
        {
            PracownikUmiejetnosc = new HashSet<PracownikUmiejetnosc>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nazwa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PracownikUmiejetnosc> PracownikUmiejetnosc { get; set; }
    }
}
