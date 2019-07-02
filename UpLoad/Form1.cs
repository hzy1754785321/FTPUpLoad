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
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Collections;

namespace UpLoad
{
    public partial class Form1 : Form
    {
        private static string FTPCONSTR = "ftp://192.168.238.1";
        private static string[] strpath;
        private static string pathStr = "/data/";
        private static string pathStr1 = "data";
        public delegate bool MethodCaller(string path, string remotePath, Action<UserControl1, int, int> updateProgress);//定义个代理 

        public delegate bool tempChange(MethodCaller mc, IAsyncResult ir, string path, Stopwatch time,int index);
        public static int[] flags;
        public static Form1 form1;
        public static int unitKB = 1024;
        public static int unitM = 1024 * 1024;
        public static int unitG = 1024 * 1024 * 1024;


        public Form1()
        {
            InitializeComponent();
            form1 = this;
            taskComboBox.Items.Add("3");
            taskComboBox.Items.Add("1");
            taskComboBox.Items.Add("2");
            taskComboBox.Items.Add("4");
            taskComboBox.Items.Add("5");
            taskComboBox.Items.Add("6");
            taskComboBox.Items.Add("7");
            taskComboBox.Items.Add("8");
            taskComboBox.SelectedIndex = 0;
            unitComboBox.Items.Add("M");
            unitComboBox.Items.Add("KB");
            unitComboBox.Items.Add("G");
            unitComboBox.SelectedIndex = 0;
            unitMinComboBox.Items.Add("M");
            unitMinComboBox.Items.Add("KB");
            unitMinComboBox.Items.Add("G");
            unitMinComboBox.SelectedIndex = 0;
            ConfirmSplit.Checked = true;
        }


        private void Uploadbutton_Click(object sender, EventArgs e)
        {

            Thread thread = new Thread(UpdateStart);
            thread.Start();
        }

        public void UpdateStart()
        {
            var path = strpath;
            Action<UserControl1, int, int> updateProgress = progressBarShow;
            List<Stopwatch> time = new List<Stopwatch>();
            CheckForIllegalCrossThreadCalls = false;
            List<string> splitFilePath = null;
            for (int i = 0; i < path.Length; i++)
            {
                if(ConfirmSplit.Checked)
                    splitFilePath = SplitFile(path[i]);
                if (splitFilePath == null)
                {
                    splitFilePath = new List<string>();
                    splitFilePath.Add(path[i]);
                }
                CreateControl(splitFilePath);
                List<MethodCaller> mcs = new List<MethodCaller>();
                List<IAsyncResult> ret = new List<IAsyncResult>();
                List<MethodCaller> mcsTmp = new List<MethodCaller>();
                flags = new int[splitFilePath.Count];
                for (int j = 0; j < splitFilePath.Count; j++)
                {
                    time.Add(new Stopwatch());
                    time[j].Start();
                    MethodCaller mc = new MethodCaller(FTPHelper.FtpUploadBroken);
                    mcs.Add(mc);
                    mcsTmp.Add(mc);
                    IAsyncResult rets = mcs[j].BeginInvoke(splitFilePath[j], pathStr1, updateProgress, null, null);
                    ret.Add(rets);
                    tempChange tc = new tempChange(UpdateResult);
                    tc.BeginInvoke(mcs[j], ret[j], splitFilePath[j], time[j], j, null, null);
                    while (mcsTmp.Count >= 2)
                    {

                        bool isBreak = false;
                        while (!isBreak)
                        {
                            for (int x = 0; x < flags.Length; x++)
                            {
                                if (flags[x] == 1)
                                {
                                    flags[x] = 2;
                                    isBreak = true;
                                    break;
                                }
                            }
                            Thread.Sleep(500);
                        }
                        mcsTmp.Remove(mcs[j]);
                    }
                }

            }
            bool isOver = false;
            while (!isOver)
            {
                for (int x = 0; x < flags.Length; x++)
                {
                    if (flags[x] == 0)
                    {
                        isOver = false;
                        break;
                    }
                    isOver = true;
                }
                Thread.Sleep(500);
            }
            MessageBox.Show("上传结束!");
        }


        public bool UpdateResult(MethodCaller mc, IAsyncResult ir, string splitPath, Stopwatch time, int index)
        {
            bool result = mc.EndInvoke(ir);
            time.Stop();
            if (result)
            {
                ChangeControl(splitPath, "上传成功", time.Elapsed.TotalSeconds.ToString("f2"));

            }
            else
            {
                ChangeControl(splitPath, "上传失败", "0");
            }
            time.Reset();
            flags[index] = 1;
            return true;
        }

        public void ChangeControl(string name, string text, string time)
        {
            foreach (UserControl1 item in panel1.Controls)
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
            if (panel1.Controls.Count != 0)
            {
                x = panel1.Controls[panel1.Controls.Count - 1].Location.X;
                y = panel1.Controls[panel1.Controls.Count - 1].Location.Y + panel1.Controls[0].Height + 10;
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
                    panel1.Controls.Add(cb);
                }));
                y += cb.Height + 10;
            }
        }

        private void AddUserControl(Control cb)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate { AddUserControl(cb); }));
                return;
            }
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
            uc.progressBar1.Maximum = allbye;
            uc.progressBar1.Minimum = 0;
            uc.progressBar1.Value = nowByte;
        }

        public UserControl1 FindUserContrl(string name)
        {
            foreach (UserControl1 item in this.panel1.Controls)
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
            }
            catch
            {
                MessageBox.Show("请选择文件!");
            }
        }

        public List<string> SplitFile(string filePaths)
        {
            string file = filePaths;
            FileInfo fileInfo = new FileInfo(file);
            string[] temp = fileInfo.Name.Split(new string[] { fileInfo.Extension }, StringSplitOptions.None);
            string splitFileFormat = @"D:\Test\Copy\" + temp[0] + "_tmp{0}" + fileInfo.Extension;
            int splitMinFileSize = 0;
            if (unitComboBox.Text == "M")
                splitMinFileSize = Convert.ToInt32(splitMinFileSizeText.Text) * unitM;
            else if (unitComboBox.Text == "KB")
                splitMinFileSize = Convert.ToInt32(splitMinFileSizeText.Text) * unitKB;
            else if (unitComboBox.Text == "G")
                splitMinFileSize = Convert.ToInt32(splitMinFileSizeText.Text) * unitG;
            int splitFileSize = 0;
            if (unitMinComboBox.Text == "M")
                splitFileSize = Convert.ToInt32(splitFileSizeText.Text) * unitM;
            else if (unitMinComboBox.Text == "KB")
                splitFileSize = Convert.ToInt32(splitFileSizeText.Text) * unitKB;
            else if (unitMinComboBox.Text == "G")
                splitFileSize = Convert.ToInt32(splitFileSizeText.Text) * unitG; 
            List<string> splitFilePath = new List<string>();
            if (fileInfo.Length < splitMinFileSize)
            { 
                return null;  //不需要分割
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

        private void Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(unitComboBox.Text);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
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
