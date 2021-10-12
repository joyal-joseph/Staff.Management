CREATE procedure [dbo].[ViewAllStaff] 
AS
BEGIN
begin try
	select SD.*, max(JT.JobTypeName) as JobType, max(case when JF.JobField='Subject'then SJD.JobFieldData end) as Subject,
 max(case when JF.JobField='Class Teacher'then SJD.JobFieldData end) as 'Class Teacher',
 max(case when JF.JobField='Department'then SJD.JobFieldData end) as 'Support',
 max(case when JF.JobField='Section'then SJD.JobFieldData end) as 'Administrative'
from StaffDetails  SD
inner join  StaffJobFieldsData  SJD on SD.StaffID=SJD.StaffID
inner join JobFields JF on JF.JobFieldId = SJD.JobFieldID
inner join JobType JT 
on JT.JobTypeId= JF.JobTypeId
where SD.StaffID = SJD.StaffID
group by SD.StaffID, SD.age, SD.Gender, SD.Salary, SD.StaffName
end try
begin catch
	print 'Cannot view all staffs'
end catch

END
