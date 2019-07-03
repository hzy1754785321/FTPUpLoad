﻿using System;
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
using System.Text.RegularExpressions;

namespace UpLoad
{
    public partial class Form1 : Form
    {
        private static string[] strpath;
        private static string pathStr1 = "data";
        private static string splitFileSavePath = "";
        public delegate bool MethodCaller(string path, string remotePath, Action<UserControl1, int, int> updateProgress);//定义个代理 

        public delegate bool tempChange(MethodCaller mc, IAsyncResult ir, string path, Stopwatch time,int index);
        public static int[] flags;   //标志位
        public static Form1 form1;  
        public static int unitKB = 1024;   //KB单位
        public static int unitM = 1024 * 1024;  //M单位
        public static int unitG = 1024 * 1024 * 1024;  //GB单位



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
            FTPHelper.FtpUserID = FtpUserText.Text;
            FTPHelper.FtpPassword = ftpPasswdText.Text;
            pathStr1 = ftpRemoteText.Text;
            ftpIPText.Text = "192.168.238.1";
        }

        /// <summary>
        /// 点击上传到FTP按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Uploadbutton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ftpIPText.Text))
            {
                MessageBox.Show("请先输入FTP服务器的IP地址!");
                return;
            }
            if (!Regex.IsMatch(ftpIPText.Text, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$"))
            {
                MessageBox.Show("输入的FTP服务器的IP格式错误!");
                return;
            }
            FTPHelper.FtpServerIP = ftpIPText.Text;
            Thread thread = new Thread(UpdateStart);
            thread.Start();
        }

        /// <summary>
        /// 上传线程
        /// </summary>
        public void UpdateStart()
        {
            var path = strpath;
            //滚动条
            Action<UserControl1, int, int> updateProgress = progressBarShow;
            List<Stopwatch> time = new List<Stopwatch>();   //计时器
            CheckForIllegalCrossThreadCalls = false;
            List<string> splitFilePath = null;
            for (int i = 0; i < path.Length; i++)
            {
                if(ConfirmSplit.Checked)
                    splitFilePath = SplitFile(path[i]);  //切割文件
                if (splitFilePath == null)
                {
                    splitFilePath = new List<string>();
                    splitFilePath.Add(path[i]);
                }
                //生成自定义控件
                CreateControl(splitFilePath);
                List<MethodCaller> mcs = new List<MethodCaller>();
                List<IAsyncResult> ret = new List<IAsyncResult>();
                List<MethodCaller> mcsTmp = new List<MethodCaller>();
                flags = new int[splitFilePath.Count];
                for (int j = 0; j < splitFilePath.Count; j++)
                {
                    time.Add(new Stopwatch());
                    time[j].Start();
                    //上传封装到委托
                    MethodCaller mc = new MethodCaller(FTPHelper.FtpUploadBroken);
                    mcs.Add(mc);
                    mcsTmp.Add(mc);
                    //启动上传的异步委托
                    IAsyncResult rets = mcs[j].BeginInvoke(splitFilePath[j], pathStr1, updateProgress, null, null);
                    ret.Add(rets);
                    ChangeControl(splitFilePath[j], "上传中", "0");
                    //上传结果封装到委托
                    tempChange tc = new tempChange(UpdateResult);
                    //启动上传结果处理的异步委托
                    tc.BeginInvoke(mcs[j], ret[j], splitFilePath[j], time[j], j, null, null);

                    int taskNumber = Convert.ToInt32(taskComboBox.Text);
                    //同时进行最大任务数
                    while (mcsTmp.Count >= taskNumber)
                    {
                        bool isBreak = false;
                        //等待任务结果
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
            //等待全部完成
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


        /// <summary>
        /// 上传结果处理
        /// </summary>
        /// <param name="mc">上传的委托方法</param>
        /// <param name="ir">上传委托的返回</param>
        /// <param name="splitPath">文件路径</param>
        /// <param name="time">计时器</param>
        /// <param name="index">索引</param>
        /// <returns></returns>
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
            //标志文件以及上传完成
            flags[index] = 1;
            return true;
        }

        /// <summary>
        /// 修改自定义控件内容
        /// </summary>
        /// <param name="name">控件名</param>
        /// <param name="text">需要修改的内容</param>
        /// <param name="time">耗时</param>
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
                    break;
                }
            }
        }

        /// <summary>
        /// 生成自定义控件
        /// </summary>
        /// <param name="spliteFile"></param>
        public void CreateControl(List<string> spliteFile)
        {
            int x = 0, y = 0;
            if (panel1.Controls.Count != 0)
            {
                //获取最后一个控件的坐标
                x = panel1.Controls[panel1.Controls.Count - 1].Location.X;
                y = panel1.Controls[panel1.Controls.Count - 1].Location.Y;
            }
            foreach (string item in spliteFile)
            {
                UserControl1 cb = new UserControl1();
                var fileInfo = new FileInfo(item);
                //获取没有后缀名的文件名
                string[] temp = fileInfo.Name.Split(new string[] { fileInfo.Extension }, StringSplitOptions.None);
                cb.label1.Text = temp[0];
                cb.label2.Text = "等待中";
                cb.Location = new Point(x, y);
                //用委托增加控件,因为是在子线程刷主线程控件
                this.Invoke(new EventHandler(delegate
                {
                    panel1.Controls.Add(cb);
                }));
                y += cb.Height + 8;
            }
        }

     
        /// <summary>
        /// 进度条刷新
        /// </summary>
        /// <param name="uc">控件</param>
        /// <param name="allbye">总进度</param>
        /// <param name="nowByte">当前进度</param>
        public void progressBarShow(UserControl1 uc, int allbye, int nowByte)
        {
            uc.progressBar1.Maximum = allbye;
            uc.progressBar1.Minimum = 0;
            uc.progressBar1.Value = nowByte;
        }

        /// <summary>
        /// 查找控件
        /// </summary>
        /// <param name="name">控件名</param>
        /// <returns></returns>
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

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Multiselect = true;
                openFileDialog1.Title = "选择需要上传的文件";
                openFileDialog1.ShowDialog();
                var path = openFileDialog1.FileNames;  //获取openFileDialog控件选择的文件名数组
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

        /// <summary>
        /// 分割文件
        /// </summary>
        /// <param name="filePaths"></param>
        /// <returns></returns>
        public List<string> SplitFile(string filePaths)
        {
            while (splitFileSavePath == "")
            {
                this.Invoke(new EventHandler(delegate
                {
                    FolderBrowserDialog P_File_Folder = new FolderBrowserDialog();
                    P_File_Folder.Description = "请选择保存文件夹";
                    if (P_File_Folder.ShowDialog() == DialogResult.OK)
                    {
                        splitFileSavePath = P_File_Folder.SelectedPath;
                    }
                    else
                    {
                        MessageBox.Show("请选择分割文件保存路径");
                    }
                }));
            }
            string file = filePaths;
            FileInfo fileInfo = new FileInfo(file);
            string[] temp = fileInfo.Name.Split(new string[] { fileInfo.Extension }, StringSplitOptions.None);
            //存放路径
            string splitFileFormat = splitFileSavePath + @"\" +temp[0] + "_tmp{0}" + fileInfo.Extension;
            //确定需要分割文件大小的单位
            int splitMinFileSize = 0;
            if (unitComboBox.Text == "M")
                splitMinFileSize = Convert.ToInt32(splitMinFileSizeText.Text) * unitM;
            else if (unitComboBox.Text == "KB")
                splitMinFileSize = Convert.ToInt32(splitMinFileSizeText.Text) * unitKB;
            else if (unitComboBox.Text == "G")
                splitMinFileSize = Convert.ToInt32(splitMinFileSizeText.Text) * unitG;
            //确定每个分割文件大小的单位
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

        /// <summary>
        /// 选择分割文件保存文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog P_File_Folder = new FolderBrowserDialog();
            P_File_Folder.Description = "请选择保存文件夹";
            if (P_File_Folder.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(P_File_Folder.SelectedPath);
                splitFileSavePath = P_File_Folder.SelectedPath;
            }
        }

        /// <summary>
        /// 合并文件,测试用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button3_Click(object sender, EventArgs e)
        {
            Graphics g = button3.CreateGraphics();
            PaintEventArgs pe = new PaintEventArgs(g, button3.ClientRectangle);
            Rectangle myRectangle = new Rectangle(0, 0, button3.Width, button3.Height);
            ControlPaint.DrawBorder(pe.Graphics, myRectangle,
                Color.Red, 6, ButtonBorderStyle.Solid,
                Color.Red, 6, ButtonBorderStyle.Solid,
                Color.Red, 6, ButtonBorderStyle.Solid,
                Color.Red, 6, ButtonBorderStyle.Solid
            );
            //string url = "D:/TestHzy/" + pathStr1 + "/";
            //var filePaths = Directory.GetFiles(url);
            //string combineFilePath = "";
            //List<string> splitFileFormat = new List<string>();
            //for (int i = 0; i < filePaths.Count(); i++)
            //{
            //    FileInfo fileInfo = new FileInfo(filePaths[i]);
            //    splitFileFormat.Add(fileInfo.FullName);
            //    string[] temp = fileInfo.Name.Split(new string[] { "_tmp" }, StringSplitOptions.None);
            //    combineFilePath = @"D:/TestHzy/" + pathStr1 + "/" + temp[0];
            //}
            //CombineFiles(splitFileFormat.ToArray(), combineFilePath);
            //MessageBox.Show("合并完成!");
        }

        private void UserControl11_MouseHover(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            PaintEventArgs pe = new PaintEventArgs(g, this.ClientRectangle);
            Rectangle myRectangle = new Rectangle(0, 0, this.Width, this.Height);
            ControlPaint.DrawBorder(pe.Graphics, myRectangle,
                Color.Gray, 2, ButtonBorderStyle.Solid,
                Color.Gray, 2, ButtonBorderStyle.Solid,
                Color.Gray, 2, ButtonBorderStyle.Solid,
                Color.Gray, 2, ButtonBorderStyle.Solid
            );
        }
    }
}
