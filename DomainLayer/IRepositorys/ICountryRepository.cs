using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.IRepositorys
{
    public interface ICountryRepository
    {
        Country Add(Country country);
        Country GetById(int id);
        IEnumerable<Country> GetAll(int? id);
        void Remove(Country country);
        void Update(Country country);
        bool Exists(Country country);
    }
}
