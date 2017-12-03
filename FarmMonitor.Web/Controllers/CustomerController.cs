using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FarmMonitor.BLL;
using FarmMonitor.Common;
using FarmMonitor.Model;
using FarmMonitor.Web.AppCode;
using WeDo.Log;

namespace FarmMonitor.Web.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/
        public ActionResult Index()
        {
            return View();
        }


        #region 上传图片

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        /// liuyicong 2017-01-17
        public ActionResult Upload()
        {
            HttpFileCollection imgFiles = System.Web.HttpContext.Current.Request.Files;
            //var imgFiles = Request.Files;
            if (imgFiles.Count == 0)
            {
                return Json(new {code = 1, msg = "请选择要上传的文件！"});
            }

            var img = imgFiles[0];

            if (img != null)
            {

                var ex = Path.GetExtension(img.FileName);
                var name = Guid.NewGuid().ToString("N");
                var aPath = DateTime.Now.ToString("yyyy/MM/dd/temp") + "/" + name + "_temp" + ex;
                var aPath2 = DateTime.Now.ToString("yyyy/MM/dd") + "/" + name + ex;

                string filePathTemp = "~/Files/UploadCampaignImages/" + aPath;
                string filePath = "~/Files/UploadCampaignImages/" + aPath2;

                if (!Directory.Exists(Path.GetDirectoryName(Server.MapPath(filePathTemp))))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(Server.MapPath(filePathTemp)));
                    img.SaveAs(Server.MapPath(filePathTemp));
                }
                else
                {
                    img.SaveAs(Server.MapPath(filePathTemp));
                }

                string json =
                    "{{\"code\":\"{0}\",\"msg\":\"{1}\",\"url\":\"{2}\",\"serverUrl\":\"{3}\",\"smallUrl\":\"{4}\"}}";
                string smallpath = "";
                var c = SizeImage(filePathTemp, filePath, out smallpath);
                if (c == "上传成功")
                {
                    var s = Content(string.Format(json, "200", "上传成功", filePath.Replace("~", ""),
                            PageTools.GetHttpUrl(filePath), smallpath));
                    return
                        s;
                }
                else
                {
                    return Json(new {code = 4, msg = c}, "text/html");
                }


            }
            return Json(new {code = 3, msg = "上传失败"}, "text/html");
        }

        /// <summary>
        /// 调整图片旋转，按照指定大小缩放图片 
        /// </summary>
        /// <param name="imagePathTemp">原图片访问路径</param>
        /// <param name="imagePath">调整旋转后图片路径</param>
        /// <param name="smallImagePath">缩略图虚拟路径</param>
        /// <param name="isResize">是否重新调整大小</param>
        /// <param name="iWidth">调整到的宽度</param>
        /// <returns></returns>
        /// liuyicong 2017-01-17
        public string SizeImage(string imagePathTemp, string imagePath, out string smallImagePath, bool isResize = false,
            int iWidth = 0)
        {
            try
            {
                //旋转图片 防止iphone上传图片颠倒
                smallImagePath = imagePath;
                imagePathTemp = Server.MapPath(imagePathTemp);
                imagePath = Server.MapPath(imagePath);
                int orien = GetOrientation(imagePathTemp);
                Image srcImage = Image.FromFile(imagePathTemp);
                switch (orien)
                {
                    case 2:
                        srcImage.RotateFlip(RotateFlipType.RotateNoneFlipX); //horizontal flip
                        break;
                    case 3:
                        srcImage.RotateFlip(RotateFlipType.Rotate180FlipNone); //right-top
                        break;
                    case 4:
                        srcImage.RotateFlip(RotateFlipType.RotateNoneFlipY); //vertical flip
                        break;
                    case 5:
                        srcImage.RotateFlip(RotateFlipType.Rotate90FlipX);
                        break;
                    case 6:
                        srcImage.RotateFlip(RotateFlipType.Rotate90FlipNone); //right-top
                        break;
                    case 7:
                        srcImage.RotateFlip(RotateFlipType.Rotate270FlipX);
                        break;
                    case 8:
                        srcImage.RotateFlip(RotateFlipType.Rotate270FlipNone); //left-bottom
                        break;
                    default:
                        break;
                }
                srcImage.Save(imagePath);
                //srcImage.Dispose();

                var a = ((float) srcImage.Height/(float) srcImage.Width);
                if (srcImage.Width < 300 || srcImage.Height < 300 ||
                    ((float) srcImage.Height/(float) srcImage.Width) < 1)
                {
                    smallImagePath = "";
                    return "为了图像效果最优，请选择像素大于300*300且高大于宽的图像";
                }

                if (isResize)
                {
                    var ex = ".png";
                    var name = Guid.NewGuid().ToString("N");
                    var aPath = DateTime.Now.ToString("yyyy/MM/dd/small") + "/" + "small" + name + ex;
                    smallImagePath = "/Files/UploadCampaignImages/" + aPath;

                    int ow = srcImage.Width;
                    int oh = srcImage.Height;

                    double multiple = (double) ow/(double) iWidth;
                    int iHeight = Convert.ToInt32((double) oh/multiple);

                    // 要保存到的图片 
                    Bitmap b = new Bitmap(iWidth, iHeight);
                    Graphics g = Graphics.FromImage(b);
                    // 插值算法的质量 
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(srcImage, new Rectangle(0, 0, iWidth, iHeight),
                        new Rectangle(0, 0, srcImage.Width, srcImage.Height), GraphicsUnit.Pixel);
                    g.Dispose();
                    srcImage.Dispose();
                    if (!Directory.Exists(Path.GetDirectoryName(Server.MapPath(smallImagePath))))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(Server.MapPath(smallImagePath)));
                        b.Save(Server.MapPath(smallImagePath));
                    }
                    else
                    {
                        b.Save(Server.MapPath(smallImagePath));
                    }
                }

                return "上传成功";
            }
            catch (Exception)
            {
                smallImagePath = "";
                return "上传失败,请重新上传";
            }
        }

        /// <summary>
        /// 获取角度
        /// </summary>
        /// <param name="filepath">图片路径</param>
        /// <returns></returns>
        /// liuyicong 2017-01-17
        private int GetOrientation(string filepath)
        {
            ExifManager exif = new ExifManager(filepath);
            int orien = (int) exif.Orientation;
            exif.Dispose();
            return orien;
        }

        #endregion

        #region 上传图片

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        /// fangyiwen 引用liuyicong 2017-01-17
        public ActionResult Upload2()
        {
            HttpFileCollection imgFiles = System.Web.HttpContext.Current.Request.Files;//获取当前文件
            //var imgFiles = Request.Files;
            if (imgFiles.Count == 0)
            {
                return Json(new { code = 1, msg = "请选择要上传的文件！" });
            }

            var img = imgFiles[0];

            if (img != null)
            {

                var ex = Path.GetExtension(img.FileName);
                var name = Guid.NewGuid().ToString("N");
                var aPath = DateTime.Now.ToString("yyyy/MM/dd") + "/temp/" + name + "_temp" + ex;
                var aPath2 = DateTime.Now.ToString("yyyy/MM/dd") + "/" + name + ex;

                string filePathTemp = "~/Files/UploadCampaignImages/" + aPath;
                string filePath = "~/Files/UploadCampaignImages/" + aPath2;

                if (!Directory.Exists(Path.GetDirectoryName(Server.MapPath(filePathTemp))))//查看是否存在该图片
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(Server.MapPath(filePathTemp)));
                    img.SaveAs(Server.MapPath(filePathTemp));
                }
                else
                {
                    img.SaveAs(Server.MapPath(filePathTemp));
                }

                string json =
                    "{{\"code\":\"{0}\",\"msg\":\"{1}\",\"url\":\"{2}\",\"serverUrl\":\"{3}\",\"smallUrl\":\"{4}\"}}";
                string smallpath = "";
                var c = SizeImage2(filePathTemp, filePath, out smallpath,true,350);//调整小图
                if (c == "上传成功")
                {
                    var s = Content(string.Format(json, "200", "上传成功", filePath.Replace("~", ""),
                            PageTools.GetHttpUrl(filePath), PageTools.GetHttpUrl(smallpath)));
                    return
                        s;
                }
                else
                {
                    return Json(new { code = 4, msg = c }, "text/html");
                }


            }
            return Json(new { code = 3, msg = "上传失败" }, "text/html");
        }
        #endregion

        /// <summary>
        /// 调整图片旋转，按照指定大小缩放图片 
        /// </summary>
        /// <param name="imagePathTemp">原图片访问路径</param>
        /// <param name="imagePath">调整旋转后图片路径</param>
        /// <param name="smallImagePath">缩略图虚拟路径</param>
        /// <param name="isResize">是否重新调整大小</param>
        /// <param name="iWidth">调整到的宽度</param>
        /// <returns></returns>
        /// fangyiwen引用liuyicong 2017-01-17
        public string SizeImage2(string imagePathTemp, string imagePath, out string smallImagePath, bool isResize = false,
            int iWidth = 0)
        {
            try
            {
                //旋转图片 防止iphone上传图片颠倒
                smallImagePath = imagePath;
                imagePathTemp = Server.MapPath(imagePathTemp);
                imagePath = Server.MapPath(imagePath);
                int orien = GetOrientation(imagePathTemp);
                Image srcImage = Image.FromFile(imagePathTemp);
                switch (orien)
                {
                    case 2:
                        srcImage.RotateFlip(RotateFlipType.RotateNoneFlipX); //horizontal flip
                        break;
                    case 3:
                        srcImage.RotateFlip(RotateFlipType.Rotate180FlipNone); //right-top
                        break;
                    case 4:
                        srcImage.RotateFlip(RotateFlipType.RotateNoneFlipY); //vertical flip
                        break;
                    case 5:
                        srcImage.RotateFlip(RotateFlipType.Rotate90FlipX);
                        break;
                    case 6:
                        srcImage.RotateFlip(RotateFlipType.Rotate90FlipNone); //right-top
                        break;
                    case 7:
                        srcImage.RotateFlip(RotateFlipType.Rotate270FlipX);
                        break;
                    case 8:
                        srcImage.RotateFlip(RotateFlipType.Rotate270FlipNone); //left-bottom
                        break;
                    default:
                        break;
                }
                srcImage.Save(imagePath);
                //srcImage.Dispose();

                var a = ((float)srcImage.Height / (float)srcImage.Width);
                if (srcImage.Width < 300 || srcImage.Height < 300)
                {
                    smallImagePath = "";
                    return "为了图像效果最优，请选择像素大于300*300且高大于宽的图像";
                }
                var test = (float)srcImage.Height * (float)srcImage.Width / System.Math.Pow(2, 20) /4 < 30;
                if ((float) srcImage.Height*(float) srcImage.Width/System.Math.Pow(2, 20)/4 > 30)
                {
                    smallImagePath = "";
                    return "请上传30M以下的图片";
                }

                if (isResize)   //重新调整大小
                {
                    var ex = ".png";
                    var name = Guid.NewGuid().ToString("N");
                    var aPath = DateTime.Now.ToString("yyyy/MM/dd") + "/small/" + "small" + name + ex;
                    smallImagePath = "~/Files/UploadCampaignImages/" + aPath;

                    int ow = srcImage.Width;
                    int oh = srcImage.Height;

                    double multiple = (double)ow / (double)iWidth;
                    int iHeight = Convert.ToInt32((double)oh / multiple);

                    // 要保存到的图片 
                    Bitmap b = new Bitmap(iWidth, iHeight);
                    Graphics g = Graphics.FromImage(b);
                    // 插值算法的质量 
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(srcImage, new Rectangle(0, 0, iWidth, iHeight),
                        new Rectangle(0, 0, srcImage.Width, srcImage.Height), GraphicsUnit.Pixel);
                    g.Dispose();
                    srcImage.Dispose();
                    if (!Directory.Exists(Path.GetDirectoryName(Server.MapPath(smallImagePath))))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(Server.MapPath(smallImagePath)));
                        var str = Server.MapPath(smallImagePath);
                        b.Save(Server.MapPath(smallImagePath));
                    }
                    else
                    {
                        b.Save(Server.MapPath(smallImagePath));
                    }
                }

                return "上传成功";
            }
            catch (Exception)
            {
                smallImagePath = "";
                return "上传失败,请重新上传";
            }
        }
    }
}