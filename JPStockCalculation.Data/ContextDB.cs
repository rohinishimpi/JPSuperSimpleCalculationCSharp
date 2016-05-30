using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPStockCalculation.Entity;

namespace JPStockCalculation.Data
{
    public class ContextDB
    {

        public Dictionary<String, StockRepository> FillStockDbData()
        {
             Dictionary<String, StockRepository> db=null;
            try
            {
                db= new Dictionary<String, StockRepository>();
                db.Add("TEA", new StockRepository("TEA", StockType.COMMON, 0.0, 0.0, 100.0));
                db.Add("POP", new StockRepository("POP", StockType.COMMON, 8.0, 0.0, 100.0));
                db.Add("ALE", new StockRepository("ALE", StockType.COMMON,23.0, 0.0, 60.0));
                db.Add("GIN", new StockRepository("GIN", StockType.PREFERRED, 8.0, 0.2, 100.0));
                db.Add("JOE", new StockRepository("JOE", StockType.COMMON, 13.0, 0.0, 250.0));
            }
            catch(Exception ex)
            {
                throw new Exception("Error occured while getting Stock Data in context" + ex);
            }
            return db;
        }
    }
}
