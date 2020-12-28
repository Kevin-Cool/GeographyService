using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOmodels
{
    public class ContinentDTO
    {
        private static string _baseURL = "http://localhost:50051/api/continent/";

        #region Attributes
        public int ID { get; set; }
        public string Name { get; set;}
        public int Population { get; set; }
        public List<string> Countries { get; set; } = new List<string>();
        #endregion
        public ContinentDTO(Continent continent)
        {
            ID = continent.ID;
            Name = continent.Name;
            Population = continent.Population;
            continent.Countries.ForEach(c => Countries.Add(_baseURL+ID+"/country/"+ c.ID));
        }
        #region Constructor

        #endregion

    }
}
