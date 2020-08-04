using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Office.Interop.Word;
using System.IO;
using PureThreads;
using PureLog;

namespace CSharp
{
    class Program
    {       
       //FUJI XEROX DocuCentre S2110
        static void Main(string[] args)
        {
            Log.CreateNewLog();
            Thread.Sleep(3000);
            //自动删除程序运行
            FileDeleteThread fdt = new FileDeleteThread();
            fdt.Start();
            //自动打印程序运行
            AutoWordPrintThread awpt = new AutoWordPrintThread();
            awpt.Start();
            AutoPDFPrintThread apdfpt = new AutoPDFPrintThread();
            apdfpt.Start();
        }

    }
}

