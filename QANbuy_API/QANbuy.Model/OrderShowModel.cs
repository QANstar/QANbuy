using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QANbuy.Model
{
    public class OrderShowModel
    {
        public int ID { get; set; }
        public int GoodID { get; set; }
        public int UserID { get; set; }
        public int Num { get; set; }
        [Required]
        [StringLength(30)]
        public string state { get; set; }
        public double Payment { get; set; }
        [Required]
        [StringLength(30)]
        public string ShipAddress { get; set; }
        [Required]
        [StringLength(30)]
        public string HarvestAddress { get; set; }
        [Column(TypeName = "date")]
        public DateTime PayDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? SendDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? HarvestDate { get; set; }
        public int StateID { get; set; }
        [Required]
        [StringLength(30)]
        public string GoodName { get; set; }
        public double GoodPrice { get; set; }
        public string GoodPic { get; set; }
        public double Postage { get; set; }
    }
}
