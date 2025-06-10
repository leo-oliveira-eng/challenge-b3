using System.Runtime.Serialization;

namespace DataContracts.Requests;

[DataContract]
public sealed class InvestmentSimulationRequestMessage
{
    [DataMember]
    public decimal Amount { get; init; }

    [DataMember]
    public int Duration { get; init; }
}
