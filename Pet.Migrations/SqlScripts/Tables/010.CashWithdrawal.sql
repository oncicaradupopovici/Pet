IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'CashWithdrawal')
begin
	CREATE TABLE CashWithdrawal(
		CashWithdrawalId uniqueidentifier NOT NULL,
		CashTerminal varchar(100) NOT NULL,
		[Value] decimal(9,2) NOT NULL,
		WithdrawalDate datetime NOT NULL

		CONSTRAINT [PK_CashWithdrawal] PRIMARY KEY CLUSTERED (CashWithdrawalId)
	)
end