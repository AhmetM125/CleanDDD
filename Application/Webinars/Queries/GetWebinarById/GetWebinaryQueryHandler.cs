using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Webinars.Queries.GetWebinarById;

public sealed class GetWebinaryQueryHandler
    : IQueryHandler<GetWebinaryByIdQuery, WebinarResponse>
{
    private readonly IWebinaryRepository _webinarRepository;

    public GetWebinaryQueryHandler(IWebinaryRepository webinarRepository)
    {
        _webinarRepository = webinarRepository;
    }

    public async Task<Result<WebinarResponse>>
        Handle(GetWebinaryByIdQuery request, CancellationToken cancellationToken)
    {

        var response = _webinarRepository.GetByIdAsync(request.WebinarId);

        if (response == null)
        {
            return Result<WebinarResponse>.Failure("Webinar not found");
        }

        return Result<WebinarResponse>.Success(new WebinarResponse
        {
            Id = response.Id,
            Name = response.Name,
            ScheduledOn = response.ScheduledOn
        });
    }
}
