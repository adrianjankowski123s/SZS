namespace SZS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using SZS.Models;

    [Table("Uczestnik")]
    public partial class Uczestnik
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Uczestnik")]

        public int IdUczestnika { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Szkolenie")]

        public int IdSzkolenia { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }
      
        public virtual Szkolenie Szkolenie { get; set; }
    }
}
