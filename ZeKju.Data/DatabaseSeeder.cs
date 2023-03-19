using EFCore.BulkExtensions;
using ZeKju.Data.Contexts;
using ZeKju.Data.Extensions;

namespace ZeKju.Data
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly ZeKjuContext _db;

        public DatabaseSeeder(ZeKjuContext db)
        {
            _db = db;
        }

        public void Initialize(string path = null)
        {
            AddRoutes(path);
            AddFlights(path);
            AddSubscriptions(path);
            _db.SaveChanges();
        }

        private void AddFlights(string path)
        {
            var data = File.ReadAllLines(path + @"\Flights.csv").Skip(1).Select(s => s.StringToFlight()).ToList();
            _db.BulkInsert(data);
        }
        private void AddRoutes(string path)
        {
            var data = File.ReadAllLines(path + @"\Routes.csv").Skip(1).Select(s => s.StringToRoute()).ToList();
            _db.BulkInsert(data);
        }
        private void AddSubscriptions(string path)
        {
            var data = File.ReadAllLines(path + @"\Subscriptions.csv").Skip(1).Select(s => s.StringToSubscription()).ToList();
            _db.BulkInsert(data);
        }
    }
}
