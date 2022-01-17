using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameMultiplayer.Server.Models
{
    public class GameRepository
    {
        public Game Game { get; set; }

        public GameRepository()
        {
            Game = new Game();
        }

    }
}
