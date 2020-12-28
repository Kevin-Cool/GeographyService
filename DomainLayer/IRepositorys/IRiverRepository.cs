using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.IRepositorys
{
    public interface IRiverRepository
    {
        River Add(River river);
        River GetById(int id);
        IEnumerable<River> GetAll();
        void Remove(River river);
        void Remove(int id);
        void Update(River river);
        bool Exists(River river);
        bool Exists(int id);
    }
}
