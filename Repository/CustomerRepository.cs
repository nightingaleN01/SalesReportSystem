using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBContext;
using Entities;
using Interfaces;
using Microsoft.Data.SqlClient;
namespace Repository
{
    

    public class CustomerRepository : ICustomerRepository
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        public CustomerRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

     

        public int Add(Customers customer)
        {
            using var cmd = new SqlCommand(
                @"INSERT INTO Customers (CustomerName, CustomerEmail, CustomerAddress, Region)
              VALUES (@Name, @Email, @Address, @Region)",
                _connection, _transaction);

            cmd.Parameters.AddWithValue("@Name", customer.CustomerName);
            cmd.Parameters.AddWithValue("@Email", customer.CustomerEmail);
            cmd.Parameters.AddWithValue("@Address", customer.CustomerAddress ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Region", customer.Region ?? (object)DBNull.Value);

            return (int)cmd.ExecuteScalar();
        }
    }

}
