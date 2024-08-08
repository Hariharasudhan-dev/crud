//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;

//public class MyBackgroundService : BackgroundService
//{
//    private readonly ILogger<MyBackgroundService> _logger;
//    private Timer _timer;

//    public MyBackgroundService(ILogger<MyBackgroundService> logger)
//    {
//        _logger = logger;
//    }

//    protected override Task ExecuteAsync(CancellationToken stoppingToken)
//    {
//        _timer = new Timer(ExecuteTask, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
//        return Task.CompletedTask;
//    }

//    private void ExecuteTask(object state)
//    {
//        if (!CancellationToken.None.IsCancellationRequested)
//        {
//            _logger.LogInformation("Background task is executing.");
//        }
//    }

//    public override Task StopAsync(CancellationToken stoppingToken)
//    {
//        _timer?.Change(Timeout.Infinite, 0);
//        return base.StopAsync(stoppingToken);
//    }

//    public override void Dispose()
//    {
//        _timer?.Dispose();
//        base.Dispose();
//    }
//}