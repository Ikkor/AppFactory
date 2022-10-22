using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class StudentRegisterVm
    {



        public int UserId { get; set; }
        public int Id { get; set; }

        [Display(Name ="Your guadian's full name: ")]
        public string GuardianName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        [Display(Name = "National Identity Card digits: ")]

        public string NID { get; set; }
        [Display(Name = "Residential Address: ")]

        public string Address { get; set; }
        [Display(Name = "Date of Birth: ")]


        [DataType(DataType.Date)]
        public DateTime DoB { get; set; }

        public List<Result> Results { get; set; }

        public List<Subject> SubjectList { get; set; }

        public int SelectedSubjectId { get; set; }



    }

}