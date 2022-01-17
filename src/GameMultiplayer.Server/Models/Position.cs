using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMultiplayer.Server.Models
{
    public class Position
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public Position()
        {
            Random random = new Random();
            Row = random.Next(0, 30);
            Column = random.Next(0, 30);
        }

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public Position Move(string direction)
        {
            string d = direction.ToLower().Trim();
            if (d == "left" && Column > 0)
                return new Position(Row, Column - 1);

            if (d == "right" && Column < 30)
                return new Position(Row, Column + 1);

            if (d == "up" && Row > 0)
                return new Position(Row - 1, Column);

            if (d == "down" && Row < 30)
                return new Position(Row + 1, Column);

            return this;
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
