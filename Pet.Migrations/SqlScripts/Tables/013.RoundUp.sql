IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'RoundUp')
begin
	CREATE TABLE RoundUp(
		RoundUpId uniqueidentifier NOT NULL,
		Iban varchar(100) NOT NULL,
		[Value] decimal(9,2) NOT NULL,
		PaymentDate datetime NOT NULL

		CONSTRAINT [PK_RoundUp] PRIMARY KEY CLUSTERED (RoundUpId)
	)
end