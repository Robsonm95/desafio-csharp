using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Movimento.Commands;

namespace Questao5.API.Controllers;

[Route("[controller]")]
[ApiController]
public class MovimentacaoContaController : ControllerBase
{
    private readonly IMediator _mediator;
    public MovimentacaoContaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMovimento(CreateMovimentoCommand command)
    {
        try
        {
            var createMovimento = await _mediator.Send(command);
            return createMovimento != null ? Ok(createMovimento) : NotFound("Movimento not found.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
