using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NBB.Application.DataContracts;
using NBB.Data.Abstractions.Linq;

namespace Pet.Application.Queries.ExpenseTracking
{
    public class GetExpenseList
    {
        public class Query : Query<List<Model>>
        {
            public int ExpenseMonthId { get; set; }
        }

        public class Model
        {
            public class RecipientModel
            {
                public Guid ExpenseRecipientId { get; }
                public string Name { get; }

                public RecipientModel(Guid expenseRecipientId, string name)
                {
                    ExpenseRecipientId = expenseRecipientId;
                    Name = name;
                }
            }

            public class CategoryModel
            {
                public int ExpenseCategoryId { get; }
                public string Name { get; }

                public CategoryModel(int expenseCategoryId, string name)
                {
                    ExpenseCategoryId = expenseCategoryId;
                    Name = name;
                }
            }

            public Guid ExpenseId { get; }
            public Byte ExpenseType { get; }
            public decimal Value { get; }
            public DateTime ExpenseDate { get; }
            public RecipientModel ExpenseRecipient { get; }
            public CategoryModel ExpenseCategory { get; }
            public Guid? ExpenseSourceId { get; }
            public string ExpenseRecipientDetailCode { get; }
            public string Details1 { get; }
            public string Details2 { get; }
            public string SourceCategory { get; }
            

            public Model(Guid expenseId, byte expenseType, decimal value, DateTime expenseDate, RecipientModel expenseRecipient, CategoryModel expenseCategory, Guid? expenseSourceId, string expenseRecipientDetailCode, string details1, string details2, string sourceCategory)
            {
                ExpenseId = expenseId;
                ExpenseType = expenseType;
                Value = value;
                ExpenseDate = expenseDate;
                ExpenseRecipient = expenseRecipient;
                ExpenseCategory = expenseCategory;
                ExpenseSourceId = expenseSourceId;
                ExpenseRecipientDetailCode = expenseRecipientDetailCode;
                Details1 = details1;
                Details2 = details2;
                SourceCategory = sourceCategory;
            }
        }

        public class QueryHandler : IRequestHandler<Query, List<Model>>
        {
            private readonly IQueryable<ReadModel.Projections.Expense> _query;

            public QueryHandler(IQueryable<ReadModel.Projections.Expense> query)
            {
                _query = query;
            }


            public async Task<List<Model>> Handle(Query request, CancellationToken cancellationToken)
            {
                var items = await _query.Where(x => x.ExpenseMonth == request.ExpenseMonthId).ToListAsync(cancellationToken);

                var result = items.Select(x => new Model(
                    x.ExpenseId,
                    x.ExpenseType,
                    x.Value,
                    x.ExpenseDate,
                    x.ExpenseRecipientId.HasValue ? new Model.RecipientModel(x.ExpenseRecipientId.Value, x.ExpenseRecipientName) : null,
                    x.ExpenseCategoryId.HasValue ? new Model.CategoryModel(x.ExpenseCategoryId.Value, x.ExpenseCategoryName) : null,
                    x.ExpenseSourceId,
                    x.ExpenseRecipientDetailCode,
                    x.Details1,
                    x.Details2,
                    x.SourceCategory
                    )).OrderByDescending(x => x.ExpenseDate).ToList();

                return result;
            }
        }
    }
}
