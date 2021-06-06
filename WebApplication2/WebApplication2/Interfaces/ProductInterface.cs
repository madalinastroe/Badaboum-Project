using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Database
{
    public interface ProductInterface
    {
        public Task<Product> Get();

        public Task<Product> Get(int id);

        public Task Add(Product product);

        public Task Update(Product product);

        public Task Delete(Product product);

        public Task<IEnumerable<Product>> GetAll();

        public Task<Product> GetPicture(string name);

    }
}
