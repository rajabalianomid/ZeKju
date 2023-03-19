using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeKju.Domain;
using ZeKju.Domain.BusinessModel;

namespace ZeKju.Domain.Mapping
{
    public static class ModelMapping
    {
        public static List<ExportModel> ToExportModel(this List<Flight> flight, ExportModel.StatusType status)
        {
            return flight.Select(s => new ExportModel
            {
                FlightId = s.Id,
                AirlineId = s.AirlineId,
                ArrivalTime = s.ArrivalTime,
                DepartureTime = s.DepartureTime,
                DestinationCityId = s.TheRoute.DestinationCityId,
                OriginCityId = s.TheRoute.OriginCityId,
                Status = status
            }).ToList();
        }

    }
}
