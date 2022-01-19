using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMultiplayer.Server.Models
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
        {}

        public Player(Player old)
        {
            Id = old.Id;
            Name = old.Name;
            Position = old.Position;
            Goals = old.Goals;
            Color = old.Color;
        }

        public Player Move(string direction)
        {
            Position = Position.Move(direction);
            return new Player(this);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Player other = obj as Player;
            if (other is null)
                return false;

            return other.Id == Id
                && other.Position.Equals(Position);
        }
    }
}
