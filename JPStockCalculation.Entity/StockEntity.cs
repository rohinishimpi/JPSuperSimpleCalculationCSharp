using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace JPStockCalculation.Entity
{
    public abstract class StockEntity
    {
        public string StockSymbol { get; set; }
        public StockType StockType { get; set; }
        public double LastDividend { get; set; }
        public double FixedDividend { get; set; }
        public double ParValue { get; set; }
        public double TickerPrice { get; set; }

        public Dictionary<DateTime, TradeEntity> Trades;

        public abstract double getDividendYield(double price);

        public abstract double getPERatio(double price);
    }
}
