using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NBB.Application.DataContracts;
using NBB.Data.Abstractions;
using Pet.ExpenseTracking.Domain.ExpenseAggregate;
using Pet.ExpenseTracking.Domain.SavingsAccountAggregate;
using Pet.ExpenseTracking.Domain.SavingsCategoryAggregate;

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
            private readonly ISavingsCategoryRepository _savingsCategoryRepository;

            public Handler(IExpenseRepository expenseRepository, ISavingsAccountRepository savingsAccountRepository, ISavingsCategoryRepository savingsCategoryRepository)
            {
                _expenseRepository = expenseRepository;
                _savingsAccountRepository = savingsAccountRepository;
                _savingsCategoryRepository = savingsCategoryRepository;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var expense = await _expenseRepository.FindById(request.ExpenseId);
                if(expense.ExpenseType == ExpenseType.BankTransfer)
                {
                    await _savingsAccountRepository.AddAsync(new SavingsAccount(expense.ExpenseRecipientDetailCode));
                    await _savingsAccountRepository.SaveChangesAsync();
                }
                else if(expense.ExpenseType == ExpenseType.OpenBankingPayment)
                {
                    await _savingsCategoryRepository.AddAsync(new SavingsCategory(expense.SourceCategory));
                    await _savingsCategoryRepository.SaveChangesAsync();
                }
            }
        }
    }
}
