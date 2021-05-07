using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWeb
{
    public class Library
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int BookCount { get; set; }
        public List<long> UserAccess { get; set; }
    }

}
