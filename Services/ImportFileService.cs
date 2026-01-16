using Interfaces;
using Microsoft.AspNetCore.Http;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using DataAccess;
namespace Services
{


    public class ImportFileService : IImportFileService
    {
        private readonly ICSVReader _csvReader;
        private readonly string _connectionString;

        public ImportFileService(
            ICSVReader csvReader,
            IConfiguration configuration)
        {
            _csvReader = csvReader;
            _connectionString = configuration?.GetConnectionString("DefaultConnection");
        }
        public async Task ImportFileData(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("CSV file is required");

            using var unitOfWork = new UnitOfWork(_connectionString);

            try
            {
                using var stream = file.OpenReadStream();
                var rows = _csvReader.ReadFile(stream);

                foreach (var row in rows)
                {
                    
                    var categoryId = unitOfWork.Categories.AddAsync(
                        new Category
                        {
                            CategoryId = row.CategoryId,
                            CategoryName=row.CategoryName
                        });

                    
                    var productId = unitOfWork.Products.Add(
                        new Products
                        {
                            ProductId = row.ProductId,
                            ProductName = row.ProductName,
                            CategoryId = row.CategoryId,

                        });

                    var customerId = unitOfWork.Customers.Add(
                        new Customers
                        {
                            CustomerName = row.CustomerName,
                            CustomerEmail = row.CustomerEmail,
                            CustomerAddress = row.CustomerAddress,
                            Region = row.Region
                        });

                    int paymentMethodId = unitOfWork.PaymentMethods.Add(
                        new PaymentMethod
                        {
                            MethodName = row.PaymentMethod.MethodName,
                            PaymentMethodId= row.PaymentMethod.PaymentMethodId
                        });
                    unitOfWork.Orders.Add(
                        new Orders
                        {
                            CustomerId = customerId,
                            ProductId = productId,
                            PaymentMethodId = paymentMethodId,
                            DateOfSale = row.DateOfSale,
                            QuantitySold = row.QuantitySold,
                            UnitPrice = row.UnitPrice,
                            Discount = row.Discount,
                            ShippingCost = row.ShippingCost
                        });
                }

                unitOfWork.SaveChangesAsync();
            }
            catch
            {
                unitOfWork.SaveChangesAsync();
                throw;
            }

        }

    }

}
