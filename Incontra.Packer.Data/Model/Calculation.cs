﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incontra.Packer.Data.Model
{
    public class Calculation
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public double ExecutionTime { get; set; }
        public DateTime ExecutionDate { get; set; }
    }
}
