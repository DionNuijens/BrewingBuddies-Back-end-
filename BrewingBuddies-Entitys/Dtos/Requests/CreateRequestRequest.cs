﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_Entitys.Dtos.Requests
{
    public class CreateRequestRequest
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public int State { get; set; }
        public string challenger { get; set; }
        public string defender { get; set; }
        public decimal challengerKDA { get; set; }
        public decimal defenderKDA { get; set; }
        public string winner { get; set; }
    }
}
