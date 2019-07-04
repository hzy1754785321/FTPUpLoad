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
       public int spliteMin = 600;
        //每个文件最大上限 
       public int spliteFile = 200;
        //spliteMin的单位索引
       public int unitComboBoxIndex = 0;
        //spliteFile的单位索引
       public int unitMinComboBoxIndex = 0;
        //任务数量的索引
       public int taskComboBoxIndex = 0;
        //是否文件切割
       public bool confirmSplit = true;
    }

//# 服务器IP地址
//    serverIP : ""  
//#目标目录
//remotePath : ""
//#ftp用户账号
//ftpUser : ""
//#ftp用户密码
//ftpPasswd : ""
//#分割文件路径
//splitePath : ""
//#文件多大需要切割
//spliteMin : 600
//#每个文件最大上限 
//spliteFile: 200
//#spliteMin的单位索引
//unitComboBoxIndex : 0
//#spliteFile的单位索引
//unitMinComboBoxIndex : 0
//#任务数量的索引
//taskComboBoxIndex : 0
//#是否文件切割
//confirmSplit : true



    public class TestYaml
    {
        public class PcClass
        {
            private string ipv4;
            private int port;
            public string Ipv4
            {
                get { return ipv4; }
                set { ipv4 = value; }
            }
            public int Port
            {
                get { return port; }
                set { port = value; }
            }
        }

        private List<PcClass> Infos = new List<PcClass>();
        public List<PcClass> Info
        {
            get
            {
                return Infos;
            }

            set
            {
                Infos = value;
            }
        }
    }

}
