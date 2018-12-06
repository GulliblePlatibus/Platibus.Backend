using System;
namespace Management.Documents.Documents
{
	public class IdResponse : Response
    {
        public Guid Id { get; private set; }

        public IdResponse(Guid id, bool isSuccessful = true, string message = null, Exception exception = null) : base(isSuccessful, exception, message)
        {
            Id = id;
        }

        public static IdResponse Successful(Guid id)
        {
            return new IdResponse(id);
        }

        public static IdResponse Unsuccessful(string messeage = null, Exception exception = null)
        {
            return new IdResponse(Guid.Empty, false, messeage, exception);
        }

    }
}
