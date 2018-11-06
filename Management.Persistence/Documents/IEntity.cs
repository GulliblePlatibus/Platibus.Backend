using System;
namespace Management.Persistence.Documents
{
	public interface IEntity
    {
		Guid Id { get; }
    }
}
