using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QANbuy.Model
{
    public class GoodAddModel
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public double Postage { get; set; }
        [Required]
        [StringLength(30)]
        public string ShipAddress { get; set; }
        public int Type { get; set; }
        public string GoodPic { get; set; }

        public int Inventory { get; set; }
    }
}
