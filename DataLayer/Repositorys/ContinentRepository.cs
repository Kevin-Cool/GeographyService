using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainLayer.IRepositorys;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositorys
{
    public class ContinentRepository : IContinentRepository
    {
        private readonly GeoContext _context;
        private readonly DbSet<Continent> _continents;

        public ContinentRepository(GeoContext context)
        {
            _context = context;
            _continents = context.Continents;
        }
        public Continent Add(Continent continent)
        {
            try
            {
                _continents.Add(continent);
                _context.SaveChanges();
                return continent;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public bool Exists(Continent continent)
        {
            try
            {
                return _continents.Any(x => x.ID == continent.ID) ? true : false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public IEnumerable<Continent> GetAll()
        {
            try
            {
                return _continents.Include(k => k.Countries).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public Continent GetById(int id)
        {
            try
            {
                return _continents.Include(k => k.Countries).FirstOrDefault(i => i.ID.Equals(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public void Remove(Continent continent)
        {
            try
            {
                if (_continents.Contains(continent)) { _continents.Remove(continent); }
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Update(Continent continent)
        {
            try
            {
                if (_continents.Contains(continent)) { _continents.Update(continent); }
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
