using System;
using System.Threading;
using System.IO;
using System.Configuration;

namespace LOG
{
    //Log file DIRECTORY must be there.
    class LogWriter
    {
        private ReaderWriterLockSlim _readWriteLock;
        private string path;
        private string filename;
        private long fileMaxLen;

        public LogWriter()
        {
            _readWriteLock = new ReaderWriterLockSlim();
            string configPath = ConfigurationManager.AppSettings["ConfigPath"].ToString();
            Configuration config = ConfigurationManager.OpenExeConfiguration(Path.Combine(configPath, "ConsoleHeartBeat.exe"));

            path = config.AppSettings.Settings["LogFilePath"].Value;
            filename = config.AppSettings.Settings["LogFileName"].Value;
            fileMaxLen = Convert.ToInt64(config.AppSettings.Settings["LogFileMaxLen"].Value);
        }
        public void WriteLog(string log, string SubCap, string trace = "Trace")
        {
            try
            {
                FileInfo xf = new FileInfo(path + filename);
                if (xf.Length > fileMaxLen) { MoveToNewLogfile(); }
            }
            catch { }


            _readWriteLock.EnterWriteLock();
            try
            {
                string output = String.Format(DateTime.Now.ToString("yyyy-dd-M  HH:mm:ss fff")) + ":" + "\t" + SubCap + "\t" + trace + "\t" + " -" + log;
                //Console.WriteLine(output);
                using (StreamWriter sw = File.AppendText(path + filename))
                {
                    sw.WriteLine(output);
                    sw.Close();
                }
            }
            catch { }
            finally
            {
                _readWriteLock.ExitWriteLock();
            }
        }
        private void MoveToNewLogfile()
        {
            try
            {
                _readWriteLock.EnterWriteLock();
                FileInfo xf = new FileInfo(path + filename);
                xf.MoveTo(path + "KIOSKLog_" + DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss-fff") + ".txt");
                _readWriteLock.ExitWriteLock();
            }
            catch (Exception ex)
            {

            }
        }

    }
}
