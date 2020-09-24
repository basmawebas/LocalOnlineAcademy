using OnlineAcademy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineAcademy.Areas.StudentArea.Data
{
    public class StudentProfile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreateDateTime { get; set; }
        public bool IsActive { get; set; }
        public string ProfileImage { get; set; }
        public string AddressDetails { get; set; }
        public int CountryId { get; set; }
        public int EstateId { get; set; }
        public int CityId { get; set; }
        public int SchooleId { get; set; }
        public int TermId { get; set; }
        public int StudyYearId { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual STUser STUser { get; set; }

    }

    public class StudyYear
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class CurrentTerm
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}