using Application.Abstractions.Messaging;

namespace Application.Webinars.Commands;

public sealed class CreateWebinarCommandHandler
    : ICommandHandler<CreateWebinarCommand>
{

    Task<Result> MediatR.IRequestHandler<CreateWebinarCommand, Result>.Handle(CreateWebinarCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
