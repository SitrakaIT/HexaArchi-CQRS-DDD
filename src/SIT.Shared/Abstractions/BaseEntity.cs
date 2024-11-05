using SIT.Shared.Abstractions.Interfaces;

namespace SIT.Shared.Abstractions;

public abstract class BaseEntity : IEntity<int>
{
    public int Id { get; private init; }
    public string CreationUser { get; set; }
    public DateTime CreationDate { get; set; }
    public string EditionUser { get; set; }
    public DateTime? EditionDate { get; set; }
}