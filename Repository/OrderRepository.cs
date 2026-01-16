using DBContext;
using Entities;
using Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Data.SqlClient;

namespace Repository
{

    public class OrderRepository : IOrderRepository
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        public OrderRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public void Add(Orders order)
        {
            using var cmd = new SqlCommand(
                @"INSERT INTO Orders
              (CustomerId, ProductId, PaymentMethodId, DateOfSale,
               QuantitySold, UnitPrice, Discount, ShippingCost)
              VALUES
              (@CustomerId, @ProductId, @PaymentMethodId, @DateOfSale,
               @QuantitySold, @UnitPrice, @Discount, @ShippingCost)",
                _connection, _transaction);

            cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
            cmd.Parameters.AddWithValue("@ProductId", order.ProductId);
            cmd.Parameters.AddWithValue("@PaymentMethodId", order.PaymentMethodId);
            cmd.Parameters.AddWithValue("@DateOfSale", order.DateOfSale);
            cmd.Parameters.AddWithValue("@QuantitySold", order.QuantitySold);
            cmd.Parameters.AddWithValue("@UnitPrice", order.UnitPrice);
            cmd.Parameters.AddWithValue("@Discount", order.Discount);
            cmd.Parameters.AddWithValue("@ShippingCost", order.ShippingCost);

            cmd.ExecuteNonQuery();
        }
    }


}
