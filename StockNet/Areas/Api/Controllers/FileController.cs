using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using StockNet.Areas.Api.Models;
using StockNet.Controllers;
using StockNet.Model;
using StockNet.Service;

namespace StockNet.Areas.Api.Controllers
{
    public class FileController : Controller
    {
        [HttpPost]
        public ActionResult UploadImage()
        {
            WebResult result = new WebResult
            {
                Code = AppConst.MSG_ERR,
                Message = string.Empty,
                Data = null
            };
            try
            {
                
                var upResult = SaveImg(Request.InputStream);
                if (string.IsNullOrEmpty(upResult))
                {
                    result.Message = "上传失败,请重新上传";
                    return
                        Content(JsonConvert.SerializeObject(result));
                }

                result.Code = AppConst.MSG_SUCCESS;
                result.Data = upResult;
                return Content(JsonConvert.SerializeObject(result));
            }
            catch (Exception e)
            {
                result.Code = AppConst.MSG_ERR;
                result.Message = e.Message;
                return Content(JsonConvert.SerializeObject(result));
            }
        }

        private string SaveImg(Stream uploadFile)
        {
            try
            {
                if (uploadFile != null && uploadFile.Length > 0)
                {
                    DateTime now = DateTime.Now;
                    string vpath = "/Site/Goods/" + now.ToString("yyyyMMdd") + "/";
                    string realpath = Server.MapPath(vpath);
                    if (!Directory.Exists(realpath))
                        Directory.CreateDirectory(realpath);
                    //var file = Request.Files[i];
                    //string fname = file.FileName;
                    //if (System.IO.File.Exists(realpath + fname))
                    //{
                    //    fname = now.Ticks.ToString() + "_" + fname;
                    //}
                    Guid imageGuid = Guid.NewGuid();
                    string fileName = string.Format("{0}{1}.jpg", realpath, imageGuid);
                    var resourceImge = Image.FromStream(uploadFile);
                    resourceImge.Save(fileName);
                    return string.Format("{0}{1}.jpg", vpath, imageGuid);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}