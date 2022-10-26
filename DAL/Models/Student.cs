using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Models
{
    public class Student
    {//use viewmodels
        
        public int UserId { get; set; }
        public int Id { get; set; }
        public string GuardianName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Phone { get; set; }   

        public string Email { get; set; }
        public string NID { get; set; }
        public string Address { get; set; }

        public Status Status { get; set; }

        public int TotalMarks { get; set; }

        public DateTime DoB { get; set; }

        public List<Result> Results { get; set; }

    }

}