namespace BookingManager.Models
{
    public class SearchDTO
    {
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public int NoOfAdult { get; set; }
            public int NoOfChildren { get; set; }
            public int NoOfRooms { get; set; }
            public string State { get; set; }
            public string Country { get; set; }
    }
}
