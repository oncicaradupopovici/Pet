IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'PosTerminal')
begin
	CREATE TABLE PosTerminal(
		Code varchar(500) NOT NULL,
		ExpenseRecipientId uniqueidentifier NULL

		CONSTRAINT [PK_PosTerminal] PRIMARY KEY CLUSTERED (Code),
		CONSTRAINT [FK_PosTerminal_ExpenseRecipientId] FOREIGN KEY(ExpenseRecipientId) REFERENCES ExpenseRecipient(ExpenseRecipientId)
	)
end