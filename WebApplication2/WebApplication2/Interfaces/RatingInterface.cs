using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Interfaces
{
    public interface RatingInterface
    {
        public Task<Rating> Get();


        public Task<Rating> Get(int id);

        public Task Add(Rating rating);

        public Task Update(Rating rating);

        public Task Delete(Rating rating);

        public Task<IEnumerable<Rating>> GetAll();
        //Task<Rating> GetComposite(int idProduct, int idUser);
    }
}
