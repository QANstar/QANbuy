using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QANbuy.Model
{
    public class GoodShowModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string GoodPic { get; set; }
        public int stateId { get; set; }
        public string state { get; set; }
        public int typeId { get; set; }
        public string type { get; set; }
        public string Description { get; set; }
        public double Postage { get; set; }
        public string ShipAddress { get; set; }
        public DateTime CreateDate { get; set; }
        public int saleNum { get; set; }
        public int Inventory { get; set; }
    }
}
