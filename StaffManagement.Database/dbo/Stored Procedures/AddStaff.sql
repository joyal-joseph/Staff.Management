CREATE procedure [dbo].[AddStaff]
@StaffName varchar(100), @Age int, @Gender char(1), @Salary int,
@JobType varchar(100),
@UDT_JobField UDT_JobField READONLY
as
BEGIN
	begin tran
	DECLARE @StaffID AS int
	begin try
		insert into StaffDetails(StaffName,Gender,age,Salary) values (@StaffName, @Gender, @Age,@Salary)
		SET @StaffID = SCOPE_IDENTITY();

	DECLARE @JobTypeId as int
	SET @JobTypeId  =
	
		(SELECT JobTypeId from JobType where JobTypeName=@JobType)

		insert into StaffJobFieldsData ( StaffID, JobFieldID, JobFieldData) 
		select @StaffID, JF.JobFieldId, UDT.JobFieldData from @UDT_JobField UDT
		inner join  JobFields JF on UDT.JobField= JF.JobField 
		inner join JobType JT on JT.JobTypeName= @JobType
	end try
	begin catch
		print 'Staff Job-Field details not added'
	end catch
	 
	commit tran

END
