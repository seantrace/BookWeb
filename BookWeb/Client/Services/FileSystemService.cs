using BookWeb.Client.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace BookWeb.Client.Services
{
    public class FileSystemService : IFileSystemService
    {
        private readonly HttpClient httpClient;

        public FileSystemService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<string>> GetDirectories(string path,string file)
        {
            path = Uri.EscapeUriString(path);
            return await ApiHelpers.MakeRequestAsync<object, List<string>>(httpClient, "GET", $"api/FileSystem/Directories/{path}/{file}");
        }

        public async Task<List<string>> GetDrives()
        {
            return await ApiHelpers.MakeRequestAsync<object, List<string>>(httpClient, "GET", "api/FileSystem/Drives");
        }
    }
}
