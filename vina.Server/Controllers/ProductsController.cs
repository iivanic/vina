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
        [HttpGet("{lang:alpha:minlength(2):maxlength(2)}")]
        public async Task<IEnumerable<DBProduct>> GetProducts(string lang)
        {
            var dBcs = new DBcs.DBcs(Seeder.Instance.ConnStringMyDb);
            var products = await dBcs.RunQueryAsync<DBProduct>(DBProduct.SelectText,  "hr");
            return products ?? [];
        }
        [HttpGet("{lang:alpha:minlength(2):maxlength(2)}/{productId:int:min(1)}")]
        public async Task<DBProduct?> GetProduct(int productId, string lang)
        {
            var dBcs = new DBcs.DBcs(Seeder.Instance.ConnStringMyDb);
            var product = await dBcs.RunQuerySingleOrDefaultAsync<DBProduct>(DBProduct.SelectSingleText, new LangIdParam { Id = productId, Lang = lang  });
            return product ;
        }

        class LangIdParam
        {
            public int Id=0;
            public string Lang="hr";
        }
        class LangParam
        {
            public string Lang="hr";
        }
    }
}
