using BookWeb.Client.Extensions;
using BookWeb.Client.Infrastructure.Managers.Preferences;
using BookWeb.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace BookWeb.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder
                          .CreateDefault(args)
                          .AddRootComponents()
                          .AddClientServices();
            var host = builder.Build();
            var storageService = host.Services.GetRequiredService<PreferenceManager>();
            if (storageService != null)
            {
                CultureInfo culture;
                var preference = await storageService.GetPreference();
                if (preference != null)
                    culture = new CultureInfo(preference.LanguageCode);
                else
                    culture = new CultureInfo("en-US");
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
            }

            builder.Services.AddScoped<ICalibreService, CalibreService>();
            builder.Services.AddScoped<IFileSystemService, FileSystemService>();

            //builder.Services.AddHttpClient<ICalibreService, CalibreService>("CalibreService");//, client =>
            //{
            //    client.BaseAddress = new Uri("https://localhost:5001/api/");
            //});

            //builder.Services.AddHttpClient<IFileSystemService, FileSystemService>("FileSystemService", client =>
            //{
            //    client.BaseAddress = new Uri("https://localhost:5001/api/FileSystem/");
            //});

            builder.Services.AddSingleton<ISimpleStateService, SimpleStateService>();

            await builder.Build().RunAsync();
        }
    }
}