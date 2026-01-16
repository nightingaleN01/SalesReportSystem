using DBContext;
using Entities;
using Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Repository
{

    public class CategoryRepository : ICategoryRepository
    {
         private readonly SqlConnection _connection;       // Database connection
        private readonly SqlTransaction _transaction;

        public CategoryRepository(SqlConnection dbConnection,SqlTransaction dbTransaction)
        {
            _connection = dbConnection;
            _transaction = dbTransaction;
        }
        public int AddAsync(Category category)
        {
            using var cmd = _connection.CreateCommand();
            cmd.Transaction = _transaction;
            cmd.CommandText =
                @"INSERT INTO Category (CategoryName)
              OUTPUT INSERTED.CategoryId
              VALUES (@Name)";

            var param = cmd.CreateParameter();
            param.ParameterName = "@Name";
            param.Value = category.CategoryName;
            cmd.Parameters.Add(param);

            return Convert.ToInt32(cmd.ExecuteScalar());

        }

    }

}
