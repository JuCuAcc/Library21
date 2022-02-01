using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Library21.Models
{
    public class BookIssue
    {
        [Key]
        public int? BookIssueID { get; set; }

        [DataType(DataType.Date)]
        public DateTime IssueDate { get; set; }
        public string MemberAddress { get; set; }


        public int BookID { get; set; }
        public virtual Book Book { get; set; } 
    }
}
