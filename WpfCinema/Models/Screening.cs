using System;
using System.Collections.Generic;

#nullable disable

namespace WpfCinema.Models
{
    public partial class Screening
    {
        public int Id { get; set; }
        public int Movie { get; set; }
        public int? Hall { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }

        public virtual Hall HallNavigation { get; set; }
        public virtual Movie MovieNavigation { get; set; }
    }
}
