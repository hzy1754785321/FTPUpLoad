using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpLoad
{
    public class Setting
    {
        //服务器IP地址
        public string serverIP = "";
        //目标目录
        public string remotePath = "";
        //ftp用户账号
        public string ftpUser = "";
        //ftp用户密码
        public string ftpPasswd = "";
        //分割文件路径
        public string splitePath = "";
        //文件多大需要切割
        public string spliteMin = "";
        //每个文件最大上限 
        public string spliteFile = "";
        //spliteMin的单位索引
        public int unitComboBoxIndex = 0;
        //spliteFile的单位索引
        public int unitMinComboBoxIndex = 0;
        //任务数量的索引
        public int taskComboBoxIndex = 0;
        //是否文件切割
        public bool confirmSplit = true;
    }


}
