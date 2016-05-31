using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPStockCalculation.Entity;
using JPStockCalculation.Data;
using System.Collections;
using System.Data;
using System.Data.Sql;
using System.Diagnostics;

namespace JPStockCalculation.Facade
{
    /// <summary>
    /// This Class is a Facade Class which acts as a main System for carrying out Stock Calculations.
    /// It internally interfaces the subsystems of Stocks, Trades to do various mentioned calculations.
    /// </summary>
    public class StockCalculationFacade:IStockCalculationFacade
    {        
       // TradeRepository trade = null;
        GBCERepository gbce = null;
        ContextDB contextdB = null;
        Dictionary<String, StockRepository> stocksDb = null;
        EventLog appLog = null;

        public StockCalculationFacade()
        {           
           // trade = new TradeRepository();
            gbce = new GBCERepository();
            contextdB = new ContextDB();
            stocksDb = new Dictionary<string, StockRepository>();
            stocksDb = contextdB.FillStockDbData();
             appLog = new EventLog();
             appLog.Source = "Application";
        }

        /// <summary>
        /// Main method which does all the Stock Calulations.
        /// This uses Facade Design pattern which is main System which interfaces all sub systems like Stocks, Trade to do the Stock Calculations.
        /// </summary>
        /// <param name="dMarketPrice"></param>
        public void DoSimpleStockCalculations(double dMarketPrice)
        {
            int i = 1;
            try
            {
                foreach (StockRepository stock in this.stocksDb.Values)
                {
                    double dividendYield = CalculateDividendYield(dMarketPrice, stock);
                    appLog.WriteEntry("Divivdend Yield for "+stock.StockSymbol+ " is " + dividendYield);

                     double PERatio = CalculatePERatio(dMarketPrice, stock);
                     appLog.WriteEntry("PE Ratio for " + stock.StockSymbol + " is " + PERatio);


                    RecordTrades(stock, i);
                    appLog.WriteEntry("Stock: " + stock.StockSymbol + " price: $" + stock.getPrice());

                    double VolumeWeightedStockPrice = CalculateVolumeWeightedStockPrice(stock);
                    appLog.WriteEntry("Volume Weighted StockPrice is " + VolumeWeightedStockPrice);               

                    i++;
                }

                double GBCEAllShareIndex = CalculateGBCEAllShareIndex();
                appLog.WriteEntry("GBCE AllShareIndex is " + GBCEAllShareIndex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured during Simple Stock Calcutations" + ex);

            }
        }

        /// <summary>
        /// Calculates Dividend Yield
        /// </summary>
        /// <returns>double</returns>
        private double CalculateDividendYield(double price, StockRepository stockRep)
        {
            double dDividendYield = 0;

            try
            {
               
                    dDividendYield = stockRep.getDividendYield(price);
                
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while calculating Dividend Yield" + ex);
            }
            return dDividendYield;
        }

        /// <summary>
        /// Calculates PE Ratio
        /// </summary>
        /// <returns>double</returns>
        private double CalculatePERatio(double price,StockRepository stockRep)
        {
            double dPERatio = 0;
            try
            {
                dPERatio = stockRep.getPERatio(price);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while calculating PE Ratio" + ex);
            }
            return dPERatio;
        }

       /// <summary>
       /// Calculates GBCE AllShareIndex
       /// </summary>
       /// <returns>double</returns>
        private Double CalculateGBCEAllShareIndex()
        {
            Dictionary<String, StockRepository> stocks = null;
            stocks = this.stocksDb;
            double dGbceAllSharesIndex = 0.0;
            try
            {
                dGbceAllSharesIndex = gbce.allShareIndex(stocks);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while calculating GBCE All Shares Index" + ex);
            }
            return dGbceAllSharesIndex;
        }

        

        
        private void RecordTrades(StockRepository stockRep, int count)
        {
           try
            {
                stockRep.RecordTrades(stockRep, count);                   
            }
            catch(Exception ex)
            {
                throw new Exception("Error occured while recording trades" + ex);
            }            
        }

        private Double CalculateVolumeWeightedStockPrice(StockRepository stockRep)
        {
            double dVolumeWeightedStockPrice = 0.0;
            try
            {
                dVolumeWeightedStockPrice = stockRep.calculateVolumeWeightedStockPrice();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while calculating Volume Weighted Stock Price" + ex);
            }
            return dVolumeWeightedStockPrice;
        }
        
 
    }
}
