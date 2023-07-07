using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace OCPP.Core.Server.Middlewares;

public sealed class Consumer : IAsyncDisposable
{
  public readonly string HostDomain = Environment.GetEnvironmentVariable("HOST_DOMAIN");
  private HubConnection _hubConnection;

  public Consumer()
  {
    _hubConnection = new HubConnectionBuilder()
      .WithUrl(new Uri($"{HostDomain}/hub/notifications"))
      .WithAutomaticReconnect()
      .Build();

    _hubConnection.On<string>("NotificationReceived", OnNotificationReceived);
  }

  public Task StartNotificationConnectionAsync() => _hubConnection.StartAsync();

  public Task SendNotificationAsync(string text) => _hubConnection.InvokeAsync("NotifyAll", text);

  private static async void OnNotificationReceived(string notification)
  {
    Console.WriteLine($"Received notification: {notification}");
    await Task.CompletedTask;
  }

  public async ValueTask DisposeAsync()
  {
    if (_hubConnection != null)
    {
      await _hubConnection.DisposeAsync();
      _hubConnection = null;
    }
  }
}