namespace FrizerskiSalon.Models.DTOs
{
    public class Worker
    {
        public long workerID { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public int jmbg { get; set; }

        public string img { get; set; }

        public long barberShopID { get; set; }


    }
}
