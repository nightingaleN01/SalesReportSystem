using DBContext;
using Interfaces;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System;
using Repository;

namespace DataAccess
{
    public class UnitOfWork: IUnitOfWork,IDisposable
    {
        private readonly SqlConnection _connection;       // Database connection
        private readonly SqlTransaction _transaction;     // Transaction object

        public IReportRepository Reports { get; }

        public ICategoryRepository Categories { get; }
        public IProductRepository Products { get; }
        public ICustomerRepository Customers { get; }
        public IPaymentMethodRepository PaymentMethods { get; }
        public IOrderRepository Orders { get; }

        public UnitOfWork(string connectionString)
        {
            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            _connection = sqlConnection;

            _transaction = sqlConnection.BeginTransaction();
            Categories = new CategoryRepository(_connection, _transaction);
            Products = new ProductRepository(_connection, _transaction);
            Customers = new CustomerRepository(_connection, _transaction);
            PaymentMethods = new PaymentMethodRepository(_connection, _transaction);
            Orders = new OrderRepository(_connection, _transaction);
            Reports = new ReportRepository(_connection, _transaction);  
        }

        public async Task<int> SaveChangesAsync()
        {

            try
            {
                _transaction.Commit();
                return 1;
            }
            catch
            {
                _transaction.Rollback();
                return 0;
            }
            finally
            {
                _transaction.Dispose();
                _connection.Close();
            }

        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }
}
