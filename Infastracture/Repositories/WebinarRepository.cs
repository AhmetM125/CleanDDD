using Domain.Abstractions;
using Domain.Entities;

namespace Infastracture.Repositories;

public class WebinarRepository : IWebinaryRepository
{
    private readonly ApplicationDbContext _context;

    public WebinarRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Webinar GetByIdAsync(Guid webinarId)
      => _context.Webinars.FirstOrDefault(x => x.Id == webinarId);

    public void Insert(Webinar webinar)
     => _context.Webinars.Add(webinar);
}
