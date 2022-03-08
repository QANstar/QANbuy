using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QANbuy.Model
{
    public class OrderCreateModel
    {
        public int GoodID { get; set; }
        public int Num { get; set; }
    }
}
