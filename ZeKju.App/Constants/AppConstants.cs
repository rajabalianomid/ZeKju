using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeKju.App.Constants
{
    public class AppConstants
    {
        public const string App_Data = "./app_data/appsettings.json";

        public const string Message_Installing = "Installing...";
        public const string Message_ConfigConnectionString = "Please enter your Sqlserver connection string:";
        public const string Message_ImportDirectory = "Please enter dataset directory:";
        public const string Message_CSVGenerated = "output.csv file generated, you can find it in application directory.";
        public const string Message_Help = "Type command like:  [StartDate] [EndDate] [AgencyId] (Date Format is: yyyy-mm-dd)";
        public const string Message_InvalidParameter = "Invalid parameter!, if you need help, enter help.";
    }
}
