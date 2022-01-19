using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
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

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;

            string url = configuration["Server:Url"];
            string key = configuration["Server:Key"];
            string password = configuration["Server:Password"];

            _hub = new HubConnectionBuilder()
                .WithUrl($"{url}{key}", opts => { opts.AccessTokenProvider = () => Task.FromResult(password); })
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
                await _hub.InvokeAsync("AddGoal");
                await Task.Delay(1000);
            }
        }
    }
}
