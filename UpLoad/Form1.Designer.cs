using System.Windows.Forms;

namespace UpLoad
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        //防闪烁
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;
        //        return cp;
        //    }
        //}

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Uploadbutton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.taskComboBox = new System.Windows.Forms.ComboBox();
            this.taskNumLaber = new System.Windows.Forms.Label();
            this.fileNameLaber = new System.Windows.Forms.Label();
            this.progressLabel = new System.Windows.Forms.Label();
            this.stateLaber = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.ConfirmSplit = new System.Windows.Forms.CheckBox();
            this.splitMinFileSizeText = new System.Windows.Forms.TextBox();
            this.splitMinFileSizeLaber = new System.Windows.Forms.Label();
            this.unitComboBox = new System.Windows.Forms.ComboBox();
            this.splitFileSizeLaber = new System.Windows.Forms.Label();
            this.splitFileSizeText = new System.Windows.Forms.TextBox();
            this.unitMinComboBox = new System.Windows.Forms.ComboBox();
            this.ftpIPLaber = new System.Windows.Forms.Label();
            this.ftpRemoteLaber = new System.Windows.Forms.Label();
            this.ftpIPText = new System.Windows.Forms.TextBox();
            this.ftpRemoteText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FtpUserText = new System.Windows.Forms.TextBox();
            this.ftpPasswdText = new System.Windows.Forms.TextBox();
            this.userControl11 = new UpLoad.UserControl1();
            this.userControl12 = new UpLoad.UserControl1();
            this.userControl13 = new UpLoad.UserControl1();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Uploadbutton
            // 
            this.Uploadbutton.Location = new System.Drawing.Point(172, 12);
            this.Uploadbutton.Name = "Uploadbutton";
            this.Uploadbutton.Size = new System.Drawing.Size(122, 35);
            this.Uploadbutton.TabIndex = 0;
            this.Uploadbutton.Text = "上传到FTP";
            this.Uploadbutton.UseVisualStyleBackColor = true;
            this.Uploadbutton.Click += new System.EventHandler(this.Uploadbutton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(23, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(140, 35);
            this.button2.TabIndex = 2;
            this.button2.Text = "选择要上传的文件";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 35);
            this.button1.TabIndex = 4;
            this.button1.Text = "选择分割文件保存路径";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(171, 66);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(122, 35);
            this.button3.TabIndex = 8;
            this.button3.Text = "合并";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.userControl13);
            this.panel1.Controls.Add(this.userControl12);
            this.panel1.Controls.Add(this.userControl11);
            this.panel1.Location = new System.Drawing.Point(12, 198);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(608, 347);
            this.panel1.TabIndex = 10;
            // 
            // taskComboBox
            // 
            this.taskComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.taskComboBox.FormattingEnabled = true;
            this.taskComboBox.Location = new System.Drawing.Point(554, 19);
            this.taskComboBox.Name = "taskComboBox";
            this.taskComboBox.Size = new System.Drawing.Size(53, 20);
            this.taskComboBox.TabIndex = 11;
            // 
            // taskNumLaber
            // 
            this.taskNumLaber.AutoSize = true;
            this.taskNumLaber.Location = new System.Drawing.Point(459, 23);
            this.taskNumLaber.Name = "taskNumLaber";
            this.taskNumLaber.Size = new System.Drawing.Size(95, 12);
            this.taskNumLaber.TabIndex = 12;
            this.taskNumLaber.Text = "同时最大任务数:";
            // 
            // fileNameLaber
            // 
            this.fileNameLaber.AutoSize = true;
            this.fileNameLaber.Location = new System.Drawing.Point(47, 178);
            this.fileNameLaber.Name = "fileNameLaber";
            this.fileNameLaber.Size = new System.Drawing.Size(41, 12);
            this.fileNameLaber.TabIndex = 13;
            this.fileNameLaber.Text = "文件名";
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(329, 178);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(29, 12);
            this.progressLabel.TabIndex = 16;
            this.progressLabel.Text = "进度";
            // 
            // stateLaber
            // 
            this.stateLaber.AutoSize = true;
            this.stateLaber.Location = new System.Drawing.Point(169, 178);
            this.stateLaber.Name = "stateLaber";
            this.stateLaber.Size = new System.Drawing.Size(29, 12);
            this.stateLaber.TabIndex = 15;
            this.stateLaber.Text = "状态";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(506, 178);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(29, 12);
            this.timeLabel.TabIndex = 17;
            this.timeLabel.Text = "耗时";
            // 
            // ConfirmSplit
            // 
            this.ConfirmSplit.AutoSize = true;
            this.ConfirmSplit.Location = new System.Drawing.Point(309, 23);
            this.ConfirmSplit.Name = "ConfirmSplit";
            this.ConfirmSplit.Size = new System.Drawing.Size(96, 16);
            this.ConfirmSplit.TabIndex = 18;
            this.ConfirmSplit.Text = "启用文件分割";
            this.ConfirmSplit.UseVisualStyleBackColor = true;
            // 
            // splitMinFileSizeText
            // 
            this.splitMinFileSizeText.Location = new System.Drawing.Point(417, 46);
            this.splitMinFileSizeText.Name = "splitMinFileSizeText";
            this.splitMinFileSizeText.Size = new System.Drawing.Size(44, 21);
            this.splitMinFileSizeText.TabIndex = 19;
            this.splitMinFileSizeText.Text = "600";
            // 
            // splitMinFileSizeLaber
            // 
            this.splitMinFileSizeLaber.AutoSize = true;
            this.splitMinFileSizeLaber.Location = new System.Drawing.Point(307, 49);
            this.splitMinFileSizeLaber.Name = "splitMinFileSizeLaber";
            this.splitMinFileSizeLaber.Size = new System.Drawing.Size(107, 12);
            this.splitMinFileSizeLaber.TabIndex = 20;
            this.splitMinFileSizeLaber.Text = "文件多大需要分割:";
            // 
            // unitComboBox
            // 
            this.unitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.unitComboBox.FormattingEnabled = true;
            this.unitComboBox.Location = new System.Drawing.Point(461, 46);
            this.unitComboBox.Name = "unitComboBox";
            this.unitComboBox.Size = new System.Drawing.Size(48, 20);
            this.unitComboBox.TabIndex = 21;
            // 
            // splitFileSizeLaber
            // 
            this.splitFileSizeLaber.AutoSize = true;
            this.splitFileSizeLaber.Location = new System.Drawing.Point(306, 77);
            this.splitFileSizeLaber.Name = "splitFileSizeLaber";
            this.splitFileSizeLaber.Size = new System.Drawing.Size(107, 12);
            this.splitFileSizeLaber.TabIndex = 22;
            this.splitFileSizeLaber.Text = "每个文件最大上限:";
            // 
            // splitFileSizeText
            // 
            this.splitFileSizeText.Location = new System.Drawing.Point(417, 72);
            this.splitFileSizeText.Name = "splitFileSizeText";
            this.splitFileSizeText.Size = new System.Drawing.Size(44, 21);
            this.splitFileSizeText.TabIndex = 23;
            this.splitFileSizeText.Text = "200";
            // 
            // unitMinComboBox
            // 
            this.unitMinComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.unitMinComboBox.FormattingEnabled = true;
            this.unitMinComboBox.Location = new System.Drawing.Point(461, 73);
            this.unitMinComboBox.Name = "unitMinComboBox";
            this.unitMinComboBox.Size = new System.Drawing.Size(48, 20);
            this.unitMinComboBox.TabIndex = 24;
            // 
            // ftpIPLaber
            // 
            this.ftpIPLaber.AutoSize = true;
            this.ftpIPLaber.Location = new System.Drawing.Point(21, 113);
            this.ftpIPLaber.Name = "ftpIPLaber";
            this.ftpIPLaber.Size = new System.Drawing.Size(77, 12);
            this.ftpIPLaber.TabIndex = 25;
            this.ftpIPLaber.Text = "FTP服务器IP:";
            // 
            // ftpRemoteLaber
            // 
            this.ftpRemoteLaber.AutoSize = true;
            this.ftpRemoteLaber.Location = new System.Drawing.Point(21, 140);
            this.ftpRemoteLaber.Name = "ftpRemoteLaber";
            this.ftpRemoteLaber.Size = new System.Drawing.Size(77, 12);
            this.ftpRemoteLaber.TabIndex = 26;
            this.ftpRemoteLaber.Text = "FTP目标目录:";
            // 
            // ftpIPText
            // 
            this.ftpIPText.Location = new System.Drawing.Point(104, 107);
            this.ftpIPText.Name = "ftpIPText";
            this.ftpIPText.Size = new System.Drawing.Size(172, 21);
            this.ftpIPText.TabIndex = 27;
            // 
            // ftpRemoteText
            // 
            this.ftpRemoteText.Location = new System.Drawing.Point(104, 136);
            this.ftpRemoteText.Name = "ftpRemoteText";
            this.ftpRemoteText.Size = new System.Drawing.Size(172, 21);
            this.ftpRemoteText.TabIndex = 28;
            this.ftpRemoteText.Text = "data";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(292, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 29;
            this.label1.Text = "FTP用户名:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(303, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 30;
            this.label2.Text = "FTP密码:";
            // 
            // FtpUserText
            // 
            this.FtpUserText.Location = new System.Drawing.Point(370, 106);
            this.FtpUserText.Name = "FtpUserText";
            this.FtpUserText.Size = new System.Drawing.Size(139, 21);
            this.FtpUserText.TabIndex = 31;
            // 
            // ftpPasswdText
            // 
            this.ftpPasswdText.Location = new System.Drawing.Point(370, 134);
            this.ftpPasswdText.Name = "ftpPasswdText";
            this.ftpPasswdText.PasswordChar = '*';
            this.ftpPasswdText.Size = new System.Drawing.Size(139, 21);
            this.ftpPasswdText.TabIndex = 32;
            // 
            // userControl11
            // 
            this.userControl11.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.userControl11.Location = new System.Drawing.Point(10, 40);
            this.userControl11.Name = "userControl11";
            this.userControl11.Size = new System.Drawing.Size(587, 36);
            this.userControl11.TabIndex = 0;
            // 
            // userControl12
            // 
            this.userControl12.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.userControl12.Location = new System.Drawing.Point(7, 82);
            this.userControl12.Name = "userControl12";
            this.userControl12.Size = new System.Drawing.Size(587, 36);
            this.userControl12.TabIndex = 1;
            // 
            // userControl13
            // 
            this.userControl13.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.userControl13.Location = new System.Drawing.Point(11, 124);
            this.userControl13.Name = "userControl13";
            this.userControl13.Size = new System.Drawing.Size(587, 36);
            this.userControl13.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 557);
            this.Controls.Add(this.ftpPasswdText);
            this.Controls.Add(this.FtpUserText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ftpRemoteText);
            this.Controls.Add(this.ftpIPText);
            this.Controls.Add(this.ftpRemoteLaber);
            this.Controls.Add(this.ftpIPLaber);
            this.Controls.Add(this.unitMinComboBox);
            this.Controls.Add(this.splitFileSizeText);
            this.Controls.Add(this.splitFileSizeLaber);
            this.Controls.Add(this.unitComboBox);
            this.Controls.Add(this.splitMinFileSizeLaber);
            this.Controls.Add(this.splitMinFileSizeText);
            this.Controls.Add(this.ConfirmSplit);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.stateLaber);
            this.Controls.Add(this.fileNameLaber);
            this.Controls.Add(this.taskNumLaber);
            this.Controls.Add(this.taskComboBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Uploadbutton);
            this.Name = "Form1";
            this.Text = "UpLoad";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Uploadbutton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private Panel panel1;
        private ComboBox taskComboBox;
        private Label taskNumLaber;
        private Label fileNameLaber;
        private Label progressLabel;
        private Label stateLaber;
        private Label timeLabel;
        private CheckBox ConfirmSplit;
        private TextBox splitMinFileSizeText;
        private Label splitMinFileSizeLaber;
        private ComboBox unitComboBox;
        private Label splitFileSizeLaber;
        private TextBox splitFileSizeText;
        private ComboBox unitMinComboBox;
        private Label ftpIPLaber;
        private Label ftpRemoteLaber;
        private TextBox ftpIPText;
        private TextBox ftpRemoteText;
        private Label label1;
        private Label label2;
        private TextBox FtpUserText;
        private TextBox ftpPasswdText;
        private UserControl1 userControl11;
        private UserControl1 userControl13;
        private UserControl1 userControl12;
    }
}

