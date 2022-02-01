using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using System.ComponentModel.DataAnnotations;

namespace Library21.Models
{
    public class Book
    {
        public int? BookID { get; set; }

        [Required]
        [Display(Name = "Book Name")]
        public string BookName { get; set; }

        [Required]
        [Display(Name = "Book Publisher")]
        public string BookPublisher { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string Category { get; set; }
        public virtual ICollection<BookIssue> BookIssues { get; set; } 
    }
}
