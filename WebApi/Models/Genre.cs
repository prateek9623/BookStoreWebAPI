using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
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
