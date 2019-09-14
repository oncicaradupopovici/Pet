IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'SavingsCategory')
begin
	CREATE TABLE SavingsCategory(
		Category varchar(100) NOT NULL,

		CONSTRAINT [PK_SavingsCategory] PRIMARY KEY CLUSTERED (Category)
	)
end