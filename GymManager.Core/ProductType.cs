using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.Core
{
    public class ProductType
    {
        public int Id { get; set; }
        public string ProductName { get; set; }

        public List<Inventory> Inventories { get; set; }
    }
}
