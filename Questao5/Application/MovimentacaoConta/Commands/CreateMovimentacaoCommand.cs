using CleanArch.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Questao5.Application.MovimentacaoConta.Commands;
public class CreateMovimentacaoCommand : MemberCommandBase
{
    public class CreateMemberCommandHandler : IRequestHandler<CreateMovimentacaoCommand, Member>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateMovimentacaoCommand> _validator;
        private readonly IMediator _mediator;
        public CreateMemberCommandHandler(IUnitOfWork unitOfWork,
                                          IValidator<CreateMovimentacaoCommand> validator,
                                          IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mediator = mediator;
        }

        public async Task<Member> Handle(CreateMovimentacaoCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            var newMember = new Member(request.FirstName, request.LastName, request.Gender, request.Email, request.IsActive);

            await _unitOfWork.MemberRepository.AddMember(newMember);
            await _unitOfWork.CommitAsync();

            await _mediator.Publish(new MemberCreatedNotification(newMember), cancellationToken);

            return newMember;
        }
    }

}
