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
using System.Diagnostics;

namespace UpLoad
{
    public partial class Form1 : Form
    {
        private static string FTPCONSTR = "ftp://192.168.238.1";
        private static string[] strpath;
        private static string pathStr = "/data/";
        private static string pathStr1 = "data";
        public delegate bool MethodCaller(string path, string remotePath, Action<UserControl1, int, int> updateProgress);//定义个代理 

        public delegate bool tempChange(MethodCaller mc, IAsyncResult ir, string path, Stopwatch time);

        public static Form1 form1;

        //private static Form1 _Singleton = new Form1();
        //private static object Singleton_Lock = new object();
        //public static Form1 GetInstance()
        //{
        //    if (_Singleton == null)
        //    {
        //        lock (Singleton_Lock)
        //        {
        //            if (_Singleton == null)
        //            {
        //                _Singleton = new Form1();
        //            }
        //        }
        //    }
        //    return _Singleton;
        //}

        //public event tempChange OntempChange;
        //string temp;
        //public string Temp
        //{
        //    get
        //    {
        //        return temp;
        //    }
        //    set
        //    {
        //        if (temp != value)
        //        {
        //            //           OntempChange(this, new EventArgs());

        //            temp = value;
        //            MessageBox.Show("change");
        //        }
        //    }
        //}

        public Form1()
        {
            InitializeComponent();
            form1 = this;
        }


        private void Uploadbutton_Click(object sender, EventArgs e)
        {
          
            Thread thread = new Thread(Test);
            thread.Start();           
        }

        public void Test()
        {
            

            var path = strpath;
            Action<UserControl1, int, int> updateProgress = progressBarShow;
            List<System.Diagnostics.Stopwatch> time = new List<System.Diagnostics.Stopwatch>();
            CheckForIllegalCrossThreadCalls = false;
            List<tempChange> tcs = new List<tempChange>();
            List<IAsyncResult> tcsRet = new List<IAsyncResult>();
            for (int i = 0; i < path.Length; i++)
            {
                var splitFilePath = SplitFile(path[i]);
                CreateControl(splitFilePath);
                List<MethodCaller> mcs = new List<MethodCaller>();
                List<IAsyncResult> ret = new List<IAsyncResult>();
                if (splitFilePath == null)
                {
                    splitFilePath = new List<string>();
                    splitFilePath.Add(path[i]);
                }
                for (int j = 0; j < splitFilePath.Count; j++)
                {
                    time.Add(new System.Diagnostics.Stopwatch());
                    time[j].Start();
                    //          MethodCaller mc = new MethodCaller(FTPHelper.FtpUploadBroken);
                    mcs.Add(new MethodCaller(FTPHelper.FtpUploadBroken));
                    //      ret.Add(IAsyncResult);
                    IAsyncResult rets = mcs[j].BeginInvoke(splitFilePath[j], pathStr1, updateProgress, null, null);
                    ret.Add(rets);
                }
              
                for (int j = 0; j < mcs.Count; j++)
                {
                    tempChange tc = new tempChange(UpdateResult);
                    tcs.Add(tc);
                    IAsyncResult rets = tcs[j].BeginInvoke(mcs[j], ret[j], splitFilePath[j], time[j], null, null);
                    tcsRet.Add(rets);
                    ////    IAsyncResult ret = mcs[j].BeginInvoke(splitFilePath[j], pathStr1, updateProgress, null, null);
                    //bool result = mcs[j].EndInvoke(ret[j]);
                    ////     var result = FTPHelper.FtpMyProgressBar();
                    //time[j].Stop();
                    //if (result)
                    //{
                    //    //    label1.Text = time.Elapsed.TotalSeconds.ToString();
                    //    //        listBox1.Items.Add(splitFilePath[j] + ": 上传成功" + "  耗时：" + time[j].Elapsed.TotalSeconds.ToString());
                    //    ChangeControl(splitFilePath[j], "上传成功", time[j].Elapsed.TotalSeconds.ToString("f2"));
                    //    File.Delete(splitFilePath[j]);
                    //    //       MessageBox.Show("上传成功!");

                    //}
                    //else
                    //{
                    //    //          listBox1.Items.Add(splitFilePath[j] + ": 上传失败");
                    //    ChangeControl(splitFilePath[j], "上传失败", "0");
                    //    //            MessageBox.Show("上传失败!");
                    //}
                    //time[j].Reset();
                }
            }
            for (int i = 0; i < tcs.Count; i++)
            {
                var ret = tcs[i].EndInvoke(tcsRet[i]); ;
            }
            FileInfo fileInfo = new FileInfo(strpath[0]);
            File.Delete(@"D:/TestHzy/data/" + fileInfo.Name);           
            
            MessageBox.Show("上传结束!");
            var newTime = new Stopwatch();
            newTime.Start();
 //           listBox2.Items.Add("开始：" + newTime.Elapsed.TotalSeconds.ToString("f2") + " s");
            var mr = new MethodCaller(RunsOnWorkerThread);
            mr(strpath[0], pathStr1, progressBarShow);
            newTime.Stop();
            listBox2.Items.Add("耗时：" + newTime.Elapsed.TotalSeconds.ToString("f2") + " s");
        }

        public bool UpdateResult(MethodCaller mc, IAsyncResult ir, string splitPath, Stopwatch time)
        {
            bool result = mc.EndInvoke(ir);
            time.Stop();
            if (result)
            {
                //    label1.Text = time.Elapsed.TotalSeconds.ToString();
           //             listBox1.Items.Add(splitPath + ": 上传成功" + "  耗时：" + time.Elapsed.TotalSeconds.ToString());
                                  ChangeControl(splitPath,"上传成功", time.Elapsed.TotalSeconds.ToString("f2"));
    //            File.Delete(splitPath);
                //       MessageBox.Show("上传成功!");

            }
            else
            {
                //          listBox1.Items.Add(splitFilePath[j] + ": 上传失败");
                              ChangeControl(splitPath , "上传失败", "0");
                //            MessageBox.Show("上传失败!");
            }
            time.Reset();
            return true;
        }

        public void ChangeControl(string name, string text, string time)
        {
            foreach (UserControl1 item in listBox1.Controls)
            {
                var fileInfo = new FileInfo(name);
                string[] temp = fileInfo.Name.Split(new string[] { fileInfo.Extension }, StringSplitOptions.None);
                if (item.label1.Text == temp[0])
                {
                    item.label2.Text = text;
                    if (time != "0")
                        item.label3.Text = "耗时: " + time + " 秒";
                    //        listBox1.DataSource = ;
                    break;
                }
            }
        }

        public void CreateControl(List<string> spliteFile)
        {
            int x = 0, y = 0;
            if (listBox1.Controls.Count != 0)
            {
                x = listBox1.Controls[listBox1.Controls.Count - 1].Location.X;
                y = listBox1.Controls[listBox1.Controls.Count - 1].Location.Y + listBox1.Controls[0].Height - 10;
            }
            foreach (string item in spliteFile)
            {
                UserControl1 cb = new UserControl1();
                var fileInfo = new FileInfo(item);
                string[] temp = fileInfo.Name.Split(new string[] { fileInfo.Extension }, StringSplitOptions.None);
                cb.label1.Text = temp[0];
                cb.label2.Text = "上传中";
                cb.Location = new Point(x, y);
                //                listBox1.Controls.Add(cb);
                this.Invoke(new EventHandler(delegate
                {
                    listBox1.Controls.Add(cb);
                }));
                y += cb.Height - 10;
            }
        }

        private void AddUserControl(Control cb)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate { AddUserControl(cb); }));
                return;
            }
            //       UserControl1 tb = new UserControl1();
            //      tb.Text = "test";
            this.Controls.Add(cb);
        }

        public class MyProgressBar : ProgressBar
        {
            public MyProgressBar()
            {
                SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                Rectangle rect = ClientRectangle;
                Graphics g = e.Graphics;

                ProgressBarRenderer.DrawHorizontalBar(g, rect);
                rect.Inflate(1, 1);
                if (Value > 0)
                {
                    var clip = new Rectangle(rect.X, rect.Y, (int)((float)Value / Maximum * rect.Width), rect.Height);
                    ProgressBarRenderer.DrawHorizontalChunks(g, clip);
                }
                var now = ((float)Value / (float)Maximum) * 100;
                string text = string.Format("{0:00.00}%", now); ;
                using (var font = new Font(FontFamily.GenericSerif, 20))
                {
                    SizeF sz = g.MeasureString(text, font);
                    var location = new PointF(rect.Width / 2 - sz.Width / 2, rect.Height / 2 - sz.Height / 2 + 2);
                    g.DrawString(text, font, Brushes.Tomato, location);
                }
            }
        }

        public void progressBarShow(UserControl1 uc, int allbye, int nowByte)
        {
            //foreach (UserControl1 item in listBox1.Controls)
            //{
            //    var fileInfo = new FileInfo(name);
            //    string[] temp = fileInfo.Name.Split(new string[] { fileInfo.Extension }, StringSplitOptions.None);
            //    if (item.label1.Text == temp[0])
            //    {
            //        item.progressBar1.Maximum = allbye;
            //        item.progressBar1.Minimum = 0;
            //        //        this.progressBar1.Visible = true;
            //        item.progressBar1.Value = nowByte;
            //        //            double percent = Convert.ToDouble(nowByte) / Convert.ToDouble(allbye);
            //        //         string result = percent.ToString("0.00%");
            //        //              label1.Text = result;
            //    }
            //}
            uc.progressBar1.Maximum = allbye;
            uc.progressBar1.Minimum = 0;
            uc.progressBar1.Value = nowByte;
        }

        public UserControl1 FindUserContrl(string name)
        {
            foreach (UserControl1 item in this.listBox1.Controls)
            {
                var fileInfo = new FileInfo(name);
                string[] temp = fileInfo.Name.Split(new string[] { fileInfo.Extension }, StringSplitOptions.None);
                if (item.label1.Text == temp[0])
                {
                    return item;
                }
            }
            return null;
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
                    //FileInfo file = new FileInfo(strpath[y]);
                    //string[] temp = file.Name.Split(new string[] { file.Extension }, StringSplitOptions.None);
                    //MessageBox.Show(temp[0]);
                }
                //       textBox1.Text = strpath;
            }
            catch
            {
                //  this.lbl_ftpStakt.Text = "请选择文件!";
                MessageBox.Show("请选择文件!");
            }
        }

        public static List<string> SplitFile(string filePaths)
        {
            /*
             *  <!--是否开启大文件分隔策略-->
                <add key="BigFile.Split" value="true"/>
                <!--当文件大于这个配置项时就执行文件分隔，单位：GB -->
                <add key="BigFile.SplitMinFileSize" value="10" />
                <!--当执行文件分割时，每个分隔出来的文件大小，单位：MB -->
                <add key="BigFile.SplitFileSize" value="200"/>
             *  <add key="BigFile.FilePath" value="\\172.x1.xx.xx\文件拷贝\xx\FTP\xx\2016-04-07\x_20160407.txt"/>
                <add key="BigFile.FileSilitPathFormate" value="\\172.x1.xx.xx\文件拷贝\liulong\FTP\xx\2016-04-07\x_20160407{0}.txt"/>
             */

            string file = filePaths;
            FileInfo fileInfo = new FileInfo(file);
            string[] temp = fileInfo.Name.Split(new string[] { fileInfo.Extension }, StringSplitOptions.None);
            string splitFileFormat = @"D:\Test\Copy\" + temp[0] + "_tmp{0}" + fileInfo.Extension;
            int splitMinFileSize = 800 * 1024 * 1024 ;
            int splitFileSize = 300 * 1024 * 1024;
            List<string> splitFilePath = new List<string>();
            //      FileInfo fileInfo = new FileInfo(file);
            if (fileInfo.Length > splitMinFileSize)
            {
                MessboxShow("判定结果：需要分隔文件！");
            }
            else
            {
                MessboxShow("判定结果：不需要分隔文件！");
                return null;
            }

            int steps = (int)(fileInfo.Length / splitFileSize);
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    int couter = 1;
                    bool isReadingComplete = false;
                    while (!isReadingComplete)
                    {
                        string filePath = string.Format(splitFileFormat, couter);


                        splitFilePath.Add(filePath);
                        byte[] input = br.ReadBytes(splitFileSize);
                        if (!File.Exists(filePath))
                        {
                            using (FileStream writeFs = new FileStream(filePath, FileMode.Create))
                            {
                                using (BinaryWriter bw = new BinaryWriter(writeFs))
                                {
                                    bw.Write(input);
                                }
                            }
                        }
                        isReadingComplete = (input.Length != splitFileSize);
                        if (!isReadingComplete)
                        {
                            couter += 1;
                        }

                    }
                }
            }
            return splitFilePath;
            //          Console.WriteLine("分隔完成，请按下任意键结束操作。。。");
            //         Console.ReadKey();
        }

        /// <summary>
        /// 合并文件
        /// </summary>
        /// <param name="filePaths">要合并的文件列表</param>
        /// <param name="combineFile">合并后的文件路径带文件名</param>
        void CombineFiles(string[] filePaths, string combineFile)
        {
            using (FileStream CombineStream = new FileStream(combineFile, FileMode.OpenOrCreate))
            {
                using (BinaryWriter CombineWriter = new BinaryWriter(CombineStream))
                {
                    foreach (string file in filePaths)
                    {

                        using (FileStream fileStream = new FileStream(file, FileMode.Open))
                        {
                            using (BinaryReader fileReader = new BinaryReader(fileStream))
                            {
                                byte[] TempBytes = fileReader.ReadBytes((int)fileStream.Length);
                                CombineWriter.Write(TempBytes);
                            }
                        }
                        File.Delete(file);
                    }
                }
            }
        }

        public static void MessboxShow(string text)
        {
            MessageBox.Show(text);
        }

        //连接ftp上传
        public bool RunsOnWorkerThread(string localFullPath, string remoteFilepath, Action<UserControl1, int, int> updateProgress = null)
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
            long startfilesize =FTPHelper.GetFileSize(newFileName, remoteFilepath);
            string[] temp = fileInf.Name.Split(new string[] { fileInf.Extension }, StringSplitOptions.None);
            var replaceFileName = temp[0];

            //var userControl = Form1.form1.FindUserContrl(replaceFileName);
            if (startfilesize >= allbye)
            {
                //if (updateprogress != null && usercontrol != null)
                //{
                //    updateprogress(usercontrol, (int)allbye, (int)startfilesize);
                return true;
                //}
            }
            long startbye = startfilesize;
            ////更新进度
            //if (updateProgress != null && userControl != null)
            //{
            //    updateProgress(userControl, (int)allbye, (int)startfilesize);//更新进度条   
            //}

            string uri;
            if (remoteFilepath.Length == 0)
            {
                uri = "ftp://" +FTPHelper.FtpServerIP + "/" + newFileName;
            }
            else
            {
                uri = "ftp://" +FTPHelper.FtpServerIP + "/" + remoteFilepath + "/" + newFileName;
            }
            FtpWebRequest reqFTP;
            // 根据uri创建FtpWebRequest对象 
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            // ftp用户名和密码 
            reqFTP.Credentials = new NetworkCredential(FTPHelper.FtpUserID, FTPHelper.FtpPassword);
            // 默认为true，连接不会被关闭 
            // 在一个命令之后被执行 
            reqFTP.KeepAlive = false;
            // 指定执行什么命令 
            reqFTP.Method = WebRequestMethods.Ftp.AppendFile;
            // 指定数据传输类型 
            reqFTP.UseBinary = true;
            // 上传文件时通知服务器文件的大小 
            reqFTP.ContentLength = fileInf.Length;
            int buffLength = 2048;// 缓冲大小设置为2kb 
            byte[] buff = new byte[buffLength];
            // 打开一个文件流 (System.IO.FileStream) 去读上传的文件 
            FileStream fs = fileInf.OpenRead();
            Stream strm = null;
            try
            {
                // 把上传的文件写入流 
                strm = reqFTP.GetRequestStream();
                // 每次读文件流的2kb   
                fs.Seek(startfilesize, 0);
                int contentLen = fs.Read(buff, 0, buffLength);
                // 流内容没有结束 
                while (contentLen != 0)
                {
                    // 把内容从file stream 写入 upload stream 
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                    startbye += contentLen;
                    ////更新进度  
                    //if (updateProgress != null && userControl != null)
                    //{
                    //    updateProgress(userControl, (int)allbye, (int)startbye);//更新进度条   
                    //}
                }
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

        private void Button1_Click(object sender, EventArgs e)
        {
          //  RunsOnWorkerThread(strpath[0]);
         //   Temp = "test";
            //string[] list = new string[] { "张三", "李四", "王五" };

            //int x = 0, y = 0;
            //foreach (string item in list)
            //{
            //    UserControl1 cb = new UserControl1();
            //    cb.label1.Text = item;
            //    cb.Location = new Point(x, y);
            //    listBox1.Controls.Add(cb);
            //    y += cb.Height - 10;
            //}
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            //          string file = strpath[i];
            //          FileInfo fileInfo = new FileInfo(file);
            //          string[] temp = fileInfo.Name.Split(new string[] { fileInfo.Extension }, StringSplitOptions.None);
            //          string splitFileFormat = @"E:\Test\Copy\" + temp[0] + "_{0}" + fileInfo.Extension;
            string url = "D:/TestHzy/" + pathStr1 + "/";
            var filePaths = Directory.GetFiles(url);
            string combineFilePath = "";
            List<string> splitFileFormat = new List<string>();
            for (int i = 0; i < filePaths.Count(); i++)
            {
                FileInfo fileInfo = new FileInfo(filePaths[i]);
                splitFileFormat.Add(fileInfo.FullName);
                string[] temp = fileInfo.Name.Split(new string[] { "_tmp" }, StringSplitOptions.None);
                combineFilePath = @"D:/TestHzy/" + pathStr1 + "/" + temp[0];
            }
            CombineFiles(splitFileFormat.ToArray(), combineFilePath);
            MessageBox.Show("合并完成!");
        }
    }
}
