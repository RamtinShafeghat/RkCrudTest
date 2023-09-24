namespace Mc2.CrudTest.Core;

public class EventData
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string AggregateId { get; set; }
    public string AggregateName { get; set; }

    public string Data { get; set; }

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }
}
