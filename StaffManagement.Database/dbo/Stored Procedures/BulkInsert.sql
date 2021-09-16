create procedure BulkInsert
@UDT_BulkInsertStaffDetails UDT_BulkInsertStaffDetails READONLY,
@UDT_StaffJobFieldsData UDT_StaffJobFieldsData READONLY
as
begin
	begin tran
	begin try
		INSERT INTO StaffDetails(StaffName, age, Gender, Salary)
		SELECT StaffName, age, Gender, Salary FROM @UDT_BulkInsertStaffDetails

		DECLARE @LastStaffID INT;
		DECLARE @StaffCountInUDT INT;
		SET @LastStaffID = SCOPE_IDENTITY();
		SET @StaffCountInUDT = (SELECT COUNT(*) FROM @UDT_BulkInsertStaffDetails)
		
		INSERT INTO StaffJobFieldsData
		select /*Will be equal to StaffID in DB*/@LastStaffID - (@StaffCountInUDT - TempID), JF.JobFieldId, JobFieldData 
		from @UDT_StaffJobFieldsData as UDT 
		inner join JobFields as JF on JF.JobField=UDT.JobField

	end try
	begin catch
		print 'List of staff not added to DB.'
	end catch
	commit tran
end