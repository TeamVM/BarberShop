using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrizerskiSalon.Infrastructure.Models
{
    public class DMService
    {
        public Guid ServiceID { get; set; }
        public string? Name { get; set; }
        public int Duration { get; set; }
        public float Price { get; set; }

        public Guid BarberShopID { get; set; }
    }
}
