using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOmodels
{
    public class RiverDTO
    {
        private static string _baseURL = "http://localhost:50051/api/river/";

        #region Attributes
        public int ID { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }

        public virtual List<string> BelongsTo { get; set; } = new List<string>();
        #endregion
        #region Constructors
        public RiverDTO(River river)
        {
            ID = river.ID;
            Name = river.Name;
            Length = river.Length;
            river.BelongsTo.ToList().ForEach(c => BelongsTo.Add(_baseURL + c.ID));
        }
        #endregion

    }
}
