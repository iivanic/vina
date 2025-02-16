using Microsoft.AspNetCore.Mvc;
using vina.Server.Models;

namespace vina.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {

        private readonly ILogger<OrdersController> _logger;

        public OrdersController(ILogger<OrdersController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetOrders")]
        public IEnumerable<DBOrder> GetOrders(string token, string language)
        {
            return new List<DBOrder>();
        }
        [HttpGet(Name = "GetOrder")]
        public DBOrder GetOrder(int orderId, string language)
        {
            return new DBOrder();
        }
        [HttpGet(Name = "CreateOrder")]
        public DBOrder CreateOrder(List<ShoppingBagItem> products, string language)
        {
            return new DBOrder();
        }
        [HttpGet(Name = "CancelOrder")]
        public DBOrder CancelOrder(int orderId, string language)
        {
            return new DBOrder();
        }
    }
}
