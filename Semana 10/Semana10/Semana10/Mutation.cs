﻿using HotChocolate.AspNetCore;
using Semana10.Type;

namespace Semana10
{
    public class Mutation
    {
        private readonly InMemoryDatabase _database;
        public Mutation(InMemoryDatabase database)
        {
            _database = database;
        }
        
        public Produto AddProduto(string name, decimal price)
        {
            int qtd = _database.Produtos.Count;
            var produto = new Produto { Id = qtd + 1, Name = name, Price = price }; 
            _database.Produtos.Add(produto);
            return produto;
        }
        public Customer UpdateEmailCustomer(int id,  string email)
        {
            var customer = _database.Customers.Find(x=> x.Id == id);
            if (customer == null)
            {
                throw new GraphQLRequestException("Cliente não encontrado.");
            }
            customer.Email = email;
            return customer;
        }
    }
}
