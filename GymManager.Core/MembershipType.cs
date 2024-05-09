using GymManager.Core.Members;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.Core
{
    public class MembershipType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string MembershipName { get; set; }
        [Required]
        [Column(TypeName = "decimal(13, 2)")]
        public decimal Price { get; set; }
        [Required]
        public int Duration { get; set; }

        public List<Member> Members{ get; set; }
    }
}
