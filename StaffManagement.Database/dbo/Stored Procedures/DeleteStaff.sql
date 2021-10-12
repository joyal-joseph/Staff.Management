CREATE procedure [dbo].[DeleteStaff](@ID int)
AS
BEGIN
	begin tran
	begin try
		delete from StaffJobFieldsData where StaffID=@ID;
		delete from StaffDetails where StaffID=@ID; 
	end try
	begin catch
		print 'StaffDetails not deleted'
	end catch
	commit tran
END
