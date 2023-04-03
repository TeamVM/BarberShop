namespace FrizerskiSalon.Models.DTOs
{
    public class Term
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
