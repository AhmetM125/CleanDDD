using System;

namespace Domain.Exceptions;

public sealed class WebinarNotFoundException : Exception
{
    public WebinarNotFoundException(Guid id) : 
        base($"Webinar with id {id} was not found.")
    {
    }
}
