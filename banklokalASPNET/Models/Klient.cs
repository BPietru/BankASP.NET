using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace banklokalASPNET.Models
{
    public partial class Klient
    {
        public Klient()
        {
            Konto = new HashSet<Konto>();
            Pozyczki = new HashSet<Pozyczki>();
        }

        public decimal KId { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Pesel { get; set; }
        public DateTime? DataUr { get; set; }

        [NotMapped]
        public string PelneImie { get => Imie + " " + Nazwisko; set => PelneImie = value; }

        public ICollection<Konto> Konto { get; set; }
        public ICollection<Pozyczki> Pozyczki { get; set; }

        [NotMapped]
        public decimal SumaPozyczek { get => Pozyczki.Sum(p => p.Wartosc); set => SumaPozyczek = value; }
    }
}
