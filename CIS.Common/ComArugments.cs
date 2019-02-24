using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CIS.Common
{
    public class ComArugments
    {
        private Dictionary<string, object> _paramList;

        /// <summary>
        /// Constructor
        /// </summary>
        public ComArugments()
        {
            _paramList = new Dictionary<string, object>();
        }

        /// <summary>
        /// To Get and Set parameter values
        /// </summary>
        public Dictionary<string, object> ParamList
        {
            get { return _paramList; }
            set { _paramList = value; }
        }

    }
}
