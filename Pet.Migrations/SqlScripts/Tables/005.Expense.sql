create view vwExpense
as
select 
	e.ExpenseId,
	e.ExpenseType,
	e.Value,
	e.ExpenseDate,
	e.ExpenseRecipientId,
	e.ExpenseCategoryId,
	e.ExpenseMonth,
	r.Name as ExpenseRecipientName,
	c.Name as ExpenseCategoryName,
	e.ExpenseSourceId as ExpenseSourceId,
	e.ExpenseRecipientDetailCode,
	e.Details1,
	e.Details2
from Expense e
	left join ExpenseRecipient r on e.ExpenseRecipientId = r.ExpenseRecipientId
	left join ExpenseCategory c on e.ExpenseCategoryId = c.ExpenseCategoryId