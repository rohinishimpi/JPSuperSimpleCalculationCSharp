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
        
    }
}
