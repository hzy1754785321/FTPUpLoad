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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Uploadbutton
            // 
            this.Uploadbutton.Location = new System.Drawing.Point(219, 151);
            this.Uploadbutton.Name = "Uploadbutton";
            this.Uploadbutton.Size = new System.Drawing.Size(122, 35);
            this.Uploadbutton.TabIndex = 0;
            this.Uploadbutton.Text = "上传到FTP";
            this.Uploadbutton.UseVisualStyleBackColor = true;
            this.Uploadbutton.Click += new System.EventHandler(this.Uploadbutton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(36, 151);
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
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(36, 74);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(305, 23);
            this.progressBar1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(128, 206);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 35);
            this.button1.TabIndex = 4;
            this.button1.Text = "上传";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 253);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Uploadbutton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Uploadbutton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button1;
    }
}

