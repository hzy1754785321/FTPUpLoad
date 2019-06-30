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
using System.Configuration;

namespace UpLoad
{
    public partial class Form1 : Form
    {
        private static string FTPCONSTR = "ftp://192.168.0.107";
        private static string[] strpath;
        private static string pathStr = "/data/";
        private static string pathStr1 = "data";
        public delegate bool MethodCaller(string path, string remotePath, Action<int, int> updateProgress);//定义个代理 
        public Form1()
        {
            InitializeComponent();
        }

        private void Uploadbutton_Click(object sender, EventArgs e)
        {
            var path = strpath;
            //try
            //{
            //    for (int i = 0; i < path.Length; i++)
            //    {
            //        var filename = path[i].ToString();
            //        CheckForIllegalCrossThreadCalls = false;
            //        //实例化事件类
            //        EntrustHandle fo = new EntrustHandle(filename);
            //        var myUpEventsHandler = new EntrustHandle.myUpEventsHandler(this.RunsOnWorkerThread);
            //        fo.startUpEvent += myUpEventsHandler;
            //        //      fo.startUpEvent += new EntrustHandle.myUpEventsHandler(this.RunsOnWorkerThread); //注册事件
            //        //      IAsyncResult result = fo.myUpEventsHandler.BeginInvoke("m", "n", null, null);
            //        fo.mythreadStart(); //调用类的方法
            //        FileInfo p = new FileInfo(path[i].ToString());
            //        //        uploadSQL(p.Name);  //上传到库
            //    }
            //    //label1.Text = "上传成功";
            //}
            //catch
            //{
            //    string s = "";
            //    for (int x = 0; x < path.Length; x++)
            //    {
            //        FileInfo file = new FileInfo(path[x].ToString());
            //        s += file.Name + " ";
            //    }
            //    //   this.lbl_ftpStakt.Text = "上传失败";
            //    MessageBox.Show(s.ToString() + " 上传失败", "提示");
            //}

            Action<int, int> updateProgress = progressBarShow;
            List<System.Diagnostics.Stopwatch> time = new List<System.Diagnostics.Stopwatch>();
            CheckForIllegalCrossThreadCalls = false;
            for (int i = 0; i < path.Length; i++)
            {
                var splitFilePath = SplitFile(path[i]);
                CreateControl(splitFilePath);
                List<MethodCaller> mcs = new List<MethodCaller>();
                List<IAsyncResult> ret = new List<IAsyncResult>();
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
                    //    IAsyncResult ret = mcs[j].BeginInvoke(splitFilePath[j], pathStr1, updateProgress, null, null);
                    bool result = mcs[j].EndInvoke(ret[j]);
                    //     var result = FTPHelper.FtpMyProgressBar();
                    time[j].Stop();
                    if (result)
                    {
                        //    label1.Text = time.Elapsed.TotalSeconds.ToString();
                //        listBox1.Items.Add(splitFilePath[j] + ": 上传成功" + "  耗时：" + time[j].Elapsed.TotalSeconds.ToString());
                        ChangeControl(splitFilePath[j],"上传成功");
                        File.Delete(splitFilePath[j]);
                        //       MessageBox.Show("上传成功!");

                    }
                    else
                    {
                        //          listBox1.Items.Add(splitFilePath[j] + ": 上传失败");
                        ChangeControl(splitFilePath[j], "上传失败");
                        //            MessageBox.Show("上传失败!");
                    }
                    time[j].Reset();
                }
           
            }
            MessageBox.Show("上传结束!");
        }

        public void ChangeControl(string name, string text)
        {
            foreach (UserControl1 item in listBox1.Controls)
            {
                var fileInfo = new FileInfo(name);
                string[] temp = fileInfo.Name.Split(new string[] { fileInfo.Extension }, StringSplitOptions.None);
                if (item.label1.Text == temp[0])
                {
                    item.label2.Text = text;
            //        listBox1.DataSource = ;
                    break;
                }
            }
        }

        public void CreateControl(List<string> spliteFile)
        {
            //int x = 0, y = 0;
            //for (int i = 0; i < spliteFile.Count; i++)
            //{
            //    var fileInfo = new FileInfo(spliteFile[i]);
            //    UserControl1 cb = new UserControl1();
            //    string[] temp = fileInfo.Name.Split(new string[] { fileInfo.Extension }, StringSplitOptions.None);
            //    cb.label1.Text = temp[0];
            //    cb.label2.Text = "上传中";
            //    cb.Location = new Point(x, y);
            //    listBox1.Controls.Add(cb);
            //    y += cb.Height - 10;
            //}
       //     string[] list = new string[] { "张三", "李四", "王五" };

            int x = 0, y = 0;
            foreach (string item in spliteFile)
            {
                UserControl1 cb = new UserControl1();
                var fileInfo = new FileInfo(item);
                string[] temp = fileInfo.Name.Split(new string[] { fileInfo.Extension }, StringSplitOptions.None);
                cb.label1.Text = temp[0];
                cb.label2.Text = "上传中";
                cb.Location = new Point(x, y);
                listBox1.Controls.Add(cb);
                y += cb.Height - 10;
            }
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

        public void progressBarShow(int allbye, int nowByte)
        {
            this.progressBar1.Maximum = allbye;
            this.progressBar1.Minimum = 0;
            //        this.progressBar1.Visible = true;
            progressBar1.Value = nowByte;
            double percent = Convert.ToDouble(nowByte) / Convert.ToDouble(allbye);
            string result = percent.ToString("0.00%");
            label1.Text = result;
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
            string splitFileFormat = @"E:\Test\Copy\" + temp[0] + "_tmp{0}" + fileInfo.Extension;
            int splitMinFileSize = 600 * 1024 * 1024;
            int splitFileSize = 200 * 1024 * 1024;
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
                    }
                }
            }
        }

        public static void MessboxShow(string text)
        {
            MessageBox.Show(text);
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
            string[] list = new string[] { "张三", "李四", "王五" };

            int x = 0, y = 0;
            foreach (string item in list)
            {
                UserControl1 cb = new UserControl1();
                cb.label1.Text = item;
                cb.Location = new Point(x, y);
                listBox1.Controls.Add(cb);
                y += cb.Height - 10;
            }

      
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            //          string file = strpath[i];
            //          FileInfo fileInfo = new FileInfo(file);
            //          string[] temp = fileInfo.Name.Split(new string[] { fileInfo.Extension }, StringSplitOptions.None);
            //          string splitFileFormat = @"E:\Test\Copy\" + temp[0] + "_{0}" + fileInfo.Extension;
            string url = "E:/FTPShare/" + pathStr1 + "/";
            var filePaths = Directory.GetFiles(url);
            string combineFilePath = "";
            List<string> splitFileFormat = new List<string>();
            for (int i=0;i<filePaths.Count();i++)
            {
                FileInfo fileInfo = new FileInfo(filePaths[i]);
                splitFileFormat.Add(fileInfo.FullName);
                string[] temp = fileInfo.Name.Split(new string[] { "_tmp" }, StringSplitOptions.None);
                combineFilePath = @"E:\Test\Copy\" + temp[0];
            }
             CombineFiles(splitFileFormat.ToArray(), combineFilePath);

        }
    }
}
