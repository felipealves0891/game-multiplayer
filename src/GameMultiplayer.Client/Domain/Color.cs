using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMultiplayer.Client.Domain
{
    public class Color
    {
        public int R { get; set; }

        public int G { get; set; }

        public int B { get; set; }

        public Color()
        {
            Random random = new Random();
            R = random.Next(0, 255);
            G = random.Next(0, 255);
            B = random.Next(0, 255);
        }   
    }
}
