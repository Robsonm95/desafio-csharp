using Questao5.Application.Members.Commands;
using Questao5.Application.Members.Queries;
using Questao5.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Questao5.API.Controllers;

[Route("[controller]")]
[ApiController]
public class SaldoContaController : ControllerBase
{
    private readonly IMediator _mediator;
    public SaldoContaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSaldoConta(string id)
    {
        var query = new SaldoContaByIdQuery { Id = id };
        var member = await _mediator.Send(query);

        return member != null ? Ok(member) : NotFound("Member not found.");
    }

   
}
