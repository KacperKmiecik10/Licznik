using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Licznik.Models
{
    public class LicznikModel
    {
        public string _counterName { get; set; }
        public int _defaultValue { get; set; }
        public int _currentValue { get; set; }
        public string _color { get; set; }

        public LicznikModel(){}

        public LicznikModel(string counterName, int defaultValue, int currentValue, string color)
        {
            _counterName = counterName;
            _defaultValue = defaultValue;
            _currentValue = currentValue;
            _color = color;
        }
    }
}
