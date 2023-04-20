using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrizerskiSalon.Core.Models
{
    public class Term
    {
        public Guid TermID { get; set; }

        public Guid BarberShopID { get; set; }

        public DateTime DateTime { get; set; }

        public TimeOnly StartTime { get; set; }

        public Guid? UserId { get; set; }

        public Guid? ServiceID { get; set; }

        public bool Busy { get; set; }

        public Guid WorkerID { get; set; }

        public int Xmin { get; set; }
    }
}
