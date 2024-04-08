using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Members.Commands;

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
    public async Task<IActionResult> CreateMember(CreateMovimentoCommand command)
    {
        var updatedMember = await _mediator.Send(command);

        return updatedMember != null ? Ok(updatedMember) : NotFound("Member not found.");
    }


   
}
