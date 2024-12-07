
using Application.Webinars.Commands;
using Application.Webinars.Queries.GetWebinarById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public sealed class WebinarController : ApiController
{
    public WebinarController(ISender sender) : base(sender)
    {
    }

    [HttpGet("{webinarId:guid}")]
    public async Task<IActionResult> GetWebinar(Guid webinarId)
    {
        var query = new GetWebinaryByIdQuery(webinarId);

        var webinar = await Sender.Send(query);
        if (webinar == null)
        {
            return NotFound();
        }
        return Ok(webinar);
    }
    [HttpPost]
    public async Task<IActionResult> CreateWebinar(CreateWebinarRequest createWebinar,
        CancellationToken cancellationToken)
    {
        var command = new CreateWebinarCommand("Webinar 1", DateTime.Now);
        var result = await Sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok();
    }
}
