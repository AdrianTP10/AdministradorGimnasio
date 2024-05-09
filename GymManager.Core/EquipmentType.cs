using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.Core
{
    public class EquipmentType
    {
        public int Id { get; set; }
        [Required]
        public string EquipmentName { get; set; }
    }
}
