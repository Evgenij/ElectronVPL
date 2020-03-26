using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronVPL
{
    interface IAuthorization
    {
        void Authorization(string surname, string password);
    }
}
