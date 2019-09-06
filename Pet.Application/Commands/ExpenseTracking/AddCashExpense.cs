using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NBB.Application.DataContracts;
using NBB.Data.Abstractions;
using Pet.ExpenseTracking.Domain.ExpenseAggregate;

namespace Pet.Application.Commands.ExpenseTracking
{
    public class AddCashExpense
    {
        public class Command : NBB.Application.DataContracts.Command
        {
            public decimal Value { get; }
            public DateTime ExpenseDate { get; }
            public string Details1 { get; }
            public string Details2 { get; }

            public Command(decimal value, DateTime expenseDate, string details1, string details2, CommandMetadata metadata = null) : base(metadata)
            {
                Value = value;
                ExpenseDate = expenseDate;
                Details1 = details1;
                Details2 = details2;
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IExpenseRepository _expenseRepository;
            private readonly ExpenseFactory _expenseFactory;

            public Handler(IExpenseRepository expenseRepository, ExpenseFactory expenseFactory)
            {
                _expenseRepository = expenseRepository;
                _expenseFactory = expenseFactory;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var expense = _expenseFactory.CreateFrom(ExpenseType.CashExpense, request.Value,
                    request.ExpenseDate, null, null, null, request.Details1, request.Details2, null);
                await _expenseRepository.AddAsync(expense);
                await _expenseRepository.SaveChangesAsync();
            }
        }
    }
}
