using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class StudentMarksVm
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int TotalMarks { get; set; }

        public int Registered { get; set; }

    }
}