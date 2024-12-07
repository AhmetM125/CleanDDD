using Domain.Entities;

namespace Domain.Abstractions;

public interface IWebinaryRepository
{
    Webinar GetByIdAsync(Guid webinarId);
    void Insert(Webinar webinar);
}
