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

        public Player(string id, string name, Position position, List<Goal> goals)
        {
            Id = id;
            Name = name;
            Position = position;
            Goals = goals;
        }

        public Player Move(string direction)
        {
            Position position = Position.Move(direction);
            return new Player(Id, Name, position, Goals);
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
