using Microsoft.Extensions.DependencyInjection;
using ZeKju.App.Commands;
using ZeKju.App.Constants;
using ZeKju.App.Extensions;

namespace ZeKju.App.Helper
{
    public class MenuHelper
    {
        public static void HandelMenu(IServiceProvider serviceProvider)
        {
            Dictionary<InputCommandType, IInputCommand> commands = new()
            {
                { InputCommandType.Exit, serviceProvider.GetService<IExitCommand>()},
                { InputCommandType.Help, serviceProvider.GetService<IHelpCommand>() },
                { InputCommandType.None, serviceProvider.GetService<INoneCommand>() },
                { InputCommandType.Export, serviceProvider.GetService<IExportCommand>() },
            };
            InputCommandType input = InputCommandType.None;
            while (input != InputCommandType.Exit)
            {
                Console.Write("Please enter your command, if you need more help please enter help:");
                var result = Console.ReadLine().ToInputCommandType();
                input = result.Item1;
                commands[input].Run(result.Item2);
            }
        }
    }
}
