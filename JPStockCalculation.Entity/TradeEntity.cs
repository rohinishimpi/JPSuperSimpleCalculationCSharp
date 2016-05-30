using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPStockCalculation.Entity
{
    public abstract class TradeEntity
    {
        public DateTime TimeStamp { get; set; }       
        public TradeType TradeType { get; set; }
        public int SharesQuantity { get; set; }        
        public double Price { get; set; }                
       
    }
}
