using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ConsolePRINT.classes
{
    class PrinterStatus
    {
        private const uint NOPAPER = 0x00000001;
        private const uint NEARPAPEREND = 0x00000004;
        private const uint TICKETOUT = 0x00000020;
        private const uint NOHEAD = 0x00000100;
        private const uint NOCOVER = 0x00000200;
        private const uint PAPERJAM = 0x00400000;

        private int _prnDevNum;
        private uint _prnModel;
        private string errMSg;

        public bool initPrinterStatus()
        {
            uint libError = uint.MaxValue;


            uint sysError = uint.MaxValue;
            uint prnStatus = 0;

            try
            {
                // Get staus from printer
                libError = IntercomModule.CePrnGetStsUsb(0, ref prnStatus, ref sysError);
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                //MessageBox.Show(ex.Message);
            }




            uint prnDevNum = uint.MaxValue;
            libError = IntercomModule.CePrnGetInterfaceNumUsb("CUSTOM VKP80 II", ref prnDevNum);//printer name should be taken from config.
            _prnDevNum = Convert.ToInt32(prnDevNum);

            if (_prnDevNum == int.MaxValue)
            {
                errMSg = "Wrong printer name";
                return false;
            }
            if (_prnDevNum == int.MaxValue)
            {
                errMSg = "Wrong printer name";
                return false;
            }
            uint outrslt = CePrnGetStsUsb(_prnDevNum);
            //InterfaceBase.PrinterStatusInt = Convert.ToInt32(outrslt);
            int rlst = Convert.ToInt32(outrslt);

            return true;
        }


        private uint CePrnGetStsUsb(int prnDevNum)
        {
            uint libError = uint.MaxValue;
            uint sysError = uint.MaxValue;
            uint prnStatus = 0;

            try
            {
                // Get staus from printer
                libError = IntercomModule.CePrnGetStsUsb(prnDevNum, ref prnStatus, ref sysError);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            // prnStatus contains current printer status
            DecodePrintStatus(prnStatus);
            return prnStatus;
        }


        private void DecodePrintStatus(uint code)
        {
            // More than one of the following status can be segnaled at the same time.
            // To know how to decode other possible printer status, see the note into
            // #region Consts

            // Verify if a paper end is segnaled.
            bool paperEnd = Convert.ToBoolean(code & NOPAPER);
            if (paperEnd)
            {
                Console.WriteLine("NO papper");
                //buttonPaperEnd.ImageIndex = 1;
            }
            else
            {
                //buttonPaperEnd.ImageIndex = 0;
            }

            // Verify if a near paper end is segnaled.
            bool nearpaperEnd = Convert.ToBoolean(code & NEARPAPEREND);
            if (nearpaperEnd)
            {
                Console.WriteLine("papper near end:");
                //buttonNearPaperEnd.ImageIndex = 1;
            }
            else
            {
                //buttonNearPaperEnd.ImageIndex = 0;
            }

            // Verify if a ticket out is segnaled.
            bool ticketOut = Convert.ToBoolean(code & TICKETOUT);
            if (ticketOut)
            {
                //buttonTicketOut.ImageIndex = 1;
            }
            else
            {
                //buttonTicketOut.ImageIndex = 0;
            }

            // Verify if a paper jam is segnaled.
            bool paperJam = Convert.ToBoolean(code & PAPERJAM);
            if (paperJam)
            {
                //buttonPaperJam.ImageIndex = 1;
            }
            else
            {
                //buttonPaperJam.ImageIndex = 0;
            }

            // Verify if a cover open / Head up status is segnaled.
            bool coverOpen = Convert.ToBoolean((code & NOCOVER) | (code & NOHEAD));
            if (coverOpen)
            {
                //buttonCoverOpen.ImageIndex = 1;
            }
            else
            {
                //buttonCoverOpen.ImageIndex = 0;
            }
        }

        internal static class IntercomModule
        {
            // 
            [System.Runtime.InteropServices.DllImport("CeSmLm.dll", EntryPoint = "CePrnInitCeUsbSI", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint CePrnInitUsb(ref uint lpdwSysError);
            // 
            [System.Runtime.InteropServices.DllImport("CeSmLm.dll", EntryPoint = "CePrnGetInterfaceNumUsb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint CePrnGetInterfaceNumUsb([MarshalAs(UnmanagedType.LPStr)]string prnName, ref uint lpdwSysError);
            // 
            [System.Runtime.InteropServices.DllImport("CeSmLm.dll", EntryPoint = "CePrnGetFullModelUsb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint CePrnGetFullModelUsb(int prnDevNum, ref uint prnModel, ref uint lpdwSysError);
            //
            [System.Runtime.InteropServices.DllImport("CeSmLm.dll", EntryPoint = "CePrnKpmGetModelUsb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint CePrnKpmGetModelUsb(int prnDevNum, [Out] byte[] lpBuffer, ref uint numBytesRead, ref uint lpdwSysError);
            // 
            [System.Runtime.InteropServices.DllImport("CeSmLm.dll", EntryPoint = "SetUsbPrinterModelGSI", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint SetUsbPrinterModelUsb(int prnDevNum, uint prnModel);
            // 
            [System.Runtime.InteropServices.DllImport("cesmlm.dll", EntryPoint = "CePrnGetStsUsb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint CePrnGetStsUsb(int prnDevNum, ref uint prnStatus, ref uint lpdwSysError);

            [System.Runtime.InteropServices.DllImport("CeSmLm.dll", EntryPoint = "CePrnGetNumTotCutUsb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint CePrnGetNumCutUsb(int prnDevNum, ref uint prnNumCuts, ref uint lpdwSysError);

            [System.Runtime.InteropServices.DllImport("CeSmLm.dll", EntryPoint = "CePrnGetTotPaperRemainingUsb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint CePrnGetTotPaperRemaingUsb(int prnDevNum, ref uint prnTotPaper, ref uint lpdwSysError);

        }
    }
}
