IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'PosPayment')
begin
	CREATE TABLE PosPayment(
		PosPaymentId uniqueidentifier NOT NULL,
		PosTerminalCode varchar(500) NOT NULL,
		[Value] decimal(9,2) NOT NULL,
		PaymentDate datetime NOT NULL

		CONSTRAINT [PK_PosPayment] PRIMARY KEY CLUSTERED (PosPaymentId)
	)
end