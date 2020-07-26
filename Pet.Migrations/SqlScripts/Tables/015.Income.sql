IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'Income')
begin
	CREATE TABLE Income(
		IncomeId uniqueidentifier NOT NULL,
		IncomeType tinyint not null,
		[Value] decimal(9,2) not null,
		IncomeDate datetime not null,
		IncomeMonth int NOT NULL,
		[From] varchar(100) NOT NULL,
		Details1 varchar(500) NOT NULL,
		Details2 varchar(500) NOT NULL,
		SourceId uniqueidentifier NULL,

		CONSTRAINT [PK_Income] PRIMARY KEY CLUSTERED (IncomeId)
	)
end