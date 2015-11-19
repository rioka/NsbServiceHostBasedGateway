using System;
using System.Threading;
using NsbServiceHostBasedGateway.Messages.Commands;
using NsbServiceHostBasedGateway.Messages.Messages;
using NServiceBus;

namespace NsbServiceHostBasedGateway.LocalSite
{
  public class MessageHandler : IHandleMessages<SendTransfer>, IHandleMessages<TransferPrepared>
  {
    public IBus Bus { get; set; }
    
    /// <summary>
    /// Receive a command to start a transfer
    /// </summary>
    /// <param name="message"></param>
    public void Handle(SendTransfer message)
    {
      Console.WriteLine("Received a command for a new transfer '{0}'", message.Id);

      // fake processing
      Thread.Sleep(250);
      
      // Send a new message to a remote site for other porcessing
      Bus.SendToSites(new[] { "RemoteSite" }, new PrepareTransfer() {
        Id = message.Id,
        Amount = message.Amount,
        Currency = message.Currency,
        Reference = message.Reference
      });

      Console.WriteLine("Message sent to remote site");
    }

    /// <summary>
    /// Receive a notification from a remote site after processing has completed
    /// </summary>
    /// <param name="message">Message</param>
    public void Handle(TransferPrepared message)
    {
      Console.WriteLine("Acknowlegment message for transfer '{0}' from remote site received", message.Id);
      Thread.Sleep(250);
      Console.WriteLine("Acknowlegment message for transfer '{0}' processed", message.Id);
    }
  }
}
