using System;

using System.Collections.Generic;

using System.Text;

using System.Runtime.InteropServices;



namespace DianDianClient.Utils

{

    public class LPTControls

    {

        #region API函数
        [StructLayout(LayoutKind.Sequential)]
        private struct OVERLAPPED
        {
            int Internal;
            int InternalHigh;
            int Offset;
            int OffSetHigh;
            int hEvent;
        }
        [DllImport("kernel32.dll")]
        private static extern int CreateFile(string lpFileName, uint dwDesiredAccess, int dwShareMode,
            int lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, int hTemplateFile);
        [DllImport("kernel32.dll")]
        private static extern bool WriteFile(int hFile, byte[] lpBuffer, int nNumberOfBytesToWrite,
            out int lpNumberOfBytesWritten, out OVERLAPPED lpOverlapped);
        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(int hObject);
        #endregion
        public enum HorPos { Left, Center, Right }
        private int iHandle;
        private int ColWidth =36;
        public bool Open(string printname)
        {
            if (printname == null)
            {
                printname = "LPT1";
            }
            else {

            }
            iHandle = CreateFile(printname, 0x40000000, 0, 0, 3, 0, 0);
            if (iHandle != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Write(string Data)
        {
            try
            {
                if (iHandle != -1)
                {
                    int i;
                    OVERLAPPED x;
                    byte[] bData = System.Text.Encoding.Default.GetBytes(Data);
                    return WriteFile(iHandle, bData, bData.Length, out i, out x);
                }
                else
                {
                    return false;
                }
            }
            catch { return false; }
        }
        public bool Write(byte[] bdata)
        {
            if (bdata.Length == 0) return false;
            if (iHandle != -1)
            {
                int i;
                OVERLAPPED x;
                return WriteFile(iHandle, bdata, bdata.Length, out i, out x);
            }
            else
            {
                return false;
            }
        }
        public bool WriteLine(string Data,string font)
        {
            if (font != null)
            {
                
                Data = font + Data;
            }
            else {
                //回复默认字体
                string fmt = "" + (char)(27) + (char)(50);
                Data = fmt + Data;
            }
            bool Result = Write(Data);
            //if (Result) Result = NewRow();
            return Result;
        }
        public bool WriteLine(string Data, string font, HorPos horpos)
        {
            
            int Length = Encoding.Default.GetBytes(Data).Length;
            if (Length > ColWidth || HorPos.Left == horpos) return WriteLine(Data,font);
            switch (horpos)
            {
                case HorPos.Center:
                    Data = Data.PadLeft(Length + (ColWidth - Length) / 2 - (Length - Data.Length), ' ');
                    break;
                case HorPos.Right:
                    Data = Data.PadLeft(ColWidth - (Length - Data.Length), ' ');
                    break;
                default:
                    break;
            }
            return WriteLine(Data,font);
        }
        //打印数据，meg打印字符串，bBold=true粗体，nZoom=2大一号字体， nHAil=2居中对齐，nHAil=3右对齐。部分打印机可能中文字体设置无效，请加上FS ！命令设置中文字体。
        public bool OnWriteData(string meg, bool bBold, bool bDTall, bool bDWide, int nHAil)
        {
            string fmt = "" + (char)(27) + (char)(64);
            Write(fmt);//回复默认字体
            fmt = "" + (char)(27) + (char)(50);
            Write(fmt);//回复默认字体
           // fmt = "" + (char)(28) + (char)(38);// 进入汉字打印方式
           // Write(fmt);//回复默认字体
                       // fmt = "" + (char)(29) + (char)(73) + (char)(69);
                       // WriteData(fs, fmt);//回复默认字体
            meg = "" + (char)(27) + (char)(51) + (char)(15) + meg;

            long nMode = 0;

            if (bBold)
                nMode += 8;

            if (bDTall)
                nMode += 16;

            if (bDWide)
                nMode += 32;

            if (nMode > 0)
            {
                string send = "" + (char)(27) + (char)(33) + (char)(nMode);//最后的48可以为0,16,32,48 设置字体大小
                meg = send + meg;
                //WriteData(fs, meg);
            }

            switch (nHAil)
            {
                case 1:
                    meg = "" + (char)(27) + (char)(97) + (char)(0) + meg;
                    break;
                case 2:
                    //strcat(s, "\x1B\x61\x01");27 64初始化打印机命令 可用，可不用 (char)(27) + (char)(64) + 
                    meg = "" + (char)(27) + (char)(97) + (char)(1) + meg;
                    // WriteData(fs, meg);
                    break;
                case 3:
                    //strcat(s, "\x1B\x61\x02");
                    meg = "" + (char)(27) + (char)(97) + (char)(2) + meg;
                    // WriteData(fs,meg);
                    break;
                default:
                    meg = "" + (char)(27) + (char)(97) + (char)(0) + meg;
                    break;
            }

            Write(meg);


            //strcpy(s, "\x1B\x21\x00");
            meg = "" + (char)(27) + (char)(97) + (char)(0) + "" + (char)(27) + (char)(33) + (char)(0);
            Write(meg);//回复默认字体
            meg = "" + (char)(13);
            Write(meg);//回复默认字体
            return true;
        }
    
    public bool Close()
        {
            return CloseHandle(iHandle);
        }
        public bool PrintLine()
        {
            return WriteLine("------------------------\n",null);
        }
        public bool PrintDate()
        {
            return WriteLine("\n打印时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),null);
        }
        public bool NewRow()
        {
            bool Result = Write(new byte[] { 27, 10, 13 });
            return Result;
        }
        public bool NewRow(int iRow)
        {
            bool Result = false;
            for (int i = 0; i < iRow; i++)
            {
                Result = NewRow();
                if (!Result) break;
            }
            return Result;
        }
        public bool CutPaper()
        {
            NewRow(5);
            return Write(new byte[] {10, 10, 10,10,10,10,27, 109 });
        }
    }
    public class Printer
    {
        #region 预定义类型
        [FlagsAttribute]
        public enum PrinterEnumFlags
        {
            PRINTER_ENUM_DEFAULT = 0x00000001,
            PRINTER_ENUM_LOCAL = 0x00000002,
            PRINTER_ENUM_CONNECTIONS = 0x00000004,
            PRINTER_ENUM_FAVORITE = 0x00000004,
            PRINTER_ENUM_NAME = 0x00000008,
            PRINTER_ENUM_REMOTE = 0x00000010,
            PRINTER_ENUM_SHARED = 0x00000020,
            PRINTER_ENUM_NETWORK = 0x00000040,
            PRINTER_ENUM_EXPAND = 0x00004000,
            PRINTER_ENUM_CONTAINER = 0x00008000,
            PRINTER_ENUM_ICONMASK = 0x00ff0000,
            PRINTER_ENUM_ICON1 = 0x00010000,
            PRINTER_ENUM_ICON2 = 0x00020000,
            PRINTER_ENUM_ICON3 = 0x00040000,
            PRINTER_ENUM_ICON4 = 0x00080000,
            PRINTER_ENUM_ICON5 = 0x00100000,
            PRINTER_ENUM_ICON6 = 0x00200000,
            PRINTER_ENUM_ICON7 = 0x00400000,
            PRINTER_ENUM_ICON8 = 0x00800000,
            PRINTER_ENUM_HIDE = 0x01000000
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct PRINTER_INFO_2
        {
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pServerName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pPrinterName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pShareName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pPortName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pDriverName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pComment;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pLocation;
            public IntPtr pDevMode;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pSepFile;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pPrintProcessor;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pDatatype;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pParameters;
            public IntPtr pSecurityDescriptor;
            public uint Attributes;
            public uint Priority;
            public uint DefaultPriority;
            public uint StartTime;
            public uint UntilTime;
            public uint Status;
            public uint cJobs;
            public uint AveragePPM;
        }
        #endregion
        #region 引用 WindowsAPI
        //引用API声明
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumPrinters(PrinterEnumFlags Flags, string Name, uint Level,
                                        IntPtr pPrinterEnum, uint cbBuf, ref uint pcbNeeded,
                                        ref uint pcReturned);
        #endregion
        public static string[] GetPrinter()
        {
            string[] Result = null;
            PRINTER_INFO_2[] printInfo;
            printInfo = EnumPrinters(PrinterEnumFlags.PRINTER_ENUM_LOCAL);
            if (printInfo != null && printInfo.Length >= 0)
            {
                Result = new string[printInfo.Length];
                for (int i = 0; i < printInfo.Length; i++)
                {
                    Result[i] = printInfo[i].pPrinterName;
                }
            }
            return Result;

        }

        #region 托管代码
        /// <summary>
        /// 遍历打印机
        /// </summary>
        /// <param name="Flags"></param>
        /// <returns></returns>
        public static PRINTER_INFO_2[] EnumPrinters(PrinterEnumFlags Flags)
        {
            PRINTER_INFO_2[] Info2 = null;
            uint cbNeeded = 0;
            uint cReturned = 0;
            bool ret = EnumPrinters(Flags, null, 2, IntPtr.Zero, 0, ref cbNeeded, ref cReturned);
            IntPtr pAddr = Marshal.AllocHGlobal((int)cbNeeded);
            ret = EnumPrinters(Flags, null, 2, pAddr, cbNeeded, ref cbNeeded, ref cReturned);
            if (ret)
            {
                Info2 = new PRINTER_INFO_2[cReturned];
                int offset = pAddr.ToInt32();
                for (int i = 0; i < cReturned; i++)
                {
                    Info2[i] = (PRINTER_INFO_2)Marshal.PtrToStructure(new IntPtr(offset), typeof(PRINTER_INFO_2));
                    offset += Marshal.SizeOf(typeof(PRINTER_INFO_2));
                }
                Marshal.FreeHGlobal(pAddr);
            }
            return Info2;
        }
        #endregion
    }
}

