namespace SZS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PracownikUmiejetnosc")]
    public partial class PracownikUmiejetnosc
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "U¿ytkownik")]

        public int IdUzytkownika { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Umiejêtnoœci")]

        public int IdUmiejetnosc { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual Umiejetnosci Umiejetnosci { get; set; }
    }
}
