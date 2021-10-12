CREATE TABLE [dbo].[JobFields] (
	[JobFieldId] INT IDENTITY(1, 1) NOT NULL
	,[JobField] VARCHAR(100) NULL
	,[JobTypeId] INT NULL
	,PRIMARY KEY CLUSTERED ([JobFieldId] ASC)
	);
