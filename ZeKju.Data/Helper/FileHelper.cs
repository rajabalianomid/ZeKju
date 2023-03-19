using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZeKju.Data.Configuration;

namespace ZeKju.Data.Helper
{
    public class FileHelper
    {
        public static void CreateAppSettings(string connectionString)
        {
            var json = JsonSerializer.Serialize(new AppSetting { ConnectionStrings = new Connectionstrings { DefaultConnection = connectionString } });
            File.WriteAllText("./app_data/appsettings.json", json);
        }
    }
}
