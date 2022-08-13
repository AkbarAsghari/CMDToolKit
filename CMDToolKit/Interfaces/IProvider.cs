using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDToolKit.Interfaces
{
    internal interface IProvider
    {
        void Process();
        void Help();
    }
}
