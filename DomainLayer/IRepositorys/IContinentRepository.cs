using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.IRepositorys
{
    public interface IContinentRepository
    {
        Continent Add(Continent continent);
        Continent GetById(int id);
        IEnumerable<Continent> GetAll();
        void Remove(Continent continent);
        void Remove(int id);
        void Update(Continent continent);
        bool Exists(Continent continent);
        bool Exists(int id);
    }
}
