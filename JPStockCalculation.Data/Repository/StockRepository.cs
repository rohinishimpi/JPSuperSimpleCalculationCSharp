using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPStockCalculation.Entity;
using System.Data;
using System.Diagnostics;
using System.Threading;

namespace JPStockCalculation.Data
{
    public class StockRepository : StockEntity
    {
        EventLog appLog = null;

        public StockRepository(String symbol, StockType type, double lastDividend, Double fixedDividend, Double parValue)
        {
            this.StockSymbol = symbol;
            this.StockType = type;
            this.LastDividend = lastDividend;
            this.FixedDividend = fixedDividend;
            this.ParValue = parValue;
            this.Trades = new Dictionary<DateTime, TradeEntity>();
            appLog = new EventLog();
            appLog.Source = "Application";
        }

       
        /// <summary>
        /// This method calculates the Dividend Yield for a given stock for given market price
        /// Formula for Coomon Type 
        //(Last Dividend)/(Market Price)
        /// Formula For Preffered
        //(Fixed Dividend .Par Value)/(Market Price)
        /// </summary>
        /// <param name="price">double</param>
        /// <returns>double</returns>
        public override double getDividendYield(double price)
        {
            switch (this.StockType)
            {
                case StockType.COMMON:
                    return this.LastDividend / price;
                case StockType.PREFERRED:
                    return this.FixedDividend * this.ParValue / price;
                default:
                    return 0.0;
            }
        }


        /// <summary>
        /// This method calculates the PE Ratio for a given stock for given market price.
        /// //Fomula provided
        /// (Market Price)/Dividend
        /// </summary>
        /// <param name="price">double</param>
        /// <returns>Double</returns>
        public override double getPERatio(double price)
        {
            double peRatio = 0.0;
            peRatio = price / this.LastDividend;
            return peRatio;
        }

        /// <summary>
        /// This method records the trade bought.
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="price"></param>
        /// <param name="dtTimeStamp"></param>
        public void buy(int quantity, Double price,DateTime dtTimeStamp)
        {
            TradeRepository trade = new TradeRepository(TradeType.BUY, quantity, price);
            this.Trades.Add(dtTimeStamp, trade);
        }

        /// <summary>
        /// This method records the sell trade.
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="price"></param>
        /// <param name="dtTimeStamp"></param>
        public void sell(int quantity, Double price, DateTime dtTimeStamp)
        {
            TradeRepository trade = new TradeRepository(TradeType.SELL, quantity, price);
            this.Trades.Add(dtTimeStamp, trade);
        }

        /// <summary>
        /// This gets the price of latest Trademeans last trade.
        /// </summary>
        /// <returns>Double</returns>
        public Double getPrice()
        {
            if (this.Trades.Count() > 0)
            {
                var last = this.Trades.Last().Value;
                return last.Price;
            }
            else
            {
                return 0.0;
            }
        }

        /// <summary>
        /// This method is used to generate some dummy trades.
        /// </summary>
        /// <returns></returns>
         public DataTable GenerateTradePrice()
        {
            DataTable dtTradeBuySellPrice = new DataTable();
            dtTradeBuySellPrice.Columns.Add(new DataColumn("BuyPrice"));
            dtTradeBuySellPrice.Columns.Add(new DataColumn("SellPrice"));
            DataRow dr1 = dtTradeBuySellPrice.NewRow();
            dr1["BuyPrice"] = 5.89;
            dr1["SellPrice"] = 5.00;
            dtTradeBuySellPrice.Rows.Add(dr1);
            DataRow dr2 = dtTradeBuySellPrice.NewRow();
            dr2["BuyPrice"] = 10.00;
            dr2["SellPrice"] = 5.00;
            dtTradeBuySellPrice.Rows.Add(dr2);
            // DataRow dr3= dtTradeBuySellPrice.NewRow();
            //dr3["BuyPrice"] = 100.00;
            //dr3["SellPrice"] = 50.00;
            //dtTradeBuySellPrice.Rows.Add(dr3);
            // DataRow dr4 = dtTradeBuySellPrice.NewRow();
            //dr4["BuyPrice"] = 20.00;
            //dr4["SellPrice"] = 25.00;
            //dtTradeBuySellPrice.Rows.Add(dr4);
            // DataRow dr5 = dtTradeBuySellPrice.NewRow();
            //dr5["BuyPrice"] = 5.89;
            //dr5["SellPrice"] = 5.00;
           // dtTradeBuySellPrice.Rows.Add(dr5);
           
            return dtTradeBuySellPrice;
        }
         public async  void RecordTrades(StockEntity stock, int count)
         {            
             DataTable dtTradePrice = GenerateTradePrice();
             int i = count;
             foreach (DataRow row in dtTradePrice.Rows)
             {
                 DateTime dtTimeStamp = DateTime.Now;
                 buy(i, Convert.ToDouble(row["BuyPrice"].ToString()), dtTimeStamp);
                 appLog.WriteEntry("TimeStamp : " + dtTimeStamp.ToString() + " | Stock: " + stock.StockSymbol + " | Share Quantity: " + i + " | Buy Price: " + row["BuyPrice"].ToString());
                 await Task.Delay(1000);

                 dtTimeStamp = DateTime.Now;
                 sell(i, Convert.ToDouble(row["SellPrice"].ToString()), dtTimeStamp);
                 appLog.WriteEntry("TimeStamp : " + dtTimeStamp.ToString() + " |Stock: " + stock.StockSymbol + " | Share Quantity: " + i + " | Sell Price: " + row["SellPrice"].ToString());
                 await Task.Delay(1000);
             }
         }
        /// <summary>
        /// This method calculated Volume weighted stock price for given stock
        /// Formula provided
         /// (∑_i▒〖 Trade 〖Price〗_i×〖Quantity〗_i 〗)/(∑_i▒〖Quantity〗_i )
        /// </summary>
         /// <returns>Double</returns>
        public Double calculateVolumeWeightedStockPrice()
        {                    
            DateTime Time15mins = DateTime.Now.AddMinutes(-15);           
            Double volumeWeigthedStockPrice = 0.0;           
            int totalQuantity = 0;
            List<DateTime> keys = new List<DateTime>(this.Trades.Keys);
           
            foreach (DateTime key in keys)
                {
                    if (key > Time15mins)
                    {
                        totalQuantity += this.Trades[key].SharesQuantity;
                        volumeWeigthedStockPrice += this.Trades[key].Price * this.Trades[key].SharesQuantity;
                    }
            }
            return volumeWeigthedStockPrice / totalQuantity;
        }

    }
}
