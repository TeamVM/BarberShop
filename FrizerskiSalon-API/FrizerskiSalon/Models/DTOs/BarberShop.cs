using Nest;

namespace FrizerskiSalon.Models.DTOs
{
    public class BarberShop
    {

        public long barberShopID { get; set; }
        public string name { get; set; }

        public string street { get; set; }

        public string city { get; set; }

        public string owner { get; set; }

        public GeoLocation location { get; set; }
    }
}
