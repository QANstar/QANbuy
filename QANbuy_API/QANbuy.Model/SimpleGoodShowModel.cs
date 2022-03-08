using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QANbuy.Model
{
    public class SimpleGoodShowModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string GoodPic { get; set; }

        public double Price { get; set; }
        public int saleNum { get; set; }
    }
}
