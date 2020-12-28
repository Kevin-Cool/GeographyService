using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.IRepositorys
{
    public interface ICityRepository
    {
        City AddCity(City city);
        City AddCapital(City city);
        City GetById(int continentid, int countryid,int id);
        IEnumerable<City> GetAll(int? id);
        void Remove(City city);
        void Remove(int continentid, int countryid, int id);
        void Update(City city);
        bool Exists(City city);
        bool Exists(int id);
    }
}
