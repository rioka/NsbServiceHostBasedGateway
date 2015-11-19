using System.Reflection;
using NServiceBus;

namespace NsbServiceHostBasedGateway.Client
{
  public static class ServiceBus
  {
    public static IBus Bus { get; private set; }

    private static readonly object SyncLock = new object();

    public static void Init()
    {
      if (Bus != null)
        return;

      lock (SyncLock)
      {
        if (Bus != null)
          return;
        var cfg = new BusConfiguration();

        cfg.EndpointName(MethodBase.GetCurrentMethod().DeclaringType.Namespace);
        cfg.UseTransport<MsmqTransport>();
        cfg.UsePersistence<InMemoryPersistence>();
        cfg.EnableInstallers();
        cfg.UseSerialization<JsonSerializer>();

        cfg.Conventions()
           .DefiningMessagesAs(t => t.Namespace != null && t.Namespace.EndsWith(".Messages"))
           .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.EndsWith(".Commands"))
           .DefiningEventsAs(t => t.Namespace != null && t.Namespace.EndsWith(".Events"));

        Bus = NServiceBus.Bus.Create(cfg).Start();
      }
    }
  }
}
