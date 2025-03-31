using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract1_Florich_I223.Logic
{
    public interface IAuthService
    {
        bool CheckData(string login, string pass);
    }
}
