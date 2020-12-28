using DomainLayer.IRepositorys;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Repositorys
{
    public class CityRepository : ICityRepository
    {
        private readonly GeoContext _context;
        private readonly DbSet<City> _cities;
        private readonly DbSet<Country> _countries;

        public CityRepository(GeoContext context)
        {
            _context = context;
            _cities = context.Cities;
            _countries = context.Countries;
        }
        public City AddCity(City city)
        {
            try
            {
                Country country = _countries.Include(c => c.Cities).Include(c => c.Capital).FirstOrDefault(x => x.ID.Equals(city.Country_ID));
                if (country is null) throw new ArgumentException("This city's Country douse not exist");
                country.AddCity(city);
                _countries.Update(country);
                _context.SaveChanges();
                return city;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public City AddCapital(City city)
        {
            try
            {
                Country country = _countries.Include(c => c.Cities).Include(c => c.Capital).FirstOrDefault(x => x.ID.Equals(city.Country_ID));
                if (country is null) throw new ArgumentException("This city's Country douse not exist");
                country.AddCapital(city);
                _countries.Update(country);
                _context.SaveChanges();
                return city;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public bool Exists(City city) => this.Exists(city.ID); 
        public bool Exists(int id)
        {
            try
            {
                return _cities.Any(x => x.ID == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        private IQueryable<City> Cities => _cities
            .Include(k => k.BelongsTo)
            .ThenInclude(k => k.BelongsTo);

        public IEnumerable<City> GetAll(int? id)
        {
            try
            {
                if (id is null) return Cities.ToList();

                return Cities.Where(c => c.BelongsTo.ID.Equals(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public City GetById(int continentid, int countryid, int id)
        {
            try
            {
                return Cities.FirstOrDefault(i => i.ID.Equals(id) && i.BelongsTo.ID.Equals(countryid) && i.BelongsTo.BelongsTo.ID.Equals(continentid));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public void Remove(int continentid, int countryid, int id) => this.Remove(GetById(continentid, countryid, id));
        public void Remove(City city)
        {
            try
            {
                if (_cities.Contains(city)) { _cities.Remove(city); }
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Update(City city)
        {
            try
            {
                if (_cities.Contains(city)) { _cities.Update(city); }
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
