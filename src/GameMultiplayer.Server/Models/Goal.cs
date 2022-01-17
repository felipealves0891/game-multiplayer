using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMultiplayer.Server.Models
{
    public class Goal
    {
        public string Id { get; set; }

        public Position Position { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Goal other = obj as Goal;
            if (other is null)
                return false;

            return other.Id == Id 
                && other.Position.Equals(Position);
        }

    }
}
