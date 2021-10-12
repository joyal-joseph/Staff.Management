CREATE PROCEDURE ViewStaff (@ID INT)
AS
BEGIN
	SELECT SD.*
		,max(JT.JobTypeName) AS JobType
		,max(CASE 
				WHEN JF.JobField = 'Subject'
					THEN SJD.JobFieldData
				END) AS Subject
		,max(CASE 
				WHEN JF.JobField = 'Class Teacher'
					THEN SJD.JobFieldData
				END) AS 'Class Teacher'
		,max(CASE 
				WHEN JF.JobField = 'Department'
					THEN SJD.JobFieldData
				END) AS 'Support'
		,max(CASE 
				WHEN JF.JobField = 'Section'
					THEN SJD.JobFieldData
				END) AS 'Administrative'
	FROM StaffDetails SD
	INNER JOIN StaffJobFieldsData SJD ON SD.StaffID = SJD.StaffID
	INNER JOIN JobFields JF ON JF.JobFieldId = SJD.JobFieldID
	INNER JOIN JobType JT ON JT.JobTypeId = JF.JobTypeId
	WHERE SD.StaffID = @ID
	GROUP BY SD.StaffID
		,SD.age
		,SD.Gender
		,SD.Salary
		,SD.StaffName
END
