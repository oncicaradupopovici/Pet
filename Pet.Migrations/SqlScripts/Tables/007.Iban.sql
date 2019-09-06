IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'Iban')
begin
	CREATE TABLE Iban(
		Code varchar(500) NOT NULL,
		ExpenseRecipientId uniqueidentifier NULL

		CONSTRAINT [PK_Iban] PRIMARY KEY CLUSTERED (Code),
		CONSTRAINT [FK_Iban_ExpenseRecipientId] FOREIGN KEY(ExpenseRecipientId) REFERENCES ExpenseRecipient(ExpenseRecipientId)
	)
end