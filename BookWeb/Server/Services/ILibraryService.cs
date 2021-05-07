using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookWeb.Server.Services
{
    public interface ILibraryService
    {
        List<Library> Libraries { get;}
        Library GetLibrary(long Id);
        Task<Library> AddLibrary(Library library);
        Task<Library> UpdateLibrary(Library library);
        Task<bool> DeleteLibrary(long Id);
    }
}
