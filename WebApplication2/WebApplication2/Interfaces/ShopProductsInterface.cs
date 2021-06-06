using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Database
{
    public interface ShopProductsInterface
    {
        public Task<ShopProducts> Get();

        public Task<ShopProducts> Get(int id);

        public Task Add(ShopProducts shopProducts);

        public Task Update(ShopProducts shopProducts);

        public Task Delete(ShopProducts shopProducts);

        public Task<IEnumerable<ShopProducts>> GetAll();

        public Task<ShopProducts> getByShopAndProduct(int shopID, int productID);

    }
}
