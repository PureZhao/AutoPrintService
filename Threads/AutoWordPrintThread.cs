using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Office.Interop.Word;
using PureLog;
namespace PureThreads
{
    public class AutoWordPrintThread
    {
        private string folderPath = @"C:\Users\XDL\Desktop\Document Print";          //打印文件存放的文件夹
        Thread thread;
        public void Start()
        {
            thread = new Thread(AutoWordPrint);
            thread.Start();
            Log.LogRecord("Word自动打印服务启动成功");
        }
        public void End()
        {
            Console.WriteLine("Word自动打印关闭..................");
            thread.Abort();
        }
        void AutoWordPrint() {
            DirectoryInfo folder = new DirectoryInfo(folderPath);
            while (true)
            {
            UNLAW_NAME:
                Thread.Sleep(5000);   //五秒钟一扫描
                foreach (FileSystemInfo f in folder.GetFileSystemInfos())
                {
                    if (f is FileInfo)
                    {
                        FileInfo fi = (FileInfo)f;                      //得到文件信息
                        if (fi.FullName.Contains("已打印")) continue;          //如果是已经打印了的文件就跳过
                        string fileExtension = Path.GetExtension(fi.FullName);              //得到文件扩展名
                        if (fileExtension == ".doc" || fileExtension == ".docx")            //判断是不是文档
                        {
                            string filePath = fi.FullName;
                            string fileName = Path.GetFileNameWithoutExtension(fi.FullName);        //得到不带扩展名的文件名
                            int copies;         //打印份数
                            WdPrintOutRange wpr;        //打印范围
                            int from;       //从第几页
                            int to;             //到第几页
                            string username;        //谁的文档
                            bool canPrint = NameProcess(fileName, out copies, out wpr, out from, out to,out username);       //得到上述参数
                            if (!canPrint) {
                                File.Delete(filePath);
                                Log.LogRecord("已删除不规范名文件：" + Path.GetFileName(fi.FullName) + "              " + DateTime.Now.ToString());
                                goto UNLAW_NAME;
                            }
                            Log.LogRecord("请稍等！正在打印：" + Path.GetFileName(fi.FullName) + "              " + DateTime.Now.ToString()); 
                            PrintWord(filePath, copies, wpr, from, to);             //打印
                            Log.LogRecord("已打印：" + Path.GetFileNameWithoutExtension(fi.FullName) + " " + DateTime.Now.ToString());
                            File.Move(fi.FullName, Path.GetDirectoryName(fi.FullName) + "\\已打印" + Path.GetFileName(fi.FullName)); //打印完成改名
                        }
                    }
                }
            }
        }
        bool NameProcess(string fileName, out int copies, out WdPrintOutRange wpr, out int from, out int to,out string username)
        {
            try
            {
                string[] printSetting = fileName.Split(',');
                copies = int.Parse(printSetting[0]);
                string[] fromTo = printSetting[1].Split('-');
                if (printSetting[1].Contains("0-"))
                {
                    wpr = WdPrintOutRange.wdPrintAllDocument;
                    from = to = 0;
                }
                else
                {
                    wpr = WdPrintOutRange.wdPrintFromTo;
                    from = int.Parse(fromTo[0]);
                    to = int.Parse(fromTo[1]);
                }
                username = printSetting[2];
                return true;
            }
            catch (Exception ex) {
                copies = 0;
                wpr = WdPrintOutRange.wdPrintAllDocument;
                from = to = 0;
                username = "p";
                Log.LogRecord(fileName + "文件名不规范");
                Log.LogRecord(ex.Message);
                return false;
            }
        }
        void PrintWord(string wordPath, int copies, WdPrintOutRange wdPrintOutRange, int from, int to)
        {
            Application printWordApp = new Application();
            printWordApp.Visible = false;
            Document wordDoc = printWordApp.Documents.Open(wordPath);  //指定word
            object oMissing = Type.Missing;         //默认状态
            /*
             * wdPrintAllDocument   打印全部（默认）
             *wdPrintFromTo     从哪里打印到哪里
             */
            object printRange = wdPrintOutRange;        //打印范围
            object fromPageCode = (wdPrintOutRange == WdPrintOutRange.wdPrintAllDocument ? oMissing : from.ToString());     //从哪里开始打印
            object toPageCode = (wdPrintOutRange == WdPrintOutRange.wdPrintAllDocument ? oMissing : to.ToString());             //打印到哪一页
            object copiesQuantity = copies.ToString();      //份数
            printWordApp.PrintOut(
                false
                , oMissing
                , printRange        //printRange
                , oMissing
                , fromPageCode           //from
                , toPageCode           ///to
                , oMissing
                , copiesQuantity           //copies
                , oMissing
                , oMissing
                , oMissing
                , true       //逐份打印
                , oMissing
                , oMissing
                , oMissing
                );
            wordDoc.Close();
            printWordApp.Quit();
            wordDoc = null;
            printWordApp = null;
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
