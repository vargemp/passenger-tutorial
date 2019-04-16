using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Core.Domain
{
    public class User
    {
        protected User()
        {
        }
        public User(string email, string username,
            string password, string salt)
        {
            Id = Guid.NewGuid();
            Email = email;
            Password = password;
            Username = username;
            Salt = salt;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string Username { get; protected set; }    
        public string FullName { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

    }
}
