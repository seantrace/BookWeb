using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWeb
{
    public partial record Author : ICalibreRecord
    {
        [NotMapped]
        public List<Book> Books { get; set; }
    }
}
