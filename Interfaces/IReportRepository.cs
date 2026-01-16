using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IReportRepository
    {
        List<ProductResultDto> GetTopProductsOverall(
            DateTime startDate,
            DateTime endDate,
            int topN);

        List<ProductResultDto> GetTopProductsByCategory(
            int categoryId,
            DateTime startDate,
            DateTime endDate,
            int topN);

        List<ProductResultDto> GetTopProductsByRegion(
            string region,
            DateTime startDate,
            DateTime endDate,
            int topN);
    }

}
