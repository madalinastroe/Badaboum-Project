using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Database
{
    public class ProductResources : ProductInterface
    {
        public readonly Context _context;

        public ProductResources(Context context)
        {
            _context = context;
        }

        public async Task Add(Product order)
        {
            await _context.Product.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Product order)
        {
            _context.Product.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> Get()
        {
            return await _context.Product.FindAsync();
        }

        public async Task<Product> Get(int id)
        {
            return await _context.Product.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
             return await _context.Product
                //rating table
                .Include(c=>c.ratings).ThenInclude(c => c.user)
                .Include(c => c.ratings).ThenInclude(c => c.product)

                //orders

                .Include(c => c.ordersP).ThenInclude(c => c.user)
                //.Include(c => c.ordersP).ThenInclude(c => c.product)
                //.Include(c => c.ordersP).ThenInclude(c => c.shop)

                //shop products table
                .Include(c=>c.purchasedProducts).ThenInclude(c=>c.shop)
                .Include(c=>c.purchasedProducts).ThenInclude(c=>c.product).ToListAsync();
            //return await _context.Product.ToListAsync();
        }


        public async Task Update(Product order)
        {
            _context.Product.Update(order);
            await _context.SaveChangesAsync();
        }

        //=====================================================================================
        public async Task<Product> GetPicture(string name)
        {
            return await _context.Product.FirstOrDefaultAsync(x => x.name == name);
        }

    }
}
