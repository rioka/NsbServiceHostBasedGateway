using System;

namespace NsbServiceHostBasedGateway.Messages.Messages
{
  /// <summary>
  /// Request to a remote site to prepare a transfer
  /// </summary>
  /// <remarks>
  /// Cross-site message cannot be command or events
  /// </remarks>
  public class PrepareTransfer
  {
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string Reference { get; set; }
  }
}
