using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SalonApp.SharedKernel;

namespace SalonApp.Modules.Services.Domain
{
    public class Service : Entity
    {
        public string Name { get; set; } = string.Empty;
        public int DurationInMinutes { get; set; }
        public decimal Price { get; set; }
    }
}
