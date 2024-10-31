using SIT.Core.Domain.ValueObjects;

namespace SIT.Core.Domain.Aggregates;

public class Project : IAggregate
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int CustomerId { get; set; }
    public Location Location { get; set; }

    public Project(string title, int customerId, Location location)
    {
        Title = title;
        CustomerId = customerId;
        Location = location;
    }
}