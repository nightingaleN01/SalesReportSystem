using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBContext;
using Entities;
using Interfaces;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;


namespace Repository
{


    public class ProductRepository : IProductRepository
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        public ProductRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public int Add(Products product)
        {
            using var cmd = new SqlCommand(
                @"INSERT INTO Products (ProductName, CategoryId)
              OUTPUT INSERTED.ProductId
              VALUES (@Name, @CategoryId)",
                _connection, _transaction);

            cmd.Parameters.AddWithValue("@Name", product.ProductName);
            cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);

            return (int)cmd.ExecuteScalar();
        }
    }


}
