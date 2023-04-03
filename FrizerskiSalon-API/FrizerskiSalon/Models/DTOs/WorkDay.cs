namespace FrizerskiSalon.Models.DTOs
{
    public class WorkDay
    {
        public long workDayId { get; set; }

        public DateTime dateTime { get; set; }

        public TimeOnly startTime { get; set; }

        public TimeOnly endTime { get; set; }

        public double barberShopID { get; set; }
    }
}
