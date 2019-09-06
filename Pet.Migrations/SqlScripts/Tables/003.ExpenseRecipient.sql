IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'ExpenseRecipient')
begin
	CREATE TABLE ExpenseRecipient(
		ExpenseRecipientId uniqueidentifier NOT NULL,
		Name varchar(100) NOT NULL,
		ExpenseCategoryId int NULL,
		PosTerminalMatchPattern varchar(100) NULL

		CONSTRAINT [PK_ExpenseRecipient] PRIMARY KEY CLUSTERED (ExpenseRecipientId),
		CONSTRAINT [FK_ExpenseRecipient_ExpenseCategoryId] FOREIGN KEY(ExpenseCategoryId) REFERENCES ExpenseCategory(ExpenseCategoryId)
	)
end