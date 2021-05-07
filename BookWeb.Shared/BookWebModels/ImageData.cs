using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWeb.Shared.BookWebModels
{
    public class ImageData
    {
        public string Url { get; set; }
        public string Data { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float AspectRatio { get; set; }

    }
}
