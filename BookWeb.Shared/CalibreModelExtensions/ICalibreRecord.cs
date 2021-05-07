using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWeb
{
    public interface ICalibreRecord
    {
        public long Id { get;}

        public string Name { get; set; }

        public string Sort { get; }

        public List<Book> Books { get; set; }
    }
}
