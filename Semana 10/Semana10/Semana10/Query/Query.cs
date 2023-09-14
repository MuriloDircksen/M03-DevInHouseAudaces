
using Semana10.Type;

namespace Semana10.Query
{
    public class Query
    {
        private readonly InMemoryDatabase _database;
        public Query(InMemoryDatabase database)
        {
            _database = database;
        }

        public Produto GetProduto(int id)
        {
            return _database.Produtos.Find(x => x.Id == id);    
        }

        public List<Produto> GetProdutos()
        {
            return _database.Produtos;
        }
    }
}
