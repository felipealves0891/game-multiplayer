using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMultiplayer.Client.Domain
{
    public class Goal
    {
        public string Id { get; set; }

        public Position Position { get; set; }

        public string Hash { get; set; }
    }
}
