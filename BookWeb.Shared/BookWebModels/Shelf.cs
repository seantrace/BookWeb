using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWeb.Shared.BookWebModels
{
    public enum ShelfType
    {
        User,
        Read,
        Download,
        Archive
    }

    public class Shelf
    {
        public long Id { get; set; }
        public long LibraryId { get; set; }
        public ShelfType Type { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public long OwnerUserId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public List<long> BookIds { get; set; }
    }
}
