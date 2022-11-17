using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Tests
{
    public abstract class QueryTestBase : IDisposable
    {
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