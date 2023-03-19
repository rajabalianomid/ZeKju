using System.Globalization;
using ZeKju.Domain;

namespace ZeKju.Data.Extensions
{
    public static class CSVExtensions
    {
        const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        const string DateFormat = "yyyy-MM-dd";

        public static Flight StringToFlight(this string value)
        {
            string[] values = value.Split(',');
            return new Flight
            {
                Id = int.Parse(values[0]),
                RouteId = int.Parse(values[1]),
                DepartureTime = DateTime.ParseExact(values[2], DateTimeFormat, CultureInfo.InvariantCulture),
                ArrivalTime = DateTime.ParseExact(values[3], DateTimeFormat, CultureInfo.InvariantCulture),
                AirlineId = int.Parse(values[4])
            };
        }
        public static Route StringToRoute(this string value)
        {
            string[] values = value.Split(',');
            return new Route
            {
                Id = int.Parse(values[0]),
                OriginCityId = int.Parse(values[1]),
                DestinationCityId = int.Parse(values[2]),
                DepartureDate = DateTime.ParseExact(values[3], DateFormat, CultureInfo.InvariantCulture)
            };
        }
        public static Subscription StringToSubscription(this string value)
        {
            string[] values = value.Split(',');
            return new Subscription
            {
                AgencyId = int.Parse(values[0]),
                OriginCityId = int.Parse(values[1]),
                DestinationCityId = int.Parse(values[2])
            };
        }
    }
}
