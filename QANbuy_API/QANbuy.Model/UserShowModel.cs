using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QANbuy.Model
{
    public class UserShowModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }
        [Required]
        [StringLength(30)]
        public string Email { get; set; }
        [StringLength(30)]
        public string Address { get; set; }
        public string HeadPic { get; set; }
    }
}
