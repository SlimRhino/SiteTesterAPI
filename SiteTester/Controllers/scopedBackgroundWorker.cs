using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace SiteTester
{
    internal interface IScopedProcessingService
    {
        void DoWork();
    }


    internal class ScopedBackgroundWorker : IScopedProcessingService
    {
        private readonly AppContext _db;
        private readonly ILogger _logger;

        public ScopedBackgroundWorker(ILogger<ScopedBackgroundWorker> logger, AppContext db)
        {
            _db = db;
            _logger = logger;
        }

        public void DoWork()
        {
            _logger.LogInformation("Timed Background Service is working.");
            Ping ping = new Ping();
            PingReply pingReplay = null;

            var sites = _db.Sites.ToList();
            foreach (var site in sites)
            {
                pingReplay = ping.Send(site.URI, 2000);
                if (pingReplay.Status == IPStatus.Success)
                {
                    _logger.LogInformation("ping start");
                    site.IsAvailable = true;
                    site.LastAvailable = DateTime.Now;
                }
                else
                {
                    site.IsAvailable = false;
                }
            }

            _db.Sites.UpdateRange(sites);
            _db.SaveChanges();
        }
    }

    internal class ConsumeScopedServiceHostedService : IHostedService
    {
        private readonly ILogger _logger;
        private Timer _timer;
        private static TimeSpan _interval = TimeSpan.FromSeconds(5);
        private static bool _intervalChanged = false;

        public ConsumeScopedServiceHostedService(IServiceProvider services,
            ILogger<ConsumeScopedServiceHostedService> logger)
        {
            Services = services;
            _logger = logger;
        }

        public IServiceProvider Services { get; }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                _interval);

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Consume Scoped Service Hosted Service is working.");

            if(_intervalChanged)
            {
                _timer.Change(TimeSpan.Zero, _interval);
                _intervalChanged = false;
            }
            
            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<IScopedProcessingService>();

                scopedProcessingService.DoWork();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public static Task ChangeInterval(TimeSpan interval)
        {
            _interval = interval;
            _intervalChanged = true;

            return Task.CompletedTask;
        }
    }
}
