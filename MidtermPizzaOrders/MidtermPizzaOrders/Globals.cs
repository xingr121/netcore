using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPizzaOrders
{
    public class Globals
    {
        // public static  PizzaOrderContext dbContext;
        private static PizzaOrderContext _dbContextInternal;
        public static PizzaOrderContext dbContextAuto
        {
            get
            {
                if(_dbContextInternal == null)
                {
                    _dbContextInternal = new PizzaOrderContext();
                }
                return _dbContextInternal;
            }
        }

    }
}
