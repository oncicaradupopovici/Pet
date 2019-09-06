IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'SavingsAccount')
begin
	CREATE TABLE SavingsAccount(
		Iban varchar(100) NOT NULL,

		CONSTRAINT [PK_SavingsAccount] PRIMARY KEY CLUSTERED (Iban)
	)
end