using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Database;
using WebApplication2.Interfaces;
using WebApplication2.Models;

namespace WebApplication2.Resources
{
    public class RatingResources : RatingInterface
    {
        public readonly Context _context;

        public RatingResources(Context context)
        {
            _context = context;
        }

        public async Task Add(Rating rating)
        {
            await _context.Rating.AddAsync(rating);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Rating rating)
        {
            _context.Rating.Remove(rating);
            await _context.SaveChangesAsync();
        }

        public async Task<Rating> Get()
        {
            return await _context.Rating.FindAsync();
        }

        public async Task<Rating> Get(int id)
        {
            return await _context.Rating.FindAsync(id);
        }

        public async Task<IEnumerable<Rating>> GetAll()
        {
            //return await _context.Rating.ToListAsync();
            return await _context.Rating
                .Include(c => c.user)
                .Include(c => c.product).ToListAsync();
        }

        public async Task Update(Rating rating)
        {
            _context.Rating.Update(rating);
            await _context.SaveChangesAsync();
        }
    }
}
