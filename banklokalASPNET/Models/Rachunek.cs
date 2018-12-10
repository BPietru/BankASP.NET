using System;
using System.Collections.Generic;

namespace banklokalASPNET.Models
{
    public partial class Rachunek
    {
        public decimal RId { get; set; }
        public decimal KoId { get; set; }
        public string Numer { get; set; }
        public decimal Saldo { get; set; }
        public string TypRachunku { get; set; }

        public Konto Ko { get; set; }
    }
}
