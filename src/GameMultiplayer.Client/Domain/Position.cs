using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMultiplayer.Client.Domain
{
    public class Position
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public void Move(string direction)
        {
            string d = direction.ToLower().Trim();
            if (d == "left" && Column > 0)
                Column--;

            if (d == "right" && Column < 30)
                Column++;

            if (d == "up" && Row > 0)
                Row--;

            if (d == "down" && Row < 30)
                Row++;
        }

        public override string ToString()
        {
            return $"Linha: {Row}, Coluna: {Column}";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Position other = obj as Position;
            if (other is null)
                return false;

            return other.Column == Column &&
                   other.Row == Row;
        }
    }
}
