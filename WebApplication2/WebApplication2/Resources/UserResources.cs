using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Database
{
    public class UserResources : IGenericInterface
    {

        public readonly Context _context;

        public UserResources(Context context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(User user)
        {
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> Get()
        {
            return await _context.User.FindAsync();
        }

        public async Task<User> Get(int id)
        {
            return await _context.User.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            //return await _context.User.ToListAsync();
            return await _context.User
               .Include(c => c.ratedProducts).ThenInclude(c => c.product)
               .Include(c => c.ratedProducts).ThenInclude(c => c.user)
               .Include(c => c.ordersU).ThenInclude(c => c.user)
               //.Include(c => c.ordersU).ThenInclude(c => c.product)
               //.Include(c => c.ordersU).ThenInclude(c => c.shop)
               .ToListAsync();

        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.email == email
                ); 
        }

        public async Task Update(User user)
        {
            _context.User.Update(user);
            await _context.SaveChangesAsync();
        }


        public async Task<User> GetByAccount(string username, string password)
        {
            //let email be user identifier(as username)
            return await _context.User.FirstOrDefaultAsync(x => x.email == username && x.password == password);
        }
    }
}
