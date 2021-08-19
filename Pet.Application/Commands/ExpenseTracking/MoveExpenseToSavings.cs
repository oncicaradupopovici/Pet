using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NBB.Application.DataContracts;
using NBB.Data.Abstractions;
using Pet.ExpenseTracking.Domain.ExpenseAggregate;
using Pet.ExpenseTracking.Domain.SavingsAccountAggregate;

namespace Pet.Application.Commands.ExpenseTracking
{
    public class MoveExpenseToSavings
    {
        public class Command : NBB.Application.DataContracts.Command
        {
            public Guid ExpenseId { get; }

            public Command(Guid expenseId, CommandMetadata metadata = null) : base(metadata)
            {
                ExpenseId = expenseId;
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IExpenseRepository _expenseRepository;
            private readonly ISavingsAccountRepository _savingsAccountRepository;

            public Handler(IExpenseRepository expenseRepository, ISavingsAccountRepository savingsAccountRepository)
            {
                _expenseRepository = expenseRepository;
                _savingsAccountRepository = savingsAccountRepository;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var expense = await _expenseRepository.FindById(request.ExpenseId);
                if(expense.ExpenseType == ExpenseType.BankTransfer)
                {
                    await _savingsAccountRepository.AddAsync(new SavingsAccount(expense.ExpenseRecipientDetailCode));
                    await _savingsAccountRepository.SaveChangesAsync(cancellationToken);
                }
                return Unit.Value;
            }
        }
    }
}
