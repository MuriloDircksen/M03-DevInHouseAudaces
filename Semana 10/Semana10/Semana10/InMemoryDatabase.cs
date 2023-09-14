using Semana10.Type;

namespace Semana10
{
    public class InMemoryDatabase
    {
        public List<Produto> Produtos { get; set; }

        public InMemoryDatabase()
        {
            Produtos = new List<Produto>
            {
                new Produto { Id = 1, Name = "Notebook", Price = 2500},
                new Produto { Id = 2, Name = "Iphone", Price = 5700},
                new Produto { Id = 3, Name = "Mouse", Price = 120}
            };
        }
    }
}
