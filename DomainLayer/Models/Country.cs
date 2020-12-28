using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace DomainLayer.Models
{
    public class Country
    {
        #region Attributes
        public int ID { get; private set; }
        public int Continent_ID { get;  set; }
        public string Name { get { return _name; } set { SetName(value); } }
        [Required]
        private string _name;
        public int Population { get { return _population; } set { SetPopulation(value); } }
        private int _population;
        public double Suface { get { return _suface; } set { SetSuface(value); } }
        private double _suface;
        public List<City> Capital { get; set; } = new List<City>();
        public List<City> Cities { get; set; }  = new List<City>();
        public Continent BelongsTo { get;set; }
        public virtual ICollection<River> Rivers { get; set; } = new HashSet<River>();
        #endregion

        #region Constructors
        public Country() { }
        public Country(string name) 
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
        public void SetPopulation(int population)
        {
            if (Cities.Sum(c => c.Population) > population) throw new Exception("The total population may not be below the total population of all cities");
            if (population < 0) throw new Exception("Population way not be below 0");
            _population = population;
        }
        public void SetSuface(double suface)
        {
            if (suface < 0) throw new Exception("Suface way not be below 0");
            _suface = suface;
        }
        public bool Exists(string name)
        {
            return Cities.Any(c => c.Name.Equals(name));
        }
        public void AddCapital(City city)
        {
            if (Cities.Contains(city)) throw new Exception("The capital needs to be a city in this country");
            Capital.Add(city);
        }
        public void RemoveCapital(City city)
        {
            if (!Capital.Contains(city)) throw new Exception("This city is not a capital");
            Capital.Remove(city);
        }
        public ReadOnlyCollection<City> GetAllCapitals()
        {
            return new ReadOnlyCollection<City>(Cities);
        }
        public City GetCityById(int id)
        {
            return Cities.FirstOrDefault(c => c.ID.Equals(id));
        }
        public ReadOnlyCollection<City> GetAllCities()
        {
            return new ReadOnlyCollection<City>(Cities);
        }
        public void AddCity(City city)
        {
            city.BelongsTo = this;
            Cities.Add(city);
        }
        #endregion
    }
}
