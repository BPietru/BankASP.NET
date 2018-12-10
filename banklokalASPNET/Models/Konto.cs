using System;
using System.Collections.Generic;

namespace banklokalASPNET.Models
{
    public partial class Konto
    {
        public Konto()
        {
            Rachunek = new HashSet<Rachunek>();
        }

        public decimal KoId { get; set; }
        public decimal KId { get; set; }
        public string TypKonta { get; set; }

        public Klient K { get; set; }
        public ICollection<Rachunek> Rachunek { get; set; }
    }
}
