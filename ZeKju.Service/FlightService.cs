using Microsoft.EntityFrameworkCore;
using System.Collections;
using ZeKju.Data.Mapping;
using ZeKju.Data.Repository;
using ZeKju.Domain;
using ZeKju.Domain.BusinessModel;
using ZeKju.Domain.Mapping;

namespace ZeKju.Service
{
    public class FlightService : IFlightService
    {
        IRepositoryAsync<Flight, int> _flightRepository;
        IRepositoryAsync<Subscription, string> _subscriptionRepository;
        IRepositoryAsync<Route, int> _routeRepository;
        public FlightService(IRepositoryAsync<Flight, int> flightRepository, IRepositoryAsync<Subscription, string> subscriptionRepository, IRepositoryAsync<Route, int> routeRepository)
        {
            this._flightRepository = flightRepository;
            this._subscriptionRepository = subscriptionRepository;
            this._routeRepository = routeRepository;
        }
        public List<ExportModel> GetFlightsAsync(DateTime startTime, DateTime endTime, int agencyId)
        {
            var flights = _subscriptionRepository.Entities.Join(_routeRepository.Entities.Include(i => i.Flights).ThenInclude(i => i.TheRoute), sub => new { sub.OriginCityId, sub.DestinationCityId }, route => new { route.OriginCityId, route.DestinationCityId }, (sub, route) => new { sub, route })
                .Where(w => w.sub.AgencyId == agencyId && w.route.DepartureDate >= startTime && w.route.DepartureDate <= endTime).SelectMany(s => s.route.Flights).ToList();

            var airlineIds = flights.Select(s => s.AirlineId).Distinct().ToList();

            List<Flight> newFlights = new();
            List<Flight> discounts = new();
            foreach (var airlineId in airlineIds)
            {
                var data = flights.Where(w => w.Id == airlineId).GroupBy(g => g.DepartureTime.Date)
                                .Select(s => new GroupByFlight { Key = s.Key, Items = s.OrderBy(o => o.DepartureTime).ToList(), MinTime = s.Min(m => m.DepartureTime), MaxTime = s.Max(m => m.DepartureTime) })
                                .ToList();

                newFlights.AddRange(PrepareFlight(data, -7));
                discounts.AddRange(PrepareFlight(data, 7));
            }

            var newFlightsResponse = newFlights.OrderBy(o => o.DepartureTime).ToList().ToExportModel(ExportModel.StatusType.New).ToList();//Here we can use auto mapper
            var DiscountResponse = discounts.OrderBy(o => o.DepartureTime).ToList().ToExportModel(ExportModel.StatusType.Discontinued).ToList();//Here we can use auto mapper
            return newFlightsResponse.Union(DiscountResponse).OrderBy(o => o.DepartureTime).ToList();
        }
        public List<Flight> PrepareFlight(List<GroupByFlight> data, int diffDay)
        {
            List<int> isNotTheseIds = new();

            foreach (var item in data)
            {
                var minDate = item.MinTime.AddDays(diffDay).AddMinutes(-30).Date;
                var maxDate = item.MaxTime.AddDays(diffDay).AddMinutes(30).Date;
                var sameSevenDate = item.MaxTime.AddDays(diffDay).Date;

                if (minDate != sameSevenDate && data.Any(a => a.Key == minDate))
                {
                    var moveCategory = data.Where(w => w.Key == minDate).Select(s => s.Items).FirstOrDefault().Where(w => w.DepartureTime.Hour == 23 && w.DepartureTime.Minute >= 30).ToList();
                    var sameCategory = data.Where(w => w.Key == sameSevenDate).Select(s => s.Items).DefaultIfEmpty(new List<Flight> { }).First().Where(w => w.DepartureTime.Hour == 00 && w.DepartureTime.Minute <= 30).ToList();
                    var currentFlights = item.Items.Where(w => w.DepartureTime.Hour == 00 && w.DepartureTime.Minute <= 30).ToList();
                    var inTwoPartition = moveCategory.Union(sameCategory).ToList();
                    var foundIds = currentFlights.Where(w => inTwoPartition.Any(a => w.DepartureTime.AddDays(diffDay).AddMinutes(-30) <= a.DepartureTime && w.DepartureTime.AddDays(diffDay).AddMinutes(30) >= a.DepartureTime)).Select(s => s.Id).ToList();
                    isNotTheseIds.AddRange(foundIds);
                }
                if (maxDate != sameSevenDate && data.Any(a => a.Key == maxDate))
                {
                    var moveCategory = data.Where(w => w.Key == maxDate).Select(s => s.Items).FirstOrDefault().Where(w => w.DepartureTime.Hour == 00 && w.DepartureTime.Minute <= 30).ToList();
                    var sameCategory = data.Where(w => w.Key == sameSevenDate).Select(s => s.Items).DefaultIfEmpty(new List<Flight> { }).First().Where(w => w.DepartureTime.Hour == 23 && w.DepartureTime.Minute >= 30).ToList();
                    var currentFlights = item.Items.Where(w => w.DepartureTime.Hour == 23 && w.DepartureTime.Minute >= 30).ToList();
                    var inTwoPartition = moveCategory.Union(sameCategory).ToList();
                    var foundIds = currentFlights.Where(w => inTwoPartition.Any(a => w.DepartureTime.AddDays(diffDay).AddMinutes(-30) <= a.DepartureTime && w.DepartureTime.AddDays(diffDay).AddMinutes(30) >= a.DepartureTime)).Select(s => s.Id).ToList();
                    isNotTheseIds.AddRange(foundIds);
                }
                if (data.Any(a => a.Key == sameSevenDate))
                {
                    var restItems = item.Items.Where(w => !isNotTheseIds.Contains(w.Id)).ToList();
                    var foundItems = data.Where(w => w.Key == sameSevenDate).Select(s => s.Items).FirstOrDefault();
                    var foundIds = restItems.Where(w => foundItems.Any(a => w.DepartureTime.AddDays(diffDay).AddMinutes(-30) <= a.DepartureTime && w.DepartureTime.AddDays(diffDay).AddMinutes(30) >= a.DepartureTime)).Select(s => s.Id).ToList();
                    isNotTheseIds.AddRange(foundIds);
                }
            }
            return data.SelectMany(s => s.Items).Where(w => !isNotTheseIds.Contains(w.Id)).ToList();
        }
    }
}