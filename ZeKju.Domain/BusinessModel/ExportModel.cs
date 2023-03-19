using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZeKju.Domain.BusinessModel
{
    public class ExportModel
    {
        public enum StatusType
        {
            New,
            Discontinued
        }
        [Display(Name = "flight_id")]
        public int FlightId { get; set; }

        [Display(Name = "origin_city_id")]
        public int OriginCityId { get; set; }

        [Display(Name = "destination_city_id")]
        public int DestinationCityId { get; set; }

        [Display(Name = "departure_time")]
        public DateTime DepartureTime { get; set; }

        [Display(Name = "arrival_time")]
        public DateTime ArrivalTime { get; set; }

        [Display(Name = "airline_id")]
        public int AirlineId { get; set; }

        [Display(Name = "status")]
        public StatusType Status { get; set; }
    }
}
