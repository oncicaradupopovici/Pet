create view vwExpenseByCategory
as
select
	agg.ExpenseMonth, 
	agg.ExpenseCategoryId,
	agg.value,
	c.Name as ExpenseCategoryName,
	row_number() over (order by agg.ExpenseMonth, agg.ExpenseCategoryId) as Id
from (
		select ExpenseMonth, ExpenseCategoryId, sum(value) as value
		from Expense
		group by ExpenseMonth, ExpenseCategoryId
	) agg
	left join ExpenseCategory c on agg.ExpenseCategoryId = c.ExpenseCategoryId