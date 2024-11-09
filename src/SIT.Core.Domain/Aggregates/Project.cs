using SIT.Core.Domain.ValueObjects;
using SIT.Shared.Abstractions;
using SIT.Shared.Abstractions.Interfaces;

namespace SIT.Core.Domain.Aggregates;

public class Project : BaseEntity, IAggregate
{
    public string Title { get; set; }
    public int CustomerId { get; set; }
    public Location Location { get; set; }

    public Project(string title, int customerId, Location location)
    {
        Title = title;
        CustomerId = customerId;
        Location = location;
    }
    
    public Project(){}
}