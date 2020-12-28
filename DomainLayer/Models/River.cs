using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainLayer.Models
{
    public class River
    {
        #region Attributes
        public int ID { get; private set; }
        public string Name { get { return _name; } set { SetName(value); } }
        [Required]
        private string _name;
        public double Length { get { return _length; } set { SetLength(value); } }
        [Required]
        private double _length;
        [NotMapped]
        public List<int> BelongsToIDs { get; set; } = new List<int>();
        public virtual ICollection<Country> BelongsTo { get; set; } = new HashSet<Country>();
        #endregion
        #region Constructors
        public River() { }
        public River(string name)
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
        public void SetLength(double Length)
        {
            if (Length < 0) throw new Exception("Length way not be below 0");
            _length = Length;
        }
        #endregion
    }
}
