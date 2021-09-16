CREATE TABLE [dbo].[StaffDetails] (
	[StaffID] INT IDENTITY(1, 1) NOT NULL
	,[StaffName] NVARCHAR(100) NULL
	,[Gender] NVARCHAR(10) NULL
	,[age] INT NULL
	,[Salary] INT NULL
	,PRIMARY KEY CLUSTERED ([StaffID] ASC)
	);
