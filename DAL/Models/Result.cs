using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Models
{
    public enum ResultMap
    {
        F = 0,
        E = 2,
        D = 4,
        C = 6,
        B = 8,
        A = 10
    }
    public class Result
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }  
        public int Marks { get; set; }
        public int StudentId { get; set; }
    }
}
