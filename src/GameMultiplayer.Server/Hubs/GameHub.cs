using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameMultiplayer.Server.Authentications;
using GameMultiplayer.Server.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace GameMultiplayer.Server.Hubs
{
    [ApiKey]
    public class GameHub : Hub
    {
        private readonly ILogger<GameHub> _logger;
        private readonly GameRepository _repository;
        
        public GameHub(GameRepository game, ILogger<GameHub> logger)
        {
            _repository = game;
            _logger = logger;
        }

        public async Task UpdateCaller()
        {
            await Clients.Caller.SendAsync("update", _repository.Game);
        }

        public async Task ConnectPlayer(Player player)
        {
            _repository.Game = _repository.Game.PlayerJoin(player);
            await Clients.All.SendAsync("update", _repository.Game);
            _logger.LogInformation("player {0} logged into the game {1}...", player.Id, _repository.Game.Id);
        }

        public async Task DesconnectPlayer(Player player)
        {
            _repository.Game = _repository.Game.PlayerLeft(player);
            await Clients.All.SendAsync("update", _repository.Game);
            _logger.LogInformation("player {0} disconnected in game {1}...", player.Id, _repository.Game.Id);
        }

        public async Task Move(string playerId, string direction)
        {
            _repository.Game = _repository.Game.MovePlayer(playerId, direction);
            await Clients.All.SendAsync("update", _repository.Game);
            _logger.LogInformation("Player {0} move to {1}...", playerId, direction);
        }

        public async Task UpdatePlayer(Player player)
        {
            _repository.Game = _repository.Game.UpdatePlayer(player);
            await Clients.All.SendAsync("update", _repository.Game);
            _logger.LogInformation("player {0} updated {1}...", player.Id, _repository.Game.Id);
        }

        public async Task AddGoal()
        {
            _repository.Game = _repository.Game.CreateGoal();
            await Clients.All.SendAsync("update", _repository.Game);
            _logger.LogInformation("Added a goal to the platform...");
        }
    }
}
