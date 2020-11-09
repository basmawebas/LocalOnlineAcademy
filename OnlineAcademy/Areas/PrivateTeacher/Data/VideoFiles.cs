using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineAcademy.Areas.PrivateTeacher.Data
{
    public class VideoFiles
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> FileSize { get; set; }
        public string FilePath { get; set; }
    }
}