using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrizerskiSalon.Infrastructure.Models
{
    internal class Term
    {
        public long termID { get; set; }

        public long barberShopID { get; set; }

        public DateTime dateTime { get; set; }

        public TimeOnly startTime { get; set; }

        public TimeOnly endTime { get; set; }

        public long userId { get; set; }

        public long serviceID { get; set; }

        public bool busy { get; set; }

        public long workerID { get; set; }
    }
}
