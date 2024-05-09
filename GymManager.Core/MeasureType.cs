using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.Core
{
    public class MeasureType
    {
        public int Id {  get; set; }
        public string MeasureName { get; set; }

        public List<Inventory> Inventories { get; set; }
    }
}
