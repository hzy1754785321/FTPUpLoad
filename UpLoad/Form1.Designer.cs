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

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
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
            this.SuspendLayout();
            // 
            // Uploadbutton
            // 
            this.Uploadbutton.Location = new System.Drawing.Point(169, 12);
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
            this.button2.Size = new System.Drawing.Size(122, 35);
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
            this.button1.Size = new System.Drawing.Size(122, 35);
            this.button1.TabIndex = 4;
            this.button1.Text = "测试";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(169, 66);
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
            this.panel1.Location = new System.Drawing.Point(12, 154);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(617, 391);
            this.panel1.TabIndex = 10;
            // 
            // taskComboBox
            // 
            this.taskComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.taskComboBox.FormattingEnabled = true;
            this.taskComboBox.Location = new System.Drawing.Point(537, 19);
            this.taskComboBox.Name = "taskComboBox";
            this.taskComboBox.Size = new System.Drawing.Size(53, 20);
            this.taskComboBox.TabIndex = 11;
            // 
            // taskNumLaber
            // 
            this.taskNumLaber.AutoSize = true;
            this.taskNumLaber.Location = new System.Drawing.Point(442, 24);
            this.taskNumLaber.Name = "taskNumLaber";
            this.taskNumLaber.Size = new System.Drawing.Size(95, 12);
            this.taskNumLaber.TabIndex = 12;
            this.taskNumLaber.Text = "同时最大任务数:";
            // 
            // fileNameLaber
            // 
            this.fileNameLaber.AutoSize = true;
            this.fileNameLaber.Location = new System.Drawing.Point(49, 136);
            this.fileNameLaber.Name = "fileNameLaber";
            this.fileNameLaber.Size = new System.Drawing.Size(41, 12);
            this.fileNameLaber.TabIndex = 13;
            this.fileNameLaber.Text = "文件名";
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(329, 136);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(29, 12);
            this.progressLabel.TabIndex = 16;
            this.progressLabel.Text = "进度";
            // 
            // stateLaber
            // 
            this.stateLaber.AutoSize = true;
            this.stateLaber.Location = new System.Drawing.Point(167, 136);
            this.stateLaber.Name = "stateLaber";
            this.stateLaber.Size = new System.Drawing.Size(29, 12);
            this.stateLaber.TabIndex = 15;
            this.stateLaber.Text = "状态";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(506, 136);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 557);
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
    }
}

