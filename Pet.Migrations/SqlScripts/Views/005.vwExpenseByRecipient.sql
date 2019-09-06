create view vwExpenseByRecipient
as
select
	agg.ExpenseMonth,
	agg.ExpenseRecipientId,
	agg.value,
	r.Name as ExpenseRecipientName,
	row_number() over (order by agg.ExpenseMonth, agg.ExpenseRecipientId) as Id
from (
		select ExpenseMonth, ExpenseRecipientId, sum(value) as value
		from Expense
		group by ExpenseMonth, ExpenseRecipientId
	) agg
	left join ExpenseRecipient r on agg.ExpenseRecipientId = r.ExpenseRecipientId