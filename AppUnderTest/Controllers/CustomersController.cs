using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AppUnderTest.Documents;
using Couchbase.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppUnderTest.Controllers
{
    [ApiController]
    [Route("customers")]
    public class CustomersController : ControllerBase
    {
        private readonly IClusterProvider _clusterProvider;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(IClusterProvider clusterProvider, ILogger<CustomersController> logger)
        {
            _clusterProvider = clusterProvider ?? throw new ArgumentNullException(nameof(clusterProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Get(
            int skip = 0,
            int take = 100,
            CancellationToken cancellationToken = default
        )
        {
            var cluster = await _clusterProvider.GetClusterAsync();

            var queryResult = await cluster.QueryAsync<Customer>(
                "SELECT default.* FROM default WHERE type = 'customer' AND lname IS NOT MISSING ORDER BY lname, fname OFFSET $skip LIMIT $take",
                new Couchbase.Query.QueryOptions()
                    .AdHoc(false)
                    .Parameter("$skip", skip)
                    .Parameter("$take", take)
                    .CancellationToken(cancellationToken));

            var customers = await queryResult.ToListAsync(cancellationToken);

            return Ok(customers);
        }
    }
}
