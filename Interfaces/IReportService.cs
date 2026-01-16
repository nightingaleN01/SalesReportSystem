using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IReportService
    {
        List<ProductResultDto> GetTopProductsOverall(
            DateTime startDate, DateTime endDate, int topProducts);

        List<ProductResultDto> GetTopProductsByCategory(
            int categoryId, DateTime startDate, DateTime endDate, int topProducts);

        List<ProductResultDto> GetTopProductsByRegion(
            string region, DateTime startDate, DateTime endDate, int topProducts);
    }

}
