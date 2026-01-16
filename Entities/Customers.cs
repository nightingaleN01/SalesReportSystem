using System.Security.Principal;

namespace Entities
{
    public class Customers
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public string Region {get; set;}
    }
}
