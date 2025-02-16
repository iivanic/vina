using System.Threading.Tasks;
using DBcs;
using Microsoft.AspNetCore.Mvc;
using vina.Server.Models;

namespace vina.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IDBcs _dBcs;
        public ProductsController(ILogger<ProductsController> logger, IDBcs dBcs)
        {
            _dBcs = dBcs;
            _logger = logger;
        }
        [HttpGet("{lang:alpha:minlength(2):maxlength(2)}")]
        public async Task<IEnumerable<TranslatedProduct>> GetProducts(string lang)
        {
            var products = await _dBcs.RunQueryAsync<TranslatedProduct>(TranslatedProduct.SelectText, lang);
            return products ?? [];
        }
        [HttpGet("{lang:alpha:minlength(2):maxlength(2)}/{productId:int:min(1)}")]
        public async Task<TranslatedProduct?> GetProduct(int productId, string lang)
        {
            var product = await _dBcs.RunQuerySingleOrDefaultAsync<TranslatedProduct>(
                TranslatedProduct.SelectSingleText,
                new { Id = productId, Lang = lang }
            );
            return product;
        }
    }
}
