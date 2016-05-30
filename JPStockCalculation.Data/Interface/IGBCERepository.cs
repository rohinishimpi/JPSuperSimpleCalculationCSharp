using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPStockCalculation.Entity;

namespace JPStockCalculation.Data
{
   public interface IGBCERepository
    {
       Double allShareIndex(Dictionary<String, StockRepository> stocks);
    }
}
