using Questao5.Application.Movimento.Commands;
using Questao5.Application.Movimento.Queries;
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
        var movimento = await _mediator.Send(query);

        return movimento != null ? Ok(movimento) : NotFound("Movimento not found.");
    }

   
}
