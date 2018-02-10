using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03Strategy
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class MyDb : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
    }

    /// <summary>
    /// Az EntityFramework amikor először példányosítja a MyDb-t, 
    /// megnézi, hogy van-e mellette DbConfiguration leszármazott osztály.
    /// Ha van, akkor használja konfigurációként.
    /// </summary>
    public class MyConfig : DbConfiguration
    {
        public MyConfig()
        {
            Console.WriteLine("MyConfig");
            SetExecutionStrategy("System.Data.SqlClient", 
                ()=>new MyStrategy(5, TimeSpan.FromSeconds(30)));
        }
    }

    public class MyStrategy : SqlAzureExecutionStrategy
    {
        public MyStrategy(int maxRetryCount, TimeSpan maxDelay)
            : base(maxRetryCount, maxDelay)
        { }

        protected override TimeSpan? GetNextDelay(Exception lastException)
        {
            var retval = base.GetNextDelay(lastException);
            Console.WriteLine("MyStrategy.GetNextDelay: {0}", retval.ToString());

            return retval;
        }

        protected override bool ShouldRetryOn(Exception exception)
        {
            var isShouldRetry = false;
            isShouldRetry = base.ShouldRetryOn(exception);

            var sqlException = exception as SqlException;

            if (sqlException != null)
            {
                foreach (SqlError se in sqlException.Errors)
                {
                    Console.WriteLine("MyStrategy.ShouldRetryOn ErrorNumber: {0}, isShouldRetry: {1}", se.Number, isShouldRetry);
                    ///Itt kiegészíthetjük az eredeti kezelés további hibaszámokra
                    ///
                    if (se.Number == -1)
                    {
                        isShouldRetry = true;
                    }
                }
            }

            return isShouldRetry;
        }

    }
}
