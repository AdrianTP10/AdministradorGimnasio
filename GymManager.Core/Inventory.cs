﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.Core
{
    public class Inventory
    {
        public int Id { get; set; }
        public ProductType ProductType { get; set; }
        public MeasureType MeasureType { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public List<Sale> Sales { get; set; }
    }
}
