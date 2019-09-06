IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'DirectDebitPayment')
begin
	CREATE TABLE DirectDebitPayment(
		DirectDebitPaymentId uniqueidentifier NOT NULL,
		DirectDebitCode varchar(500) NOT NULL,
		[Value] decimal(9,2) NOT NULL,
		Details varchar(500) NOT NULL,
		PaymentDate datetime NOT NULL

		CONSTRAINT [PK_DirectDebitPayment] PRIMARY KEY CLUSTERED (DirectDebitPaymentId)
	)
end