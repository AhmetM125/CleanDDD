using Domain.Primitives;

namespace Domain.Entities;

public class Webinar : Entity
{
    public string Name { get; private set; }
    public DateTime ScheduledOn { get; private set; }
    public Webinar() : base()
    {
        
    }

    public Webinar(Guid id,string name, DateTime scheduledOn) : base(id)
    {
        Name = name;
        ScheduledOn = scheduledOn;
    }
}

