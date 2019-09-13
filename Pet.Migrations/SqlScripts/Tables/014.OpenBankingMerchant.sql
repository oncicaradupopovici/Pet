IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'OpenBankingMerchant')
begin
	CREATE TABLE OpenBankingMerchant(
		Code varchar(500) NOT NULL,
		ExpenseRecipientId uniqueidentifier NULL

		CONSTRAINT [PK_OpenBankingMerchant] PRIMARY KEY CLUSTERED (Code),
		CONSTRAINT [FK_OpenBankingMerchant_ExpenseRecipientId] FOREIGN KEY(ExpenseRecipientId) REFERENCES ExpenseRecipient(ExpenseRecipientId)
	)
end