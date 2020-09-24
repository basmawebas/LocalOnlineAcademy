using OnlineAcademy.Areas.StudentArea.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineAcademy.Areas.StudentArea.StudeventDataViewModel
{
    public class StudentProfileVM
    {
        public int ProfileId { get; set; }
        public StudentProfile StudentProfile { get; set; }            
    }
}