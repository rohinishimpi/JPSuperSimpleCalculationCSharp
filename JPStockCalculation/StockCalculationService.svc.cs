using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
//using JPStockCalculation.Data;
using JPStockCalculation.Facade;

namespace JPStockCalculation
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "StockCalculationService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select StockCalculationService.svc or StockCalculationService.svc.cs at the Solution Explorer and start debugging.
    public class StockCalculationService : IStockCalculationService
    {       

        public void DoSimpleStockCalculations()
        {
            try
            {
                StockCalculationFacade stockCalculation = new StockCalculationFacade();
                //Given Market Price
                double dGivenMarketPrice = 12.1;
                stockCalculation.DoSimpleStockCalculations(dGivenMarketPrice);
            }
            catch(Exception ex)
            {
                throw new Exception("Error occured while running service for Simple Stock Calcutations" + ex);
            }
            
        }
    }
}
