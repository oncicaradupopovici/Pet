IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'BankTransfer')
begin
	CREATE TABLE BankTransfer(
		BankTransferId uniqueidentifier NOT NULL,
		Iban varchar(100) NOT NULL,
		RecipientName varchar(200) NOT NULL,
		Details varchar(500) NOT NULL,
		[Value] decimal(9,2) NOT NULL,
		PaymentDate datetime NOT NULL

		CONSTRAINT [PK_BankTransfer] PRIMARY KEY CLUSTERED (BankTransferId)
	)
end