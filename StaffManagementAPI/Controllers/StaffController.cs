using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StaffDatabaseHelper;
using StaffManagementConsole;

namespace StaffManagementAPI.Controllers
{
    
    [Route("api/Staff")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private static IConfiguration configuration;
        public StaffController(IConfiguration iconfiguration) { configuration = iconfiguration; }
        static IConfiguration ConfigBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        static IStaffDatabaseHandler DBHandler = new StaffDatabaseSQLHandler(ConfigBuilder.GetValue<string>("ConnectionString"));

        [HttpGet]
        public object ViewAllStaff()
        {
            List<IStaffOperation> staffList = DBHandler.ViewAllStaff();
            if (staffList == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            return StatusCode((int)HttpStatusCode.OK, staffList);
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
            IStaffOperation _staff = null;
            _staff = AssignStaffValuesInIStaffOperation(staff);
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
    }
}
