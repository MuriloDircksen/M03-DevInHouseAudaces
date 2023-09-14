using Semana10.Type;

namespace Semana10
{
    public class InMemoryDatabase
    {
        public List<Produto> Produtos { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Order> Orders { get; set; }

        public InMemoryDatabase()
        {
            Produtos = new List<Produto>
            {
                new Produto { Id = 1, Name = "Notebook", Price = 2500},
                new Produto { Id = 2, Name = "Iphone", Price = 5700},
                new Produto { Id = 3, Name = "Mouse", Price = 120}
            };
            Customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "João Silva", Email= "js@mail.com" },
                new Customer { Id = 2, Name = "Jessica Pereira", Email = "jp@mail.com"}
            };
            Orders = new List<Order>
            {
                new Order { Id = 1, CustomerId = 1, Date = DateTime.Now },
                new Order { Id = 2, CustomerId = 1, Date = DateTime.Now },
                new Order { Id = 3, CustomerId = 2, Date = DateTime.Now }
            };
            
        }
    }
}
