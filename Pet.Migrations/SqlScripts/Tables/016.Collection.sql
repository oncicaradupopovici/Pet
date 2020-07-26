IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'Collection')
begin
	CREATE TABLE [Collection](
		CollectionId uniqueidentifier NOT NULL,
		[From] varchar(100) NOT NULL,
		FromIban varchar(100) NOT NULL,
		Details varchar(500) NOT NULL,
		[Value] decimal(9,2) NOT NULL,
		IncomeDate datetime NOT NULL

		CONSTRAINT [PK_Collection] PRIMARY KEY CLUSTERED (CollectionId)
	)
end