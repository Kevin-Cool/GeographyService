using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainLayer.Models
{
    public class City
    {
        #region Attributes
        public int ID { get; private set; }
        public int Country_ID { get; set; }
        public string Name { get { return _name; } set { SetName(value); } }
        [Required]
        private string _name;
        public int Population { get { return _population; } set { SetPopulation(value); } }
        [Required]
        private int _population;
        public Country BelongsTo { get; set; }
        #endregion
        #region Constructors
        public City() { }
        public City(string name)
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
            if (population < 0) throw new Exception("Population way not be below 0");
            _population = population;
        }
        #endregion
    }
}
