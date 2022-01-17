using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameMultiplayer.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly HubConnection _hub;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _hub = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/game")
                .Build();

            _hub.Closed += Reconnect;
            _hub.StartAsync();
        }

        private async Task Reconnect(Exception ex)
        {
            await Task.Delay(500);
            await _hub.StartAsync();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
                await _hub.InvokeAsync("AddGoal");
            }
        }
    }
}
