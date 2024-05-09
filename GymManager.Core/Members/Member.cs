using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.Core.Members
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        public int MembershipTypeId { get; set; } // foreign key
        public MembershipType MembershipType { get; set; }

        [StringLength(15)]
        [Required(ErrorMessage ="Debe ingresar el nombre del miembro")]
        public string Name { get; set; }

        [StringLength(20)]
        [Required]
        public string LastName { get; set; }

        [BindProperty, DisplayFormat(DataFormatString = "{0:dd-MM-yyy}" , ApplyFormatInEditMode = true)]
        public DateTime BirthDay { get; set; }

        //[Range(1, 100)]
        public int CityId { get; set; } // foreign key
        public City City { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }
        

        public bool AllowNewsletter { get; set; }
    }
}
