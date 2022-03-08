using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QANbuy.Model
{
    public class CommentShowModel
    {
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }
        public string HeadPic { get; set; }
        public int ID { get; set; }
        public int GoodID { get; set; }
        [Required]
        [StringLength(1000)]
        public string Comment { get; set; }
        public DateTime CommentDate { get; set; }
        public int UserID { get; set; }
    }
}
