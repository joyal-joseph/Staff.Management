CREATE TYPE [dbo].[StaffDetailsUDT] AS TABLE (
	[StaffName] NVARCHAR(100) NULL
	,[Gender] NVARCHAR(10) NULL
	,[age] INT NULL
	,[Salary] INT NULL
	,[JobFieldID1] INT NULL
	,[JobFieldData1] VARCHAR(100) NULL
	,[JobFieldID2] INT NULL
	,[JobFieldData2] VARCHAR(100) NULL
	);
