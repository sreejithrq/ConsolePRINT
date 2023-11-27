using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Custom.PowerToolApi;
using System.Configuration;

namespace ConsolePRINT.classes
{
    class ReceiptPrinter
    {
        public string PrintReceipt(string pritArgs)
        {
            //GeneralLog.AddLog(pritArgs);
            try
            {
                string[] baseArg = pritArgs.Split(',');
                string type = baseArg[0];
                // GeneralLog.AddLog("TEMP TEST" + type);
                Console.WriteLine("Printing process started.");
                if (Program.DebugMode) { Program.addLog.WriteLog("Printing process started.", Program.subCap); }
                switch (type)
                {
                    case "OredoReceipt":
                        Print_OredoReceipt(pritArgs);
                        break;
                    case "OriduErrorTicketReceipt":
                        Print_OredoErrorTicket(pritArgs);
                        break;
                    case "OredoPrepaidCreditReceipt":
                        Print_OredoPrepaidCreditReceipt(pritArgs);
                        break;
                    case "OredoBill_Mfaisaa":
                        Print_mFaisa(pritArgs);
                        break;
                    case "OreduRechargeReceipt":
                        Print_OreduRechargeReceipt(pritArgs);
                        break;
                    case "OredoPreapidReceipt":
                        Print_OredoPreapidReceipt(pritArgs);
                        break;
                    case "Admin_CashAcceptorReceiptType2":
                        Print_Admin_CashAcceptorReceiptType2(pritArgs);
                        break;
                    case "AdminUtility_CDMReceipt":
                        Print_AdminUtility_CDMReceipt(pritArgs);
                        break;
                    case "Admin_CashAcceptorReceiptType3":
                        Print_Admin_CashAcceptorReceiptType3(pritArgs);
                        break;
                    case "OreduRechargeCard":
                        Print_OreduRechargeCard(pritArgs);
                        break;
                    case "Oredo_CardforRBF":
                        Print_Oredo_CardforRBF(pritArgs);
                        break;
                    case "ErrorTicket_Card":
                        Print_ErrorTicket_Card(pritArgs);
                        break;
                    case "Oredo_PreapidCard":
                        Print_Oredo_PreapidCard(pritArgs);
                        break;
                    case "Ordeo_TouristCard":
                        Print_Ordeo_TouristCard(pritArgs);
                        break;
                    default:
                        break;

                }




                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //Prepaid SIM purchase using Cash
        private bool Print_OredoPrepaidCreditReceipt(string args)//receipt type-1
        {
            try
            {
                Console.WriteLine("Selected Receipt- OredoReceipt...");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt- OredoReceipt...", Program.subCap); }              
                string[] baseArg = args.Split(',');
                if (args.Length < 1) { return false; }
                CustomXmlPrintResult xmlPrintresult;
                CustomXmlPrinter xprinter = new CustomXmlPrinter();
                string ReceiptPrinterName = ConfigurationManager.AppSettings["PrinterName"];// "CUSTOM VKP80 II";//taken from config file
                string filename = @"C:\Kiosk\PrintTemplates\OredoPrepaidCreditReceipt\OredoPrepaidCreditReceipt.xml";

                xmlPrintresult = xprinter.SetPrinterName(ReceiptPrinterName);
                xmlPrintresult = xprinter.SetXmlFileName(filename);
                string dString = String.Format("{0:dd-MM-yy HH:mm}", DateTime.Now);
                xmlPrintresult = xprinter.SetXmlObjectTag("TextDateTime.text", baseArg[1].ToString().Replace("=", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextMobile.text", baseArg[2].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTxnType.text", baseArg[3].ToString().Replace("_", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextSIMCharge.text", baseArg[4].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextCredit.text", baseArg[5].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextGST.text", baseArg[6].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextAmount.text", baseArg[7].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextRefNo.text", baseArg[8].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTxnStatus.text", baseArg[9].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextPayType.text", baseArg[10].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextVia.text", baseArg[11].ToString());
                string parseInfo = "";
                xmlPrintresult = xprinter.GetXmlCheckCorrectParsing(out parseInfo);
                xmlPrintresult = xprinter.PrintXml(true, false);
                xprinter.Dispose();
                Console.WriteLine("Selected Receipt[credited] printing status OK");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt[credited] printing status OK", Program.subCap); }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Selected Receipt printing status ERROR: " + ex.ToString());
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt printing status ERROR: " + ex.ToString(), Program.subCap); }
                return false;

            }
        }

        //Prepaid SIM purchase using mfaisaa
        private bool Print_OredoPreapidReceipt(string args)
        {
            try
            {
                Console.WriteLine("Selected Receipt- OredoReceipt...");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt- OredoPreapidReceipt...", Program.subCap); }
                string[] baseArg = args.Split(',');
                if (args.Length < 1) { return false; }


                CustomXmlPrintResult xmlPrintresult;
                CustomXmlPrinter xprinter = new CustomXmlPrinter();
                string ReceiptPrinterName = ConfigurationManager.AppSettings["PrinterName"];// "CUSTOM VKP80 II";//taken from config file
                string filename = @"C:\Kiosk\PrintTemplates\OredoPreapidReceipt\OredoPreapidReceipt.xml";
                xmlPrintresult = xprinter.SetPrinterName(ReceiptPrinterName);
                xmlPrintresult = xprinter.SetXmlFileName(filename);
                string dString = String.Format("{0:dd-MM-yy HH:mm}", DateTime.Now);        
                xmlPrintresult = xprinter.SetXmlObjectTag("TextDateTime.text", baseArg[1].ToString().Replace("=", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextMobile.text", baseArg[2].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTxnType.text", baseArg[3].ToString().Replace("_", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextSIMCharge.text", baseArg[4].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextGST.text", baseArg[5].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextAmount.text", baseArg[6].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextRefNo.text", baseArg[7].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTxnStatus.text", baseArg[8].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextPayType.text", baseArg[9].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextVia.text", baseArg[10].ToString());

                string parseInfo = "";
                xmlPrintresult = xprinter.GetXmlCheckCorrectParsing(out parseInfo);
                xmlPrintresult = xprinter.PrintXml(true, false);
                xprinter.Dispose();
                Console.WriteLine("Selected Receipt[credited] printing status OK");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt[credited] printing status OK", Program.subCap); }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Selected Receipt printing status ERROR: " + ex.ToString());
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt printing status ERROR: " + ex.ToString(), Program.subCap); }
                return false;

            }
        }
       
        //Prepaid SIM purchase using Card
        private bool Print_Oredo_PreapidCard(string args)
        {
            try
            {
                Console.WriteLine("Selected Receipt- Oredo_PreapidCard...");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt- Oredo_PreapidCard...", Program.subCap); }              
                string[] baseArg = args.Split(',');
                if (args.Length < 1) { return false; }

                CustomXmlPrintResult xmlPrintresult;
                CustomXmlPrinter xprinter = new CustomXmlPrinter();
                string ReceiptPrinterName = ConfigurationManager.AppSettings["PrinterName"];// "CUSTOM VKP80 II";//taken from config file
                string filename = @"C:\Kiosk\PrintTemplates\Oredo_PreapidCard\Oredo_PreapidCard.xml";
                xmlPrintresult = xprinter.SetPrinterName(ReceiptPrinterName);
                xmlPrintresult = xprinter.SetXmlFileName(filename);
                string dString = String.Format("{0:dd-MM-yy HH:mm}", DateTime.Now);             
                xmlPrintresult = xprinter.SetXmlObjectTag("TextDateTime.text", baseArg[1].ToString().Replace("=", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextMobile.text", baseArg[2].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTxnType.text", baseArg[3].ToString().Replace("_", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextSIMCharge.text", baseArg[4].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextGST.text", baseArg[5].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextAmount.text", baseArg[6].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextRefNo.text", baseArg[7].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTxnStatus.text", baseArg[8].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextPayType.text", baseArg[9].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextVia.text", baseArg[10].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextBankRefNo.text", baseArg[11].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextCardScheme.text", baseArg[12].ToString().Replace("-", " "));
                string parseInfo = "";
                xmlPrintresult = xprinter.GetXmlCheckCorrectParsing(out parseInfo);
                xmlPrintresult = xprinter.PrintXml(true, false);
                xprinter.Dispose();
                Console.WriteLine("Selected Receipt[credited] printing status OK");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt[credited] printing status OK", Program.subCap); }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Selected Receipt printing status ERROR: " + ex.ToString());
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt printing status ERROR: " + ex.ToString(), Program.subCap); }
                return false;

            }
        }

        //Recharge using cash and m-Faisaa
        private bool Print_OreduRechargeReceipt(string args)
        {
            try
            {
                Console.WriteLine("Selected Receipt- OreduRechargeReceipt...");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt- OreduRechargeReceipt...", Program.subCap); }
                string[] baseArg = args.Split(',');
                if (args.Length < 1) { return false; }

                CustomXmlPrintResult xmlPrintresult;
                CustomXmlPrinter xprinter = new CustomXmlPrinter();
                string ReceiptPrinterName = ConfigurationManager.AppSettings["PrinterName"];// "CUSTOM VKP80 II";//taken from config file
                string filename = @"C:\Kiosk\PrintTemplates\OreduRechargeReceipt\OreduRechargeReceipt.xml";
                xmlPrintresult = xprinter.SetPrinterName(ReceiptPrinterName);
                xmlPrintresult = xprinter.SetXmlFileName(filename);
                string dString = String.Format("{0:dd-MM-yy HH:mm}", DateTime.Now);           
                xmlPrintresult = xprinter.SetXmlObjectTag("TextDateTime.text", baseArg[1].ToString().Replace("=", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextMobile.text", baseArg[2].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTxnType.text", baseArg[3].ToString().Replace("_", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextRecharge.text", baseArg[4].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextGST.text", baseArg[5].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextAmount.text", baseArg[6].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextRefNo.text", baseArg[7].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTxnStatus.text", baseArg[8].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextPayType.text", baseArg[9].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextVia.text", baseArg[10].ToString());
                string parseInfo = "";
                xmlPrintresult = xprinter.GetXmlCheckCorrectParsing(out parseInfo);
                xmlPrintresult = xprinter.PrintXml(true, false);
                xprinter.Dispose();
                Console.WriteLine("Selected Receipt[credited] printing status OK");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt[credited] printing status OK", Program.subCap); }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Selected Receipt printing status ERROR: " + ex.ToString());
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt printing status ERROR: " + ex.ToString(), Program.subCap); }
                return false;

            }
        }


        //Recharge using card
        private bool Print_OreduRechargeCard(string args)
        {
            try
            {
                Console.WriteLine("Selected Receipt- OreduRechargeCard...");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt- OreduRechargeCard...", Program.subCap); }
                //GeneralLog.AddLog("Reached -2");
                string[] baseArg = args.Split(',');
                if (args.Length < 1) { return false; }


                CustomXmlPrintResult xmlPrintresult;
                CustomXmlPrinter xprinter = new CustomXmlPrinter();
                string ReceiptPrinterName = ConfigurationManager.AppSettings["PrinterName"];// "CUSTOM VKP80 II";//taken from config file
                string filename = @"C:\Kiosk\PrintTemplates\OreduRechargeCard\OreduRechargeCard.xml";

                xmlPrintresult = xprinter.SetPrinterName(ReceiptPrinterName);
                xmlPrintresult = xprinter.SetXmlFileName(filename);

                string dString = String.Format("{0:dd-MM-yy HH:mm}", DateTime.Now);
                //string DateString = DateTime.Now.ToShortDateString();
                //string TimeString = DateTime.Now.ToShortTimeString();

                string textout;
                // xmlPrintresult = xprinter.GetXmlObjectTag("TextDateTime.text", out textout);
                xmlPrintresult = xprinter.SetXmlObjectTag("TextDateTime.text", baseArg[1].ToString().Replace("=", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextMobile.text", baseArg[2].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTxnType.text", baseArg[3].ToString().Replace("_", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextRecharge.text", baseArg[4].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextGST.text", baseArg[5].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextAmount.text", baseArg[6].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextRefNo.text", baseArg[7].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTxnStatus.text", baseArg[8].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextPayType.text", baseArg[9].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextVia.text", baseArg[10].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextBankRefNo.text", baseArg[11].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextCardScheme.text", baseArg[12].ToString().Replace("-"," "));
                string parseInfo = "";
                xmlPrintresult = xprinter.GetXmlCheckCorrectParsing(out parseInfo);
                xmlPrintresult = xprinter.PrintXml(true, false);
                xprinter.Dispose();
                Console.WriteLine("Selected Receipt[credited] printing status OK");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt[credited] printing status OK", Program.subCap); }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Selected Receipt printing status ERROR: " + ex.ToString());
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt printing status ERROR: " + ex.ToString(), Program.subCap); }
                return false;

            }
        }

        //Tourist SIM using Cash and mfaisaa
        private bool Print_OredoReceipt(string args)//receipt type-1
        {
            try
            {
                Console.WriteLine("Selected Receipt- OredoReceipt...");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt- OredoReceipt...", Program.subCap); }
                string[] baseArg = args.Split(',');
                if (args.Length < 1) { return false; }

                CustomXmlPrintResult xmlPrintresult;
                CustomXmlPrinter xprinter = new CustomXmlPrinter();
                string ReceiptPrinterName = ConfigurationManager.AppSettings["PrinterName"];// "CUSTOM VKP80 II";//taken from config file
                string filename = @"C:\Kiosk\PrintTemplates\OoredoReceipt\OriduReceipt.xml";
                xmlPrintresult = xprinter.SetPrinterName(ReceiptPrinterName);
                xmlPrintresult = xprinter.SetXmlFileName(filename);
                string dString = String.Format("{0:dd-MM-yy HH:mm}", DateTime.Now);
                xmlPrintresult = xprinter.SetXmlObjectTag("TextDateTime.text", baseArg[1].ToString().Replace("=", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextMobile.text", baseArg[2].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTxnType.text", baseArg[3].ToString().Replace("_", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextAmount.text", baseArg[4].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextRefNo.text", baseArg[5].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTxnStatus.text", baseArg[6].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextPayType.text", baseArg[7].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextVia.text", baseArg[8].ToString());
                string parseInfo = "";
                xmlPrintresult = xprinter.GetXmlCheckCorrectParsing(out parseInfo);
                xmlPrintresult = xprinter.PrintXml(true, false);
                xprinter.Dispose();
                Console.WriteLine("Selected Receipt printing status OK");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt printing status OK", Program.subCap); }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Selected Receipt printing status ERROR: " + ex.ToString());
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt printing status ERROR: " + ex.ToString(), Program.subCap); }
                return false;

            }
        }

        //Tourist SIM using Card
        private bool Print_Ordeo_TouristCard(string args)
        {
            try
            {
                Console.WriteLine("Selected Receipt- Ordeo_TouristCard...");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt- Ordeo_TouristCard...", Program.subCap); }
                string[] baseArg = args.Split(',');
                if (args.Length < 1) { return false; }

                CustomXmlPrintResult xmlPrintresult;
                CustomXmlPrinter xprinter = new CustomXmlPrinter();
                string ReceiptPrinterName = ConfigurationManager.AppSettings["PrinterName"];// "CUSTOM VKP80 II";//taken from config file
                string filename = @"C:\Kiosk\PrintTemplates\Ordeo_TouristCard\Ordeo_TouristCard.xml";
                xmlPrintresult = xprinter.SetPrinterName(ReceiptPrinterName);
                xmlPrintresult = xprinter.SetXmlFileName(filename);
                string dString = String.Format("{0:dd-MM-yy HH:mm}", DateTime.Now);
                xmlPrintresult = xprinter.SetXmlObjectTag("TextDateTime.text", baseArg[1].ToString().Replace("=", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextMobile.text", baseArg[2].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTxnType.text", baseArg[3].ToString().Replace("_", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextAmount.text", baseArg[4].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextRefNo.text", baseArg[5].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTxnStatus.text", baseArg[6].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextPayType.text", baseArg[7].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextVia.text", baseArg[8].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextBankRefNo.text", baseArg[9].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextCardScheme.text", baseArg[10].ToString().Replace("-", " "));
                string parseInfo = "";
                xmlPrintresult = xprinter.GetXmlCheckCorrectParsing(out parseInfo);
                xmlPrintresult = xprinter.PrintXml(true, false);
                xprinter.Dispose();
                Console.WriteLine("Selected Receipt printing status OK");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt printing status OK", Program.subCap); }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Selected Receipt printing status ERROR: " + ex.ToString());
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt printing status ERROR: " + ex.ToString(), Program.subCap); }
                return false;

            }
        }

        //BillPayment/ m-Faisaa Cash in using Cash /m-faisaa
        private bool Print_mFaisa(string args)//receipt type-1
        {
            try
            {
                Console.WriteLine("Selected Receipt- OredoBill_Mfaisaa...");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt- OredoBill_Mfaisaa...", Program.subCap); }
                string[] baseArg = args.Split(',');
                if (args.Length < 1) { return false; }

                CustomXmlPrintResult xmlPrintresult;
                CustomXmlPrinter xprinter = new CustomXmlPrinter();
                string ReceiptPrinterName = ConfigurationManager.AppSettings["PrinterName"];// "CUSTOM VKP80 II";//taken from config file
                string filename = @"C:\Kiosk\PrintTemplates\OredoBill_Mfaisaa\OredoBill_Mfaisaa.xml";
                xmlPrintresult = xprinter.SetPrinterName(ReceiptPrinterName);
                xmlPrintresult = xprinter.SetXmlFileName(filename);
                string dString = String.Format("{0:dd-MM-yy HH:mm}", DateTime.Now);
                xmlPrintresult = xprinter.SetXmlObjectTag("TextDateTime.text", baseArg[1].ToString().Replace("=", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextMobile.text", baseArg[2].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTxnType.text", baseArg[3].ToString().Replace("_", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextAmount.text", baseArg[4].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextRefNo.text", baseArg[5].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTxnStatus.text", baseArg[6].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextPayType.text", baseArg[7].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextVia.text", baseArg[8].ToString());

                string parseInfo = "";
                xmlPrintresult = xprinter.GetXmlCheckCorrectParsing(out parseInfo);
                xmlPrintresult = xprinter.PrintXml(true, false);
                xprinter.Dispose();
                Console.WriteLine("Selected Receipt printing status OK");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt printing status OK", Program.subCap); }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Selected Receipt printing status ERROR: " + ex.ToString());
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt printing status ERROR: " + ex.ToString(), Program.subCap); }
                return false;

            }
        }

        //BillPayment/m-Faisaa Cash in using Card
        public bool Print_Oredo_CardforRBF(string args)
        {
            try
            {
                Console.WriteLine("Selected Receipt- Oredo_CardforRBF...");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt- Oredo_CardforRBF...", Program.subCap); }
                string[] baseArg = args.Split(',');
                if (args.Length < 1) { return false; }

                CustomXmlPrintResult xmlPrintresult;
                CustomXmlPrinter xprinter = new CustomXmlPrinter();
                string ReceiptPrinterName = ConfigurationManager.AppSettings["PrinterName"];// "CUSTOM VKP80 II";//taken from config file
                string filename = @"C:\Kiosk\PrintTemplates\Oredo_CardforRBF\Oredo_CardforRBF.xml";
                xmlPrintresult = xprinter.SetPrinterName(ReceiptPrinterName);
                xmlPrintresult = xprinter.SetXmlFileName(filename);
                string dString = String.Format("{0:dd-MM-yy HH:mm}", DateTime.Now);
                xmlPrintresult = xprinter.SetXmlObjectTag("TextDateTime.text", baseArg[1].ToString().Replace("=", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextMobile.text", baseArg[2].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTxnType.text", baseArg[3].ToString().Replace("_", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextAmount.text", baseArg[4].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextRefNo.text", baseArg[5].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTxnStatus.text", baseArg[6].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextPayType.text", baseArg[7].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextVia.text", baseArg[8].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextBankRefNo.text", baseArg[9].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextCardScheme.text", baseArg[10].ToString().Replace("-", " "));
                string parseInfo = "";
                xmlPrintresult = xprinter.GetXmlCheckCorrectParsing(out parseInfo);
                xmlPrintresult = xprinter.PrintXml(true, false);
                xprinter.Dispose();
                Console.WriteLine("Selected Receipt printing status OK");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt printing status OK", Program.subCap); }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Selected Receipt printing status ERROR: " + ex.ToString());
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt printing status ERROR: " + ex.ToString(), Program.subCap); }
                return false;

            }

        }

        //Support Ticket for payment type Cash/m-Faisaa
        private bool Print_OredoErrorTicket(string args)//receipt type-1
        {
            try
            {
                args = args.Replace(" ", "");
                Console.WriteLine("Selected Receipt- OredoErrorTicket...");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt- OredoErrorTicket...", Program.subCap); }
                string[] baseArg = args.Split(',');
                if (args.Length < 1) { return false; }

                CustomXmlPrintResult xmlPrintresult;
                CustomXmlPrinter xprinter = new CustomXmlPrinter();
                string ReceiptPrinterName = ConfigurationManager.AppSettings["PrinterName"].ToString();// "CUSTOM VKP80 II";//taken from config file
                string filename = @"C:\Kiosk\PrintTemplates\OriduErrorTicketReceipt\OriduErrorTicketReceipt.xml";
                xmlPrintresult = xprinter.SetPrinterName(ReceiptPrinterName);
                xmlPrintresult = xprinter.SetXmlFileName(filename);
                string dString = String.Format("{0:dd-MM-yy HH:mm}", DateTime.Now);
                xmlPrintresult = xprinter.SetXmlObjectTag("TextDateTime.text", baseArg[1].ToString().Replace("=", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextMobile.text", baseArg[2].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTxnType.text", baseArg[3].ToString().Replace("_", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextAmount.text", baseArg[4].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextRefNo.text", baseArg[5].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTerminal.text", baseArg[6].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextPayType.text", baseArg[7].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextCase.text", baseArg[8].ToString());
                string parseInfo = "";
                xmlPrintresult = xprinter.GetXmlCheckCorrectParsing(out parseInfo);
                xmlPrintresult = xprinter.PrintXml(true, false);
                xprinter.Dispose();
                Console.WriteLine("Selected Receipt printing status OK");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt printing status OK", Program.subCap); }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Selected Receipt printing status ERROR: " + ex.ToString());
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt printing status ERROR: " + ex.ToString(), Program.subCap); }
                return false;

            }
        }

        //Support Ticket for payment type Card
        public bool Print_ErrorTicket_Card(string args)
        {
            try
            {
                args = args.Replace(" ", "");
                Console.WriteLine("Selected Receipt- ErrorTicket_Card...");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt- ErrorTicket_Card...", Program.subCap); }
                string[] baseArg = args.Split(',');
                if (args.Length < 1) { return false; }

                CustomXmlPrintResult xmlPrintresult;
                CustomXmlPrinter xprinter = new CustomXmlPrinter();
                string ReceiptPrinterName = ConfigurationManager.AppSettings["PrinterName"].ToString();// "CUSTOM VKP80 II";//taken from config file
                string filename = @"C:\Kiosk\PrintTemplates\ErrorTicket_Card\ErrorTicket_Card.xml";
                xmlPrintresult = xprinter.SetPrinterName(ReceiptPrinterName);
                xmlPrintresult = xprinter.SetXmlFileName(filename);
                string dString = String.Format("{0:dd-MM-yy HH:mm}", DateTime.Now);
                xmlPrintresult = xprinter.SetXmlObjectTag("TextDateTime.text", baseArg[1].ToString().Replace("=", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextMobile.text", baseArg[2].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTxnType.text", baseArg[3].ToString().Replace("_", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextAmount.text", baseArg[4].ToString().Replace("-", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextRefNo.text", baseArg[5].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTerminal.text", baseArg[6].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextPayType.text", baseArg[7].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextCase.text", baseArg[8].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextBankRefNo.text", baseArg[9].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextCardScheme.text", baseArg[10].ToString().Replace("-", " "));
                string parseInfo = "";
                xmlPrintresult = xprinter.GetXmlCheckCorrectParsing(out parseInfo);
                xmlPrintresult = xprinter.PrintXml(true, false);
                xprinter.Dispose();
                Console.WriteLine("Selected Receipt printing status OK");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt printing status OK", Program.subCap); }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Selected Receipt printing status ERROR: " + ex.ToString());
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt printing status ERROR: " + ex.ToString(), Program.subCap); }
                return false;

            }
        }

        //Type 2 Cash acceptor reciept
        private bool Print_Admin_CashAcceptorReceiptType2(string args)//receipt type-1
        {
            try
            {
                Console.WriteLine("Selected Receipt- Admin_CashAcceptorReceiptType2...");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt- Admin_CashAcceptorReceiptType2...", Program.subCap); }
                string[] baseArg = args.Split(',');
                if (args.Length < 1) { return false; }


                CustomXmlPrintResult xmlPrintresult;
                CustomXmlPrinter xprinter = new CustomXmlPrinter();
                string ReceiptPrinterName = ConfigurationManager.AppSettings["PrinterName"].ToString();// "CUSTOM VKP80 II";//taken from config file
                string filename = @"C:\Kiosk\PrintTemplates\Admin_CashAcceptorReceiptType2\Admin_CashAcceptorReceiptType2.xml";

                xmlPrintresult = xprinter.SetPrinterName(ReceiptPrinterName);
                xmlPrintresult = xprinter.SetXmlFileName(filename);


                xmlPrintresult = xprinter.SetXmlObjectTag("TextFromDate.text", baseArg[1].ToString().Replace("=", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextToDate.text", baseArg[2].ToString().Replace("=", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextUser.text", baseArg[3].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextBatchNo.text", baseArg[4].ToString());

                //MVR
                string[] SubArg = baseArg[5].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text5CntMVR.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text5MVRTotal.text", SubArg[1].ToString());
                SubArg = baseArg[6].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text10CntMVR.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text10MVRTotal.text", SubArg[1].ToString());
                SubArg = baseArg[7].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text20CntMVR.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text20MVRTotal.text", SubArg[1].ToString());
                SubArg = baseArg[8].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text50CntMVR.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text50MVRTotal.text", SubArg[1].ToString());
                SubArg = baseArg[9].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text100CntMVR.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text100MVRTotal.text", SubArg[1].ToString());
                SubArg = baseArg[10].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text500CntMVR.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text500MVRTotal.text", SubArg[1].ToString());
                SubArg = baseArg[11].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text1000CntMVR.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text1000MVRTotal.text", SubArg[1].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTotalMVR.text", baseArg[12].ToString());



                string parseInfo = "";
                xmlPrintresult = xprinter.GetXmlCheckCorrectParsing(out parseInfo);
                xmlPrintresult = xprinter.PrintXml(true, false);
                xprinter.Dispose();
                Console.WriteLine("Selected Receipt printing status OK");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt printing status OK", Program.subCap); }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Selected Receipt printing status ERROR: " + ex.ToString());
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt printing status ERROR: " + ex.ToString(), Program.subCap); }
                return false;

            }
        }

        //Type 3 Cash acceptor reciept
        private bool Print_Admin_CashAcceptorReceiptType3(string args)//receipt type-1
        {
            try
            {
                Console.WriteLine("Selected Receipt- Admin_CashAcceptorReceiptType3...");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt- Admin_CashAcceptorReceiptType3...", Program.subCap); }
                string[] baseArg = args.Split(',');
                if (args.Length < 1) { return false; }


                CustomXmlPrintResult xmlPrintresult;
                CustomXmlPrinter xprinter = new CustomXmlPrinter();
                string ReceiptPrinterName = ConfigurationManager.AppSettings["PrinterName"].ToString();// "CUSTOM VKP80 II";//taken from config file
                string filename = @"C:\Kiosk\PrintTemplates\Admin_CashAcceptorReceiptType3\Admin_CashAcceptorReceiptType3.xml";

                xmlPrintresult = xprinter.SetPrinterName(ReceiptPrinterName);
                xmlPrintresult = xprinter.SetXmlFileName(filename);

              
                xmlPrintresult = xprinter.SetXmlObjectTag("TextFromDate.text", baseArg[1].ToString().Replace("=", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextToDate.text", baseArg[2].ToString().Replace("=", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextUser.text", baseArg[3].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextBatchNo.text", baseArg[4].ToString());

                //MVR
                string[] SubArg = baseArg[5].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text5CntMVR.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text5MVRTotal.text", SubArg[1].ToString());
                SubArg = baseArg[6].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text10CntMVR.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text10MVRTotal.text", SubArg[1].ToString());
                SubArg = baseArg[7].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text20CntMVR.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text20MVRTotal.text", SubArg[1].ToString());
                SubArg = baseArg[8].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text50CntMVR.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text50MVRTotal.text", SubArg[1].ToString());
                SubArg = baseArg[9].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text100CntMVR.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text100MVRTotal.text", SubArg[1].ToString());
                SubArg = baseArg[10].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text500CntMVR.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text500MVRTotal.text", SubArg[1].ToString());
                SubArg = baseArg[11].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text1000CntMVR.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text1000MVRTotal.text", SubArg[1].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTotalMVR.text", baseArg[12].ToString());

                //USD
                SubArg = baseArg[13].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text1CntUSD.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text1USDTotal.text", SubArg[1].ToString());
                SubArg = baseArg[14].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text2CntUSD.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text2USDTotal.text", SubArg[1].ToString());
                SubArg = baseArg[15].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text5CntUSD.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text5USDTotal.text", SubArg[1].ToString());
                SubArg = baseArg[16].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text10CntUSD.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text10USDTotal.text", SubArg[1].ToString());
                SubArg = baseArg[17].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text20CntUSD.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text20USDTotal.text", SubArg[1].ToString());
                SubArg = baseArg[18].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text50CntUSD.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text50USDTotal.text", SubArg[1].ToString());
                SubArg = baseArg[19].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text100CntUSD.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text100USDTotal.text", SubArg[1].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTotalUSD.text", baseArg[20].ToString());

                xmlPrintresult = xprinter.SetXmlObjectTag("TotalMVR.text", baseArg[12].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TotalUSD.text", baseArg[20].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TotalAmount.text", baseArg[21].ToString());

                string parseInfo = "";
                xmlPrintresult = xprinter.GetXmlCheckCorrectParsing(out parseInfo);
                xmlPrintresult = xprinter.PrintXml(true, false);
                xprinter.Dispose();
                Console.WriteLine("Selected Receipt printing status OK");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt printing status OK", Program.subCap); }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Selected Receipt printing status ERROR: " + ex.ToString());
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt printing status ERROR: " + ex.ToString(), Program.subCap); }
                return false;

            }
        }

        //Type 2 CDM reciept
        private bool Print_AdminUtility_CDMReceipt(string args)//receipt type-1
        {
            try
            {
                Console.WriteLine("Selected Receipt- AdminUtility_CDMReceipt...");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt- AdminUtility_CDMReceipt...", Program.subCap); }
                string[] baseArg = args.Split(',');
                if (args.Length < 1) { return false; }
                CustomXmlPrintResult xmlPrintresult;
                CustomXmlPrinter xprinter = new CustomXmlPrinter();
                string ReceiptPrinterName = ConfigurationManager.AppSettings["PrinterName"].ToString();// "CUSTOM VKP80 II";//taken from config file
                string filename = @"C:\Kiosk\PrintTemplates\AdminUtility_CDMReceipt\AdminUtility_CDMReceipt.xml";
                xmlPrintresult = xprinter.SetPrinterName(ReceiptPrinterName);
                xmlPrintresult = xprinter.SetXmlFileName(filename);
                xmlPrintresult = xprinter.SetXmlObjectTag("TextLastBatchDate.text", baseArg[1].ToString().Replace("=", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextCurrentBatchDate.text", baseArg[2].ToString().Replace("=", " "));
                xmlPrintresult = xprinter.SetXmlObjectTag("TextUser.text", baseArg[3].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextBatchNo.text", baseArg[4].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextEvent.text", baseArg[5].ToString().Replace("="," "));

                //opening balance
                string[] SubArg = baseArg[6].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text5CntOpen.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text5OpenTotal.text", SubArg[1].ToString());
                SubArg = baseArg[7].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text10CntOpen.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text10OpenTotal.text", SubArg[1].ToString());
                SubArg = baseArg[8].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text20CntOpen.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text20OpenTotal.text", SubArg[1].ToString());
                SubArg = baseArg[9].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text50CntOpen.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text50OpenTotal.text", SubArg[1].ToString());
                SubArg = baseArg[10].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text100CntOpen.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text100OpenTotal.text", SubArg[1].ToString());
                SubArg = baseArg[11].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text500CntOpen.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text500OpenTotal.text", SubArg[1].ToString());
                SubArg = baseArg[12].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text1000CntOpen.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text1000OpenTotal.text", SubArg[1].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTotalOpen.text", baseArg[13].ToString());


                // Dispensed count
                SubArg = baseArg[14].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text5CntDisp.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text5DispTotal.text", SubArg[1].ToString());
                SubArg = baseArg[15].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text10CntDisp.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text10DispTotal.text", SubArg[1].ToString());
                SubArg = baseArg[16].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text20CntDisp.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text20DispTotal.text", SubArg[1].ToString());
                SubArg = baseArg[17].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text50CntDisp.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text50DispTotal.text", SubArg[1].ToString());
                SubArg = baseArg[18].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text100CntDisp.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text100DispTotal.text", SubArg[1].ToString());
                SubArg = baseArg[19].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text500CntDisp.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text500DispTotal.text", SubArg[1].ToString());
                SubArg = baseArg[20].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text1000CntDisp.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text1000DispTotal.text", SubArg[1].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTotalDisp.text", baseArg[21].ToString());

                //Rejected count

                SubArg = baseArg[22].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text5CntReject.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text5RejectTotal.text", SubArg[1].ToString());
                SubArg = baseArg[23].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text10CntReject.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text10RejectTotal.text", SubArg[1].ToString());
                SubArg = baseArg[24].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text20CntReject.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text20RejectTotal.text", SubArg[1].ToString());
                SubArg = baseArg[25].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text50CntReject.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text50RejectTotal.text", SubArg[1].ToString());
                SubArg = baseArg[26].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text100CntReject.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text100RejectTotal.text", SubArg[1].ToString());
                SubArg = baseArg[27].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text500CntReject.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text500RejectTotal.text", SubArg[1].ToString());
                SubArg = baseArg[28].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text1000CntReject.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text1000RejectTotal.text", SubArg[1].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTotalReject.text", baseArg[29].ToString());

                //Retracted count

                SubArg = baseArg[30].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text5CntRetract.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text5RetractTotal.text", SubArg[1].ToString());
                SubArg = baseArg[31].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text10CntRetract.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text10RetractTotal.text", SubArg[1].ToString());
                SubArg = baseArg[32].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text20CntRetract.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text20RetractTotal.text", SubArg[1].ToString());
                SubArg = baseArg[33].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text50CntRetract.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text50RetractTotal.text", SubArg[1].ToString());
                SubArg = baseArg[34].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text100CntRetract.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text100RetractTotal.text", SubArg[1].ToString());
                SubArg = baseArg[35].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text500CntRetract.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text500RetractTotal.text", SubArg[1].ToString());
                SubArg = baseArg[36].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text1000CntRetract.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text1000RetractTotal.text", SubArg[1].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTotalRetract.text", baseArg[37].ToString());


                //Refilled count

                SubArg = baseArg[38].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text5CntRefill.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text5RefillTotal.text", SubArg[1].ToString());
                SubArg = baseArg[39].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text10CntRefill.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text10RefillTotal.text", SubArg[1].ToString());
                SubArg = baseArg[40].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text20CntRefill.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text20RefillTotal.text", SubArg[1].ToString());
                SubArg = baseArg[41].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text50CntRefill.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text50RefillTotal.text", SubArg[1].ToString());
                SubArg = baseArg[42].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text100CntRefill.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text100RefillTotal.text", SubArg[1].ToString());
                SubArg = baseArg[43].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text500CntRefill.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text500RefillTotal.text", SubArg[1].ToString());
                SubArg = baseArg[44].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text1000CntRefill.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text1000RefillTotal.text", SubArg[1].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTotalRefill.text", baseArg[45].ToString());



                //Cash Taken out 

                SubArg = baseArg[46].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text5CntTakenOut.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text5TakenOutTotal.text", SubArg[1].ToString());
                SubArg = baseArg[47].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text10CntTakenOut.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text10TakenOutTotal.text", SubArg[1].ToString());
                SubArg = baseArg[48].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text20CntTakenOut.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text20TakenOutTotal.text", SubArg[1].ToString());
                SubArg = baseArg[49].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text50CntTakenOut.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text50TakenOutTotal.text", SubArg[1].ToString());
                SubArg = baseArg[50].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text100CntTakenOut.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text100TakenOutTotal.text", SubArg[1].ToString());
                SubArg = baseArg[51].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text500CntTakenOut.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text500TakenOutTotal.text", SubArg[1].ToString());
                SubArg = baseArg[52].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text1000CntTakenOut.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text1000TakenOutTotal.text", SubArg[1].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTotalTakenOut.text", baseArg[53].ToString());

                //Closing balance


                SubArg = baseArg[54].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text5CntClosing.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text5ClosingTotal.text", SubArg[1].ToString());
                SubArg = baseArg[55].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text10CntClosing.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text10ClosingTotal.text", SubArg[1].ToString());
                SubArg = baseArg[56].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text20CntClosing.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text20ClosingTotal.text", SubArg[1].ToString());
                SubArg = baseArg[57].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text50CntClosing.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text50ClosingTotal.text", SubArg[1].ToString());
                SubArg = baseArg[58].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text100CntClosing.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text100ClosingTotal.text", SubArg[1].ToString());
                SubArg = baseArg[59].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text500CntClosing.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text500ClosingTotal.text", SubArg[1].ToString());
                SubArg = baseArg[60].Split('~');
                xmlPrintresult = xprinter.SetXmlObjectTag("Text1000CntClosing.text", SubArg[0].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("Text1000ClosingTotal.text", SubArg[1].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTotalClosing.text", baseArg[61].ToString());

                //

                xmlPrintresult = xprinter.SetXmlObjectTag("TextTotalInDisp.text", baseArg[61].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TextTotalInKisok.text", baseArg[61].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("AmtTakenFromDisp.text", baseArg[53].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TotalAmtRejected.text", baseArg[29].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TotalAmtRetracted.text", baseArg[37].ToString());
                xmlPrintresult = xprinter.SetXmlObjectTag("TotalAmtTakenFromKiosk.text", baseArg[62].ToString());

                string parseInfo = "";
                xmlPrintresult = xprinter.GetXmlCheckCorrectParsing(out parseInfo);
                xmlPrintresult = xprinter.PrintXml(true, false);
                xprinter.Dispose();
                Console.WriteLine("Selected Receipt printing status OK");
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt printing status OK", Program.subCap); }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Selected Receipt printing status ERROR: " + ex.ToString());
                if (Program.DebugMode) { Program.addLog.WriteLog("Selected Receipt printing status ERROR: " + ex.ToString(), Program.subCap); }
                return false;

            }
        }





        public string GetPrinterStatus()
        {
            ConsolePRINT.classes.PrinterStatus pst = new PrinterStatus();
            bool rslt = pst.initPrinterStatus();
            return "ok";
        }
    }
}
