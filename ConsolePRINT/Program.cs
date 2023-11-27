using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Threading;
using System.Reflection;

namespace ConsolePRINT
{
    #region "PREFACE"
    /*----------------------PREFACE----------------------------+
    |             Property of Recharge Qatar                   |
    |        CONSOLE PRINT [PRNT management Module]            |
    |                                                          |
    |  Created By: Sreejith           On: 23-04-2019           |
    |  Updated By:                    On:                      |
    +---------------------------------------------------------*/
    #endregion

    class Program
    {
        private static string PrinterArgs;
        public static bool DebugMode = false;//fpr log -2/3
        public static bool ReadConsole = false;
        public static LOG.LogWriter addLog;
        public static string subCap = "PRINT";

        public static Configuration config;
        public static string KioskID;
        public static string KioskType;
        public static string TxnRefNo;//from FE

        private static string LedSlot;
        private const string LEDcommandFile = @"C:\Kiosk\LEDcommand.txt";
        //public static SerialPort tPort = null;
        //public static string LedStartCmd;
        //public static string LedStopCmd;

        static int Main(string[] args)
        {
            handler = new ConsoleEventDelegate(ConsoleEventCallback);
            SetConsoleCtrlHandler(handler, true);

            int InterfaceType = 1;
            string BaseArgument = "";
            Console.WriteLine("PRINT receipt Activity Started.");
            string currVer = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Console.WriteLine("Console Version :" + currVer);

            try//for log-3/3
            {
                Console.WriteLine("Log writing initiating...");
                addLog = new LOG.LogWriter();
                Console.WriteLine("Log writing initiated");
                addLog.WriteLog("<----PRINT----->", subCap, "Log");
                addLog.WriteLog("Print Receipt Process begin.", subCap, "Log");
                addLog.WriteLog("Log writing initiated", subCap, "Log");
            }
            catch { Console.WriteLine("ERROR on Log writing initiation."); return 1; }

            KillOtherProcess();
            //if (CheckSIMProcess())//NOT REQUIRED NOW
            //{
            //    Console.WriteLine("SIM process conflict Print END");
            //    addLog.WriteLog("SIM process conflict Print END", subCap, "ERROR");
            //    return -1;
            //}


            try//Reading app.config file from directory.
            {
                Console.WriteLine("Reading Master app.config...");
                string configPath = ConfigurationManager.AppSettings["ConfigPath"].ToString();
                string configApp = ConfigurationManager.AppSettings["ConfigApp"].ToString();
                config = ConfigurationManager.OpenExeConfiguration(Path.Combine(configPath, configApp));
                string logpath = config.AppSettings.Settings["LogFilePath"].Value;
                KioskID = config.AppSettings.Settings["TerminalID"].Value;
                KioskType = config.AppSettings.Settings["KioskType"].Value;
                LedSlot = ConfigurationManager.AppSettings["LEDslot"].ToString();

                ReadConsole = Convert.ToBoolean(ConfigurationManager.AppSettings["ReadConsole"]);
                DebugMode = Convert.ToBoolean(ConfigurationManager.AppSettings["Debug"]);

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR on reading config :" + ex.Message.ToString());
                addLog.WriteLog("ERROR on reading config :", subCap, "ERROR");
                return -1;
            }
            //string ledSlot = ConfigurationManager.AppSettings["LEDslot"].ToString();
            //LedStartCmd = config.AppSettings.Settings["LEDonCmd"].Value.ToString().Replace("X", ledSlot.Trim());// ConfigurationManager.AppSettings["LEDonCmd"].ToString().Replace("X", "6");
            //LedStopCmd = LedStartCmd.Remove(8, 1).Insert(8, "0"); // ConfigurationManager.AppSettings["LEDoffCmd"].ToString().Replace("X", "6");


            try//Reading app.config file from directory.
            {
                Console.WriteLine("Reading app.config...");
                ReadConsole = Convert.ToBoolean(ConfigurationManager.AppSettings["ReadConsole"]);
                DebugMode = Convert.ToBoolean(ConfigurationManager.AppSettings["Debug"]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR on reading config :" + ex.Message.ToString());
                addLog.WriteLog("ERROR on reading config :", subCap, "ERROR");
            }


            if (args.Length > 0)
            {

                InterfaceType = Convert.ToInt32(args[0]);
                BaseArgument = args[1];
                Console.WriteLine("Selected Txn Type : " + InterfaceType.ToString() + " | Base Argument : " + BaseArgument);
                if (DebugMode) { addLog.WriteLog("Selected Txn Type : " + InterfaceType.ToString() + " | Base Argument : " + BaseArgument, subCap); }

            }
            else
            {
                Console.WriteLine("PRINT Arguments are not available- PRINT process EXIT.");
                if (DebugMode) { addLog.WriteLog("PRINT Arguments are not available- PRINT process EXIT.", subCap); }
                 return 1;//COMMENT FOR TESTING
                //  BaseArgument = "AdminUtility_CDMReceipt,07-Oct-19=13:37,08-Oct-19=13:37,NASIRA@GMAILASDFADSASDF.COM,2255885522,Cash Refill/taken out,3~3000,8~4000,4~200,120~15000,850~3650,1200~12000,0~0,2550,3~3000,8~4000,4~200,120~15000,850~3650,1200~12000,0~0,1250,3~3000,8~4000,4~200,120~15000,850~3650,1200~12000,0~0,1000,3~3000,8~4000,4~200,120~15000,850~3650,1200~12000,0~0,500,3~3000,8~4000,4~200,120~15000,850~3650,1200~12000,0~0,5050,3~3000,8~4000,4~200,120~15000,850~3650,1200~12000,0~0,1000,3~3000,8~4000,4~200,120~15000,850~3650,1200~12000,0~0,2000,110";
                //  InterfaceType = 1;//UNCOMMENT FOR TESTING
                // BaseArgument = "AdminUtility_CDMReceipt,14-Jan-20=18:37,14-Jan-20=18:39,kioskcashintransit@rechargeindia.com,20200114183901,Cash set,44~220,44~440,56~1120,94~4700,167~16700,106~4700,0~0,76180,0~0,0~0,0~0,0~0,0~0,0~0,0~0,0,0~0,0~0,0~0,0~0,0~0,0~0,0~0,0,0~0,0~0,0~0,0~0,0~0,0~0,0~0,0,18~90,18~180,34~680,36~1800,63~6300,40~20000,0~0,29050,0~0,0~0,0~0,0~0,0~0,1~500,0~0,500,62~310,62~620,90~1800,130~6500,230~23000,145~72500,0~0,104730,500";
               // BaseArgument = "OriduErrorTicketReceipt,07-Oct-19=13:37,9621242,TEST,MVR-10,OM2019107113718,OM1001,Cash,888888888";  
              //  BaseArgument = "ErrorTicket_Card,07-Oct-19=13:37,9621242,PREPAID_SIM,MVR-10,OM2019107113718,OM1001,Card,888888888,5241,VISA-DEBIT";             
                //BaseArgument = "OredoPrepaidCreditReceipt,16-Dec-2019=10:48,9609430034,PREPAID_SIM,MVR-30.00,MVR-48.20,MVR-1.80,MVR-80.00,OM2019111104811,Successful,Cash,RQOML002";
                //BaseArgument = "OredoPreapidReceipt,07-Oct-19=13:37,9621242,TEST-TEST,MVR-10,MVR-3.5,MVR-1.1,MVR-15.00,OM2019107113718,Successful,Cash,OM1001";
               // BaseArgument = "Oredo_PreapidCard,07-Oct-19=13:37,9621242,PrepaidSIM,MVR-30,MVR-1.8,MVR-31.80,OM2019107113718,Successful,Card,OM1001,98765,VISA-DEBIT";
                // BaseArgument = "Admin_CashAcceptorReceiptType3,07-Oct-19=13:37,08-Oct-19=13:37,abc@ooredoo.mv,2222222,3~3000,8~4000,4~200,120~15000,850~3650,1200~12000,580~4568,1250,4~3050,7~6000,4~250,110~1500,870~3890,120~1200,580~468,1250,78520";
                // BaseArgument = "OredoReceipt,07-Oct-19=13:37,9621242,TEST-TEST,MVR-10,OM2019107113718,Successful,Cash,OM1001";
                //BaseArgument = "OredoBill_Mfaisaa,07-Oct-19=13:37,9621242,TEST-TEST,MVR-10,OM2019107113718,Successful,Cash,OM1001"; 
                // BaseArgument = "Oredo_CardforRBF,07-Oct-19=13:37,9621242,TEST-TEST,MVR-10,OM2019107113718,Successful,Card,OM1001,462555,VISA-DEBIT";
                // BaseArgument = "OreduRechargeReceipt,07-Oct-19=13:37,9621242,TEST-TEST,MVR-10,MVR-3.5,MVR-1.1,OM2019107113718,Successful,Cash,OM1001";
                // BaseArgument = "OredoReceipt,07-Oct-19=13:37,9621242,TouristSIM,MVR-10,OM2019107113718,OM1001,Cash,888888888";
                // BaseArgument = "Ordeo_TouristCard,07-Oct-19=13:37,9621242,TouristSIM,MVR-10,OM2019107113718,OM1001,Card,OM1001,45211,VISA-DEBIT";
               //   BaseArgument = "OreduRechargeCard,07-Oct-19=13:37,9621242,TEST-TEST,MVR-10,MVR-3.5,MVR-1.1,OM2019107113718,Successful,Card,OM1001,45211,VISA-DEBIT";
                //BaseArgument = "Admin_CashAcceptorReceiptType2,07-Oct-19=13:37,08-Oct-19=13:37,abc@ooredoo.mv,2222222,3~3000,8~4000,4~200,120~15000,850~3650,1200~12000,580~4568,1250";
            }

            switch (InterfaceType)
            {
                case 1://START PRINT
                    Console.WriteLine("Initiating Print Receipt");
                    addLog.WriteLog("Initiating Print Receipt", subCap, "Log");
                    PrinterArgs = BaseArgument;
                    if (PrinterArgs.Length < 5) { Console.WriteLine("Print Arguments are not proper- PRINT exit."); if (ReadConsole) { Console.Read(); } return 1; }
                    //PrinterArgs = "OredoReceipt,1234567,Bill Payment,20,OM201932612418,Successful,Cash,123456";//dummy params
                    int outPReslt = AsyncDoPrint().GetAwaiter().GetResult();
                    Console.WriteLine("END Print Receipt");
                    addLog.WriteLog("END Print Receipt", subCap, "Log");

                    CheckPrinterPaperStatus();
                    if (ReadConsole) { Console.Read(); }
                    return 101;
                case 2://CANCEL PRINT

                case 3://PRINTER STATUS
                    Console.WriteLine("Initiating Printer Status");
                    addLog.WriteLog("Initiating Printer Status", subCap, "Log");
                    PrinterArgs = BaseArgument;
                    //PrinterArgs = "OredoReceipt,1234567,Bill Payment,20,OM201932612418,Successful,Cash,123456";//dummy params
                    int outStatReslt = AsyncGetStatus().GetAwaiter().GetResult();
                    Console.WriteLine("END Printeter Status");
                    addLog.WriteLog("END Printeter Status", subCap, "Log");
                    if (ReadConsole) { Console.Read(); }
                    return 103;
                default:
                    Console.WriteLine("Default case");
                    //string outResltx = AsyncCashAcceptor().GetAwaiter().GetResult();
                    //return 100;

                    break;
            }

            return 0;
        }

        private static async Task<int> AsyncDoPrint()
        {
            try
            {
                ConsolePRINT.classes.ReceiptPrinter ReceiptPrint = new classes.ReceiptPrinter();
                string output = ReceiptPrint.PrintReceipt(PrinterArgs);
                if (output.ToLower() == "success")
                {
                    //LED start--
                    string ledStartCmd = "STARTLED " + LedSlot + " 10";
                    try { File.WriteAllText(LEDcommandFile, ledStartCmd); } catch { }
                }
            }
            catch (Exception ex)
            {
                return 119;
            }

            await Task.Delay(1000);
            return 108;
        }

        private static async Task<int> AsyncGetStatus()
        {
            try
            {
                ConsolePRINT.classes.ReceiptPrinter ReceiptPrint = new classes.ReceiptPrinter();
                string output = ReceiptPrint.GetPrinterStatus();

            }
            catch (Exception ex)
            {
                return 119;
            }

            await Task.Delay(1000);
            return 108;
        }
        private static bool CheckPrinterPaperStatus()
        {
            string iType = "Paper";
            try
            {
                Console.WriteLine(iType + " status Check initiated...<external console>.");
                using (Process myProcess = new Process())
                {
                    myProcess.StartInfo.UseShellExecute = false;
                    myProcess.StartInfo.FileName = @"C:\Kiosk\Library\ConsolePaperStat.exe";
                    //myProcess.StartInfo.Arguments = "     ";
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.Start();
                }
                Console.WriteLine(iType + " status Check Completed.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception on " + iType + " status Check." + ex.Message);
                return false;
            }
        }
        //public static SerialPort LEDport()
        //{
        //    try
        //    {
        //        string configPath = ConfigurationManager.AppSettings["ConfigPath"].ToString();
        //        Configuration config = ConfigurationManager.OpenExeConfiguration(Path.Combine(configPath, "ConsoleHeartBeat.exe"));
        //        // string logpath = config.AppSettings.Settings["TerminalID"].Value;

        //        SerialPort LedPort;
        //        int baudRate = Convert.ToInt32(config.AppSettings.Settings["BaundRate"].Value);
        //        int DataBits = Convert.ToInt32(config.AppSettings.Settings["DataBits"].Value);
        //        string Comport = config.AppSettings.Settings["COMPort"].Value;
        //        StopBits stopBits;
        //        Enum.TryParse(config.AppSettings.Settings["StopBits"].Value, out stopBits);
        //        Parity parity;
        //        Enum.TryParse(config.AppSettings.Settings["Parity"].Value, out parity);
        //        LedPort = new SerialPort(Comport, baudRate, parity, DataBits, stopBits);
        //        LedPort.Open();
        //        return LedPort;
        //        // LedPort.Write(LEDCommand);
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //private static void LedTimer()
        //{
        //    try
        //    {
        //        tPort = LEDport();
        //        if (tPort != null)
        //        {
        //            tPort.Write(LedStartCmd);
        //        }
        //        int duration = Convert.ToInt32(ConfigurationManager.AppSettings["LEDduration"]);
        //        Thread.Sleep(duration * 1000);
        //        //LED end--
        //        tPort.Write(LedStopCmd);
        //        tPort.Close();

        //    }
        //    catch { }
        //}
        private static bool KillOtherProcess()
        {
            try
            {
                Console.WriteLine("Kill process intitiated");
                if (DebugMode) { addLog.WriteLog("Kill process intitiated", subCap); }
                int nProcessID = Process.GetCurrentProcess().Id;
                Console.WriteLine(nProcessID.ToString());
                string procName = "ConsolePRINT";
                foreach (var process in Process.GetProcessesByName(procName))
                {
                    if (nProcessID != process.Id)
                    {
                        //Console.WriteLine(process.Id);
                        process.Kill();
                    }
                    else
                    {
                        // Console.WriteLine("This is current process");
                    }
                }
                Console.WriteLine("END process kill");
                if (DebugMode) { addLog.WriteLog("END process kill", subCap); }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR on process kill");
                if (DebugMode) { addLog.WriteLog("ERROR on process kill", subCap, "ERROR"); }
                return false;
            }

        }
        private static bool CheckSIMProcess()//TEMP-tackling compatibility issue
        {
            try
            {
                Console.WriteLine("SIM process status check intitiated");
                if (DebugMode) { addLog.WriteLog("SIM process status check intitiated", subCap); }
                string procName = "ConsoleSIM";

                int checkTimeout = 60;
                int i = 0;
                bool procFlag = false;
                while (i < checkTimeout)//-----------------LOOP start------checking SIM process running or not
                {
                    foreach (var process in Process.GetProcessesByName(procName))
                    {
                        if (procName == process.ProcessName)
                        {
                            procFlag = true;
                        }
                        else
                        {
                            procFlag = false;
                        }
                    }
                    if (procFlag == false)
                    {
                        Console.WriteLine("SIM process not running.");
                        addLog.WriteLog("SIM process not running.", subCap);
                        return false;
                    }
                    procFlag = false;
                    Thread.Sleep(1000);
                    i++;
                    Console.WriteLine("-" + i.ToString() + "-");
                }
                Console.WriteLine("SIM process STILL running. Exiting PRIINT...");
                addLog.WriteLog("SIM process STILL running. Exiting PRIINT...", subCap);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR SIM process check.");
                addLog.WriteLog("ERROR SIM process check.", subCap, "ERROR");
                return true;
            }

        }

        //------------------console application events-----------------------------
        static ConsoleEventDelegate handler;
        private delegate bool ConsoleEventDelegate(int eventType);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);
        static bool ConsoleEventCallback(int eventType)
        {
            try
            {
                string ledStopCmd = "STOPLED " + LedSlot;
                try { File.WriteAllText(LEDcommandFile, ledStopCmd); } catch { }
                Console.WriteLine("PRINT console exit.");
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
