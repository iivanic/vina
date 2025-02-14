using Microsoft.AspNetCore.Mvc;

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
        public IEnumerable<WeatherForecast> GetOrders(string token, string language)
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = "Freezing"
            })
            .ToArray();
        }
        [HttpGet(Name = "GetOrder")]
        public WeatherForecast GetOrder(int orderId, string language)
        {
            return new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = "Freezing"
            };
        }
        [HttpGet(Name = "CreateOrder")]
        public WeatherForecast CreateOrder(List<ShoppingBagItem> products, string language)
        {
            return new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = "Freezing"
            };
        }
        [HttpGet(Name = "CancelOrder")]
        public WeatherForecast CancelOrder(int orderId, string language)
        {
            return new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = "Freezing"
            };
        }
    }
}
