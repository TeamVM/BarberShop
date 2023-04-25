namespace FrizerskiSalon.Models.DTOs
{
    public class ServiceDTO
    {
        public Guid ServiceID { get; set; }
        public string? Name { get; set; }
        public int Duration { get; set; }
        public float Price { get; set; }

        public Guid BarberShopID { get; set; }
    }
}
