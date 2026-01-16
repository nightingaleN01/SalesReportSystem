using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace SalesReportSystem.Controllers
{
    [ApiController]
    [Route("api/sales")]
    public class ImportFileController : Controller
    {


        private readonly IImportFileService _importService;

        public ImportFileController(IImportFileService importService)
        {
            _importService = importService;
        }

        // POST api/sales/import
        [HttpPost("import")]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("CSV file is required.");

            await _importService.ImportFileData(file);

            return Ok("File imported successfully.");
        }
    }
}
