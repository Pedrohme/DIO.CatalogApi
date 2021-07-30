using System;
using System.ComponentModel.DataAnnotations;

namespace DIO.CatalogApi.InputModel {
    public class SongInputModel {
        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "The song's name must contain at least one character")]
        public string Name { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "The album's name must contain at least one character")]
        public string Album { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "The author's name must contain at least one character")]
        public string Author { get; set; }
    }
}
