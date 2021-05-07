using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWeb.Shared.BookWebModels
{
    public class AuthorData
    {
        public string Name { get; set; }
        public List<ImageData> Images { get; set; }
        public List<AuthorPageData> Pages { get; set; }
        public string BioHtml { get; set; }
    }
}
