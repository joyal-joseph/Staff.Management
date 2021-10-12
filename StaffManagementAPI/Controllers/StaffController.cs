using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StaffDatabaseHelper;
using StaffManagementConsole;


namespace StaffManagementAPI.Controllers
{
    
    [Route("api/Staff")]
    [ApiController]
    [EnableCors("_myorigins")]
    public class StaffController : ControllerBase
    {
        private static IConfiguration configuration;
        public StaffController(IConfiguration iconfiguration) { configuration = iconfiguration; }
        static IConfiguration ConfigBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        static IStaffDatabaseHandler DBHandler = new StaffDatabaseSQLHandler(ConfigBuilder.GetValue<string>("ConnectionString"));

        [HttpGet]
        public object ViewAllStaff()
        {
            List<IStaffOperation> staffList = DBHandler.ViewAllStaff().ToList();


            if (staffList == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            return StatusCode((int)HttpStatusCode.OK, staffList.ConvertAll(ConvertToModelsStaff));
        }

        [HttpGet("{StaffID}")]
        public object ViewStaff(int StaffID)
        {
            IStaffOperation staff = DBHandler.ViewStaff(StaffID);
            if(staff == null)
            {
                return NotFound();
            }
            return StatusCode((int)HttpStatusCode.OK, staff);            
        }

        [HttpPost]
        public object AddStaff([FromBody] Models.Staff staff)//post
        {
            IStaffOperation _staff = AssignStaffValuesInIStaffOperation(staff);
            if(_staff == null)
            {
                return BadRequest();
            }
            DBHandler.AddStaff(_staff);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPost]
        [Route("BulkInsert")]
        public object BulkInsertStaff([FromBody] List<Models.Staff> staffList)
        {            
            if (staffList.Count == 0)
            {
                return BadRequest();
            }
            List<IStaffOperation> _staffList = new() ;
            foreach (Models.Staff i in staffList)
            {
                IStaffOperation staff = AssignStaffValuesInIStaffOperation(i);
                if(staff == null)
                {
                    return BadRequest();
                }
                _staffList.Add(staff);
            }
            DBHandler.BulkInsert(_staffList);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut("{id}")]
        public object Update(int id, [FromBody] Models.Staff staff)
        {
            if (DBHandler.ViewStaff(id) == null)
            {
                return NotFound();
            }
            IStaffOperation _staff =null;
            if (AssignStaffValuesInIStaffOperation(staff) == null)//in case of wrong JobType
            {
                return BadRequest();
            }
            else
            {
                _staff = AssignStaffValuesInIStaffOperation( staff);
            }
            _staff.StaffID = id;
            DBHandler.UpdateStaff(_staff,id);
            return Ok();
        }
      
        [HttpDelete("{StaffID}")]
        public object DeleteStaff(int StaffID)
        {
            if (DBHandler.ViewStaff(StaffID) == null)
            {
                return NotFound();
            }
            DBHandler.DeleteStaff(StaffID);
            return Ok();
        }
        public IStaffOperation AssignStaffValuesInIStaffOperation( Models.Staff staff)
        {
            IStaffOperation _staff = null;
           
                if (staff.JobType == "Teacher")
                {
                    _staff = new Teaching { ClassTeacher = staff.ClassTeacher, Subject = staff.Subject };
                }
                else if (staff.JobType == "Support")
                {
                    _staff = new Support { Lab = staff.Lab };
                }
                else if (staff.JobType == "Admin")
                {
                    _staff = new Administrative { Section = staff.Section };
                }
                else
                {
                    return null; //For handling wrong Jobtype.
                }
                _staff.JobType = staff.JobType;
                _staff.Name = staff.Name;
                _staff.StaffID = staff.StaffID;
                if (staff.Age >= 18 && staff.Age <= 75)
                {
                    _staff.Age = staff.Age;
                }
                else
                {
                    return null;
                }
                _staff.DailyWage = staff.DailyWage;
                _staff.Gender = staff.Gender;
                return _staff;
        }
        private static Models.Staff ConvertToModelsStaff(IStaffOperation staff)
        {
            Models.Staff modelsStaff=new Models.Staff { StaffID=staff.StaffID, Name=staff.Name,
                Age=staff.Age, DailyWage=staff.DailyWage, JobType=staff.JobType, Gender= staff.Gender };
            switch (staff.JobType)
            {
                case "Teacher":

                    modelsStaff.Subject=((Teaching)staff).Subject;
                    modelsStaff.ClassTeacher= ((Teaching)staff).ClassTeacher;

                    break;
                case "Support":

                    modelsStaff.Lab= ((Support)staff).Lab;

                    break;
                case "Admin":
                    
                    modelsStaff.Section=((Administrative)staff).Section;
                    break;
            }
            return modelsStaff;
        }
    }

}
