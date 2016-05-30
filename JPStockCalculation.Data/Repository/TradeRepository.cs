using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPStockCalculation.Entity;

namespace JPStockCalculation.Data
{
    public class TradeRepository : TradeEntity
    {
        public TradeRepository()
        {
        }
        public TradeRepository(TradeType type, int quantity, Double price)
        {
            this.TradeType = type;
            this.SharesQuantity = quantity;
            this.Price = price;
        }
    }
}
