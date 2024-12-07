using Application.Abstractions.Messaging;

namespace Application.Webinars.Commands;

public sealed record CreateWebinarCommand(string Name,DateTime ScheduledOn)
    : ICommand;
