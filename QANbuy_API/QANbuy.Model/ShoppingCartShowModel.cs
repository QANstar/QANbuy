using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QANbuy.Model
{
    public class ShoppingCartShowModel
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public double Price { get; set; }
        public int Num { get; set; }
        public int UserID { get; set; }
        public int GoodID { get; set; }
        [Required]
        public string GoodPic { get; set; }
        public double Postage { get; set; }
    }
}
