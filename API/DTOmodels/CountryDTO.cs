using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOmodels
{
    public class CountryDTO
    {
        private static string _baseURL = "http://localhost:50051/api/continent/";

        #region Attributes
        public int ID { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public double Suface { get; set; }
        public List<string> Capital { get; set; } = new List<string>();
        public List<string> Cities { get; set; } = new List<string>();
        public string BelongsTo { get; set; }
        public virtual List<string> Rivers { get; set; } = new List<string>();
        #endregion
        #region Constructor
        public CountryDTO(Country country)
        {
            ID = country.ID;
            Name = country.Name;
            Population = country.Population;
            Suface = country.Suface;
            country.Capital.ForEach(c => Capital.Add(_baseURL+ country.BelongsTo.ID + "/country/"+ID+ "/city/" + c.ID));
            country.Cities.ForEach(c => Cities.Add(_baseURL + country.BelongsTo.ID + "/country/" + ID + "/city/" + c.ID));
            BelongsTo = _baseURL + country.BelongsTo.ID;
            country.Rivers.ToList().ForEach(r => Rivers.Add("http://localhost:50051/api/river/" + r.ID));
        }
        #endregion
    }
}
