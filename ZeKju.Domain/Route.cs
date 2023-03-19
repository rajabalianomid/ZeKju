namespace ZeKju.Domain
{
    public class Route : BaseEntity<int>
    {
        public Route()
        {
            Flights = new HashSet<Flight>();
        }
        public int OriginCityId { get; set; }
        public int DestinationCityId { get; set; }
        public DateTime DepartureDate { get; set; }

        public ICollection<Flight> Flights { get; set; }
    }
}