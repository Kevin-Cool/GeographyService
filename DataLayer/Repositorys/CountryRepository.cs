using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainLayer.IRepositorys;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositorys
{
    public class CountryRepository : ICountryRepository
    {
        private readonly GeoContext _context;
        private readonly DbSet<Country> _countries;
        private readonly DbSet<Continent> _continent;

        public CountryRepository(GeoContext context)
        {
            _context = context;
            _countries = context.Countries;
            _continent = context.Continents;
        }
        public Country Add(Country country)
        {
            try
            {
                Continent continent = _continent.Include(c => c.Countries).FirstOrDefault(x => x.ID.Equals(country.Continent_ID));
                if (country is null) throw new ArgumentException("This country's continent douse not exist");
                continent.AddCountry(country);
                _continent.Update(continent);
                _context.SaveChanges();
                return country;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public bool Exists(Country country)
        {
            try
            {
                return _countries.Any(x => x.ID == country.ID) ? true : false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        private IQueryable<Country> Countries => _countries
            .Include(k => k.BelongsTo)
            .Include(c => c.Rivers)
            .Include(c => c.Cities)
            .Include(c => c.Capital);

        public IEnumerable<Country> GetAll(int? id)
        {
            try
            {
                if(id is null) return Countries.ToList();

                return Countries.Where(c => c.BelongsTo.ID.Equals(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public Country GetById(int id)
        {
            try
            {
                return Countries.FirstOrDefault(i => i.ID.Equals(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public void Remove(Country country)
        {
            try
            {
                if (_countries.Contains(country)) { _countries.Remove(country); }
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Update(Country country)
        {
            try
            {
                if (_countries.Contains(country)) { _countries.Update(country); }
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
