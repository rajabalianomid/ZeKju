using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeKju.Data
{
    public interface IDatabaseSeeder
    {
        void Initialize(string path = null);
    }
}
