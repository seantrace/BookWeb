using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookWeb.Client.Services
{
    public interface ISimpleStateService
    {
        public Author Author { get; set; }

        public Series Series { get; set; }
    }
}
