using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PureLog
{
    public class Log
    {
        static string logPath = @"C:\Users\XDL\Desktop\";        //日志路径
        public static void CreateNewLog(){
            DateTime yesterday = DateTime.Now.AddDays(-1);
            string logYesterday = yesterday.Year.ToString() + "_" + yesterday.Month.ToString() + "_" + yesterday.Day.ToString();
            if(File.Exists(logPath + "PrintLog " + logYesterday + ".txt")){
                File.Delete(logPath + "PrintLog " + logYesterday + ".txt");
            }
            string logDay = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString();
            string logFileName =logPath + "PrintLog " + logDay + ".txt";
            StreamWriter logWriter = File.CreateText(logFileName);
            logWriter.WriteLine("今天是" + logDay);
            logWriter.Close();
            logWriter.Dispose();
        }
        public static void LogRecord(string content){
            string logDay = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString();
            string logFileName = logPath + "PrintLog " + logDay + ".txt";
            StreamWriter logWriter = File.AppendText(logFileName);
            logWriter.WriteLine(content);
            logWriter.Close();
            logWriter.Dispose();
        }
    }
}
