using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QANbuy.Model
{
    public class PutCommentModel
    {
        [Required]
        public int GoodID { get; set; }
        [Required]
        [StringLength(1000)]
        public string Comment { get; set; }
    }
}
