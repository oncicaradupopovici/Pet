create view vwExpenseMonth
as
With 
ExpenseMonth AS(
	select ExpenseMonth as MonthId, sum(Value) as TotalExpenses
	from Expense
	group by ExpenseMonth
),
SavingsMonth AS(
	select TransactionMonth as MonthId, sum(Value) as TotalSavings
	from SavingsTransaction
	group by TransactionMonth
)
select e.MonthId as ExpenseMonthId, isnull(e.TotalExpenses, 0) as TotalExpenses, isnull(s.TotalSavings, 0) as TotalSavings
from ExpenseMonth e
	left join SavingsMonth s on e.MonthId = s.MonthId