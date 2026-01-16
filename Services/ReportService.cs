using DataAccess;
using Entities;
using Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ReportService : IReportService
    {
        private readonly string _connectionString;

        public ReportService(IConfiguration config)
        {
            _connectionString = config["ConnectionStrings:DefaultConnection"];
        }

        public List<ProductResultDto> GetTopProductsOverall(
            DateTime startDate, DateTime endDate, int topN)
        {
            using var uow = new UnitOfWork(_connectionString);
            return uow.Reports.GetTopProductsOverall(startDate, endDate, topN);
        }

        public List<ProductResultDto> GetTopProductsByCategory(
            int categoryId, DateTime startDate, DateTime endDate, int topN)
        {
            using var uow = new UnitOfWork(_connectionString);
            return uow.Reports.GetTopProductsByCategory(categoryId, startDate, endDate, topN);
        }

        public List<ProductResultDto> GetTopProductsByRegion(
            string region, DateTime startDate, DateTime endDate, int topN)
        {
            using var uow = new UnitOfWork(_connectionString);
            return uow.Reports.GetTopProductsByRegion(region, startDate, endDate, topN);
        }
    }

}
