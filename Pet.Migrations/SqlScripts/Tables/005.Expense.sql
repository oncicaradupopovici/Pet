IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'Expense')
begin
	CREATE TABLE Expense(
		[ExpenseId] [uniqueidentifier] NOT NULL,
		[ExpenseType] [tinyint] NOT NULL,
		[Value] [decimal](9, 2) NOT NULL,
		[ExpenseDate] [datetime] NOT NULL,
		[ExpenseRecipientId] [uniqueidentifier] NULL,
		[ExpenseCategoryId] [int] NULL,
		[ExpenseMonth] [int] NOT NULL,
		[ExpenseSourceId] [uniqueidentifier] NULL,
		[ExpenseRecipientDetailCode] [varchar](500) NULL,
		[Details1] [varchar](500) NULL,
		[Details2] [varchar](500) NULL,

		CONSTRAINT [PK_Expense] PRIMARY KEY CLUSTERED (ExpenseId),
		CONSTRAINT [FK_Expense_ExpenseCategoryId] FOREIGN KEY([ExpenseCategoryId]) REFERENCES [ExpenseCategory] ([ExpenseCategoryId]),
		CONSTRAINT [FK_Expense_ExpenseRecipientId] FOREIGN KEY([ExpenseRecipientId]) REFERENCES [ExpenseRecipient] ([ExpenseRecipientId])
	)
end