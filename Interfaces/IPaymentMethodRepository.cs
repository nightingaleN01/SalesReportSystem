using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IPaymentMethodRepository
    {
        public int Add(PaymentMethod paymentMethod);
    }
}
