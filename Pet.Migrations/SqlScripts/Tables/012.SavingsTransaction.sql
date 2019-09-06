IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'SavingsTransaction')
begin
	CREATE TABLE SavingsTransaction(
		SavingsTransactionId uniqueidentifier NOT NULL,
		SavingsTransactionType tinyint not null,
		[Value] decimal(9,2) not null,
		TransactionDate datetime not null,
		TransactionMonth int NOT NULL,
		Details1 varchar(500) NOT NULL,
		Details2 varchar(500) NOT NULL,
		SourceId uniqueidentifier NULL,

		CONSTRAINT [PK_SavingsTransaction] PRIMARY KEY CLUSTERED (SavingsTransactionId)
	)
end