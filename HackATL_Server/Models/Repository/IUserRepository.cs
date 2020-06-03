using System;
using System.Collections.Generic;
using HackATL_Server.Models.Model;

namespace HackATL_Server.Models.Repository
{
    public interface IUserRepository
    {
        void Add(User user);
        void Update(User user);
        User Remove(string key);
        User Get(string id);
        IEnumerable<User> GetAll();
    }
}
