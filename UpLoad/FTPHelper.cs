using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UpLoad
{
    public class FTPHelper
    {
        string ftpRemotePath;

        #region 变量属性
        /// <summary>
        /// Ftp服务器ip
        /// </summary>
        public static string FtpServerIP = "192.168.238.1";
        /// <summary>
        /// Ftp 指定用户名
        /// </summary>
        public static string FtpUserID = "";
        /// <summary>
        /// Ftp 指定用户密码
        /// </summary>
        public static string FtpPassword = "";

        public static string ftpURI = "ftp://" + FtpServerIP + "/";

        #endregion

        /// <summary>
        /// 上传文件到FTP服务器(断点续传)
        /// </summary>
        /// <param name="localFullPath">本地文件全路径名称：C:\Users\JianKunKing\Desktop\IronPython脚本测试工具</param>
        /// <param name="remoteFilepath">远程文件所在文件夹路径</param>
        /// <param name="updateProgress">报告进度的处理(第一个参数：总大小，第二个参数：当前进度)</param>
        /// <returns></returns>       
        public static bool FtpUploadBroken(string localFullPath, string remoteFilepath, Action<UserControl1,int, int> updateProgress = null)
        {
            if (remoteFilepath == null)
            {
                remoteFilepath = "";
            }
            string newFileName = string.Empty;
            bool success = true;
            FileInfo fileInf = new FileInfo(localFullPath);
            //        Form1.MessboxShow(fileInf.Extension);
            long allbye = (long)fileInf.Length;
            if (fileInf.Name.IndexOf("#") == -1)
            {
                newFileName = fileInf.Name;
            }
            else
            {
                newFileName = fileInf.Name;
            }
            long startfilesize = GetFileSize(newFileName, remoteFilepath);
            string[] temp = fileInf.Name.Split(new string[] { fileInf.Extension }, StringSplitOptions.None);
            var replaceFileName = temp[0];

            var userControl = Form1.form1.FindUserContrl(replaceFileName);
            if (startfilesize >= allbye)
            {
                if (updateProgress != null && userControl != null)
                {
                    updateProgress(userControl, (int)allbye, (int)startfilesize);
                    return true;
                }
            }
            long startbye = startfilesize;
            //更新进度
            //if (updateProgress != null && userControl != null)
            //{
            //    updateProgress(userControl, (int)allbye, (int)startfilesize);//更新进度条   
            //}

            string uri;
            if (remoteFilepath.Length == 0)
            {
                uri = "ftp://" + FtpServerIP + "/" + newFileName;
            }
            else
            {
                uri = "ftp://" + FtpServerIP + "/" + remoteFilepath + "/" + newFileName;
            }
            FtpWebRequest reqFTP;
            // 根据uri创建FtpWebRequest对象 
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            // ftp用户名和密码 
            reqFTP.Credentials = new NetworkCredential(FtpUserID, FtpPassword);
            // 默认为true，连接不会被关闭 
            // 在一个命令之后被执行 
            reqFTP.KeepAlive = false;
            // 指定执行什么命令 
            reqFTP.Method = WebRequestMethods.Ftp.AppendFile;
            // 指定数据传输类型 
            reqFTP.UseBinary = true;
            // 上传文件时通知服务器文件的大小 
            reqFTP.ContentLength = fileInf.Length;
            //int buffLength = 2 * 1024;// 缓冲大小设置为2kb 
            //byte[] buff = new byte[buffLength];
            // 打开一个文件流 (System.IO.FileStream) 去读上传的文件 
            FileStream fs = fileInf.OpenRead();
            int buffLength = 1 * 1024 * 1024;
            byte[] buff = new byte[buffLength];
            Stream strm = null;
            try
            {
                // 把上传的文件写入流 
                strm = reqFTP.GetRequestStream();
                // 每次读文件流的2kb   
                fs.Seek(startfilesize, 0);

                int contentLen = fs.Read(buff, 0, buffLength);
                startbye += contentLen;
                // 流内容没有结束 
                while (contentLen != 0)
                {
                    // 把内容从file stream 写入 upload stream 
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                    startbye += contentLen;
                    //更新进度  
                    if (updateProgress != null && userControl != null)
                    {
                        updateProgress(userControl, (int)allbye, (int)startbye);//更新进度条   
                    }
                }
                ////更新进度  
                //if (updateProgress != null && userControl != null)
                //{
                //    updateProgress(userControl, (int)allbye, (int)startbye);//更新进度条   
                //}
                // 关闭两个流 
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                var ss = ex.Message;
                success = false;
                throw;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
                if (strm != null)
                {
                    strm.Close();
                }
            }
            return success;
        }

        /// <summary>
        /// 获取已上传文件大小
        /// </summary>
        /// <param name="filename">文件名称</param>
        /// <param name="path">服务器文件路径</param>
        /// <returns></returns>
        public static long GetFileSize(string filename, string remoteFilepath)
        {
            long filesize = 0;
            try
            {
                FtpWebRequest reqFTP;
                FileInfo fi = new FileInfo(filename);
                string uri;
                if (remoteFilepath.Length == 0)
                {
                    uri = "ftp://" + FtpServerIP + "/" + fi.Name;
                }
                else
                {
                    uri = "ftp://" + FtpServerIP + "/" + remoteFilepath + "/" + fi.Name;
                }
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(uri);
                reqFTP.KeepAlive = false;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(FtpUserID, FtpPassword);//用户，密码
                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                filesize = response.ContentLength;
                return filesize;
            }
            catch
            {
                return 0;
            }
        }

    }
}
