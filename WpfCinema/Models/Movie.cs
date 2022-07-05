using System;
using System.Collections.Generic;

#nullable disable

namespace WpfCinema.Models
{
    public partial class Movie
    {
        public Movie()
        {
            Screenings = new HashSet<Screening>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public TimeSpan? Duration { get; set; }
        public string Description { get; set; }
        public int? Category { get; set; }

        public virtual Category CategoryNavigation { get; set; }
        public virtual ICollection<Screening> Screenings { get; set; }
    }
}
