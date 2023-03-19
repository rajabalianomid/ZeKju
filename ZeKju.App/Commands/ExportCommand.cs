using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeKju.App.Constants;
using ZeKju.App.Extensions;
using ZeKju.App.Model;
using ZeKju.Service;

namespace ZeKju.App.Commands
{
    public class ExportCommand : IExportCommand
    {
        private IFlightService _flightService;
        public ExportCommand(IFlightService flightService)
        {
            _flightService = flightService;
        }
        public void Run(params object[] values)
        {
            var input = values.First() as RequestModel;
            var result = _flightService.GetFlightsAsync(input.StartDate.Value, input.EndDate.Value, input.AgencyId);
            result.ToCsv();
            Console.WriteLine(AppConstants.Message_CSVGenerated);
        }
    }
}
