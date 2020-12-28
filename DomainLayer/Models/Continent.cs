using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace DomainLayer.Models
{
    public class Continent
    {
        #region Attributes
        public int ID { get; private set; }
        public string Name {get{ return _name; }set{ SetName(value); } }
        [Required]
        private string _name;
        public int Population {  get{ return GetPopulation(); } }
        public List<Country> Countries { get; set; }  = new List<Country>();
        #endregion

        #region Constructors
        public Continent() { }
        public Continent(string name)
        {
            SetName(name);
        }
        #endregion

        #region Methodes
        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new Exception("Name way not be empty");
            _name = name;
        }
        public int GetPopulation()
        {
            return Countries.Sum(c => c.Population);
        }
        public Country GetCountryByName(string name)
        {
            return Countries.FirstOrDefault(c => c.Name.Equals(name));
        }
        public Country GetCountryById(int id)
        {
            return Countries.FirstOrDefault(c => c.ID.Equals(id));
        }
        public ReadOnlyCollection<Country> GetAllCountries()
        {
            return new ReadOnlyCollection<Country>(Countries);
        }
        public bool Exists(string name)
        {
            return Countries.Any(c => c.Name.Equals(name));
        }
        public void AddCountry(Country country)
        {
            if (Exists(country.Name)) throw new Exception("Country with this name already exists");
            Countries.Add(country);
        }
        #endregion
    }

}
