using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    class StudentEnrollViewModel
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
            public Status Status { get; set; }
            public int TotalMarks
            {
                get
                {
                    return this.Results.Sum(result => result.Marks);
                }

            }
            public DateTime DateOfBirth { get; set; }
            public List<Result> Results { get; set; }
        }

    
}
