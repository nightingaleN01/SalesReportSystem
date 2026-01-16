using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Orders
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int PaymentMethodId { get; set; }

        public DateTime DateOfSale { get; set; }
        public int QuantitySold { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal ShippingCost { get; set; }

        
        public Category Category { get; set; }
        public Customers Customer { get; set; }
        public Products Product { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }

    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string MethodName { get; set; }
    }
       
    //public enum PaymentMethodId
    //{
    //    Cash=0,
    //    Gpay=1,
    //    CreditCard=2
    //}

}
