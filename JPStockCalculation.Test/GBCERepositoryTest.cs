using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JPStockCalculation.Data;
using System.Collections;
using System.Collections.Generic;
using JPStockCalculation.Entity;
using System.Threading.Tasks;
using System.Threading;

namespace JPStockCalculation.Test
{
    [TestClass]
    public class GBCERepositoryTest
    {
        [TestMethod]
        public void TestallShareIndex()
        {
            Dictionary<string, StockRepository>  stocksDb = new Dictionary<string, StockRepository>();
           // GBCERepository GBCE = new GBCERepository();
            ContextDB contextdB = new ContextDB();
            stocksDb = contextdB.FillStockDbData();
            
            foreach (StockRepository stock in  stocksDb.Values) {
                
            // Record some trades
                for (int i = 1; i <= 2; i++)
                {
                    DateTime dtTimeStamp = DateTime.Now;
                    stock.buy(i, 1.0, dtTimeStamp);
                    Thread.Sleep(1000);
                    dtTimeStamp = DateTime.Now;
                    stock.sell(i, 1.0,dtTimeStamp);
                    Thread.Sleep(1000);
                }
        }
            Double GBCEallShareIndex = GBCERepository.allShareIndex(stocksDb);
            Assert.AreEqual(1.4142135623730951, GBCEallShareIndex);
            
        }
    }
}
