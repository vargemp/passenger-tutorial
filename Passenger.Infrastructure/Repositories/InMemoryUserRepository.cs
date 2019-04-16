using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;

namespace Passenger.Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private static ISet<User> _users = new HashSet<User>
        {
            new User("user1@gmail.com", "user1", "secret", "salt"),
            new User("user2@gmail.com", "user2", "secret", "salt"),
            new User("user3@gmail.com", "user3", "secret", "salt")
        }; 
        public User Get(Guid id)
        {
            return _users.Single(u => u.Id == id);
        }

        public User Get(string email)
        {
            return _users.Single(u => u.Email == email.ToLowerInvariant());
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public void Add(User user)
        {
            _users.Add(user);
        }

        public void Update(User user)
        {
        }

        public void Remove(Guid id)
        {
            var user = Get(id);
            _users.Remove(user);
        }
    }
}
