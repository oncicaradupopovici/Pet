IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'Exchange')
begin
	CREATE TABLE Exchange(
		ExchangeId uniqueidentifier NOT NULL,
		Iban varchar(100) NOT NULL,
		ExchangeValue varchar(100) NOT NULL,
		ExchangeRate varchar(100) NOT NULL,
		Details varchar(100) NOT NULL,
		[Value] decimal(9,2) NOT NULL,
		PaymentDate datetime NOT NULL

		CONSTRAINT [PK_Exchange] PRIMARY KEY CLUSTERED (ExchangeId)
	)
end