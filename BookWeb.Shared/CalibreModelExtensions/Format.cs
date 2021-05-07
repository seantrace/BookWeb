using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWeb
{
    public class Format : ICalibreRecord
    {
        public string Name { get; set; }

        public long Id { get; set; }

        public string Sort
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
            }
        }

        public List<Book> Books { get; set; }
    }
}
