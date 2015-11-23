using System;

namespace NsbServiceHostBasedGateway.Messages.Messages
{
  /// <summary>
  /// Notify a remote host about the completion of a task
  /// </summary>
  /// <remarks>
  /// Cross-site message cannot be command or events
  /// </remarks>
  public class TransferPrepared
  {
    public Guid Id { get; set; }
    public string Receiver { get; set; }
  }
}
