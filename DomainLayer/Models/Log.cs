using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Models
{
    public class Log
    {
        #region Attributes
        public int ID { get; private set; }
        public DateTime RequestDate { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        #endregion
        #region Construcotrs
        public Log() { }
        public Log(DateTime tRequestDate, string tMethod, string tPath)
        {
            RequestDate = tRequestDate;
            Method = tMethod;
            Path = tPath;
        }
        #endregion
    }
}
