using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMultiplayer.Client.Authentication
{
    public interface IConnectionData
    {
        public string Url { get; }

        public string Password { get; }

    }
}
