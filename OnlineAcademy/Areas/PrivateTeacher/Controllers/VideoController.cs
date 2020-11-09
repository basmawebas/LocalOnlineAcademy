using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineAcademy.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using OnlineAcademy.Areas.PrivateTeacher.Data;

namespace OnlineAcademy.Areas.PrivateTeacher.Controllers
{
    public class VideoController : Controller
    {
        PrivateTeacherDbContext db = new PrivateTeacherDbContext();
        // GET: PrivateTeacher/Video
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UploadVideo()
        {
            var model = db.VideoFiles.ToList();


            return View(model);
            //List<VideoFiles> videolist = new List<VideoFiles>();
            //string CS = ConfigurationManager.ConnectionStrings["PrivateTeacherConnection"].ConnectionString;
            //using (SqlConnection con = new SqlConnection(CS))
            //{
            //    SqlCommand cmd = new SqlCommand("spGetAllVideoFile", con);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    con.Open();
            //    SqlDataReader rdr = cmd.ExecuteReader();
            //    while (rdr.Read())
            //    {
            //        VideoFiles video = new VideoFiles();
            //        video.ID = Convert.ToInt32(rdr["ID"]);
            //        video.Name = rdr["Name"].ToString();
            //        video.FileSize = Convert.ToInt32(rdr["FileSize"]);
            //        video.FilePath = rdr["FilePath"].ToString();

            //        videolist.Add(video);
            //    }
            //}
            //return View(videolist);
        }
        [HttpPost]
        public ActionResult UploadVideo(HttpPostedFileBase fileupload)
        {
            if (fileupload != null)
            {
                string fileName = Path.GetFileName(fileupload.FileName);
                int fileSize = fileupload.ContentLength;
                int Size = fileSize / 1000;
                fileupload.SaveAs(Server.MapPath("~/VideoFileUpload/" + fileName));

                VideoFiles model = new VideoFiles();
                model.Name = fileName;
                model.FilePath = "~/VideoFileUpload/" + fileName;
                model.FileSize = Size;
                if (ModelState.IsValid)
                {
                    db.VideoFiles.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("UploadVideo");

                }
                return View(model);
            }
            return RedirectToAction("UploadVideo");
        }
        
        public ActionResult Delete(string id)
        {
            var vid = Convert.ToInt32(id);
            VideoFiles deletevideo = db.VideoFiles.Find(vid);
            if (deletevideo != null)
            {
                string videoPath = Server.MapPath(deletevideo.FilePath.ToString());
                System.IO.File.Delete(videoPath);
                db.VideoFiles.Remove(deletevideo);
                db.SaveChanges();
                
            }
            return RedirectToAction("UploadVideo");
        }
    }
}