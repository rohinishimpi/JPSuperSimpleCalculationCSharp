using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPStockCalculation.Entity;
using JPStockCalculation.Data;

namespace JPStockCalculation.Facade
{
    public interface IStockCalculationFacade
    {
        void DoSimpleStockCalculations(double dMarketPrice);
        //double CalculateDividendYield(double price, StockRepository stockRep);
        //double CalculatePERatio(double price, StockRepository stockRep);
        //void RecordTrades(StockRepository stockRep, int Count);
        //Double CalculateGBCEAllShareIndex();
    }
}
