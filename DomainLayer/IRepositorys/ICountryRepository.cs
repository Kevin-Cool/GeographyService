using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.IRepositorys
{
    public interface ICountryRepository
    {
        Country Add(Country country);
        Country GetById(int continentid,int countryid);
        Country GetById(int countryid);
        IEnumerable<Country> GetAll(int? id);
        void Remove(Country country);
        void Remove(int continentid, int countryid);
        void Update(Country country);
        bool Exists(Country country);
        bool Exists(int id);
    }
}
