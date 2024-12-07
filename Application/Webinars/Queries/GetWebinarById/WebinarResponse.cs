namespace Application.Webinars.Queries.GetWebinarById;

public record WebinarResponse(Guid Id, string Name, DateTime ScheduledOn);