using System;
using System.Threading;
using NsbServiceHostBasedGateway.Messages.Messages;
using NServiceBus;

namespace NsbServiceHostBasedGateway.RemoteSite
{
  public class PrepareTransferHandler : IHandleMessages<PrepareTransfer>
  {
    public IBus Bus { get; set; }

    /// <summary>
    /// Receive a message from a "remote" host, and process
    /// </summary>
    /// <param name="message">Message</param>
    public void Handle(PrepareTransfer message)
    {
      Console.WriteLine("PrepareTransfer received: {0}", message.Id);

      // fake processing
      Thread.Sleep(250);

      // Notify the remote host
      Bus.Reply<TransferPrepared>(m => {
        m.Id = message.Id;
      });

      Console.WriteLine("Aknowledgment message for transfer {0} sent", message.Id);
    }
  }
}
