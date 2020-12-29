using DomainLayer.IRepositorys;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repositorys
{
    public class LogRepository : ILogRepository
    {
        private readonly GeoContext _context;
        private readonly DbSet<Log> _logs;

        public LogRepository(GeoContext context)
        {
            _context = context;
            _logs = context.Logs;
        }
        public void Add(Log log)
        {
            try
            {
                _logs.Add(log);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
