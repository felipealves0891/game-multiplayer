using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMultiplayer.Client.Domain
{
    public class Player
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Position Position { get; set; }

        public List<Goal> Goals { get; set; }

        public int Points => Goals.Count;

        public Color Color { get; set; }

        public Player()
        {
            Goals = new List<Goal>();
            Color = new Color();
        }

    }
}
