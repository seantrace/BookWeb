using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookWeb
{
    public record GetBookListResponseDto
    {
        public int PageSize { get; set; }

        public int CurrentPage { get; init; }

        public int TotalItems { get; init; }

        public int TotalPages { get; init; }

        public long FetchTime { get; set; }

        public List<Book> Items { get; init; }
    }
}
