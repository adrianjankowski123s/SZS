using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SZS.Models
{

    public class GroupedUserViewModel
    {
        public List<UserViewModel> allUsers { get; set; }
        public List<UserViewModel> noRolesUsers { get; set; }
        public List<UserViewModel> Uczestnicy { get; set; }
        public List<UserViewModel> Trenerzy { get; set; }
        public List<UserViewModel> Kierownicy { get; set; }

    }

    public class UserViewModel : GroupedUserViewModel
    {

        public List<CustomUserRole> RoleList { get; set; }
        public IEnumerable<SelectListItem> RoleSelectList
        {
            get
            {
                return new List<SelectListItem>
                {
                     new SelectListItem { Text = "Kierownik", Value = "Kierownik"},
                     new SelectListItem { Text = "Trener", Value = "Trener" },
                     new SelectListItem { Text = "Uczestnik", Value = "Uczestnik"}
                };
            }
        }
        public int Id { get; set; }
        [Display(Name = "Wybrana rola")]

        public string SelectedRole { get; set; }

        public string Email { get; set; }

        public string Imie { get; set; }
        public string Nazwisko { get; set; }

        public string Nazwa { get; set; }
        public string Roles { get; set; }

 
    }


}
