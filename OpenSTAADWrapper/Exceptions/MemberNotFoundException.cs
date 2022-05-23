using System;
using System.Runtime.Serialization;

namespace OpenStaadSharp.Exceptions
{
    [Serializable]
    internal class MemberNotFoundException : Exception
    {
        public MemberNotFoundException()
        {
        }

        public MemberNotFoundException(string message) : base(message)
        {
        }

        public MemberNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MemberNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}