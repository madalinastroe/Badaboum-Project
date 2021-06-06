using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Database
{
    public interface ShopInterface
    {
        public Task<Shop> Get();

        public Task<Shop> Get(int id);

        public Task Add(Shop shop);

        public Task Update(Shop shop);

        public Task Delete(Shop shop);

        public Task<IEnumerable<Shop>> GetAll();

        public Task<Shop> GetID(string name);



    }
}
