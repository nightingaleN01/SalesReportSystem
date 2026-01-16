namespace Interfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        ICustomerRepository Customers { get; }
        IPaymentMethodRepository PaymentMethods { get; }
        IOrderRepository Orders { get; }
        public Task<int> SaveChangesAsync();
        public void Dispose();
    }
}
