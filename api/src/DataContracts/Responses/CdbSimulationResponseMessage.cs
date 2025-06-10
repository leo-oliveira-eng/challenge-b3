using System.Runtime.Serialization;

namespace DataContracts.Responses;

[DataContract]
public sealed class CdbSimulationResponseMessage
{
    [DataMember]
    public decimal InitialAmount { get; init; }

    [DataMember]
    public decimal GrossAmount { get; init; }

    [DataMember]
    public decimal NetAmount { get; init; }

    [DataMember]
    public decimal TaxAmount { get; init; }

    [DataMember]
    public decimal TaxRate { get; init; }

    [DataMember]
    public int Duration { get; init; }

    [DataMember]
    public decimal MonthlyRate { get; init; }

    [DataMember]
    public decimal TotalInterest { get; init; }
}
