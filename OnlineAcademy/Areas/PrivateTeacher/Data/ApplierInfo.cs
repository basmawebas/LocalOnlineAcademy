using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineAcademy.Areas.PrivateTeacher.Data
{
    public class ApplierInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public float Age { get; set; }
        public string GraduationLevel  { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }
        public int advertiseId { get; set; }
        [ForeignKey(nameof(advertiseId))]
        public virtual PTAssistant PTAssistant { get; set; }
    }
}