
ALTER TABLE dbo._QuoteDetail ADD
	UOM nchar(20) NULL
GO

CREATE TABLE dbo._UnitOfMeasure
	(
	ID uniqueidentifier NULL ROWGUIDCOL,
	Name nchar(20) NULL
	)  ON [PRIMARY]
GO

ALTER TABLE dbo._UnitOfMeasure ADD CONSTRAINT
	DF__UnitOfMeasure_ID DEFAULT (newid()) FOR ID
GO

ALTER TABLE dbo._UnitOfMeasure SET (LOCK_ESCALATION = TABLE)
GO

INSERT INTO dbo._UnitOfMeasure (Name) 
VALUES ('Feet')
GO

INSERT INTO dbo._UnitOfMeasure (Name) 
VALUES ('Decameter')
GO

INSERT INTO dbo._UnitOfMeasure (Name) 
VALUES ('Each')
GO

