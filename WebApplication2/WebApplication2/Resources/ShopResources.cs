using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Database
{
    public class ShopResources : ShopInterface
    {
        public readonly Context _context;

        public ShopResources(Context context)
        {
            _context = context;
        }

        public async Task Add(Shop shop)
        {
            await _context.Shop.AddAsync(shop);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Shop shop)
        {
            _context.Shop.Remove(shop);
            await _context.SaveChangesAsync();
        }

        public async Task<Shop> Get()
        {
            return await _context.Shop.FindAsync();
        }

        public async Task<Shop> Get(int id)
        {
            return await _context.Shop.FindAsync(id);
        }

        public async Task<IEnumerable<Shop>> GetAll()
        {
            //return await _context.Shop.ToListAsync();
            return await _context.Shop

               .Include(c => c.sells).ThenInclude(c => c.shop)
               .Include(c => c.sells).ThenInclude(c => c.product)

               /*
                //orders
                .Include(c => c.orders).ThenInclude(c => c.user)
                .Include(c => c.orders).ThenInclude(c => c.product)
                .Include(c => c.orders).ThenInclude(c => c.shop)*/

               .ToListAsync();

        }

        public async Task<Shop> GetID(string name)
        {
            return await _context.Shop.FirstOrDefaultAsync(x => x.name == name);
        }


        public async Task Update(Shop shop)
        {
            _context.Shop.Update(shop);
            await _context.SaveChangesAsync();
        }


    }
}
