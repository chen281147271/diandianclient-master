using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DianDianClient.Utils
{
    public class PrintImpact
    {
        //打开串口失败
        public const int INVALID_HANDLE_VALUE = -1;
        // 函数执行成功
        public const int Impact_SUCCESS = 1001;
        //函数执行失败
        public const int Impact_FAIL = 1002;
        //端口或文件的句柄无效
        public const int Impact_ERROR_INVALID_HANDLE = 1101;
        // 参数无效
        public const int Impact_ERROR_INVALID_PARAMETER = 1102;
        //不是位图格式的文件
        public const int Impact_ERROR_NOT_BITMAP = 1103;
        //位图不是单色的
        public const int Impact_ERROR_NOT_MONO_BITMAP = 1104;
        //位图超出打印机可以
        public const int Impact_ERROR_BEYONG_AREA = 1105;
        //没有找到指定的文件路径或名称
        public const int Impact_ERROR_INVALID_PATH = 1106;
        //流控制为DTR/DST
        public const int Impact_COM_DTR_DSR = 0x00;
        //流控制为RTS/CTS 
        public const int Impact_COM_RTS_CTS = 0x01;
        //流控制为XON/OFF
        public const int Impact_COM_XON_XOFF = 0x02;
        //无握手
        public const int Impact_COM_NO_HANDSHAKE = 0x03;
        //打开串口通讯端口
        public const int Impact_OPEN_SERIAL_PORT = 0x11;
        //打开并口通讯端口
        public const int Impact_OPEN_PARALLEL_PORT = 0x12;
        //打开USB通讯端口 
        public const int Impact_OPEN_BYUSB_PORT = 0x13;
        //打开打印机驱动程序
        public const int Impact_OPEN_PRINTNAME = 0x14;
        //打开以太网打印机
        public const int Impact_OPEN_NETPORT = 0x15;
        //标准 ASCII
        public const int Impact_FONT_TYPE_STANDARD = 0x00;
        //压缩 ASCII 
        public const int Impact_FONT_TYPE_COMPRESSED = 0x01;
        //标准 “宋体”
        public const int Impact_FONT_TYPE_CHINESE = 0x03;
        //正常字体
        public const int Impact_FONT_STYLE_NORMAL = 0x00;
        //压缩
        public const int Impact_FONT_STYLE_COMPRESSED = 0x01;
        //加粗
        public const int Impact_FONT_STYLE_BOLD = 0x08;
        //下划线
        public const int Impact_FONT_STYLE_UNDERLINE = 0x80;
        //正常
        public const int Impact_BITMAP_PRINT_NORMAL = 0x00;
        //倍宽
        public const int Impact_BITMAP_PRINT_DOUBLE_WIDTH = 0x01;
        //默认全切,不找标记
        public const int Impact_CUT_MODE_FULL_NoMarker = 0x00;
        //默认半切，不找标记
        public const int Impact_CUT_MODE_PARTIAL_NoMarker = 0x01;
        //默认全切
        public const int Impact_CUT_MODE_FULL = 0x41;
        //默认半切
        public const int Impact_CUT_MODE_PARTIAL = 0x42;
        //停止位为1(指定串口通讯时的数据停止位数)
        public const int Impact_COM_ONESTOPBIT = 0x01;
        //停止位为2(指定串口通讯时的数据停止位数)
        public const int Impact_COM_TWOSTOPBITS = 0x02;
        //无校验(指定串口的奇偶校验方法)
        public const int Impact_COM_NOPARITY = 0x00;
        //奇校验(指定串口的奇偶校验方法)
        public const int Impact_COM_ODDPARITY = 0x01;
        //偶校验(指定串口的奇偶校验方法)
        public const int Impact_COM_EVENPARITY = 0x02;
        //标记校验(指定串口的奇偶校验方法)
        public const int Impact_COM_MARKPARITY = 0x03;
        //空格校验(指定串口的奇偶校验方法)
        public const int Impact_COM_SPACEPARITY = 0x04;
        //串口
        public const int Impact_COM_STATUS = 0;
        //并口
        public const int Impact_LPT_STATUS = 1;
        //USB接口
        public const int Impact_USB_STATUS = 2;
        //网络
        public const int Impact_INTER_STATUS = 3;
        //驱动程序
        public const int Impact_DRIVER_STATUS = 4;
        //托管Windows句柄
        private IntPtr g_hImpactdll = IntPtr.Zero;
        //打印机句柄
        private IntPtr callHandle;
        //Windows API装载动态库函数定义
        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);
        //Windows API获取要引入的函数，将符号名或标识号转换为 DLL 内部地址
        [DllImport("kernel32.dll")]
        static extern IntPtr GetProcAddress(IntPtr g_hImpactdll, string lpProcName);
        //Windows API释放动态连接库函数
        [DllImport("kernel32", EntryPoint = "FreeLibrary", SetLastError = true)]
        static extern bool FreeLibrary(IntPtr hModule);
        //打开端口
        private delegate IntPtr Impact_Open(String ipName, int nComBaudrate, int nComDataBits, int nComStopBits, int nComParity, int nParam);
        //关闭已经打开的并口或串口，USB端口，网络接口或打印机。
        private delegate int Impact_Close(IntPtr hPort, int nPortType);
        //复位打印机，把打印缓冲区中的数据清除，字符和行高的设置被清除，打印模式被恢复到上电时的缺省模式。
        private delegate int Impact_Reset(IntPtr hPort, int nPortType);
        //选择国际字符集和代码页。
        private delegate int Impact_SetCharSetAndCodePage(IntPtr hPort, int nPortType, int nCharSet, int nCodePage);
        //向前走纸。
        private delegate int Impact_FeedLine(IntPtr hPort, int nPortType);
        //打印并走纸n行
        private delegate int Impact_FeedLines(IntPtr hPort, int nPortType, int nLines);
        //向前走纸。
        private delegate int Impact_CheckOut(IntPtr hPort, int nPortType);
        //设置字符的行高
        private delegate int Impact_SetLineSpacing(IntPtr hPort, int nPortType, int nDistance);
        //设置字符的右间距（相邻两个字符的间隙距离）。
        private delegate int Impact_SetRightSpacing(IntPtr hPort, int nPortType, int nDistance);
        //预下载一幅或若干幅位图到打印机的 Flash 中。
        private delegate int Impact_PreDownloadBmpsToFlash(IntPtr hPort, int nPortType, string[] pszPaths, int nCount, ref int ErrBmpID);
        //查询打印机当前的状态。此函数是非实时的。
        private delegate int Impact_QueryStatus(IntPtr hPort, int nPortType, ref byte pszStatus, int nTimeouts);
        //返回当前打印机的状态。此函数是实时的。
        private delegate int Impact_RTQueryStatus(IntPtr hPort, int nPortType, ref byte pszStatus);
        //通过网络接口查询返回当前打印机的状态。
        private delegate int Impact_NETQueryStatus(int nPortType, string ipAddress, ref string pszStatus);
        //往钱箱引脚发送脉冲以打开钱箱。
        private delegate int Impact_KickOutDrawer(IntPtr hPort, int nPortType, int nID, int nOnTimes, int nOffTimes);
        //切纸。
        private delegate int Impact_CutPaper(IntPtr hPort, int nPortType, int nMode, int nDistance);
        //新建一个打印作业。
        private delegate bool Impact_StartDoc(IntPtr hPort, int nPortType);
        //结束一个打印作业。
        private delegate bool Impact_EndDoc(IntPtr hPort, int nPortType);
        //开始把发往打印机（端口）的数据保存到指定的文件。
        private delegate void Impact_BeginSaveFile(IntPtr hPort, String lpFileName, bool bToPrinter);
        //结束保存数据到文件的操作。
        private delegate void Impact_EndSaveFile(IntPtr hPort);
        //把将要打印的字符串数据发送到打印缓冲区中，指定每个字符宽度和高度方向上的放大倍数、类型和风格。
        private delegate int Impact_S_TextOut(IntPtr hPort, int nPortType, string pszString, int nWidthTimes, int nHeightTimes, int nFontType, int nFontStyle);
        //下载并打印位图
        private delegate int Impact_S_DownloadAndPrintBmp(IntPtr hPort, int nPortType, string pszPath, int nMode);
        //打印已经下载到 Flash 中的位图。
        private delegate int Impact_S_PrintBmpInFlash(IntPtr hPort, int nPortType, int nID, int nMode);
        //发送数据到端口。
        private delegate int Impact_WriteFile(IntPtr hPort, int nPortType, string pszData, int nBytesToWrite);
        //从串口或USB端口读数据到指定的缓冲区。
        private delegate int Impact_ReadFile(IntPtr hPort, int nPortType, string pszData, int nBytesToRead, int nTimeouts);
        //获取当前 dll 的发布版本号。
        private delegate int Impact_GetVersionInfo(ref int pnMajor, ref int pnMinor);
        //选择/取消单向打印
        private delegate int Impact_BidirecPrint(IntPtr hPort, int nPortType, int nDirection);
        //选择/取消双重打印。
        private delegate int Impact_DuplePrint(IntPtr hPort, int nPortType, int nDuple);
        //选择打印颜色
        private delegate int Impact_SelectColor(IntPtr hPort, int nPortType, int nMode);
        //获取函数地址
        private Delegate GetFunctionAddress(IntPtr dllModule, string functionName, Type t)
        {
            IntPtr address = GetProcAddress(dllModule, functionName);
            if (address == IntPtr.Zero)
                return null;
            else
                return Marshal.GetDelegateForFunctionPointer(address, t);
        }
        //打开端口
        public IntPtr OpenPrint(String ipName, int nComBaudrate, int nComDataBits, int nComStopBits, int nComParity, int nParam)
        {
            try
            {
                //得到DLL路径
                string dllPath = Environment.CurrentDirectory + "\\ImpactDLL.dll";
                Console.Write("===" + dllPath);
                //加载DLL
                g_hImpactdll = LoadLibrary(dllPath);
                //判断是否加载
                if (g_hImpactdll.Equals(IntPtr.Zero))
                {
                    Console.Write("错误");


                    return new IntPtr(INVALID_HANDLE_VALUE);
                }
                //将要调用的方法转换为委托：g_hImpactdll为DLL的句柄，"CommOpen"为DLL中方法的名称
                Impact_Open openImp = (Impact_Open)GetFunctionAddress(g_hImpactdll, "Impact_Open", typeof(Impact_Open));
                if (openImp == null)
                {
                    FreeLibrary(g_hImpactdll);
                    g_hImpactdll = new IntPtr(Impact_FAIL);
                    Console.Write("错误");
                    return g_hImpactdll;
                }
                callHandle = openImp(ipName, nComBaudrate, nComDataBits, nComStopBits, nComParity, nParam);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return callHandle;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = new IntPtr(Impact_FAIL);
                return g_hImpactdll;
            }
        }
        //关闭端口
        public int ClosePrint(IntPtr hPort, int nPortType)
        {
            try
            {
                //得到DLL路径
                string dllPath = Environment.CurrentDirectory + "\\ImpactDLL.dll";
                Console.Write("===" + dllPath);
                //加载DLL
                g_hImpactdll = LoadLibrary(dllPath);
                //判断是否加载
                if (g_hImpactdll.Equals(IntPtr.Zero))
                {
                    Console.Write("错误");
                    return Impact_FAIL;
                }
                //将要调用的方法转换为委托：g_hImpactdll为DLL的句柄，"CommOpen"为DLL中方法的名称
                Impact_Close closeImp = (Impact_Close)GetFunctionAddress(g_hImpactdll, "Impact_Close", typeof(Impact_Close));
                if (closeImp == null)
                {
                    FreeLibrary(g_hImpactdll);
                    g_hImpactdll = IntPtr.Zero;
                    Console.Write("错误");
                    return Impact_FAIL;
                }
                int status = closeImp(hPort, nPortType);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return status;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return Impact_FAIL;
            }
        }
        //新建一个打印作业
        public bool StartDoc(IntPtr hPort, int nPortType)
        {
            try
            {
                //得到DLL路径
                string dllPath = Environment.CurrentDirectory + "\\ImpactDLL.dll";
                Console.Write("===" + dllPath);
                //加载DLL
                g_hImpactdll = LoadLibrary(dllPath);
                //判断是否加载
                if (g_hImpactdll.Equals(IntPtr.Zero))
                {
                    Console.Write("错误");
                    return false;
                }
                //将要调用的方法转换为委托：g_hImpactdll为DLL的句柄，"CommOpen"为DLL中方法的名称
                Impact_StartDoc startDoc = (Impact_StartDoc)GetFunctionAddress(g_hImpactdll, "Impact_StartDoc", typeof(Impact_StartDoc));
                if (startDoc == null)
                {
                    FreeLibrary(g_hImpactdll);
                    g_hImpactdll = IntPtr.Zero;
                    Console.Write("错误");
                    return false;
                }
                bool falg = startDoc(hPort, nPortType);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return falg;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return false;
            }
        }
        //开始保存到文件
        public void BeginSaveFile(IntPtr hPort, String lpFileName, bool bToPrinter)
        {
            try
            {
                //得到DLL路径
                string dllPath = Environment.CurrentDirectory + "\\ImpactDLL.dll";
                Console.Write("===" + dllPath);
                //加载DLL
                g_hImpactdll = LoadLibrary(dllPath);
                //判断是否加载
                if (g_hImpactdll.Equals(IntPtr.Zero))
                {
                    Console.Write("错误");
                    return;
                }
                //将要调用的方法转换为委托：g_hImpactdll为DLL的句柄，"CommOpen"为DLL中方法的名称
                Impact_BeginSaveFile saveFile = (Impact_BeginSaveFile)GetFunctionAddress(g_hImpactdll, "Impact_BeginSaveFile", typeof(Impact_BeginSaveFile));
                if (saveFile == null)
                {
                    FreeLibrary(g_hImpactdll);
                    g_hImpactdll = IntPtr.Zero;
                    Console.Write("错误");
                    return;
                }
                saveFile(hPort, lpFileName, bToPrinter);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return;
            }
        }
        //复位打印机
        public int ResetPrint(IntPtr hPort, int nPortType)
        {
            try
            {
                //得到DLL路径
                string dllPath = Environment.CurrentDirectory + "\\ImpactDLL.dll";
                Console.Write("===" + dllPath);
                //加载DLL
                g_hImpactdll = LoadLibrary(dllPath);
                //判断是否加载
                if (g_hImpactdll.Equals(IntPtr.Zero))
                {
                    Console.Write("错误");
                    return Impact_FAIL;
                }
                //将要调用的方法转换为委托：g_hImpactdll为DLL的句柄，"CommOpen"为DLL中方法的名称
                Impact_Reset resetPrint = (Impact_Reset)GetFunctionAddress(g_hImpactdll, "Impact_Reset", typeof(Impact_Reset));
                if (resetPrint == null)
                {
                    FreeLibrary(g_hImpactdll);
                    g_hImpactdll = IntPtr.Zero;
                    Console.Write("错误");
                    return Impact_FAIL;
                }
                int status = resetPrint(hPort, nPortType);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return status;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return Impact_FAIL;
            }
        }
        //设置字符的右间距（相邻两个字符的间隙距离）。
        public int SetRightSpacing(IntPtr hPort, int nPortType, int nDistance)
        {
            try
            {
                //得到DLL路径
                string dllPath = Environment.CurrentDirectory + "\\ImpactDLL.dll";
                Console.Write("===" + dllPath);
                //加载DLL
                g_hImpactdll = LoadLibrary(dllPath);
                //判断是否加载
                if (g_hImpactdll.Equals(IntPtr.Zero))
                {
                    Console.Write("错误");
                    return Impact_FAIL;
                }
                //将要调用的方法转换为委托：g_hImpactdll为DLL的句柄，"CommOpen"为DLL中方法的名称
                Impact_SetRightSpacing setRightSpacing = (Impact_SetRightSpacing)GetFunctionAddress(g_hImpactdll, "Impact_SetRightSpacing", typeof(Impact_SetRightSpacing));
                if (setRightSpacing == null)
                {
                    FreeLibrary(g_hImpactdll);
                    g_hImpactdll = IntPtr.Zero;
                    Console.Write("错误");
                    return Impact_FAIL;
                }
                int status = setRightSpacing(hPort, nPortType, nDistance);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return status;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return Impact_FAIL;
            }
        }
        //设置字符的行高。
        public int SetLineSpacing(IntPtr hPort, int nPortType, int nDistance)
        {
            try
            {
                //得到DLL路径
                string dllPath = Environment.CurrentDirectory + "\\ImpactDLL.dll";
                Console.Write("===" + dllPath);
                //加载DLL
                g_hImpactdll = LoadLibrary(dllPath);
                //判断是否加载
                if (g_hImpactdll.Equals(IntPtr.Zero))
                {
                    Console.Write("错误");
                    return Impact_FAIL;
                }
                //将要调用的方法转换为委托：g_hImpactdll为DLL的句柄，"CommOpen"为DLL中方法的名称
                Impact_SetLineSpacing setLineSpacing = (Impact_SetLineSpacing)GetFunctionAddress(g_hImpactdll, "Impact_SetLineSpacing", typeof(Impact_SetLineSpacing));
                if (setLineSpacing == null)
                {
                    FreeLibrary(g_hImpactdll);
                    g_hImpactdll = IntPtr.Zero;
                    Console.Write("错误");
                    return Impact_FAIL;
                }
                int status = setLineSpacing(hPort, nPortType, nDistance);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return status;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return Impact_FAIL;
            }
        }
        //打印文本
        public int TextOut(IntPtr hPort, int nPortType, string pszString, int nWidthTimes, int nHeightTimes, int nFontType, int nFontStyle)
        {
            try
            {
                //得到DLL路径
                string dllPath = Environment.CurrentDirectory + "\\ImpactDLL.dll";
                Console.Write("===" + dllPath);
                //加载DLL
                g_hImpactdll = LoadLibrary(dllPath);
                //判断是否加载
                if (g_hImpactdll.Equals(IntPtr.Zero))
                {
                    Console.Write("错误");
                    return Impact_FAIL;
                }
                //将要调用的方法转换为委托：g_hImpactdll为DLL的句柄，"CommOpen"为DLL中方法的名称
                Impact_S_TextOut textOut = (Impact_S_TextOut)GetFunctionAddress(g_hImpactdll, "Impact_S_TextOut", typeof(Impact_S_TextOut));
                if (textOut == null)
                {
                    FreeLibrary(g_hImpactdll);
                    g_hImpactdll = IntPtr.Zero;
                    Console.Write("错误");
                    return Impact_FAIL;
                }
                int status = textOut(hPort, nPortType, pszString, nWidthTimes, nHeightTimes, nFontType, nFontStyle);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return status;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return Impact_FAIL;
            }
        }
        //切纸
        public int CutPaper(IntPtr hPort, int nPortType, int nMode, int nDistance)
        {
            try
            {
                //得到DLL路径
                string dllPath = Environment.CurrentDirectory + "\\ImpactDLL.dll";
                Console.Write("===" + dllPath);
                //加载DLL
                g_hImpactdll = LoadLibrary(dllPath);
                //判断是否加载
                if (g_hImpactdll.Equals(IntPtr.Zero))
                {
                    Console.Write("错误");
                    return Impact_FAIL;
                }
                //将要调用的方法转换为委托：g_hImpactdll为DLL的句柄，"CommOpen"为DLL中方法的名称
                Impact_CutPaper cutPaper = (Impact_CutPaper)GetFunctionAddress(g_hImpactdll, "Impact_CutPaper", typeof(Impact_CutPaper));
                if (cutPaper == null)
                {
                    FreeLibrary(g_hImpactdll);
                    g_hImpactdll = IntPtr.Zero;
                    Console.Write("错误");
                    return Impact_FAIL;
                }
                int status = cutPaper(hPort, nPortType, nMode, nDistance);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return status;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return Impact_FAIL;
            }
        }
        //结束保存数据到文件的操作。
        public void EndSaveFile(IntPtr hPort)
        {
            try
            {
                //得到DLL路径
                string dllPath = Environment.CurrentDirectory + "\\ImpactDLL.dll";
                Console.Write("===" + dllPath);
                //加载DLL
                g_hImpactdll = LoadLibrary(dllPath);
                //判断是否加载
                if (g_hImpactdll.Equals(IntPtr.Zero))
                {
                    Console.Write("错误");
                    return;
                }
                //将要调用的方法转换为委托：g_hImpactdll为DLL的句柄，"CommOpen"为DLL中方法的名称
                Impact_EndSaveFile endSaveFile = (Impact_EndSaveFile)GetFunctionAddress(g_hImpactdll, "Impact_EndSaveFile", typeof(Impact_EndSaveFile));
                if (endSaveFile == null)
                {
                    FreeLibrary(g_hImpactdll);
                    g_hImpactdll = IntPtr.Zero;
                    Console.Write("错误");
                    return;
                }
                endSaveFile(hPort);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return;
            }
        }
        //结束一个打印作业。
        public bool EndDoc(IntPtr hPort, int nPortType)
        {
            try
            {
                //得到DLL路径
                string dllPath = Environment.CurrentDirectory + "\\ImpactDLL.dll";
                Console.Write("===" + dllPath);
                //加载DLL
                g_hImpactdll = LoadLibrary(dllPath);
                //判断是否加载
                if (g_hImpactdll.Equals(IntPtr.Zero))
                {
                    Console.Write("错误");
                    return false;
                }
                //将要调用的方法转换为委托：g_hImpactdll为DLL的句柄，"CommOpen"为DLL中方法的名称
                Impact_EndDoc endDoc = (Impact_EndDoc)GetFunctionAddress(g_hImpactdll, "Impact_EndDoc", typeof(Impact_EndDoc));
                if (endDoc == null)
                {
                    FreeLibrary(g_hImpactdll);
                    g_hImpactdll = IntPtr.Zero;
                    Console.Write("错误");
                    return false;
                }
                bool falg = endDoc(hPort, nPortType);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return falg;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return false;
            }
        }
        //向前走纸
        public int FeedLine(IntPtr hPort, int nPortType)
        {
            try
            {
                //得到DLL路径
                string dllPath = Environment.CurrentDirectory + "\\ImpactDLL.dll";
                Console.Write("===" + dllPath);
                //加载DLL
                g_hImpactdll = LoadLibrary(dllPath);
                //判断是否加载
                if (g_hImpactdll.Equals(IntPtr.Zero))
                {
                    Console.Write("错误");
                    return Impact_FAIL;
                }
                //将要调用的方法转换为委托：g_hImpactdll为DLL的句柄，"CommOpen"为DLL中方法的名称
                Impact_FeedLine feedLine = (Impact_FeedLine)GetFunctionAddress(g_hImpactdll, "Impact_FeedLine", typeof(Impact_FeedLine));
                if (feedLine == null)
                {
                    FreeLibrary(g_hImpactdll);
                    g_hImpactdll = IntPtr.Zero;
                    Console.Write("错误");
                    return Impact_FAIL;
                }
                int status = feedLine(hPort, nPortType);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return status;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return Impact_FAIL;
            }
        }
        //查询当前打印机状态非实时
        public int QueryStatus(IntPtr hPort, int nPortType, ref byte pszStatus, int nTimeouts)
        {
            try
            {
                //得到DLL路径
                string dllPath = Environment.CurrentDirectory + "\\ImpactDLL.dll";
                Console.Write("===" + dllPath);
                //加载DLL
                g_hImpactdll = LoadLibrary(dllPath);
                //判断是否加载
                if (g_hImpactdll.Equals(IntPtr.Zero))
                {
                    Console.Write("错误");
                    return Impact_FAIL;
                }
                //将要调用的方法转换为委托：g_hImpactdll为DLL的句柄，"CommOpen"为DLL中方法的名称
                Impact_QueryStatus queryStatus = (Impact_QueryStatus)GetFunctionAddress(g_hImpactdll, "Impact_QueryStatus", typeof(Impact_QueryStatus));
                if (queryStatus == null)
                {
                    FreeLibrary(g_hImpactdll);
                    g_hImpactdll = IntPtr.Zero;
                    Console.Write("错误");
                    return Impact_FAIL;
                }
                int status = queryStatus(hPort, nPortType, ref pszStatus, nTimeouts);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return status;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return Impact_FAIL;
            }
        }
        //实时查询打印状态
        public int RTQueryStatus(IntPtr hPort, int nPortType, ref byte pszStatus)
        {
            try
            {
                //得到DLL路径
                string dllPath = Environment.CurrentDirectory + "\\ImpactDLL.dll";
                Console.Write("===" + dllPath);
                //加载DLL
                g_hImpactdll = LoadLibrary(dllPath);
                //判断是否加载
                if (g_hImpactdll.Equals(IntPtr.Zero))
                {
                    Console.Write("错误");
                    return Impact_FAIL;
                }
                //将要调用的方法转换为委托：g_hImpactdll为DLL的句柄，"CommOpen"为DLL中方法的名称
                Impact_RTQueryStatus queryStatus = (Impact_RTQueryStatus)GetFunctionAddress(g_hImpactdll, "Impact_RTQueryStatus", typeof(Impact_RTQueryStatus));
                if (queryStatus == null)
                {
                    FreeLibrary(g_hImpactdll);
                    g_hImpactdll = IntPtr.Zero;
                    Console.Write("错误");
                    return Impact_FAIL;
                }
                int status = queryStatus(hPort, nPortType, ref pszStatus);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return status;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                FreeLibrary(g_hImpactdll);
                g_hImpactdll = IntPtr.Zero;
                return Impact_FAIL;
            }
        }
    }
}
