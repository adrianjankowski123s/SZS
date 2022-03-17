using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SZS.Models
{
    public class RoleViewModel : SZS.Models.SZSModel
    {

        public int Id { get; set; }
        [Display(Name = "Nazwa")]

        public string Name { get; set; }


    }
}