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
        void Update(River river);
        bool Exists(River river);
    }
}
