using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.MovimentacaoConta.Commands.Requests;
using Questao5.Application.MovimentacaoConta.Handlers;
using Questao5.Infrastructure.Database.CommandStore.Responses;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovimentacaoContaCorrente : ControllerBase
    {
        private readonly ILogger<MovimentacaoContaCorrente> _logger;
        private readonly IMediator _mediator;

        public MovimentacaoContaCorrente(ILogger<MovimentacaoContaCorrente> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public MovimentacaoContaResponse Get(
            [FromServices] IMovimentacaoContaHandler handler,
            [FromBody] MovimentacaoContaRequest command
        ){
            return handler.Handle(command);
        }
    }
}