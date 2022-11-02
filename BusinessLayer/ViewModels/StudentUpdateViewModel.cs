using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class StudentUpdateViewModel
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
    
            public DateTime DateOfBirth { get; set; }
        }

    
}
