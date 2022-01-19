using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMultiplayer.Server.Models
{
    public class Game
    {
        public string Id { get; private set; }

        public IEnumerable<Player> Players { get; private set; }

        public IEnumerable<Goal> Goals { get; private set; }

        public Game()
        {
            Id = Guid.NewGuid().ToString();
            Players = new List<Player>();
            Goals = new List<Goal>();
        }

        public Game(string id, IEnumerable<Player> players, IEnumerable<Goal> goals)
        {
            Id = id;
            Players = players;
            Goals = goals;
        }

        public Game PlayerJoin(Player player)
        {
            if (Players.Count() >= 15)
                throw new InvalidOperationException("Numero maximo de jogadores atingido!");

            List<Player> players = Players.ToList();
            bool isPlaying = players.Exists(p => p.Id == player.Id);
            if (isPlaying)
                return this;

            players.Add(player);
            return new Game(Id, players, Goals);
        }

        public Game PlayerLeft(Player player)
        {
            List<Player> players = Players.ToList();
            players.Remove(player);
            return new Game(Id, players, Goals);
        }

        public Game CreateGoal()
        {
            int objectsInPlatform = Goals.Count() + Players.Count();
            if (objectsInPlatform > 25)
                return this;

            Goal goal = new Goal()
            {
                Id = Guid.NewGuid().ToString("N"),
                Position = CreatePosition()
            };

            List<Goal> goals = Goals.ToList();
            goals.Add(goal);
            return new Game(Id, Players, goals);
        }

        public Position CreatePosition()
        {
            Position position = new Position();

            while (IsPositionConflict(position))
                position = new Position();

            return position;
        }

        public bool IsPositionConflict(Position position)
        {
            List<Position> positions = Players.Select(p => p.Position)
                                              .Union(Goals.Select(g => g.Position))
                                              .ToList();

            return positions.Exists(p => p.Equals(position));
        } 

        public Game MovePlayer(string playerId, string direction)
        {
            Player player = Players.Where(x => x.Id == playerId)
                                   .FirstOrDefault();
            if (player is null)
                return this;

            List<Player> players = Players.ToList();
            List<Goal> goals = Goals.ToList();
            players.Remove(player);

            Player movedPlayer = player.Move(direction);
            Goal goal = Goals.Where(g => g.Position.Equals(movedPlayer.Position))
                             .FirstOrDefault();

            if(goal != null)
            {
                goals.Remove(goal);
                movedPlayer.Goals.Add(goal);
            }

            Player playerDown = Players.Where(x => x.Id != movedPlayer.Id && x.Position.Equals(movedPlayer.Position))
                                       .FirstOrDefault();

            if(playerDown != null)
            {
                players.Remove(playerDown);
                movedPlayer.Goals.AddRange(playerDown.Goals);
                playerDown.Goals = new List<Goal>();
                players.Add(playerDown);
            }

            players.Add(movedPlayer);
            return new Game(Id, players, goals);
        }
    }
}
