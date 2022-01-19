using GameMultiplayer.Client.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMultiplayer.Client.Authentication
{
    public static class AuthenticateConnection
    {
        public static Connection Authenticate()
        {
            IConnectionData data = AuthenticationWindow.Authenticate();
            if (data is null)
                return null;

            return new Connection(data.Url, data.Password);
        }
    }
}
