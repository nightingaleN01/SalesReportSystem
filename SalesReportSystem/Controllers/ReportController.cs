using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace SalesReportSystem.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpGet("top-products/overall")]
        public IActionResult Overall(DateTime from, DateTime to, int noOfProducts = 10)
            => Ok(_service.GetTopProductsOverall(from, to, noOfProducts));

        [HttpGet("top-products/category")]
        public IActionResult ByCategory(int categoryId, DateTime from, DateTime to, int noOfProducts = 10)
            => Ok(_service.GetTopProductsByCategory(categoryId, from, to, noOfProducts));

        [HttpGet("top-products/region")]
        public IActionResult ByRegion(string region, DateTime from, DateTime to, int noOfProducts = 10)
            => Ok(_service.GetTopProductsByRegion(region, from, to, noOfProducts));
    }

}
