IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'ExpenseCategory')
begin
	CREATE TABLE ExpenseCategory(
		ExpenseCategoryId int identity(1,1) NOT NULL,
		Name varchar(100) NOT NULL

		CONSTRAINT [PK_ExpenseCategory] PRIMARY KEY CLUSTERED (ExpenseCategoryId)
	)
end