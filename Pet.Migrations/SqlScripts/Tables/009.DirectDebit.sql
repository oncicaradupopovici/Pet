IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'DirectDebit')
begin
	CREATE TABLE DirectDebit(
		Code varchar(500) NOT NULL,
		ExpenseRecipientId uniqueidentifier NULL

		CONSTRAINT [PK_DirectDebit] PRIMARY KEY CLUSTERED (Code),
		CONSTRAINT [FK_DirectDebit_ExpenseRecipientId] FOREIGN KEY(ExpenseRecipientId) REFERENCES ExpenseRecipient(ExpenseRecipientId)
	)
end