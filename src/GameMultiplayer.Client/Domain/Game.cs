using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMultiplayer.Client.Domain
{
    public class Game
    {
        public string Id { get; set; }

        public IEnumerable<Player> Players { get; set; }

        public IEnumerable<Goal> Goals { get; set; }

    }
}
