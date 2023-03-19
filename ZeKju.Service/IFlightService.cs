using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeKju.Domain.BusinessModel;

namespace ZeKju.Service
{
    public interface IFlightService
    {
        public List<ExportModel> GetFlightsAsync(DateTime startTime, DateTime endTime, int agencyId);
    }
}
