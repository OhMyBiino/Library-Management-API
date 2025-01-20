using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementModels
{
    public class BookModel
    {
        [Required]
        [Key]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "ISBN must be 10 - 13 characters.")]
        [DisplayName("ISBN")]
        public string ISBN { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [DisplayName("Book Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [DisplayName("Genre")]
        public string Genre { get; set; }

        [StringLength(200, MinimumLength = 2)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [DisplayName("Author")]
        public string Author { get; set; }

        public bool isBorrowed { get; set; }
    }
}
