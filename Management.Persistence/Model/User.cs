using System;
using Management.Persistence.Documents;
namespace Management.Persistence.Model
{
	public class User : IEntity
    {
		public Guid Id { get; set; }

		public string Name { get; set; }
		public string Address { get; set; }
		public int Age { get; set; }
    }
}
