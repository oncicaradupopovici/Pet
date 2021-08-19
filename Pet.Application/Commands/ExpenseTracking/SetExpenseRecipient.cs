using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NBB.Application.DataContracts;
using NBB.Data.Abstractions;
using Pet.ExpenseTracking.Domain.ExpenseAggregate;
using Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate;

namespace Pet.Application.Commands.ExpenseTracking
{
    public class SetExpenseRecipient
    {
        public class Command : NBB.Application.DataContracts.Command
        {
            public class RecipientModel
            {
                public Guid? ExpenseRecipientId { get; }
                public string Name { get; }
                public bool IsNew { get; }

                public RecipientModel(Guid? expenseRecipientId, string name, bool isNew)
                {
                    ExpenseRecipientId = expenseRecipientId;
                    Name = name;
                    IsNew = isNew;
                }
            }

            public Guid ExpenseId { get; }
            public RecipientModel Recipient { get; }

            public Command(Guid expenseId, RecipientModel recipient, CommandMetadata metadata = null) : base(metadata)
            {
                ExpenseId = expenseId;
                Recipient = recipient;
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IExpenseRepository _expenseRepository;
            private readonly IExpenseRecipientRepository _expenseRecipientRepository;

            public Handler(IExpenseRepository expenseRepository, IExpenseRecipientRepository expenseRecipientRepository)
            {
                _expenseRepository = expenseRepository;
                _expenseRecipientRepository = expenseRecipientRepository;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var expenseRecipient = request.Recipient.IsNew
                    ? new ExpenseRecipient(request.Recipient.Name)
                    : await _expenseRecipientRepository.FindById(request.Recipient.ExpenseRecipientId.Value);

                var expense = await _expenseRepository.FindById(request.ExpenseId);

                if (expense.ExpenseType == ExpenseType.PosPayment)
                {
                    expenseRecipient.AddPosTerminal(expense.ExpenseRecipientDetailCode);
                }
                else if (expense.ExpenseType == ExpenseType.BankTransfer)
                {
                    expenseRecipient.AddIban(expense.ExpenseRecipientDetailCode);
                }
                else if (expense.ExpenseType == ExpenseType.DirectDebitPayment)
                {
                    expenseRecipient.AddDirectDebit(expense.ExpenseRecipientDetailCode);
                }

                if (request.Recipient.IsNew)
                {
                    await _expenseRecipientRepository.AddAsync(expenseRecipient);
                }

                await _expenseRecipientRepository.SaveChangesAsync();

                if (expense.ExpenseType == ExpenseType.CashWithdrawal)
                {
                    expense.SetExpenseRecipient(expenseRecipient.ExpenseRecipientId);
                    expense.SetExpenseCategory(expenseRecipient.ExpenseCategoryId);
                    await _expenseRepository.UpdateAsync(expense);
                    await _expenseRepository.SaveChangesAsync();
                }
                
                return Unit.Value;
            }
        }
    }
}
