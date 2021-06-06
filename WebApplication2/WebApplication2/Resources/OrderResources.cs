using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Database
{
    public class OrderResources : OrderInterface
    {

        public readonly Context _context;

        public OrderResources(Context context)
        {
            _context = context;
        }

        public async Task Add(Order order)
        {
            await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Order order)
        {
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> Get()
        {
            return await _context.Order.FindAsync();
        }

        public async Task<Order> Get(int id)
        {
            return await _context.Order.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAll(int userID)
        {
            //return await _context.Order.ToListAsync();

            return await _context.Order
              .Include(c => c.user)
              .Include(c => c.product)
              .Include(c => c.shop)
              .Where(x => x.user.Id == userID).
              ToListAsync();
        }

        

        public async Task Update(Order order)
        {
            _context.Order.Update(order);
            await _context.SaveChangesAsync();
        }


    }
}
