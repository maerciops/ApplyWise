using ApplyWise.Domain.Interfaces;

namespace ApplyWise.Domain.Common;

public abstract class BaseEntity: IEntidadeAuditavel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid OwnerId { get; set; }
}
