using System;
using System.Collections.Generic;
using System.Linq;
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

      var sites = new List<string>(new[] {"RemoteSite"});
      if ((new[] { '1', '2', '3', '4', '5', '6' }).Contains(message.Id.ToString().First()))
        sites.Add("SecondRemoteSite"); 
      
      // Send a new message to remote sites for further processing
      Bus.SendToSites(sites, new PrepareTransfer() {
        Id = message.Id,
        Amount = message.Amount,
        Currency = message.Currency,
        Reference = message.Reference
      });

      Console.WriteLine("Message sent to {0} remote sites", sites.Count);
    }

    /// <summary>
    /// Receive a notification from a remote site after processing has completed
    /// </summary>
    /// <param name="message">Message</param>
    public void Handle(TransferPrepared message)
    {
      Console.WriteLine("Acknowlegment message for transfer '{0}' from remote site '{1}' received", message.Id, message.Receiver);
      Thread.Sleep(250);
      Console.WriteLine("Acknowlegment message for transfer '{0}' from remote site '{1}' processed", message.Id, message.Receiver);
    }
  }
}
