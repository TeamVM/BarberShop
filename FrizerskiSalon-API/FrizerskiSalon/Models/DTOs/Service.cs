namespace FrizerskiSalon.Models.DTOs
{
    public class Service
    {
        public long serviceID { get; set; }
        public string name { get; set; }
        public int duration { get; set; }
        public float price { get; set; }

        public long barberShopID { get; set; }
    }
}
