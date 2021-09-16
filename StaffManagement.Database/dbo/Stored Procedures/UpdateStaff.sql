CREATE PROCEDURE [dbo].[UpdateStaff]
@StaffID int, @StaffName nvarchar(100), @Age int, @Gender char(1), @Salary int,
@JobType varchar(100),
@UDT_JobField UDT_JobField READONLY
as
begin
	begin tran
	begin try
		update StaffDetails 
		set
		StaffName=@StaffName, Gender=@Gender, age= @Age, Salary= @Salary
		where StaffID=@StaffID

		delete from StaffJobFieldsData where StaffID=@StaffID; -- since updation between 1-> many and many-> 1
		insert into StaffJobFieldsData (StaffID,JobFieldID,JobFieldData)
		select @StaffID, JF.JobFieldId, UDT.JobFieldData from @UDT_JobField UDT
		inner join  JobFields JF on UDT.JobField= JF.JobField 
		inner join JobType JT on JT.JobTypeName= @JobType
	end try
	begin catch
		print 'Staff not updated'
	end catch
	commit tran
end
