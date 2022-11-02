using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace Models
{
    public enum EnrollmentStatus
    {
        Rejected = 0,
        Pending = 1,
        Approved = 2
    }


    public class Student
    {
        public int UserId { get; set; }
        public int StudentId { get; set; }
        public string GuardianName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string NationalIdentity { get; set; }
        public string Address { get; set; }
        public EnrollmentStatus EnrollmentStatus { get; set; }
        public string EnrollmentStatusName { get
            {
                return Enum.GetName(typeof(EnrollmentStatus),this.EnrollmentStatus);
            } 
        }
        public int TotalMarks { get
            {
                return this.Results.Sum(result => result.Marks); 
            }

            }
        public DateTime DateOfBirth { get; set; }
        public List<Result> Results { get; set; }
    }
}

























