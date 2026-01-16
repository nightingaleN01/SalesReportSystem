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



    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        public PaymentMethodRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public int Add(PaymentMethod paymentMethod)
        {
            using var cmd = new SqlCommand(
                @"INSERT INTO PaymentMethods (MethodName)
              OUTPUT INSERTED.PaymentMethodId
              VALUES (@Name)",
                _connection, _transaction);

            cmd.Parameters.AddWithValue("@Name", paymentMethod.MethodName);

            return (int)cmd.ExecuteScalar();
        }
    }


}
