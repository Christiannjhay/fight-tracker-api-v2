using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiv2.models
{
    public class Fight
    {
        
         public int FightId { get; set; }

        public string Reason { get; set; } = string.Empty;

        public string Details { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public int CoupleId { get; set; }

        public Couple? Couples { get; set; }
        
    }
}