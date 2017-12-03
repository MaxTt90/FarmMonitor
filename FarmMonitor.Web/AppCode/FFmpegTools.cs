using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FarmMonitor.Common;
using WeDo.Log;

namespace FarmMonitor.Web.AppCode
{
    public class FFmpegTools
    {

        static FFmpegCommon ftCom = new FFmpegCommon();

        /// <summary>
        /// ffmpeg可执行文件路径
        /// </summary>
        static string ffmpegFile { get { return AppDomain.CurrentDomain.BaseDirectory + "\\Files\\FFmpeg\\ffmpeg.exe"; } }
        /// <summary>
        /// 将Amr格式转化为Mp3格式
        /// </summary>
        /// <param name="sourceFile">amr格式文件物理路径</param>
        /// <param name="targetFile">mp3格式文件物理路径</param>
        /// <returns>转化成功返回true,失败返回false</returns>
        public static bool ConvertToMp3(string sourceFile, string targetFile)
        {
            try
            {
                ftCom.ConvertToMp3(ffmpegFile, sourceFile, targetFile);
                return true;
            }
            catch (Exception ex)
            {

                LogExceptionMan.AddLog("Amr转mp3格式", WeDo.Log.Model.EnumListLog.LogLevel.ERROR, ex, "Amr转mp3格式");
                return false;
            }
        }
    }
}