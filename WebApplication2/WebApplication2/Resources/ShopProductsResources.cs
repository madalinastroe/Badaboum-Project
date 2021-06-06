using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Database
{
    public class ShopProductsResources : ShopProductsInterface
    {

        public readonly Context _context;

        public ShopProductsResources(Context context)
        {
            _context = context;
        }

        public async Task Add(ShopProducts product)
        {
            await _context.ShopProducts.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(ShopProducts product)
        {
            _context.ShopProducts.Remove(product);
            await _context.SaveChangesAsync();
        }


        public async Task<ShopProducts> Get()
        {
            return await _context.ShopProducts.FindAsync();
        }

        public async Task<ShopProducts> Get(int id)
        {
            return await _context.ShopProducts.FindAsync(id);
        }

        public async Task<IEnumerable<ShopProducts>> GetAll()
        {
            //return await _context.ShopProducts.ToListAsync();
            return await _context.ShopProducts
                .Include(c => c.shop)
                .Include(c => c.product).
                ToListAsync();
        }


        public async Task Update(ShopProducts product)
        {
            _context.ShopProducts.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task<ShopProducts> getByShopAndProduct(int shopID, int productID)
        {
            //let email be user identifier(as username)
            return await _context.ShopProducts.FirstOrDefaultAsync(x => x.shopID == shopID && x.productID == productID);
        }


    }
}
