using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Director { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        [Range(1,10)]
        public int Rating { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Actors { get; set; }
        [Required]
        public string DateAdded { get; set; }
    }
}
