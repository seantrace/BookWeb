using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookWeb.Server.Controllers.Utilities
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
#if !DEBUG
    [Authorize]
#endif
    public class FileSystemController : ControllerBase
    {
        // GET: api/<FileSystemController>
        [HttpGet("Drives")]
        public IEnumerable<string> GetDrives()
        {
            List<string> driveNames = new List<string>();
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                driveNames.Add(d.Name);
            }
            return driveNames;
        }

        [HttpGet("Directories/{path}/{file}")]
        public IEnumerable<string> GetDirectories(string path,string file)
        {
            List<string> directories = new List<string>();
            foreach (string d in Directory.GetDirectories(path))
            {
                string dir = d;
                if (System.IO.File.Exists($"{d}\\{file}")) dir += "*";
                directories.Add(dir);
            }
            return directories;
        }
    }
}
