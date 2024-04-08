using FluentValidation;
using MediatR;
using Questao5.Domain.Abstractions;
using Questao5.Domain.Entities;
using System.Text.Json;

namespace Questao5.Application.Movimento.Commands;
public class CreateMovimentoCommand : MovimentoCommandBase
{
    public class CreateMovimentoCommandHandler : IRequestHandler<CreateMovimentoCommand, Domain.Entities.Movimento>
    {
        private readonly IMovimentacaoRepository _movimentoRepository;
        private readonly IContaCorrenteRepository _contaRepository;
        private readonly IIdempotenciaRepository _idempotenciaRepository;
        private readonly IValidator<CreateMovimentoCommand> _validator;
        
        public CreateMovimentoCommandHandler(IMovimentacaoRepository movimentoRepository,
                                          IContaCorrenteRepository contaRepository,
                                          IIdempotenciaRepository idempotenciaRepository,
                                          IValidator<CreateMovimentoCommand> validator)
        {
            _contaRepository = contaRepository;
            _movimentoRepository = movimentoRepository;
            _idempotenciaRepository = idempotenciaRepository;
            _validator = validator;
        }
        public async Task<Domain.Entities.Movimento> Handle(CreateMovimentoCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            var movimento = new Domain.Entities.Movimento(request.IdContaCorrente, request.DataMovimento, request.TipoMovimento, request.Valor);

            var idempotencia = await _idempotenciaRepository.GetIdempotenciaById(request.IdRequest);
            
            if (idempotencia != null )
                return JsonSerializer.Deserialize<Domain.Entities.Movimento>(idempotencia.Resultado); 

            var conta = await _contaRepository.GetContaCorrenteById(request.IdContaCorrente);

            if (conta == null)
                throw new ArgumentException("INVALID_ACCOUNT");
            if (!conta.Ativo)
                throw new ArgumentException("INACTIVE_ACCOUNT");

            if (request.TipoMovimento != "D" && request.TipoMovimento != "C")
                throw new ArgumentException("INVALID_TYPE");

            var novoMovimento = await _movimentoRepository.AddMovimentacao(movimento);

            await _idempotenciaRepository.AddIdempotencia(new Idempotencia() { 
                Chave_Idempotencia = request.IdRequest,
                Requisicao = JsonSerializer.Serialize(request),
                Resultado = JsonSerializer.Serialize(novoMovimento)
            });

            return novoMovimento;

        }
    }

}
