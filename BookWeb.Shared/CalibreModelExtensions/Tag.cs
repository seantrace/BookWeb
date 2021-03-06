using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWeb
{
    public partial record Tag : ICalibreRecord
    {
        [NotMapped]
        public List<Book> Books { get; set; }
        [NotMapped]
        public string Sort
        {
            get
            {
                return Name;
            }
        }
    }
}

