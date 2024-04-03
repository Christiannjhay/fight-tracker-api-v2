using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiv2.models
{
    public class Couple
    {
        public int CoupleId { get; set; }

        public int CoupleCode { get; set; }

        public int Couple_1 { get; set; }

        public int Couple_2 { get; set; }

        public DateTime Anniversary { get; set; }

        public List<Fight> Fights { get; set; } = new List<Fight>();
    }
}