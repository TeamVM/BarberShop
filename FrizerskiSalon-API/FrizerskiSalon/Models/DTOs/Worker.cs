namespace FrizerskiSalon.Models.DTOs
{
    public class Worker
    {
        public Guid WorkerId { get; set; }
        public string Name { get; set; }

        public string Img { get; set; }

        public Guid BarberShopID { get; set; }
    }
}
