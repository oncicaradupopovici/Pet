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
	p.PosTerminalCode as ExpenseSourceRecipientCode,
	null as ExpenseSourceDetails
from Expense e
	left join ExpenseRecipient r on e.ExpenseRecipientId = r.ExpenseRecipientId
	left join ExpenseCategory c on e.ExpenseCategoryId = c.ExpenseCategoryId
	left join PosPayment p on p.PosPaymentId = e.ExpenseSourceId
where ExpenseType = 1
UNION
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
	cw.CashTerminal as ExpenseSourceRecipientCode,
	null as ExpenseSourceDetails
from Expense e
	left join ExpenseRecipient r on e.ExpenseRecipientId = r.ExpenseRecipientId
	left join ExpenseCategory c on e.ExpenseCategoryId = c.ExpenseCategoryId
	left join CashWithdrawal cw on cw.CashWithdrawalId = e.ExpenseSourceId
where ExpenseType = 2
UNION
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
	bt.RecipientName as ExpenseSourceRecipientCode,
	bt.Details as ExpenseSourceDetails
from Expense e
	left join ExpenseRecipient r on e.ExpenseRecipientId = r.ExpenseRecipientId
	left join ExpenseCategory c on e.ExpenseCategoryId = c.ExpenseCategoryId
	left join BankTransfer bt on bt.BankTransferId = e.ExpenseSourceId
where ExpenseType = 3
UNION
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
	ddp.DirectDebitCode as ExpenseSourceRecipientCode,
	ddp.Details as ExpenseSourceDetails
from Expense e
	left join ExpenseRecipient r on e.ExpenseRecipientId = r.ExpenseRecipientId
	left join ExpenseCategory c on e.ExpenseCategoryId = c.ExpenseCategoryId
	left join DirectDebitPayment ddp on ddp.DirectDebitPaymentId = e.ExpenseSourceId
where ExpenseType = 4