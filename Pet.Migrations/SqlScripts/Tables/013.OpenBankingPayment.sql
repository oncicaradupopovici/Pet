IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'OpenBankingPayment')
begin
	CREATE TABLE OpenBankingPayment(
		OpenBankingPaymentId uniqueidentifier NOT NULL,
		[Value] decimal(9,2) NOT NULL,
		PaymentDate datetime NOT NULL,
		Currency varchar(10) NOT NULL,
		ExchangeRate decimal(9,2) NOT NULL,
		Merchant varchar(100) NOT NULL,
		Category varchar(100) NOT NULL,
		Location varchar(100) NOT NULL,

		CONSTRAINT [PK_OpenBankingPayment] PRIMARY KEY CLUSTERED (OpenBankingPaymentId)
	)
end