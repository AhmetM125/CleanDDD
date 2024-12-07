using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class ApiController : ControllerBase
{
    protected readonly ISender Sender;

    public ApiController(ISender sender)
    {
        Sender = sender;
    }
}
