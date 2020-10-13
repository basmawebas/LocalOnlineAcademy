using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineAcademy.Areas.PrivateTeacher.Data
{
    public class PTAssistant
    {
        [Key]
        public int Id { get; set; }
        //public string F { get; set; }
        //public string L { get; set; }
        //public string Day { get; set; }
        //public string montrh { get; set; }
        //public string year { get; set; }
        public DateTime DateTime { get; set; }
        
        public string AdDetails { get; set; }
        public DateTime CreateDate { get; set; }
        public float salary { get; set; }
        //public HttpPostedFileBase File { get; set; }
        public string Duration { get; set; }
        public bool Statue { get; set; }

        //public virtual AspNetUsers AspNetUsers { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual AspNetUsers PTUser { get; set; }

        //public string FullName
        //{
        //    get
        //    {
        //        return F + "" + L; 
        //    }

        //}
        //public string GetBirthDay
        //{
        //    get
        //    {
        //        return Day + "/" + montrh + "/" + year;
        //    }
        //}
    }


    //public class ViewModelCalss
    //{
    //    public List<PTAssistant> pTAssistants { get; set; }
    //    public List<PTAssistant> sd { get; set; }
    //}
}