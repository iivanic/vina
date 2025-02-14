using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vina.Server.Classes;

namespace vina.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetProducts")]
        public async Task<IEnumerable<DBProduct>> GetProducts()
        {
            var dBcs = new DBcs.DBcs(Seeder.Instance.ConnStringMyDb);
            var products = await dBcs.RunQueryAsync<DBProduct>(DBProduct.SelectText);
            return products ?? [];
        }
        [HttpGet(Name = "GetProduct")]
        public async Task<DBProduct> GetProduct(int productId)
        {
            var dBcs = new DBcs.DBcs(Seeder.Instance.ConnStringMyDb);
            var product = await dBcs.RunQuerySingleOrDefaultAsync<DBProduct>(DBProduct.SelectSingleText, new P { Id = productId });
            return product ?? new DBProduct(); ;
        }

        class P
        {
            public int Id;
        }
    }
}
