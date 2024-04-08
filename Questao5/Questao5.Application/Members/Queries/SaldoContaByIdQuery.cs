using MediatR;
using Questao5.Application.Movimento.Queries.Response;
using Questao5.Domain.Abstractions;

namespace Questao5.Application.Movimento.Queries;

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
            var movimento = await _movimentacaoRepository.GetMovimentacaoById(request.Id);
            var result = movimento.Select(x => new { Valor = x.Valor * (x.TipoMovimento == "D" ? -1m : 1m) }).Sum(x => x.Valor);
            return new SaldoContaResponse { Saldo = result };
        }
    }
}