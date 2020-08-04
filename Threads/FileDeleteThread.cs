using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using PureLog;
namespace PureThreads
{
    public class FileDeleteThread
    {
        public string folderPath = @"C:\Users\XDL\Desktop\Document Print";          //打印文件存放的文件夹
        Thread thread;
        public void Start() {
            //每次启动应用程序删除FTP中所有文件
            DirectoryInfo folder = new DirectoryInfo(folderPath);
            foreach (FileSystemInfo f in folder.GetFileSystemInfos())
            {
                FileInfo fi = (FileInfo)f;
                File.Delete(fi.FullName);
            }
            thread = new Thread(DeleteThread);
            thread.Start();
            Log.LogRecord("自动删除功能启动成功");
        }
        public void End() {
            Log.LogRecord("自动删除功能关闭");
            thread.Abort();
        }
        void DeleteThread()
        {
            while (true)
            {
                Thread.Sleep(600000);     //计时删除 10分钟
                DirectoryInfo folder = new DirectoryInfo(folderPath);
                foreach (FileSystemInfo f in folder.GetFileSystemInfos())
                {
                    FileInfo fi = (FileInfo)f;
                    if (fi.FullName.Contains("已打印"))
                    {
                        Log.LogRecord("已删除：" + Path.GetFileNameWithoutExtension(fi.FullName) + " " + DateTime.Now.ToString());
                        File.Delete(fi.FullName);
                    }
                }
            }
        }
    
    }
}
