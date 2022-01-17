using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GameMultiplayer.Client.Domain;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace GameMultiplayer.Client.Connections
{
    public class Connection
    {
        private HubConnection _hub;

        public bool IsOpen { get; private set; }

        public Connection()
        {
            IsOpen = false;
            _hub = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/game", (opts) => 
                {
                    opts.HttpMessageHandlerFactory = (message) =>
                    {
                        // bypass SSL certificate
                        if (message is HttpClientHandler clientHandler)
                            clientHandler.ServerCertificateCustomValidationCallback += (sender, certificate, chain, sslPolicyErrors) => { return true; };
                        return message;
                    };
                })
                .Build();

            _hub.Closed += Reconnect;
        }

        private async Task Reconnect(Exception ex)
        {
            await Task.Delay(500);
            await _hub.StartAsync();
        }

        public async Task Open(Action<Game> update)
        {
            _hub.On<Game>("update", update);
            await _hub.StartAsync();
            IsOpen = true;
        }

        public async Task<Player> RegisterPlayer(string name)
        {
            Random random = new Random();
            Player player = new Player()
            {
                Id = Guid.NewGuid().ToString("N"),
                Name = name,
                Position = new Position() 
                { 
                    Column = new Random().Next(0, 30),
                    Row = new Random().Next(0, 30)
                }
            };

            await _hub.InvokeAsync("ConnectPlayer", player);
            return player;
        }

        public async Task Desconect(Player player)
        {
            await _hub.InvokeAsync("DesconnectPlayer", player);
            IsOpen = false;
        }

        public async Task Update()
        {
            await _hub.InvokeAsync("UpdateCaller");
            await _hub.StopAsync();
        }

        public async Task MovePlayer(Player player, string direction)
        {
            await _hub.InvokeAsync("Move", player.Id, direction);
        }

    }
}
