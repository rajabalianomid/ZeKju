using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeKju.Domain;
using ZeKju.Domain.BusinessModel;

namespace ZeKju.App.Mapping
{
    public class ModelMapping
    {
        public static ExportModel ToExportModel(Flight flight, ExportModel.StatusType status)
        {
            return new ExportModel
            {
                FlightId = flight.Id,
                AirlineId = flight.AirlineId,
                ArrivalTime = flight.ArrivalTime,
                DepartureTime = flight.DepartureTime,
                DestinationCityId = flight.TheRoute.DestinationCityId,
                OriginCityId = flight.TheRoute.OriginCityId,
                Status = status
            };
        }
    }
}
