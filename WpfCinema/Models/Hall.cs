using System;
using System.Collections.Generic;

#nullable disable

namespace WpfCinema.Models
{
    public partial class Hall
    {
        public Hall()
        {
            Screenings = new HashSet<Screening>();
        }

        public int Number { get; set; }
        public int NumberOfSeats { get; set; }

        public virtual ICollection<Screening> Screenings { get; set; }
    }
}
