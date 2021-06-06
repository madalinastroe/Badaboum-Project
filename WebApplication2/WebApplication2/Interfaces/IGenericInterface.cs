using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Database
{
    public interface IGenericInterface
    {
        public Task<User> Get();

        public Task<User> GetByEmail(string email);

        public Task<User> GetByAccount(string username, string password);

        public Task<User> Get(int id);

        public Task Add(User user);

        public Task Update(User user);

        public Task Delete(User user);

        public Task<IEnumerable<User>> GetAll();
    }
}
