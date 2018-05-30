using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class Genre
    {
        #region Genre Parameter
        public string GenreName { get; set; }
        #endregion

        public Genre ( string Name)
        {
            GenreName = Name;
        }
    }
}
