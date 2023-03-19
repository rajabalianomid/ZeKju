using System.ComponentModel.DataAnnotations;
using System.Text;
using ZeKju.App.Constants;
using ZeKju.App.Model;

namespace ZeKju.App.Extensions
{
    public static class GeneralExtensions
    {
        public static (InputCommandType, RequestModel) ToInputCommandType(this string input)
        {
            InputCommandType resultCommand;
            RequestModel requestModel = new();
            try
            {
                input = input.ToLower();
                var inputValidated = IsValidDate(input);
                if (inputValidated.Item1)
                {
                    requestModel = inputValidated.Item2;
                    input = InputCommandType.Export.ToString();
                }
                var result = string.Concat(input[..1].ToUpper(), input.AsSpan(1));
                resultCommand = (InputCommandType)Enum.Parse(typeof(InputCommandType), result);
            }
            catch (ArgumentException)
            {
                resultCommand = InputCommandType.None;
            }
            return (resultCommand, requestModel);
        }
        private static (bool, RequestModel) IsValidDate(string input)
        {
            var inputParts = input.Split(' ');
            if (inputParts.Count() == 3)
            {
                var isValidStartDate = DateTime.TryParse(inputParts[0], out DateTime startDate);
                var isValidEndDate = DateTime.TryParse(inputParts[1], out DateTime endDate);
                var isValidAgencyId = int.TryParse(inputParts[2], out int agencyId);
                if (isValidStartDate && isValidEndDate && isValidAgencyId)
                {
                    return (true, new RequestModel { StartDate = startDate, EndDate = endDate, AgencyId = agencyId });
                }
            }
            return (false, new RequestModel { });
        }
        public static void ToCsv<T>(this IEnumerable<T> items)
        {
            var csvBuilder = new StringBuilder();
            var properties = typeof(T).GetProperties();

            // Add header row
            foreach (var property in properties)
            {
                var displayAttribute = property.GetCustomAttributes(typeof(DisplayAttribute), false)
                    .Cast<DisplayAttribute>()
                    .FirstOrDefault();

                var columnName = displayAttribute != null ? displayAttribute.Name : property.Name;

                csvBuilder.Append(columnName + ",");
            }

            csvBuilder.AppendLine();

            // Add data rows
            foreach (var item in items)
            {
                foreach (var property in properties)
                {
                    var value = property.GetValue(item, null);
                    csvBuilder.Append(value + ",");
                }

                csvBuilder.AppendLine();
            }
            var csv = csvBuilder.ToString();
            File.WriteAllText("output.csv", csv);
        }
    }
}
