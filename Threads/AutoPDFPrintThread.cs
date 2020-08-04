using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using PureLog;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Collections.Specialized;
using O2S.Components.PDFRender4NET;
using O2S.Components.PDFRender4NET.Printing;
namespace PureThreads
{
    public class AutoPDFPrintThread
    {
        Thread thread;
        private string folderPath = @"C:\Users\XDL\Desktop\Document Print";          //打印文件存放的文件夹
        public void Start() {
            thread = new Thread(AutoPDFPrint);
            thread.Start();
            Log.LogRecord("PDF自动打印程序启动成功");
        }
        public void End() {
            thread.Abort();
            Log.LogRecord("PDF自动打印程序已关闭");
        }
        bool NameProcess(string fileName,out int copies) {
            string[] printInfo = fileName.Split(',');
            try
            {
                copies = int.Parse(printInfo[0]);
                return true;
            }
            catch (Exception ex) {
                copies = 0;
                Log.LogRecord(fileName + "文件名不规范");
                Log.LogRecord(ex.Message);
                return false;
            }
        }
        void AutoPDFPrint()
        {
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
                        if (fileExtension == ".pdf")            //判断是不是PDF
                        {
                            Log.LogRecord("请稍等！正在打印：" + Path.GetFileNameWithoutExtension(fi.FullName) + "              " + DateTime.Now.ToString());
                            int copies;
                            bool canPrint = NameProcess(Path.GetFileNameWithoutExtension(fi.FullName), out copies);
                            if (!canPrint)
                            {
                                File.Delete(fi.FullName);
                                Log.LogRecord("已删除不规范名文件：" + Path.GetFileName(fi.FullName) + "              " + DateTime.Now.ToString());
                                goto UNLAW_NAME;
                            }
                            PrintPDF(fi.FullName, copies);
                            Log.LogRecord("已打印：" + Path.GetFileNameWithoutExtension(fi.FullName) + " " + DateTime.Now.ToString());
                            File.Move(fi.FullName, Path.GetDirectoryName(fi.FullName) + "\\已打印" + Path.GetFileName(fi.FullName)); //打印完成改名
                        }
                    }
                }
            }
        }
        void PrintPDF(string pdfPath,int copies) {
            PDFFile file = PDFFile.Open(pdfPath);
            PrinterSettings setting = new PrinterSettings();
            PrintDocument pd = new PrintDocument();
            setting.PrinterName = "FUJI XEROX DocuCentre S2110";
            setting.Duplex = Duplex.Vertical;
            setting.PrintToFile = false;
            setting.Copies = (short)copies;
            setting.Collate = true;
            PDFPrintSettings pdfps = new PDFPrintSettings(setting);
            try
            {
                file.Print(pdfps);
            }
            finally
            {
                file.Dispose();
            }
        }
    }
}
