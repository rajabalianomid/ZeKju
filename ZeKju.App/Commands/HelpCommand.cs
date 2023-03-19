using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeKju.App.Constants;

namespace ZeKju.App.Commands
{
    public class HelpCommand : IHelpCommand
    {
        public void Run(params object[] values)
        {
            Console.WriteLine(AppConstants.Message_Help);
        }
    }
}
