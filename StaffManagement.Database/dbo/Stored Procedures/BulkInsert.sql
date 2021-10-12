CREATE PROCEDURE BulkInsert @UDT_BulkInsertStaffDetails UDT_BulkInsertStaffDetails READONLY
	,@UDT_StaffJobFieldsData UDT_StaffJobFieldsData READONLY
AS
BEGIN
	BEGIN TRAN

	BEGIN TRY
		INSERT INTO StaffDetails (
			StaffName
			,age
			,Gender
			,Salary
			)
		SELECT StaffName
			,age
			,Gender
			,Salary
		FROM @UDT_BulkInsertStaffDetails

		DECLARE @LastStaffID INT;
		DECLARE @StaffCountInUDT INT;

		SET @LastStaffID = SCOPE_IDENTITY();
		SET @StaffCountInUDT = (
				SELECT COUNT(*)
				FROM @UDT_BulkInsertStaffDetails
				)

		INSERT INTO StaffJobFieldsData
		SELECT /*Will be equal to StaffID in DB*/ @LastStaffID - (@StaffCountInUDT - TempID)
			,JF.JobFieldId
			,JobFieldData
		FROM @UDT_StaffJobFieldsData AS UDT
		INNER JOIN JobFields AS JF ON JF.JobField = UDT.JobField
	END TRY

	BEGIN CATCH
		PRINT 'List of staff not added to DB.'
	END CATCH

	COMMIT TRAN
END
