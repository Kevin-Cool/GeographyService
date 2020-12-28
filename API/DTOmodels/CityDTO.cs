using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOmodels
{
    public class CityDTO
    {
        private static string _baseURL = "http://localhost:50051/api/continent/";

        #region Attributes
        public int ID { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public string BelongsTo { get; set; }
        #endregion
        #region Constructors
        public CityDTO(City city)
        {
            ID = city.ID;
            Name = city.Name;
            Population = city.Population;
            BelongsTo = _baseURL + city.BelongsTo.BelongsTo.ID + "/country/" + city.BelongsTo.ID;
        }
        #endregion
    }
}
