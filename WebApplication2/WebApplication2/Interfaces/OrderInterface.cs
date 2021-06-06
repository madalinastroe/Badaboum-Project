using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Database
{
    public interface OrderInterface
    {
        public Task<Order> Get();

        public Task<Order> Get(int id);

        public Task Add(Order order);

        public Task Update(Order order);

        public Task Delete(Order order);

        public Task<IEnumerable<Order>> GetAll(int userID);


    }
}
