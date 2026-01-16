using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using System.Data;
using Entities;

namespace Repository
{
   

    public class ReportRepository : IReportRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public ReportRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public List<ProductResultDto> GetTopProductsOverall(
            DateTime startDate,
            DateTime endDate,
            int topN)
        {
            var result = new List<ProductResultDto>();

            using var cmd = _connection.CreateCommand();
            cmd.Transaction = _transaction;
            cmd.CommandText = @"
            SELECT TOP (@TopN)
                p.ProductId,
                p.ProductName,
                SUM(o.QuantitySold) AS TotalQuantitySold
            FROM Orders o
            JOIN Products p ON o.ProductId = p.ProductId
            WHERE o.DateOfSale BETWEEN @StartDate AND @EndDate
            GROUP BY p.ProductId, p.ProductName
            ORDER BY TotalQuantitySold DESC";

            AddParam(cmd, "@TopN", topN);
            AddParam(cmd, "@StartDate", startDate);
            AddParam(cmd, "@EndDate", endDate);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new ProductResultDto
                {
                    ProductId = (int)reader["ProductId"],
                    ProductName = reader["ProductName"].ToString(),
                    TotalQuantitySold = Convert.ToInt32(reader["TotalQuantitySold"])
                });
            }

            return result;
        }

        public List<ProductResultDto> GetTopProductsByCategory(
            int categoryId,
            DateTime startDate,
            DateTime endDate,
            int topN)
        {
            var result = new List<ProductResultDto>();

            using var cmd = _connection.CreateCommand();
            cmd.Transaction = _transaction;
            cmd.CommandText = @"
            SELECT TOP (@TopN)
                p.ProductId,
                p.ProductName,
                SUM(o.QuantitySold) AS TotalQuantitySold
            FROM Orders o
            JOIN Products p ON o.ProductId = p.ProductId
            WHERE p.CategoryId = @CategoryId
              AND o.DateOfSale BETWEEN @StartDate AND @EndDate
            GROUP BY p.ProductId, p.ProductName
            ORDER BY TotalQuantitySold DESC";

            AddParam(cmd, "@TopN", topN);
            AddParam(cmd, "@CategoryId", categoryId);
            AddParam(cmd, "@StartDate", startDate);
            AddParam(cmd, "@EndDate", endDate);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new ProductResultDto
                {
                    ProductId = (int)reader["ProductId"],
                    ProductName = reader["ProductName"].ToString(),
                    TotalQuantitySold = Convert.ToInt32(reader["TotalQuantitySold"])
                });
            }

            return result;
        }

        public List<ProductResultDto> GetTopProductsByRegion(
            string region,
            DateTime startDate,
            DateTime endDate,
            int topN)
        {
            var result = new List<ProductResultDto>();

            using var cmd = _connection.CreateCommand();
            cmd.Transaction = _transaction;
            cmd.CommandText = @"
            SELECT TOP (@TopN)
                p.ProductId,
                p.ProductName,
                SUM(o.QuantitySold) AS TotalQuantitySold
            FROM Orders o
            JOIN Products p ON o.ProductId = p.ProductId
            JOIN Customers c ON o.CustomerId = c.CustomerId
            WHERE c.Region = @Region
              AND o.DateOfSale BETWEEN @StartDate AND @EndDate
            GROUP BY p.ProductId, p.ProductName
            ORDER BY TotalQuantitySold DESC";

            AddParam(cmd, "@TopN", topN);
            AddParam(cmd, "@Region", region);
            AddParam(cmd, "@StartDate", startDate);
            AddParam(cmd, "@EndDate", endDate);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new ProductResultDto
                {
                    ProductId = (int)reader["ProductId"],
                    ProductName = reader["ProductName"].ToString(),
                    TotalQuantitySold = Convert.ToInt32(reader["TotalQuantitySold"])
                });
            }

            return result;
        }

        private static void AddParam(IDbCommand cmd, string name, object value)
        {
            var p = cmd.CreateParameter();
            p.ParameterName = name;
            p.Value = value;
            cmd.Parameters.Add(p);
        }
    }

}
