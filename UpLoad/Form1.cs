using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;

namespace UpLoad
{
    public partial class Form1 : Form
    {
        private static string FTPCONSTR = "ftp://172.31.132.91:21";
        private static string[] strpath;
        private static string pathStr = "/data/";
        public Form1()
        {
            InitializeComponent();
        }

        private void Uploadbutton_Click(object sender, EventArgs e)
        {
            //    this.lbl_ftpStakt.Visible = true;   //设置上传信息标签可见
            //     this.lbl_ftpStakt.Text = "连接服务器...";
            var path = strpath;
            try
            {
                for (int i = 0; i < path.Length; i++)
                {
                    var filename = path[i].ToString();
                    CheckForIllegalCrossThreadCalls = false;
                    //实例化事件类
                    EntrustHandle fo = new EntrustHandle(filename);
                    var myUpEventsHandler = new EntrustHandle.myUpEventsHandler(this.RunsOnWorkerThread);
                    fo.startUpEvent += myUpEventsHandler;
                    //      fo.startUpEvent += new EntrustHandle.myUpEventsHandler(this.RunsOnWorkerThread); //注册事件
                    //      IAsyncResult result = fo.myUpEventsHandler.BeginInvoke("m", "n", null, null);
                    fo.mythreadStart(); //调用类的方法
                    FileInfo p = new FileInfo(path[i].ToString());
                    //        uploadSQL(p.Name);  //上传到库
                }
                //label1.Text = "上传成功";
            }
            catch
            {
                string s = "";
                for (int x = 0; x < path.Length; x++)
                {
                    FileInfo file = new FileInfo(path[x].ToString());
                    s += file.Name + " ";
                }
                //   this.lbl_ftpStakt.Text = "上传失败";
                MessageBox.Show(s.ToString() + " 上传失败", "提示");
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //      label1.Text = "";
            try
            {
                this.openFileDialog1.Multiselect = true;
                this.openFileDialog1.ShowDialog();
                var path = this.openFileDialog1.FileNames;  //获取openFileDialog控件选择的文件名数组
                strpath = new string[path.Length];
                for (int y = 0; y < path.Length; y++)
                {
                    strpath[y] = path[y];
                }
                //       textBox1.Text = strpath;
            }
            catch
            {
                //  this.lbl_ftpStakt.Text = "请选择文件!";
                MessageBox.Show("请选择文件!");
            }
        }

        //连接ftp上传
        public void RunsOnWorkerThread(string _filename)
        {
            //IAsyncResult result =  BeginInvoke(, 2000, null, null);
            ////阻塞线程
            //mt.WaitOne();

            //       Interlocked.Increment(ref flag);    //状态值+1

            //   this.lbl_ftpStakt.Text = "连接服务器中...";
            FileInfo fileInf = new FileInfo(_filename);
            FtpWebRequest reqFTP;
            // 根据uri创建FtpWebRequest对象
            var filePath = FTPCONSTR + pathStr + fileInf.Name;
             reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(filePath));
            // ftp用户名和密码
            //     reqFTP.Credentials = new NetworkCredential("record", "files");
            // 默认为true，连接不会被关闭
            // 在一个命令之后被执行

            reqFTP.KeepAlive = false;
            // 指定执行什么命令
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            // 指定数据传输类型
            reqFTP.UseBinary = true;
            // 上传文件时通知服务器文件的大小
            reqFTP.ContentLength = fileInf.Length;
            //long _length = fileInf.Length;  /////////
            // 缓冲大小设置为2kb
            int buffLength = 2048;  ////
            byte[] buff = new byte[buffLength];
            int contentLen;
            // 打开一个文件流 (System.IO.FileStream) 去读上传的文件
            FileStream fs = fileInf.OpenRead();

            try
            {
                // 把上传的文件写入流
                Stream strm = reqFTP.GetRequestStream();
                // 每次读文件流的2kb
                contentLen = fs.Read(buff, 0, buffLength);
                int allbye = (int)fileInf.Length;
                int startbye = 0;
                this.progressBar1.Maximum = allbye;
                this.progressBar1.Minimum = 0;
                this.progressBar1.Visible = true;
                //    this.lbl_ftpStakt.Visible = true;
                //     this.lbl_ftpStakt.Text = "服务器连接中...";
                // 流内容没有结束
                while (contentLen != 0)
                {
                    // 把内容从file stream 写入 upload stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                    startbye += contentLen;
                    //                 this.lbl_ftpStakt.Text = "已上传:" + (int)(startbye / 1024) + "KB/" + "总长度:" + (int)(allbye / 1024) + "KB" + " " + " 文件名:" + fileInf.Name;
                    progressBar1.Value = startbye;
                }
                // 关闭两个流
                strm.Close();
                fs.Close();
                this.progressBar1.Visible = false;
                MessageBox.Show("上传成功!");
                //   this.lbl_ftpStakt.Text = "上传成功!";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "上传失败!");
            }
            //   Interlocked.Decrement(ref flag);
            //   mt.ReleaseMutex();//释放线程
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string erroinfo = "";
            var path = strpath[0];
            FileInfo f = new FileInfo(path);
            path = path.Replace("\\", "/");
            path = FTPCONSTR + pathStr + f.Name;//这个路径是我要传到ftp目录下的这个目录下
            FtpWebRequest reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(path));
            reqFtp.UseBinary = true;
            //       reqFtp.Credentials = new NetworkCredential(FTPUSERNAME, FTPPASSWORD);
            reqFtp.KeepAlive = false;
            reqFtp.Method = WebRequestMethods.Ftp.UploadFile;
            reqFtp.ContentLength = f.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = f.OpenRead();
            try
            {
                Stream strm = reqFtp.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                strm.Close();
                fs.Close();
                MessageBox.Show("成功");
            }
            catch (Exception ex)
            {
                erroinfo = string.Format("因{0},无法完成上传", ex.Message);
                MessageBox.Show(erroinfo);
            }
        }
    }
}
