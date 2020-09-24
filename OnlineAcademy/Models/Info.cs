using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineAcademy.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Estate>  Estates { get; set; }
    }

    public class Estate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; }
        public ICollection<City> Cities { get; set; }
    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EstateId { get; set; }
        [ForeignKey(nameof(EstateId))]
        public virtual Estate Estate { get; set; }
    }

    public class Schoole
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
   
}