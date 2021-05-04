using System;
using System.Runtime.Serialization;

[Serializable]
internal class ItemsManagerException : Exception
{
    public ItemsManagerException()
    {
    }

    public ItemsManagerException(string message) : base(message)
    {
    }

    public ItemsManagerException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected ItemsManagerException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}