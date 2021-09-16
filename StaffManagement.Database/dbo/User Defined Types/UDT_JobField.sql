CREATE TYPE [dbo].[UDT_JobField] AS TABLE (
	[JobFieldId] INT DEFAULT((0)) NULL
	,[JobField] VARCHAR(100) NULL
	,[JobFieldData] VARCHAR(100) NULL
	);
