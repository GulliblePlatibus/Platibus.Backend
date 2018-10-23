using System;
namespace Management.Documents.Documents
{
	public class IdResponse : Response
    {
        public Guid Id { get; private set; }

        public IdResponse(Guid id, bool isSuccessful = true, Exception exception = null) : base(isSuccessful, exception)
        {
            Id = id;
        }
    }
}
