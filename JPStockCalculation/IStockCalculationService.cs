using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using JPStockCalculation.Entity;
using JPStockCalculation.Facade;
using JPStockCalculation.Data;

namespace JPStockCalculation
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IStockCalculationService" in both code and config file together.
    [ServiceContract]
    public interface IStockCalculationService
    {
        //[OperationContract]
        //void DoWork();

        [OperationContract]
        void DoSimpleStockCalculations();
        
    }
}
