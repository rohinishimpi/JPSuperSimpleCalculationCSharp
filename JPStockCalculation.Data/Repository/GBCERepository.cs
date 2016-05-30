using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPStockCalculation.Entity;
using System.Collections;

namespace JPStockCalculation.Data
{
    public class GBCERepository
    {
        private Dictionary<String, StockRepository> stocks = new Dictionary<string, StockRepository>();

        /// <summary>
        /// This method caluclates GBCE all Share index
        /// (∑_i▒〖 Trade 〖Price〗_i×〖Quantity〗_i 〗)/(∑_i▒〖Quantity〗_i )
        /// </summary>
        /// <param name="stocks"></param>
        /// <returns></returns>
        public static Double allShareIndex(Dictionary<String, StockRepository> stocks)
        {
            Double allShareIndex = 0.0;

            foreach (StockRepository stock in stocks.Values)
            {
                allShareIndex += stock.getPrice();
            }
            return Math.Pow(allShareIndex, 1.0 / stocks.Count());
        }
    }
}
