using System;
using System.Collections.Generic;

namespace banklokalASPNET.Models
{
    public partial class Pozyczki
    {
        public decimal PId { get; set; }
        public decimal KId { get; set; }
        public decimal Wartosc { get; set; }
        public DateTime DataSplaty { get; set; }

        public Klient K { get; set; }
    }
}
