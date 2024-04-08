using Questao5.Domain.Abstractions;
using Questao5.Domain.Entities;
using MediatR;
using Questao5.Application.Members.Queries.Response;

namespace Questao5.Application.Members.Queries;

public class SaldoContaByIdQuery : IRequest<SaldoContaResponse>
{
    public string Id { get; set; }

    public class GetMovimentacaoByIdQueryHandler : IRequestHandler<SaldoContaByIdQuery, SaldoContaResponse>
    {
        private readonly IMovimentacaoRepository _movimentacaoRepository;

        public GetMovimentacaoByIdQueryHandler(IMovimentacaoRepository movimentacaoRepository)
        {
            _movimentacaoRepository = movimentacaoRepository;
        }

        public async Task<SaldoContaResponse> Handle(SaldoContaByIdQuery request, CancellationToken cancellationToken)
        {
            var member = await _movimentacaoRepository.GetMovimentacaoById(request.Id);
            var result = member.Select(x => new { Valor = x.Valor * (x.TipoMovimento == "D" ? -1m : 1m) }).Sum(x => x.Valor);
            return new SaldoContaResponse { Saldo = result };
        }
    }
}