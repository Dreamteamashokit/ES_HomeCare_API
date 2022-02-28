using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Model
{
    public class Student
    {
        public int SID { get; set; }
        public string SName { get; set; }
        public IEnumerable<Course> Course { get; set; }
    }

    public class Course
    {
        public int CID { get; set; }
        public string CName { get; set; }
    }

}
