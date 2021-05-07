using JsonFlatFileDataStore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookWeb.Server.Services
{
    public class LibraryService : ILibraryService
    {
        IDocumentCollection<Library> libraryCollection;

        public LibraryService(IWebHostEnvironment env)
        {
            DataStore libraryDataStore = new DataStore($"{env.ContentRootPath}\\LibraryData.json");
            libraryCollection = libraryDataStore.GetCollection<Library>();
        }
        public List<Library> Libraries
        {
            get
            {
                return libraryCollection.AsQueryable().ToList();
            }
        }

        public async Task<Library> AddLibrary(Library library)
        {
            var id = libraryCollection.GetNextIdValue();
            library.Id = id;
            await libraryCollection.InsertOneAsync(library);
            return library;
        }

        public Library GetLibrary(long Id)
        {
            Library lib = libraryCollection.AsQueryable()
                                    .First(l => l.Id == Id);
            return lib;
        }

        public async Task<Library> UpdateLibrary(Library library)
        {
            bool result = await libraryCollection.UpdateOneAsync(library.Id,library);
            if (result)
            {
                return library;
            } else
            {
                return null;
            }
        }

        public async Task<bool> DeleteLibrary(long Id)
        {
            return await libraryCollection.DeleteOneAsync(Id);
        }
    }
}
