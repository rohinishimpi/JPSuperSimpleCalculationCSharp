using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JPStockCalculation.Entity;
using System.Threading;
using JPStockCalculation.Data;

namespace JPStockCalculation.Test
{
    [TestClass]
    public class StockTest
    {
        [TestMethod]
        public void testGetDividendYield()
        {
            StockRepository stockALE = new StockRepository("ALE", StockType.COMMON, 23.0, 0.0, 60.0);
            StockRepository stockGIN = new StockRepository("GIN", StockType.PREFERRED, 8.0, 0.2, 100.0);
            // Test dividend for Common
            Double dividendALE = stockALE.getDividendYield(1.0);
            Double expectedDividendALE = stockALE.LastDividend / 1.0;
            Assert.AreEqual(expectedDividendALE, dividendALE, 0.0);
            // Test dividend for Preferred
            Double dividendGIN = stockGIN.getDividendYield(1.0);
            Double expectedDividendGIN = stockGIN.FixedDividend * stockGIN.ParValue / 1.0;
            Assert.AreEqual(expectedDividendGIN, dividendGIN, 0.0);
        }

        [TestMethod]
        public void testPERatio()
        {
            StockRepository stockALE = new StockRepository("ALE", StockType.COMMON, 23.0, 0.0, 60.0);
            Double peRatioALE = stockALE.getPERatio(1.0);
            Double expectedPeRatioALE = 1.0 / stockALE.LastDividend;
            Assert.AreEqual(expectedPeRatioALE, peRatioALE, 0.0);
        }

        [TestMethod]
        public void testBuy()
        {
            StockRepository stockALE = new StockRepository("ALE", StockType.COMMON, 23.0, 0.0, 60.0);
            string strdt = "30/05/2016 12:00 am";
            DateTime dt = Convert.ToDateTime(strdt);
            stockALE.buy(1, 10.0, dt);
            Assert.AreEqual(10.0, stockALE.getPrice(), 0.0);
        }

        [TestMethod]
        public void testSell()
        {
            StockRepository stockALE = new StockRepository("ALE", StockType.COMMON, 23.0, 0.0, 60.0);
            string strdt = "30/05/2016 12:00 am";
            DateTime dt = Convert.ToDateTime(strdt);
            stockALE.sell(1, 10.0, dt);
            Assert.AreEqual(10.0, stockALE.getPrice(), 0.0);
        }

        [TestMethod]
        public void testGetPrice()
        {
            StockRepository stockALE = new StockRepository("ALE", StockType.COMMON, 23.0, 0.0, 60.0);
            string strdt = "30/05/2016 12:00 am";
            DateTime dt = Convert.ToDateTime(strdt);
            stockALE.sell(1, 10.0, dt);
            dt = dt.AddSeconds(30);
            stockALE.buy(1, 11.0, dt);
            Assert.AreEqual(11.0, stockALE.getPrice(), 0.0);
        }

        [TestMethod]
        public void testCalculateVolumeWeightedStockPrice()
        {
            StockRepository stockALE = new StockRepository("ALE", StockType.COMMON, 23.0, 0.0, 60.0);
           // string strdt = "30/05/2016 12:00 am";
            DateTime dt = DateTime.Now;// Convert.ToDateTime(strdt);
            
            stockALE.sell(1, 10.0, dt);
            dt = dt.AddSeconds(30);
            Thread.Sleep(1000);
            dt = DateTime.Now;
            stockALE.buy(1, 10.0, dt);
            Double volumeWeightedStockPrice = stockALE.calculateVolumeWeightedStockPrice();
            Assert.AreEqual(10.0, volumeWeightedStockPrice, 0.0);
           
            DateTime Time15mins = DateTime.Now.AddMinutes(-15);

            stockALE.Trades.Add(Time15mins, new TradeRepository(TradeType.BUY, 1, 20.0));
            
            volumeWeightedStockPrice = stockALE.calculateVolumeWeightedStockPrice();
            Assert.AreEqual(10.0, volumeWeightedStockPrice, 0.0);
        }
    }
}
