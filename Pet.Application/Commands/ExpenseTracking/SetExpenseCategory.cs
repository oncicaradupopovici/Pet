using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NBB.Application.DataContracts;
using NBB.Data.Abstractions;
using Pet.ExpenseTracking.Domain.ExpenseAggregate;
using Pet.ExpenseTracking.Domain.ExpenseCategoryAggregate;
using Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate;

namespace Pet.Application.Commands.ExpenseTracking
{
    public class SetExpenseCategory
    {
        public class Command : NBB.Application.DataContracts.Command
        {
            public class CategoryModel
            {
                public int? ExpenseCategoryId { get; }
                public string Name { get; }
                public bool IsNew { get; }

                public CategoryModel(int? expenseCategoryId, string name, bool isNew)
                {
                    ExpenseCategoryId = expenseCategoryId;
                    Name = name;
                    IsNew = isNew;
                }
            }

            public Guid ExpenseId { get; }
            public CategoryModel Category { get; }
            public bool JustThisOne { get; }

            public Command(Guid expenseId, CategoryModel category, bool justThisOne, CommandMetadata metadata = null) : base(metadata)
            {
                ExpenseId = expenseId;
                Category = category;
                JustThisOne = justThisOne;
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IExpenseRepository _expenseRepository;
            private readonly IExpenseRecipientRepository _expenseRecipientRepository;
            private readonly IExpenseCategoryRepository _expenseCategoryRepository;

            public Handler(IExpenseRepository expenseRepository, IExpenseRecipientRepository expenseRecipientRepository, IExpenseCategoryRepository expenseCategoryRepository)
            {
                _expenseRepository = expenseRepository;
                _expenseRecipientRepository = expenseRecipientRepository;
                _expenseCategoryRepository = expenseCategoryRepository;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                int expenseCategoryId;
                if (request.Category.IsNew)
                {
                    var expenseCategory = new ExpenseCategory(request.Category.Name);
                    await _expenseCategoryRepository.AddAsync(expenseCategory);
                    await _expenseCategoryRepository.SaveChangesAsync();
                    expenseCategoryId = expenseCategory.ExpenseCategoryId;
                }
                else
                {
                    expenseCategoryId = request.Category.ExpenseCategoryId.Value;
                }

                var expense = await _expenseRepository.FindById(request.ExpenseId);
                if (!request.JustThisOne && expense.ExpenseRecipientId.HasValue)
                {
                    var expenseRecipient = await _expenseRecipientRepository.FindById(expense.ExpenseRecipientId.Value);
                    expenseRecipient.ChangeExpenseCategory(expenseCategoryId, expense.ExpenseMonth);
                    await _expenseRecipientRepository.SaveChangesAsync();
                }
                else
                {
                    expense.SetExpenseCategory(expenseCategoryId);
                    await _expenseRepository.SaveChangesAsync();
                }
            }
        }
    }
}
