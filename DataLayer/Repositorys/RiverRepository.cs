using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainLayer.IRepositorys;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositorys
{
    public class RiverRepository : IRiverRepository
    {
        private readonly GeoContext _context;
        private readonly DbSet<River> _rivers;
        private readonly DbSet<Country> _countries;

        public RiverRepository(GeoContext context)
        {
            _context = context;
            _rivers = context.Rivers;
            _countries = context.Countries;
        }
        public River Add(River river)
        {
            try
            {
                if (river.BelongsTo.Count.Equals(0)) throw new ArgumentException("This river belongs nowhere");
                _rivers.Add(river);
                _context.SaveChanges();
                return river;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public bool Exists(River river) => this.Exists(river.ID);
        public bool Exists(int id)
        {
            try
            {
                return _rivers.Any(x => x.ID == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public IEnumerable<River> GetAll()
        {
            try
            {
                return _rivers.Include(k => k.BelongsTo).ThenInclude(b => b.BelongsTo).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public River GetById(int id)
        {
            try
            {
                return _rivers.Include(k => k.BelongsTo).ThenInclude(b => b.BelongsTo).FirstOrDefault(i => i.ID.Equals(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public void Remove(int id) => this.Remove(GetById(id));
        public void Remove(River river)
        {
            try
            {
                if (_rivers.Contains(river)) { _rivers.Remove(river); }
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Update(River river)
        {
            try
            {
                if (_rivers.Contains(river)) { _rivers.Update(river); }
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
