using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using HackATL_Server.Models.Model;

namespace HackATL_Server.Models.Repository
{
    public class UserRepository : IUserRepository
    {
        private static ConcurrentDictionary<string, User> users =
            new ConcurrentDictionary<string, User>();
        public UserRepository()
        {
        }

        public IEnumerable<User> GetAll()
        {
            return users.Values;
        }
        public void Add(User user)
        {
            user.Id = Guid.NewGuid().ToString();
            users[user.Id] = user;
        }

        public User Get(string id)
        {
            users.TryGetValue(id, out User user);
            return user;
        }

        public User Remove(string id)
        {
            users.TryRemove(id, out User user);
            return user;
        }
        public void Update(User user)
        {
            users[user.Id] = user;
        }
        
    }

    
}
