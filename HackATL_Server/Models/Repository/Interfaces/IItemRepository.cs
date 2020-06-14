using System;
using System.Collections.Generic;
using HackATL_Server.Models.Model;

namespace HackATL_Server.Models.Repository
{
    public interface IItemRepository
    {
        void Add(Item item);
        void Update(Item item);
        Item Remove(string key);
        Item Get(string id);
        IEnumerable<Item> GetAll();
    }
}
