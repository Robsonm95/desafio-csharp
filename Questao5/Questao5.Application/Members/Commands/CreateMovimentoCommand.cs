using FluentValidation;
using MediatR;
using Questao5.Domain.Abstractions;
using Questao5.Domain.Entities;
using System.Text.Json;

namespace Questao5.Application.Members.Commands;
public class CreateMovimentoCommand : MovimentoCommandBase
{
    public class CreateMovimentoCommandHandler : IRequestHandler<CreateMovimentoCommand, Movimento>
    {
        private readonly IMovimentacaoRepository _memberRepository;
        private readonly IContaCorrenteRepository _contaRepository;
        private readonly IIdempotenciaRepository _idempotenciaRepository;
        private readonly IValidator<CreateMovimentoCommand> _validator;
        
        public CreateMovimentoCommandHandler(IMovimentacaoRepository memberRepository,
                                          IContaCorrenteRepository contaRepository,
                                          IIdempotenciaRepository idempotenciaRepository,
                                          IValidator<CreateMovimentoCommand> validator)
        {
            _contaRepository = contaRepository;
            _memberRepository = memberRepository;
            _idempotenciaRepository = idempotenciaRepository;
            _validator = validator;
        }
        public async Task<Movimento> Handle(CreateMovimentoCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            var movimento = new Movimento(request.IdContaCorrente, request.DataMovimento, request.TipoMovimento, request.Valor);

            var idempotencia = await _idempotenciaRepository.GetIdempotenciaById(request.IdRequest);
            
            if (idempotencia != null )
                return JsonSerializer.Deserialize<Movimento>(idempotencia.Resultado); 

            var conta = await _contaRepository.GetContaCorrenteById(request.IdContaCorrente);

            if (conta == null)
                throw new ArgumentException("INVALID_ACCOUNT");
            if (!conta.Ativo)
                throw new ArgumentException("INACTIVE_ACCOUNT");

            if (request.TipoMovimento != "D" && request.TipoMovimento != "C")
                throw new ArgumentException("INVALID_TYPE");

            var novoMovimento = await _memberRepository.AddMovimentacao(movimento);

            await _idempotenciaRepository.AddIdempotencia(new Idempotencia() { 
                Chave_Idempotencia = request.IdRequest,
                Requisicao = JsonSerializer.Serialize(request),
                Resultado = JsonSerializer.Serialize(novoMovimento)
            });

            return novoMovimento;

        }
    }

}
