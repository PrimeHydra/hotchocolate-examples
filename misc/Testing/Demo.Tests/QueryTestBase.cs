using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Tests
{
    public abstract class QueryTestBase : IDisposable
    {
        /// <summary>
        /// Use a separate service scope for each test class instance
        /// so that the test can change data without affecting other
        /// tests being run in parallel.
        /// </summary>
        private readonly IServiceScope serviceScope;

        protected QueryTestBase()
        {
            this.serviceScope = TestServices.Services.CreateScope();
        }

        public void Dispose()
        {
            this.serviceScope?.Dispose();
        }

        protected async Task<string> ExecuteQueryAsync(string query)
        {
            return await TestServices.ExecuteRequestAsync(
                b => b.SetQuery(
                    query),
                this.serviceScope);
        }
    }
}