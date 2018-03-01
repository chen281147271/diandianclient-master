using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using static DianDianClient.Utils.PrintUtil;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using PosPrintService;
using System.IO.Ports;
using System.IO;
using DianDianClient.Models;
using DianDianClient.Utils;

namespace DianDianClient.Biz
{
    class MyPrintHandler
    {
        log4net.ILog log = log4net.LogManager.GetLogger("MyPrintHandler");
        PrintDocument printDocument1;
        BeiYangOPOS opos = new BeiYangOPOS();
        private Object thisLock = new object();
        private Form form;

        private Dictionary<string, object> pcontent = null;
        private Queue<Dictionary<string, object>> printque = new Queue<Dictionary<string, object>>();//打印队列

        public MyPrintHandler(Dictionary<string, object> pcontent , Form form) {
            this.pcontent = pcontent;
            this.form = form;
            printDocument1 = new PrintDocument();
            PrintController printController = new StandardPrintController();
            printDocument1.PrintController = printController;
            printque.Enqueue(pcontent);
        }

        // NumberOfDigits 静态方法计算
        // 传递的字符串中数字字符的数目：
        public static int NumberOfDigits(string theString)
        {
            int count = 0;
            for (int i = 0; i < theString.Length; i++)
            {
                if (Char.IsDigit(theString[i]))
                {
                    count++;
                }
            }
            return count;
        }
        //汉字个数
        public static int GetHanNumFromString(string str)
        {
            int count = 0;
            Regex regex = new Regex(@"^[\u4E00-\u9FA5]{0,}$");

            for (int i = 0; i < str.Length; i++)
            {
                if (regex.IsMatch(str[i].ToString()))
                {
                    count++;
                }
            }

            return count;
        }
        /// <summary>
        /// 获取中英文混排字符串的实际长度(字节数)
        /// </summary>
        /// <param name="str">要获取长度的字符串</param>
        /// <returns>字符串的实际长度值（字节数）</returns>
        public int getStringLength(string str)
        {
            if (str.Equals(string.Empty))
                return 0;
            int strlen = 0;
            ASCIIEncoding strData = new ASCIIEncoding();
            //将字符串转换为ASCII编码的字节数字
            byte[] strBytes = strData.GetBytes(str);
            for (int i = 0; i <= strBytes.Length - 1; i++)
            {
                if (strBytes[i] == 63)  //中文都将编码为ASCII编码63,即"?"号
                    strlen++;
                strlen++;
            }
            return strlen;
        }
        /// <summary>
        /// 获取中英文混排字符串的实际长度(字节数)
        /// </summary>
        /// <param name="str">要获取长度的字符串</param>
        /// <returns>字符串的实际长度值（字节数）</returns>
        public int getHStringLength(string str)
        {
            if (str.Equals(string.Empty))
                return 0;
            int strlen = 0;
            ASCIIEncoding strData = new ASCIIEncoding();
            //将字符串转换为ASCII编码的字节数字
            byte[] strBytes = strData.GetBytes(str);
            for (int i = 0; i <= strBytes.Length - 1; i++)
            {
                if (strBytes[i] == 63)  //中文都将编码为ASCII编码63,即"?"号
                    strlen++;
            }
            return strlen;
        }
        public  int getpos(int style,string str)
        {
            int len = NumberOfDigits(str);
            len = str.Length - len + (int)Math.Ceiling((Double)len/(Double)2);
            int nPos = 0;
            if (style == 0)
            {//居中显示
                nPos = len /2; //一行宽度为42个字符
            } else if (style == 1) {//居右显示
                nPos =len;
            }
            return nPos;
        }
        /// <summary>
        /// 判断字符串中是否包含中文
        /// </summary>
        /// <param name="str">需要判断的字符串</param>
        /// <returns>判断结果</returns>
        public bool HasChinese(string str)
        {
            return Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
        }

        //档口小票的打印0
        public void windowPrint(string printname)
        {
            lock (thisLock)
            {
                Console.WriteLine("档口小票的打印:" + printname);
                int vindex = printname.IndexOf("ip");
                int cindex = printname.IndexOf("COM");
                int uindex = printname.IndexOf("USB\\VID");
                int lindex = printname.IndexOf("LPT");
                BizPrinter dao = new BizPrinter();

                dd_printers printer = dao.QueryPrinters(2, printname).FirstOrDefault();
                if (vindex >= 0)
                {
                    printname = printname.Substring(vindex + 2);
                    //打印的IP一定要预先设置好
                    
                    bool b = opos.OpenNetPort(printname);//"192.168.1.254"
                    if (!b)
                    {
                        Console.WriteLine("初始化'" + printname + "'的打印机参数失败。请检测打印机配置");
                        b = opos.OpenNetPort(printname);//"192.168.1.254"
                        Thread.Sleep(1000);
                        if (!b)
                        {
                            Console.WriteLine("初始化'" + printname + "'的打印机参数失败。请检测打印机配置");

                            utils.ShowTip("警告", "初始化'" + printname + "'的打印机参数失败。请检测打印机配置", 5000);
                            //form.showmsg("初始化'" + printname + "'的打印机参数失败。请检测打印机配置");
                            return;
                        }

                    }


                    Byte res = new Byte();
                    int ret = BeiYangOPOS.POS_NETQueryStatus(printname, out res);
                    StringBuilder sb = new StringBuilder();
                    #region 检测打印机状态
                    if ((res & 0x10) == 0x10)
                    {
                        sb.AppendLine("打印机出错！");
                    }
                    if ((res & 0x02) == 0x02)
                    {
                        sb.AppendLine("打印机脱机！");
                    }
                    if ((res & 0x04) == 0x04)
                    {
                        sb.AppendLine("上盖打开！");
                    }
                    if ((res & 0x20) == 0x20)
                    {
                        sb.AppendLine("切刀出错！");
                    }
                    if ((res & 0x40) == 0x40)
                    {
                        sb.AppendLine("纸将尽！");
                    }
                    if ((res & 0x80) == 0x80)
                    {
                        sb.AppendLine("缺纸！");
                    }
                    #endregion
                    if (sb.Length > 0)
                    {
                        string errormsg =("'"+ printname + "'的打印机处于非正常状态："+ sb.ToString() + "。请检测打印机配置。");
                        Console.WriteLine("Error", errormsg);
                        if (pcontent != null)
                        {
                            JObject main = (JObject)pcontent["main"];
                            try
                            {
                                dao.addPrintQueue(printname, JsonUtils.ObjectToJson(pcontent), pcontent["cfmainkey"].ToString(), 0);//0档口小票1，l划单小票
                            }
                            catch (Exception e)
                            {
                                log.Error(errormsg);
                            }

                        }
                        utils.ShowTip("警告", errormsg, 5000);
                        return;
                    }
                    windowPrintPage(printname, printer);
                    return;
                }
                else if (cindex >= 0)
                {
                    //printname = printname.Substring(cindex + 4);
                    SerialPort com = new SerialPort();
                    com.BaudRate = printer.pbites.Value;
                    com.PortName = printname;
                    com.DataBits = 8;
                    bool b = opos.OpenComPort(ref com);
                    if (!b)
                    {
                        Console.WriteLine("初始化'{0}'的打印机参数失败。请检测打印机配置1");
                        b = opos.OpenComPort(ref com);
                        if (!b)
                        {
                            string errormsg = string.Format("初始化'{0}'的打印机参数失败。请检测打印机配置2",
                                            printname);
                            Console.WriteLine(errormsg);
                            utils.ShowTip("警告", errormsg, 5000);
                            return;
                        }
                    }

                    windowPrintPage(printname, printer);
                    return;
                }
                else if (uindex >= 0)
                {
                    windowUsbPrintPage(printname, printer);
                    return;
                }
                else if (lindex >= 0)
                {
                    windowLptPrintPage(printname, printer);
                    return;
                }
                //string status=PrintUtil.GetPrinterStatus(printname);
                // Console.WriteLine(printname+"结款小票的打印:" + status);
                if (printname == null || "".Equals(printname) || "default".Equals(printname))
                {
                    //this.printDocument1.PrinterSettings.PrinterName = printname;
                }
                else
                {
                    this.printDocument1.PrinterSettings.PrinterName = printname;
                }
                // SPrinterStatus status = PrintUtil.getStatus(printname);
                //PrintQueue pq = LocalPrintServer.GetDefaultPrintQueue();
                // if (status.IndexOf("打印纸用完") >= 0) {
                //     Console.WriteLine("打印纸用完");
                //    return;
                // }
                this.printDocument1.DocumentName = "test窗口票" + DateTime.Now.TimeOfDay.ToString();
                printDocument1.PrintPage +=

                new PrintPageEventHandler(this.printDocument1_windowPrintPage);
                this.printDocument1.Print();
            }
        }
       
        private void windowPrintPage(string printname, dd_printers printer)
        {
          
          
                if (pcontent != null)
                {
                    JObject main = (JObject)pcontent["main"];

                String isyicaiyidan ="0";//是否开启一单一打
                if (pcontent.Keys.Contains("isyicaiyidan")) {
                    isyicaiyidan = (String)pcontent["isyicaiyidan"];
                }

                #region 执行指令打印
                uint width =2;
               
                int psize = printer.psize.Value;
                int pwidth = psize;
                string linestr = "------------------------------------------------";
                if (psize == 58)
                {
                    BeiYangOPOS.POS_SetLineSpacing(30);
                    width =1;
                    pwidth = 190;
                    linestr = "--------------------------------";
                }
                else if (psize == 70)
                {
                    width = 2;
                    pwidth = 250;
                    linestr = "-----------------------------------------";
                }
                else if (psize == 76)
                {
                    width =2;
                    pwidth = 270;
                    linestr = "---------------------------------------------";
                }
                else if (psize == 80)
                {
                    width = 2;
                    pwidth =286;
                }

                BeiYangOPOS.POS_SetRightSpacing(0);
                BeiYangOPOS.POS_SetLineSpacing(30);
                if (isyicaiyidan.Equals("0"))
                {
                    
                    int tlength = getStringLength("点点菜单");
                    BeiYangOPOS.POS_S_TextOut("点点菜单", (uint)(pwidth - tlength *6) - 12 * width, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();

                    string tablename = main["tablename"].ToString();
                    bool _actual1 = HasChinese(tablename);
                    string serialno = main["serialno"].ToString();
                    try
                    {
                        int no = Convert.ToInt32(serialno);
                        serialno = string.Format("{0,-10:D3}", no);
                    }
                    catch (Exception es)
                    {

                        Console.WriteLine("结款小票的打印:" + es.Message);
                    }
                    if (_actual1)
                    {
                        tablename = tablename + " " + serialno;
                    }
                    else
                    {
                        tablename = tablename + "号桌 " + serialno;
                    }
                   
                    int talength = getStringLength(tablename);
                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_S_TextOut(tablename, (uint)(pwidth - tlength * 12) - 12 * width, width, 2, opos.POS_FONT_TYPE_CHINESE, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_S_TextOut("订单编号", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    int olength = getStringLength(main["orderno"].ToString());
                    BeiYangOPOS.POS_S_TextOut(main["orderno"].ToString(), (uint)(pwidth * 2 - olength * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_S_TextOut("点餐时间", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    int dclength = getStringLength(main["addtime"].ToString());
                    BeiYangOPOS.POS_S_TextOut(main["addtime"].ToString(), (uint)(pwidth * 2 - dclength * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_S_TextOut("用餐时间", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    int sclength = getStringLength(main["sctime"].ToString());
                    BeiYangOPOS.POS_S_TextOut(main["sctime"].ToString(), (uint)(pwidth * 2 - sclength * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_S_TextOut("项目", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    
                    BeiYangOPOS.POS_S_TextOut("数量", (uint)(pwidth - getpos(1, "数量") * 12) * 2, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();
                    JArray zitems = (JArray)pcontent["zitems"];
                    if (zitems.Count > 0)
                    {
                        for (int i = 0; i < zitems.Count; i++)
                        {
                            //菜品打印h20
                            string num = zitems[i]["num"].ToString();
                            string price = zitems[i]["price"].ToString();
                            if (zitems[i]["weight"] != null)
                            {
                                string weight = zitems[i]["weight"].ToString();
                                float weig = float.Parse(weight);
                                if (weig > 0)
                                {
                                    num = weig + "";

                                    price = float.Parse(price) / weig + "";
                                }
                            }
                            string zuofa = zitems[i]["zuofa"].ToString();
                            string name = zitems[i]["name"].ToString();
                           
                            object isprint = zitems[i]["isprint"];
                            if (isprint != null && !"0".Equals(isprint.ToString()))
                            {
                                continue;
                            }
                            if (zitems[i]["isset"] != null)
                            {
                                string isset = zitems[i]["isset"].ToString();
                                if (isset.Equals("1"))
                                {//如果套餐不打印
                                    continue;
                                }
                            }

                           


                            if (zitems[i]["guigename"] != null && !zitems[i]["guigename"].ToString().Equals(""))
                            {
                                name += "(" + zitems[i]["guigename"].ToString() + ")";
                            }
                           
                            if (zitems[i]["istaocan"] != null)
                            {
                                string istaocan = zitems[i]["istaocan"].ToString();
                                if (istaocan.Equals("0"))
                                {//如果套餐加特殊标记
                                    //name = "△" + name;
                                    name = name + "(套)";
                                }
                            }
                            if (zitems[i]["unit"] != null && !"".Equals(zitems[i]["unit"].ToString()))
                            {
                                name = name + "/" + zitems[i]["unit"];
                            }
                            string ename = "";
                            if (name.Length > 10)
                            {

                                ename = name.Substring(10, name.Length - 10);
                                name = name.Substring(0, 10);
                            }
                            BeiYangOPOS.POS_S_TextOut(name, 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                            num = string.Format("{0,6}", num);
                            int flength = getStringLength(num);
                            BeiYangOPOS.POS_S_TextOut(num, (uint)(pwidth * 2 - flength * 12 * width), width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                             


                            BeiYangOPOS.POS_FeedLine();
                            if (!ename.Equals(""))
                            {
                                BeiYangOPOS.POS_S_TextOut(ename, 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                                BeiYangOPOS.POS_FeedLine();
                            }

                            if (zuofa != null && !"".Equals(zuofa))
                            {
                                BeiYangOPOS.POS_S_TextOut("    <"+zuofa+">", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                                BeiYangOPOS.POS_FeedLine();
                            }

                        }
                    }
                    BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();
                    string beizhu = "备注:" + main["remark"] + main["customremark"];
                    BeiYangOPOS.POS_S_TextOut(beizhu, 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
 
                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_CutPaper(1, 50);
                }
                else {
                    JArray zitems = (JArray)pcontent["zitems"];
                    if (zitems.Count > 0)
                    {
                        for (int i = 0; i < zitems.Count; i++)
                        {
                            int tlength = getStringLength("点点菜单");
                            BeiYangOPOS.POS_S_TextOut("点点菜单", (uint)(pwidth - tlength *6) - 12 * width, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                            BeiYangOPOS.POS_FeedLine();

                            string tablename = main["tablename"].ToString();
                            bool _actual1 = HasChinese(tablename);
                            string serialno = main["serialno"].ToString();
                            try
                            {
                                int no = Convert.ToInt32(serialno);
                                serialno = string.Format("{0,-10:D3}", no);
                            }
                            catch (Exception es)
                            {

                                Console.WriteLine("结款小票的打印:" + es.Message);
                            }
                            if (_actual1)
                            {
                                tablename = tablename + " " + serialno;
                            }
                            else
                            {
                                tablename = tablename + "号桌 " + serialno;
                            }

                            int talength = getStringLength(tablename);
                            BeiYangOPOS.POS_FeedLine();
                            BeiYangOPOS.POS_S_TextOut(tablename, (uint)(pwidth - tlength * 12) - 12 * width, width, 2, opos.POS_FONT_TYPE_CHINESE, opos.POS_FONT_STYLE_NORMAL);
                            BeiYangOPOS.POS_FeedLine();
                            BeiYangOPOS.POS_S_TextOut("订单编号", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                            int olength = getStringLength(main["orderno"].ToString());
                            BeiYangOPOS.POS_S_TextOut(main["orderno"].ToString(), (uint)(pwidth * 2 - olength * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                            BeiYangOPOS.POS_FeedLine();
                            BeiYangOPOS.POS_S_TextOut("点餐时间", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                            int dclength = getStringLength(main["addtime"].ToString());
                            BeiYangOPOS.POS_S_TextOut(main["addtime"].ToString(), (uint)(pwidth * 2 - dclength * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                            BeiYangOPOS.POS_FeedLine();
                            BeiYangOPOS.POS_S_TextOut("用餐时间", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                            int sclength = getStringLength(main["sctime"].ToString());
                            BeiYangOPOS.POS_S_TextOut(main["sctime"].ToString(), (uint)(pwidth * 2 - sclength * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                            BeiYangOPOS.POS_FeedLine();
                            BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                            BeiYangOPOS.POS_FeedLine();
                            BeiYangOPOS.POS_S_TextOut("项目", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                            BeiYangOPOS.POS_S_TextOut("数量", (uint)(pwidth - getpos(1, "数量") * 12) * 2, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                            BeiYangOPOS.POS_FeedLine();
                            BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();
                   
                            //菜品打印h20
                            string num = zitems[i]["num"].ToString();
                            string price = zitems[i]["price"].ToString();
                            if (zitems[i]["weight"] != null)
                            {
                                string weight = zitems[i]["weight"].ToString();
                                float weig = float.Parse(weight);
                                if (weig > 0)
                                {
                                    num = weig + "";

                                    price = float.Parse(price) / weig + "";
                                }
                            }
                            string zuofa = zitems[i]["zuofa"].ToString();
                            string name = zitems[i]["name"].ToString();
                          
                            object isprint = zitems[i]["isprint"];
                            if (isprint != null && !"0".Equals(isprint.ToString()))
                            {
                                continue;
                            }
                            if (zitems[i]["isset"] != null)
                            {
                                string isset = zitems[i]["isset"].ToString();
                                if (isset.Equals("1"))
                                {//如果套餐不打印
                                    continue;
                                }
                            }

                           


                            if (zitems[i]["guigename"] != null && !zitems[i]["guigename"].ToString().Equals(""))
                            {
                                name += "(" + zitems[i]["guigename"].ToString() + ")";
                            }
                            if (zitems[i]["istaocan"] != null)
                            {
                                string istaocan = zitems[i]["istaocan"].ToString();
                                if (istaocan.Equals("0"))
                                {//如果套餐加特殊标记
                                    //name = "△" + name;
                                    name = name + "(套)";
                                }
                            }
                            if (zitems[i]["unit"] != null && !"".Equals(zitems[i]["unit"].ToString()))
                            {
                                name = name + "/" + zitems[i]["unit"];
                            }
                            string ename = "";
                            if (name.Length > 10)
                            {

                                ename = name.Substring(10, name.Length - 10);
                                name = name.Substring(0, 10);
                            }
                            BeiYangOPOS.POS_S_TextOut(name, 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                            num = string.Format("{0,6}", num);
                            int flength = getStringLength(num);
                            BeiYangOPOS.POS_S_TextOut(num, (uint)(pwidth * 2 - flength * 12 * width), width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                            BeiYangOPOS.POS_FeedLine();
                            if (!ename.Equals(""))
                            {
                                BeiYangOPOS.POS_S_TextOut(ename, 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                                BeiYangOPOS.POS_FeedLine();
                            }
                            if (zuofa != null && !"".Equals(zuofa))
                            {
                                BeiYangOPOS.POS_S_TextOut("    <" + zuofa + ">", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                                BeiYangOPOS.POS_FeedLine();
                            }

                            BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();
                    string beizhu = "备注:" + main["remark"] + main["customremark"];
                    BeiYangOPOS.POS_S_TextOut(beizhu, 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                 
                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_CutPaper(1, 50);
                        }
                    }
                }
                   
                opos.ClosePrinterPort();
                #endregion
            }

            

        }
        private void windowUsbPrintPage(string printname, dd_printers printer)
        {


            if (pcontent != null)
            {
                JObject main = (JObject)pcontent["main"];
                String isyicaiyidan = "0";//是否开启一单一打
                if (pcontent.Keys.Contains("isyicaiyidan"))
                {
                    isyicaiyidan = (String)pcontent["isyicaiyidan"];
                }

                #region 执行指令打印

                int psize = printer.psize.Value;
                int pwidth = psize;
                string linestr = "------------------------------------------------";
                if (psize == 58)
                {
                    pwidth = 190;
                    linestr = "--------------------------------";
                }
                else if (psize == 70)
                {
                    
                    pwidth = 250;
                    linestr = "-----------------------------------------";
                }
                else if (psize == 76)
                {
                   
                    pwidth = 260;
                    linestr = "----------------------------------------";
                }
                else if (psize == 80)
                {
                    
                    pwidth = 260;
                }
                using (var sh = UsbPrinterResolver.OpenUSBPrinterBydeviceId(printname))
                {
                    using (var f = new System.IO.FileStream(sh, System.IO.FileAccess.ReadWrite))
                    {


                        if (isyicaiyidan.Equals("0"))
                        {
                            OnWriteData(f, "点点菜单\n\n", false, false, false, 2);

                            string tablename = main["tablename"].ToString();
                            bool _actual1 = HasChinese(tablename);
                            string serialno = main["serialno"].ToString();
                            try
                            {
                                int no = Convert.ToInt32(serialno);
                                serialno = string.Format("{0,-10:D3}", no);
                            }
                            catch (Exception es)
                            {

                                Console.WriteLine("结款小票的打印:" + es.Message);
                            }
                            if (_actual1)
                            {
                                tablename = tablename + " " + serialno;
                            }
                            else
                            {
                                tablename = tablename + "号桌 " + serialno;
                            }
                            OnWriteData(f, "      " + tablename + "\n\n", true, true, false, 2);
                            //OnWriteData(f, "订单编号", false, false, false,1);
                            string orderno = main["orderno"].ToString();
                            orderno = string.Format("{0,19}", orderno);
                            
                           
                            OnWriteData(f, "订单编号     " + orderno + "\n", false, false, false, 1);
                            
                            OnWriteData(f, "点餐时间             " + main["addtime"].ToString() + "\n", false, false, false, 1);
                            //OnWriteData(f, "用餐时间", false, false, false, 1);
                            if (main["sctime"].ToString().Length < 3)
                                OnWriteData(f, "用餐时间                    " + main["sctime"].ToString() + "\n", false, false, false, 1);
                            else
                                OnWriteData(f, "用餐时间                " + main["sctime"].ToString() + "\n", false, false, false, 1);
                            OnWriteData(f, linestr + "\n", false, false, false, 1);
                            //OnWriteData(f, "项目", false, false, false, 1);
                            OnWriteData(f, "项目                        " + "数量\n", false, false, false, 1);
                            OnWriteData(f, linestr + "\n", false, false, false, 1);

                            JArray zitems = (JArray)pcontent["zitems"];
                            if (zitems.Count > 0)
                            {
                                for (int i = 0; i < zitems.Count; i++)
                                {
                                    //菜品打印h20
                                    string num = zitems[i]["num"].ToString();
                                    string price = zitems[i]["price"].ToString();
                                    if (zitems[i]["weight"] != null)
                                    {
                                        string weight = zitems[i]["weight"].ToString();
                                        float weig = float.Parse(weight);
                                        if (weig > 0)
                                        {
                                            num = weig + "";

                                            price = float.Parse(price) / weig + "";
                                        }
                                    }
                                    string name = zitems[i]["name"].ToString();
                                  
                                    string zuofa = zitems[i]["zuofa"].ToString();
                                    string ename = "";//为了菜名过长设定，截断字符串
                                    object isprint = zitems[i]["isprint"];
                                    if (isprint != null && !"0".Equals(isprint.ToString()))
                                    {
                                        continue;
                                    }
                                    if (zitems[i]["isset"] != null)
                                    {
                                        string isset = zitems[i]["isset"].ToString();
                                        if (isset.Equals("1"))
                                        {//如果套餐不打印
                                            continue;
                                        }
                                    }
                                   
                                    if (zitems[i]["guigename"] != null && !zitems[i]["guigename"].ToString().Equals(""))
                                    {
                                        name += "（" + zitems[i]["guigename"].ToString() + "）";
                                        // nPos=nPos + 1;
                                    }
                                    if (zitems[i]["istaocan"] != null)
                                    {
                                        string istaocan = zitems[i]["istaocan"].ToString();
                                        if (istaocan.Equals("0"))
                                        {//如果套餐加特殊标记
                                            //name = "△" + name;
                                            name = name + "(套)";
                                        }
                                    }
                                    if (zitems[i]["unit"] != null && !"".Equals(zitems[i]["unit"].ToString()))
                                    {
                                        name = name + "/" + zitems[i]["unit"];
                                    }
                                    var slen =getStringLength(name);
                                    if (slen >24)
                                    {
                                        
                                         ename = name.Substring(12, name.Length - 12);
                                        name = name.Substring(0, 12);
                                        int chakong = 24 - getStringLength(name);
                                        //name = string.Format("{0,-12}", name);
                                        //name = name.Replace(" ", "  ");

                                        if (chakong > 0)
                                        {
                                            string kongge = string.Format("{0,-" + chakong + "}", "");
                                            name = name + kongge;
                                        }
                                    }
                                    else
                                    {

                                        int chakong = 24-getStringLength(name);
                                        //name = string.Format("{0,-12}", name);
                                        //name = name.Replace(" ", "  ");
                                        
                                        if (chakong > 0) {
                                            string kongge = string.Format("{0,-"+ chakong + "}", "");
                                            name= name+kongge;
                                        }
                                    }
                                    num = string.Format("{0,3}", num);
                                    // OnWriteData(f, name, true, true, false, 1);



                                    if (i == zitems.Count - 1)
                                        OnWriteData(f, name + "    " + num + ename + "\n", false, true, false, 1);
                                    else
                                        OnWriteData(f, name + "    " + num + ename + "\n", false, true, false, 1);
                                    if (zuofa != null && !"".Equals(zuofa))
                                    {
                                        OnWriteData(f, "    <" + zuofa + ">\n", false, false, false, 1);
                                    }
                                }
                            }
                            OnWriteData(f, linestr + "\n", false, false, false, 1);
                            string beizhu = "备注:" + main["remark"] + main["customremark"];
                            OnWriteData(f, beizhu, true, true, false, 1);

                            cutPage(f);

                        }
                        else {

                            JArray zitems = (JArray)pcontent["zitems"];
                            if (zitems.Count > 0)
                            {
                                for (int i = 0; i < zitems.Count; i++)
                                {
                                    OnWriteData(f, "点点菜单\n\n", false, false, false, 2);

                            string tablename = main["tablename"].ToString();
                            bool _actual1 = HasChinese(tablename);
                            string serialno = main["serialno"].ToString();
                            try
                            {
                                int no = Convert.ToInt32(serialno);
                                serialno = string.Format("{0,-10:D3}", no);
                            }
                            catch (Exception es)
                            {

                                Console.WriteLine("结款小票的打印:" + es.Message);
                            }
                            if (_actual1)
                            {
                                tablename = tablename + " " + serialno;
                            }
                            else
                            {
                                tablename = tablename + "号桌 " + serialno;
                            }
                            OnWriteData(f, "      " + tablename + "\n\n", true, true, false, 2);
                            //OnWriteData(f, "订单编号", false, false, false,1);
                            string orderno = main["orderno"].ToString();
                            if (orderno.Length > 14)
                            {
                                OnWriteData(f, "订单编号       " + main["orderno"].ToString() + "\n", false, false, false, 1);
                            }
                            else
                            {
                                OnWriteData(f, "订单编号          " + main["orderno"].ToString() + "\n", false, false, false, 1);
                            }
                            OnWriteData(f, "点餐时间             " + main["addtime"].ToString() + "\n", false, false, false, 1);
                            //OnWriteData(f, "用餐时间", false, false, false, 1);
                            if (main["sctime"].ToString().Length < 3)
                                OnWriteData(f, "用餐时间                    " + main["sctime"].ToString() + "\n", false, false, false, 1);
                            else
                                OnWriteData(f, "用餐时间                " + main["sctime"].ToString() + "\n", false, false, false, 1);
                            OnWriteData(f, linestr + "\n", false, false, false, 1);
                            //OnWriteData(f, "项目", false, false, false, 1);
                            OnWriteData(f, "项目                        " + "数量\n", false, false, false, 1);
                            OnWriteData(f, linestr + "\n", false, false, false, 1);

                           
                                    //菜品打印h20
                                    string num = zitems[i]["num"].ToString();
                                    string price = zitems[i]["price"].ToString();
                                    if (zitems[i]["weight"] != null)
                                    {
                                        string weight = zitems[i]["weight"].ToString();
                                        float weig = float.Parse(weight);
                                        if (weig > 0)
                                        {
                                            num = weig + "";

                                            price = float.Parse(price) / weig + "";
                                        }
                                    }
                                    string zuofa = zitems[i]["zuofa"].ToString();
                                    string name = zitems[i]["name"].ToString();
                                   
                                    string ename = "";//为了菜名过长设定，截断字符串
                                    object isprint = zitems[i]["isprint"];
                                    if (isprint != null && !"0".Equals(isprint.ToString()))
                                    {
                                        continue;
                                    }
                                    if (zitems[i]["isset"] != null)
                                    {
                                        string isset = zitems[i]["isset"].ToString();
                                        if (isset.Equals("1"))
                                        {//如果套餐不打印
                                            continue;
                                        }
                                    }
                                    
                                    if (zitems[i]["guigename"] != null && !zitems[i]["guigename"].ToString().Equals(""))
                                    {
                                        name += "（" + zitems[i]["guigename"].ToString() + "）";
                                        // nPos=nPos + 1;
                                    }
                                    if (zitems[i]["istaocan"] != null)
                                    {
                                        string istaocan = zitems[i]["istaocan"].ToString();
                                        if (istaocan.Equals("0"))
                                        {//如果套餐加特殊标记
                                            //name = "△" + name;
                                            name = name + "(套)";
                                        }
                                    }
                                    if (zitems[i]["unit"] != null && !"".Equals(zitems[i]["unit"].ToString()))
                                    {
                                        name = name + "/" + zitems[i]["unit"];
                                    }
                                    var slen = getStringLength(name);
                                    if (slen > 24)
                                    {

                                        ename = name.Substring(12, name.Length - 12);
                                        name = name.Substring(0, 12);
                                        int chakong = 24 - getStringLength(name);
                                        //name = string.Format("{0,-12}", name);
                                        //name = name.Replace(" ", "  ");

                                        if (chakong > 0)
                                        {
                                            string kongge = string.Format("{0,-" + chakong + "}", "");
                                            name = name + kongge;
                                        }
                                    }
                                    else
                                    {

                                        int chakong = 24 - getStringLength(name);
                                        //name = string.Format("{0,-12}", name);
                                        //name = name.Replace(" ", "  ");

                                        if (chakong > 0)
                                        {
                                            string kongge = string.Format("{0,-" + chakong + "}", "");
                                            name = name + kongge;
                                        }
                                    }
                                    num = string.Format("{0,3}", num);
                                    // OnWriteData(f, name, true, true, false, 1);



                                    if (i == zitems.Count - 1)
                                        OnWriteData(f, name + "    " + num + ename + "\n", false, true, false, 1);
                                    else
                                        OnWriteData(f, name + "    " + num + ename + "\n", false, true, false, 1);
                                    if (zuofa != null && !"".Equals(zuofa))
                                    {
                                        OnWriteData(f, "    <" + zuofa + ">\n", false, false, false, 1);
                                    }
                                    OnWriteData(f, linestr + "\n", false, false, false, 1);
                            string beizhu = "备注:" + main["remark"] + main["customremark"]+ "\n\n";
                            OnWriteData(f, beizhu, true, true, false, 1);

                            cutPage(f);
                                }
                            }
                        }

                       
                        #endregion
                    }
                }
            }


        }
        private void windowLptPrintPage(string printname, dd_printers printer)
        {


            if (pcontent != null)
            {
                JObject main = (JObject)pcontent["main"];
                
                String isyicaiyidan = "0";//是否开启一单一打
                if (pcontent.Keys.Contains("isyicaiyidan"))
                {
                    isyicaiyidan = (String)pcontent["isyicaiyidan"];
                }
                #region 执行指令打印

                int psize = printer.psize.Value;
                int pwidth = psize;
                string linestr = "------------------------------------------------";
                string kongestr = "";
                if (psize == 58)
                {
                    pwidth = 190;
                    linestr = "--------------------------------";
                }
                else if (psize == 70)
                {
                    
                    pwidth = 250;
                    linestr = "-----------------------------------------";
                }
                else if (psize == 76)
                {

                    pwidth = 260;
                    linestr = "----------------------------------------";
                }
                else if (psize == 80)
                {
                    kongestr = "                ";
                    pwidth = 260;
                }

                LPTControls lpt = new LPTControls();
                lpt.Open(printname);
                if (isyicaiyidan.Equals("0"))
                {
                    lpt.OnWriteData("点点菜单\n\n", false, false, false, 2);

                    string tablename = main["tablename"].ToString();
                    bool _actual1 = HasChinese(tablename);
                    string serialno = main["serialno"].ToString();
                    try
                    {
                        int no = Convert.ToInt32(serialno);
                        serialno = string.Format("{0,-10:D3}", no);
                    }
                    catch (Exception es)
                    {

                        Console.WriteLine("结款小票的打印:" + es.Message);
                    }
                    if (_actual1)
                    {
                        tablename = tablename + " " + serialno;
                    }
                    else
                    {
                        tablename = tablename + "号桌 " + serialno;
                    }
                    lpt.OnWriteData("      " + tablename + "\n\n", true, true, false, 2);
                    //OnWriteData(f, "订单编号", false, false, false,1);
                    string orderno = main["orderno"].ToString();
                    if (orderno.Length > 14)
                    {
                        lpt.OnWriteData("订单编号       " + kongestr + main["orderno"].ToString() + "\n", false, false, false, 1);
                    }
                    else
                    {
                        lpt.OnWriteData("订单编号          " + kongestr + main["orderno"].ToString() + "\n", false, false, false, 1);
                    }
                    lpt.OnWriteData("点餐时间             " + kongestr + main["addtime"].ToString() + "\n", false, false, false, 1);
                    //OnWriteData(f, "用餐时间", false, false, false, 1);
                    if (main["sctime"].ToString().Length < 3)
                        lpt.OnWriteData("用餐时间                    " + kongestr + main["sctime"].ToString() + "\n", false, false, false, 1);
                    else
                        lpt.OnWriteData("用餐时间                " + kongestr + main["sctime"].ToString() + "\n", false, false, false, 1);
                    lpt.OnWriteData(linestr + "\n", false, false, false, 1);
                    //OnWriteData(f, "项目", false, false, false, 1);
                    lpt.OnWriteData("项目                        " + kongestr + "数量\n", false, false, false, 1);
                    lpt.OnWriteData(linestr + "\n", false, false, false, 1);

                    JArray zitems = (JArray)pcontent["zitems"];
                    if (zitems.Count > 0)
                    {
                        for (int i = 0; i < zitems.Count; i++)
                        {
                            //菜品打印h20
                            string num = zitems[i]["num"].ToString();
                            string price = zitems[i]["price"].ToString();
                            if (zitems[i]["weight"] != null)
                            {
                                string weight = zitems[i]["weight"].ToString();
                                float weig = float.Parse(weight);
                                if (weig > 0)
                                {
                                    num = weig + "";

                                    price = float.Parse(price) / weig + "";
                                }
                            }
                            string name = zitems[i]["name"].ToString();
                           
                            string zuofa = zitems[i]["zuofa"].ToString();
                            object isprint = zitems[i]["isprint"];
                            if (isprint != null && !"0".Equals(isprint.ToString()))
                            {
                                continue;
                            }
                            string ename = "";//为了菜名过长设定，截断字符串
                            if (zitems[i]["isset"] != null)
                            {
                                string isset = zitems[i]["isset"].ToString();
                                if (isset.Equals("1"))
                                {//如果套餐不打印
                                    continue;
                                }
                            }
                            
                            if (zitems[i]["guigename"] != null && !zitems[i]["guigename"].ToString().Equals(""))
                            {
                                name += "（" + zitems[i]["guigename"].ToString() + "）";
                                // nPos=nPos + 1;
                            }
                            if (zitems[i]["istaocan"] != null)
                            {
                                string istaocan = zitems[i]["istaocan"].ToString();
                                if (istaocan.Equals("0"))
                                {//如果套餐加特殊标记
                                    //name = "△" + name;
                                    name = name + "(套)";
                                }
                            }
                            if (zitems[i]["unit"] != null && !"".Equals(zitems[i]["unit"].ToString()))
                            {
                                name = name + "/" + zitems[i]["unit"];
                            }
                            var slen = getStringLength(name);
                            if (slen > 24)
                            {

                                ename = name.Substring(12, name.Length - 12);
                                name = name.Substring(0, 12);
                                int chakong = 24 - getStringLength(name);
                                //name = string.Format("{0,-12}", name);
                                //name = name.Replace(" ", "  ");

                                if (chakong > 0)
                                {
                                    string kongge = string.Format("{0,-" + chakong + "}", "");
                                    name = name + kongge;
                                }
                            }
                            else
                            {

                                int chakong = 24 - getStringLength(name);
                                //name = string.Format("{0,-12}", name);
                                //name = name.Replace(" ", "  ");

                                if (chakong > 0)
                                {
                                    string kongge = string.Format("{0,-" + chakong + "}", "");
                                    name = name + kongge;
                                }
                            }
                            num = string.Format("{0,3}", num);
                            // OnWriteData(f, name, true, true, false, 1);



                            if (i == zitems.Count - 1)
                                lpt.OnWriteData(name + "    " + kongestr + num + ename + "\n", false, true, false, 1);
                            else
                                lpt.OnWriteData(name + "    " + kongestr + num + ename + "\n\n", false, true, false, 1);
                            if ( zuofa!= null && !"".Equals(zuofa)) {
                                lpt.OnWriteData("    <"+ zuofa + ">\n\n", false, false, false, 1);
                            }
                        }
                    }
                    lpt.OnWriteData(linestr + "\n", false, false, false, 1);
                    string beizhu = "备注:" + main["remark"] + main["customremark"];
                    lpt.OnWriteData(beizhu, true, true, false, 1);

                    lpt.CutPaper();
                }
                else {

                    JArray zitems = (JArray)pcontent["zitems"];
                    if (zitems.Count > 0)
                    {
                        for (int i = 0; i < zitems.Count; i++)
                        {
                            lpt.OnWriteData("点点菜单\n\n", false, false, false, 2);

                    string tablename = main["tablename"].ToString();
                    bool _actual1 = HasChinese(tablename);
                    string serialno = main["serialno"].ToString();
                    try
                    {
                        int no = Convert.ToInt32(serialno);
                        serialno = string.Format("{0,-10:D3}", no);
                    }
                    catch (Exception es)
                    {

                        Console.WriteLine("结款小票的打印:" + es.Message);
                    }
                    if (_actual1)
                    {
                        tablename = tablename + " " + serialno;
                    }
                    else
                    {
                        tablename = tablename + "号桌 " + serialno;
                    }
                    lpt.OnWriteData("      " + tablename + "\n\n", true, true, false, 2);
                    //OnWriteData(f, "订单编号", false, false, false,1);
                    string orderno = main["orderno"].ToString();
                    if (orderno.Length > 14)
                    {
                        lpt.OnWriteData("订单编号       " + kongestr + main["orderno"].ToString() + "\n", false, false, false, 1);
                    }
                    else
                    {
                        lpt.OnWriteData("订单编号          " + kongestr + main["orderno"].ToString() + "\n", false, false, false, 1);
                    }
                    lpt.OnWriteData("点餐时间             " + kongestr + main["addtime"].ToString() + "\n", false, false, false, 1);
                    //OnWriteData(f, "用餐时间", false, false, false, 1);
                    if (main["sctime"].ToString().Length < 3)
                        lpt.OnWriteData("用餐时间                    " + kongestr + main["sctime"].ToString() + "\n", false, false, false, 1);
                    else
                        lpt.OnWriteData("用餐时间                " + kongestr + main["sctime"].ToString() + "\n", false, false, false, 1);
                    lpt.OnWriteData(linestr + "\n", false, false, false, 1);
                    //OnWriteData(f, "项目", false, false, false, 1);
                    lpt.OnWriteData("项目                        " + kongestr + "数量\n", false, false, false, 1);
                    lpt.OnWriteData(linestr + "\n", false, false, false, 1);

                            //菜品打印h20
                            string num = zitems[i]["num"].ToString();
                            string price = zitems[i]["price"].ToString();
                            if (zitems[i]["weight"] != null)
                            {
                                string weight = zitems[i]["weight"].ToString();
                                float weig = float.Parse(weight);
                                if (weig > 0)
                                {
                                    num = weig + "";

                                    price = float.Parse(price) / weig + "";
                                }
                            }
                            string name = zitems[i]["name"].ToString();
                           
                            string zuofa = zitems[i]["zuofa"].ToString();
                            object isprint = zitems[i]["isprint"];
                            if (isprint != null && !"0".Equals(isprint.ToString()))
                            {
                                continue;
                            }
                            string ename = "";//为了菜名过长设定，截断字符串
                            if (zitems[i]["isset"] != null)
                            {
                                string isset = zitems[i]["isset"].ToString();
                                if (isset.Equals("1"))
                                {//如果套餐不打印
                                    continue;
                                }
                            }
                            
                            if (zitems[i]["guigename"] != null && !zitems[i]["guigename"].ToString().Equals(""))
                            {
                                name += "（" + zitems[i]["guigename"].ToString() + "）";
                                // nPos=nPos + 1;
                            }
                            if (zitems[i]["istaocan"] != null)
                            {
                                string istaocan = zitems[i]["istaocan"].ToString();
                                if (istaocan.Equals("0"))
                                {//如果套餐加特殊标记
                                    //name = "△" + name;
                                    name = name + "(套)";
                                }
                            }
                            if (zitems[i]["unit"] != null && !"".Equals(zitems[i]["unit"].ToString()))
                            {
                                name = name + "/" + zitems[i]["unit"];
                            }
                            var slen = getStringLength(name);
                            if (slen > 24)
                            {

                                ename = name.Substring(12, name.Length - 12);
                                name = name.Substring(0, 12);
                                int chakong = 24 - getStringLength(name);
                                //name = string.Format("{0,-12}", name);
                                //name = name.Replace(" ", "  ");

                                if (chakong > 0)
                                {
                                    string kongge = string.Format("{0,-" + chakong + "}", "");
                                    name = name + kongge;
                                }
                            }
                            else
                            {

                                int chakong = 24 - getStringLength(name);
                                //name = string.Format("{0,-12}", name);
                                //name = name.Replace(" ", "  ");

                                if (chakong > 0)
                                {
                                    string kongge = string.Format("{0,-" + chakong + "}", "");
                                    name = name + kongge;
                                }
                            }
                            num = string.Format("{0,3}", num);
                            // OnWriteData(f, name, true, true, false, 1);



                            if (i == zitems.Count - 1)
                                lpt.OnWriteData(name + "    " + kongestr + num + ename + "\n", false, true, false, 1);
                            else
                                lpt.OnWriteData(name + "    " + kongestr + num + ename + "\n\n", false, true, false, 1);
                            if (zuofa != null && !"".Equals(zuofa))
                            {
                                lpt.OnWriteData("    <" + zuofa + ">\n\n", false, false, false, 1);
                            }
                            lpt.OnWriteData(linestr + "\n", false, false, false, 1);
                    string beizhu = "备注:" + main["remark"] + main["customremark"];
                    lpt.OnWriteData(beizhu, true, true, false, 1);

                    lpt.CutPaper();
                        }
                    }
                }
               
                lpt.Close();
                #endregion
            }
               


        }
        private void printDocument1_windowPrintPage(object sender, PrintPageEventArgs e)
        {
            if (pcontent != null)
            {
                JObject main = (JObject)pcontent["main"];
                String isyicaiyidan = "0";//是否开启一单一打
                if (pcontent.Keys.Contains("isyicaiyidan"))
                {
                    isyicaiyidan = (String)pcontent["isyicaiyidan"];
                }
                e.Graphics.Clear(Color.White);
                // 开始绘制文档  
                // 默认为横排文字  
                Graphics g = form.CreateGraphics();
                Font cfont = new Font(new FontFamily("宋体"), 9, FontStyle.Bold);
                Font tfont = new Font(new FontFamily("宋体"), 16, FontStyle.Bold);
                Font cpfont = new Font(new FontFamily("宋体"), 13, FontStyle.Bold);
                int pwidth = e.PageBounds.Width;//纸张宽度
                Pen pen1 = new Pen(Color.Black);
                pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                StringFormat SF = new StringFormat();
                SF.LineAlignment = StringAlignment.Center;                                        //设置属性为水平居中
                SF.Alignment = StringAlignment.Center;                                               //设置属性为垂直居中
                  //其中e.PageBounds属性表示页面全部区域的矩形区域
                if (isyicaiyidan.Equals("0"))
                {
                    RectangleF rect = new RectangleF(0, 0, pwidth, e.Graphics.MeasureString("点点菜单", cfont).Height);
                    e.Graphics.DrawString("点点菜单", cfont, Brushes.Black, rect, SF);
                    string tablename = main["tablename"].ToString();
                    bool _actual1 = HasChinese(tablename);
                    string serialno = main["serialno"].ToString();
                    try
                    {
                        int no = Convert.ToInt32(serialno);
                        serialno = string.Format("{0,-10:D3}", no);
                    }
                    catch (Exception es)
                    {

                        Console.WriteLine("结款小票的打印:" + es.Message);
                    }
                    if (_actual1)
                    {
                        tablename = tablename + " " + serialno;
                    }
                    else
                    {
                        tablename = tablename + "号桌 " + serialno;
                    }
                    RectangleF rect1 = new RectangleF(0, 35, pwidth, e.Graphics.MeasureString(tablename, tfont).Height);
                    e.Graphics.DrawString(tablename, tfont, Brushes.Black, rect1, SF);

                    e.Graphics.DrawString("订单编号", cfont, System.Drawing.Brushes.Black, 0, 70);
                    e.Graphics.DrawString(main["orderno"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(main["orderno"].ToString(), cfont).Width, 70);

                    e.Graphics.DrawString("点餐时间", cfont, System.Drawing.Brushes.Black, 0, 85);
                    e.Graphics.DrawString(main["addtime"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(main["addtime"].ToString(), cfont).Width, 85);
                    e.Graphics.DrawString("用餐时间", cfont, System.Drawing.Brushes.Black, 0, 100);
                    e.Graphics.DrawString(main["sctime"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(main["sctime"].ToString(), cfont).Width, 100);
                    // 横线  
                    e.Graphics.DrawLine(pen1, 0, 120, pwidth, 120);

                    e.Graphics.DrawString("项目", cfont, System.Drawing.Brushes.Black, 0, 125);
                    e.Graphics.DrawString("数量", cfont, System.Drawing.Brushes.Black, pwidth - 40 + (40 - e.Graphics.MeasureString("数量", cfont).Width) / 2, 125);
                    // 横线  
                    e.Graphics.DrawLine(pen1, 0, 145, pwidth, 145);
                    int cheight = 150;
                    JArray zitems = (JArray)pcontent["zitems"];
                    if (zitems.Count > 0)
                    {
                        for (int i = 0; i < zitems.Count; i++)
                        {
                            //菜品打印h20
                            string num = zitems[i]["num"].ToString();
                            string price = zitems[i]["price"].ToString();
                            if (zitems[i]["weight"] != null)
                            {
                                string weight = zitems[i]["weight"].ToString();
                                float weig = float.Parse(weight);
                                if (weig > 0)
                                {
                                    num = weig + "";

                                    price = float.Parse(price) / weig + "";
                                }
                            }
                            string name = zitems[i]["name"].ToString();
                           
                            string zuofa = zitems[i]["zuofa"].ToString();
                            object isprint = zitems[i]["isprint"];
                            if (isprint != null && !"0".Equals(isprint.ToString()))
                            {
                                continue;
                            }
                            if (zitems[i]["isset"] != null)
                            {
                                string isset = zitems[i]["isset"].ToString();
                                if (isset.Equals("1"))
                                {//如果套餐不打印
                                    continue;
                                }
                            }
                           
                           
                            if (zitems[i]["guigename"] != null && !zitems[i]["guigename"].ToString().Equals(""))
                            {
                                name += "(" + zitems[i]["guigename"].ToString() + ")";
                            }
                            if (zitems[i]["istaocan"] != null)
                            {
                                string istaocan = zitems[i]["istaocan"].ToString();
                                if (istaocan.Equals("0"))
                                {//如果套餐加特殊标记
                                 // name = "△" + name;
                                    name = name + "(套)";
                                }
                            }
                            if (zitems[i]["unit"] != null && !"".Equals(zitems[i]["unit"].ToString()))
                            {
                                name = name + "/" + zitems[i]["unit"];
                            }
                            StringFormat fmt = new StringFormat();
                            fmt.LineAlignment = StringAlignment.Near;//左对齐
                            fmt.FormatFlags = StringFormatFlags.LineLimit;//自动换行

                            //设定文本打印区域 b是左上角坐标，Size是打印区域（矩形） float mmtopt = 2.835f;
                            //Rectangle r = new Rectangle();
                            double sheight = 25;

                            var slen = getStringLength(name);
                            if (slen >12)
                            {
                                sheight = sheight * Math.Ceiling(slen /12.0);
                            }
                            Point b = new Point(0, cheight);
                            Rectangle r = new Rectangle(b, new Size(pwidth - 40, 25 * 2));
                            //e.Graphics.DrawString(name, cpfont, System.Drawing.Brushes.Black, 0, cheight);
                            e.Graphics.DrawString(name, cpfont, new SolidBrush(Color.Black), r, fmt);
                            e.Graphics.DrawString(num, cpfont, System.Drawing.Brushes.Black, pwidth - 40 + (40 - e.Graphics.MeasureString(num, cpfont).Width) / 2, cheight);
                            cheight += (int)sheight;
                            if (zuofa != null && !"".Equals(zuofa)) {
                                Point b1 = new Point(0, cheight);
                                Rectangle r1 = new Rectangle(b1, new Size(pwidth, 25 * 2)); 
                                e.Graphics.DrawString("    <"+zuofa+">", cfont, new SolidBrush(Color.Black), r1, fmt);
                                cheight += 20;
                            }



                        }
                    }
                    cheight += 2;
                    // 横线 
                    e.Graphics.DrawLine(pen1, 0, cheight, pwidth, cheight);
                    string beizhu = "备注:" + main["remark"] + main["customremark"];
                    beizhu = AutomaticLine(beizhu, 8, pwidth);//8, 36  
                    RectangleF drawRect = new RectangleF(0, cheight + 5, pwidth, e.Graphics.MeasureString(beizhu, tfont).Height); //设定这个就行了
                    e.Graphics.DrawString(beizhu, tfont, Brushes.Black, drawRect, null);
                }
                else {
                    int cheight = 0;
                    JArray zitems = (JArray)pcontent["zitems"];
                    if (zitems.Count > 0)
                    {
                        for (int i = 0; i < zitems.Count; i++)
                        {
                            RectangleF rect = new RectangleF(0, cheight, pwidth, e.Graphics.MeasureString("点点菜单", cfont).Height);
                            e.Graphics.DrawString("点点菜单", cfont, Brushes.Black, rect, SF);
                            cheight += 25;
                    string tablename = main["tablename"].ToString();
                    bool _actual1 = HasChinese(tablename);
                    string serialno = main["serialno"].ToString();
                    try
                    {
                        int no = Convert.ToInt32(serialno);
                        serialno = string.Format("{0,-10:D3}", no);
                    }
                    catch (Exception es)
                    {

                        Console.WriteLine("结款小票的打印:" + es.Message);
                    }
                    if (_actual1)
                    {
                        tablename = tablename + " " + serialno;
                    }
                    else
                    {
                        tablename = tablename + "号桌 " + serialno;
                    }
                    RectangleF rect1 = new RectangleF(0, cheight, pwidth, e.Graphics.MeasureString(tablename, tfont).Height);
                    e.Graphics.DrawString(tablename, tfont, Brushes.Black, rect1, SF);
                            cheight +=35;
                            e.Graphics.DrawString("订单编号", cfont, System.Drawing.Brushes.Black, 0, cheight);
                    e.Graphics.DrawString(main["orderno"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(main["orderno"].ToString(), cfont).Width, cheight);
                            cheight += 15;
                            e.Graphics.DrawString("点餐时间", cfont, System.Drawing.Brushes.Black, 0, cheight);
                    e.Graphics.DrawString(main["addtime"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(main["addtime"].ToString(), cfont).Width, cheight);
                            cheight += 15;
                            e.Graphics.DrawString("用餐时间", cfont, System.Drawing.Brushes.Black, 0, cheight);
                    e.Graphics.DrawString(main["sctime"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(main["sctime"].ToString(), cfont).Width, cheight);
                            cheight += 20;
                            // 横线  
                            e.Graphics.DrawLine(pen1, 0, cheight, pwidth, cheight);
                            cheight += 5;
                            e.Graphics.DrawString("项目", cfont, System.Drawing.Brushes.Black, 0, cheight);
                    e.Graphics.DrawString("数量", cfont, System.Drawing.Brushes.Black, pwidth - 40 + (40 - e.Graphics.MeasureString("数量", cfont).Width) / 2, cheight);
                            cheight +=20;
                            // 横线  
                            e.Graphics.DrawLine(pen1, 0, cheight, pwidth, cheight);
                     cheight+= 5;
                   
                            //菜品打印h20
                            string num = zitems[i]["num"].ToString();
                            string price = zitems[i]["price"].ToString();
                            if (zitems[i]["weight"] != null)
                            {
                                string weight = zitems[i]["weight"].ToString();
                                float weig = float.Parse(weight);
                                if (weig > 0)
                                {
                                    num = weig + "";

                                    price = float.Parse(price) / weig + "";
                                }
                            }
                            string zuofa = zitems[i]["zuofa"].ToString();
                            string name = zitems[i]["name"].ToString();
                            
                            object isprint = zitems[i]["isprint"];
                            if (isprint != null && !"0".Equals(isprint.ToString()))
                            {
                                continue;
                            }
                            if (zitems[i]["isset"] != null)
                            {
                                string isset = zitems[i]["isset"].ToString();
                                if (isset.Equals("1"))
                                {//如果套餐不打印
                                    continue;
                                }
                            }
                            
                            if (zitems[i]["guigename"] != null && !zitems[i]["guigename"].ToString().Equals(""))
                            {
                                name += "(" + zitems[i]["guigename"].ToString() + ")";
                            }
                            
                            StringFormat fmt = new StringFormat();
                            fmt.LineAlignment = StringAlignment.Near;//左对齐
                            fmt.FormatFlags = StringFormatFlags.LineLimit;//自动换行

                            //设定文本打印区域 b是左上角坐标，Size是打印区域（矩形） float mmtopt = 2.835f;
                            //Rectangle r = new Rectangle();
                            if (zitems[i]["istaocan"] != null)
                            {
                                string istaocan = zitems[i]["istaocan"].ToString();
                                if (istaocan.Equals("0"))
                                {//如果套餐加特殊标记
                                    //name = "△" + name;
                                    name = name + "(套)";
                                }
                            }
                            if (zitems[i]["unit"] != null && !"".Equals(zitems[i]["unit"].ToString()))
                            {
                                name = name + "/" + zitems[i]["unit"];
                            }
                            double sheight = 25;
                            var slen = getStringLength(name);
                            if (slen > 12)
                            {
                                sheight = sheight * Math.Ceiling(slen / 12.0);
                            }
                            Point b = new Point(0, cheight);
                            Rectangle r = new Rectangle(b, new Size(pwidth - 40, 25 * 2));
                            //e.Graphics.DrawString(name, cpfont, System.Drawing.Brushes.Black, 0, cheight);
                            e.Graphics.DrawString(name, cpfont, new SolidBrush(Color.Black), r, fmt);
                            e.Graphics.DrawString(num, cpfont, System.Drawing.Brushes.Black, pwidth - 40 + (40 - e.Graphics.MeasureString(num, cpfont).Width) / 2, cheight);
                            cheight += (int)sheight;
                            if (zuofa != null && !"".Equals(zuofa))
                            {
                                Point b1 = new Point(0, cheight);
                                Rectangle r1 = new Rectangle(b1, new Size(pwidth, 25 * 2));
                                e.Graphics.DrawString("    <" + zuofa + ">", cfont, new SolidBrush(Color.Black), r1, fmt);
                                cheight += 20;
                            }
                            cheight += 2;
                    // 横线 
                    e.Graphics.DrawLine(pen1, 0, cheight, pwidth, cheight);
                    string beizhu = "备注:" + main["remark"] + main["customremark"];
                    beizhu = AutomaticLine(beizhu, 8, pwidth);//8, 36 
                            cheight += 5;
                    RectangleF drawRect = new RectangleF(0, cheight , pwidth, e.Graphics.MeasureString(beizhu, tfont).Height); //设定这个就行了
                    e.Graphics.DrawString(beizhu, tfont, Brushes.Black, drawRect, null);
                            cheight +=60;
                        }
                    }
                }                                                                                                  //e.Graphics.MeasureString("点点菜单", new Font("Times New Roman", 20)).Height;
                                                                                                                   //表示获取你要打印字符串的高度
               
            }
        }

        //结款小票的打印1
        public void completePrint(string s)
        {
            Console.WriteLine("结款小票的打印:" + s);
           

        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            MessageBox.Show("开始打印  " + DateTime.Now.ToString());
            e.Graphics.Clear(Color.White);
            // 开始绘制文档  
            // 默认为横排文字  
            Graphics g = form.CreateGraphics();
            Font cfont = new Font(new FontFamily("宋体"), 9, FontStyle.Bold);
            Font tfont = new Font(new FontFamily("宋体"), 16, FontStyle.Bold);
            int pwidth = e.PageBounds.Width;//纸张宽度
            Pen pen1 = new Pen(Color.Black);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            StringFormat SF = new StringFormat();
            SF.LineAlignment = StringAlignment.Center;                                        //设置属性为水平居中
            SF.Alignment = StringAlignment.Center;                                               //设置属性为垂直居中
            RectangleF rect = new RectangleF(0, 0, pwidth, e.Graphics.MeasureString("点点菜单", cfont).Height);    //其中e.PageBounds属性表示页面全部区域的矩形区域
                                                                                                               //e.Graphics.MeasureString("点点菜单", new Font("Times New Roman", 20)).Height;
                                                                                                               //表示获取你要打印字符串的高度
            e.Graphics.DrawString("点点菜单", cfont, Brushes.Black, rect, SF);

            RectangleF rect1 = new RectangleF(0, 35, pwidth, e.Graphics.MeasureString("桌号：1号桌", tfont).Height);
            e.Graphics.DrawString("桌号：1号桌", tfont, Brushes.Black, rect1, SF);

            e.Graphics.DrawString("订单编号", cfont, System.Drawing.Brushes.Black, 0, 70);
            e.Graphics.DrawString("201603221452", cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString("201603221452", cfont).Width, 70);

            e.Graphics.DrawString("点餐时间", cfont, System.Drawing.Brushes.Black, 0, 85);
            e.Graphics.DrawString("2016-03-22 14:52", cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString("2016-03-22 14:52", cfont).Width, 85);
            // 横线  
            e.Graphics.DrawLine(pen1, 0, 105, pwidth, 105);

            e.Graphics.DrawString("项目", cfont, System.Drawing.Brushes.Black, 0, 110);
            e.Graphics.DrawString("数量", cfont, System.Drawing.Brushes.Black, pwidth - 89 + (49 - e.Graphics.MeasureString("数量", cfont).Width) / 2, 110);
            e.Graphics.DrawString("金额", cfont, System.Drawing.Brushes.Black, pwidth - 40 + (40 - e.Graphics.MeasureString("金额", cfont).Width) / 2, 110);
            // 横线  
            e.Graphics.DrawLine(pen1, 0, 130, pwidth, 130);
            int cheight = 135;

            // 横线 
            e.Graphics.DrawLine(pen1, 0, cheight, pwidth, cheight);
            e.Graphics.DrawString("总金额", cfont, System.Drawing.Brushes.Black, 0, cheight + 5);
            e.Graphics.DrawString("25.5", cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString("25.5", cfont).Width, cheight + 5);

        }
        public void print(string printname)
        {
            Console.WriteLine("档口小票的打印:" + printname);
           // MessageBox.Show("进入打印  " + DateTime.Now.ToString());
            int vindex = printname.IndexOf("ip");
            int cindex = printname.IndexOf("COM");
            int uindex = printname.IndexOf("USB\\VID");
            int lindex = printname.IndexOf("LPT");
            if (vindex >= 0)
            {
                printname = printname.Substring(vindex + 2);
                //打印的IP一定要预先设置好
                bool b = opos.OpenNetPort(printname);//"192.168.1.254"
                if (!b)
                {
                    Console.WriteLine("初始化'{0}'的打印机参数失败。请检测打印机配置");
                    b = opos.OpenNetPort(printname);//"192.168.1.254"
                    if (!b)
                    {
                        string errormsg = string.Format("初始化'{0}'的打印机参数失败。请检测打印机配置",
                                         printname);
                        Console.WriteLine(errormsg);
                        utils.ShowTip("警告", errormsg, 5000);
                        return;
                    }

                }


                Byte res = new Byte();
                int ret = BeiYangOPOS.POS_NETQueryStatus(printname, out res);
                StringBuilder sb = new StringBuilder();
                #region 检测打印机状态
                if ((res & 0x10) == 0x10)
                {
                    sb.AppendLine("打印机出错！");
                }
                if ((res & 0x02) == 0x02)
                {
                    sb.AppendLine("打印机脱机！");
                }
                if ((res & 0x04) == 0x04)
                {
                    sb.AppendLine("上盖打开！");
                }
                if ((res & 0x20) == 0x20)
                {
                    sb.AppendLine("切刀出错！");
                }
                if ((res & 0x40) == 0x40)
                {
                    sb.AppendLine("纸将尽！");
                }
                if ((res & 0x80) == 0x80)
                {
                    sb.AppendLine("缺纸！");
                }
                #endregion
                if (sb.Length > 0)
                {
                    Console.WriteLine("Error",
                               string.Format("'{0}'的打印机处于非正常状态：{1}。请检测打印机配置。",
                                          printname, sb.ToString()));
                    return;
                }
                testPrint(printname);
                return;
            }
            else if (cindex >= 0)
            {
                //printname = printname.Substring(cindex + 4);
                SerialPort com = new SerialPort();
                string[] sArray = printname.Split('#');
                int pbites = 19200;
                if (sArray.Count() > 1)
                {
                    pbites = int.Parse(sArray[1]);
                }
                com.BaudRate = pbites;
                com.PortName = sArray[0];
                com.DataBits = 8;
                bool b = opos.OpenComPort(ref com);
                if (!b)
                {
                    string errormsg = string.Format("初始化'{0}'的打印机参数失败。请检测打印机配置",
                                         printname);
                    Console.WriteLine(errormsg);
                    utils.ShowTip("警告", errormsg, 5000);
                    return;
                }

                testPrint(printname);
                return;
            }
            else if (uindex >= 0) {
               MessageBox.Show("进入打印  " + DateTime.Now.ToString());
                testUsbPrint(printname);
                return;
            }
            else if (lindex >= 0)
            {

               
                testlpt(printname);
                return;
            }

            //printname= printname.Replace("\\\\", "\\");
            if (printname == null || "".Equals(printname) || "default".Equals(printname))
            {
                //this.printDocument1.PrinterSettings.PrinterName = printname;
            }
            else
            {

                this.printDocument1.PrinterSettings.PrinterName = printname;


            }
            this.printDocument1.DocumentName = "test";
            printDocument1.PrintPage +=

            new PrintPageEventHandler(this.printDocument1_PrintPage);
            try
            {
                this.printDocument1.Print();
            }
            catch (Exception e) {
                MessageBox.Show("发生错误:"+e, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
       
        //划单小票的打印主要用于餐饮行业2
        public void testPrint(string printname)
        {
                #region 执行指令打印
                uint width = 1;
                BeiYangOPOS.POS_SetRightSpacing(0);
            BeiYangOPOS.POS_SetLineSpacing(30);
            //BeiYangOPOS.POS_SetLineSpacing(100);
                BeiYangOPOS.POS_S_TextOut("菜单 【455】",0, width, 4, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut("-----------------------------------------", 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut("厨房:123 送单人:44", 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut("桌台:1 @ 菜类：5454", 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut("-----------------------", 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();
            BeiYangOPOS.POS_CutPaper(1,50);
            BeiYangOPOS.POS_S_TextOut("菜单 【455】", 0, width, 4, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
            BeiYangOPOS.POS_FeedLine();
            BeiYangOPOS.POS_S_TextOut("-----------------------------------------", 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
            BeiYangOPOS.POS_FeedLine();
            BeiYangOPOS.POS_S_TextOut("厨房:123 送单人:44", 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
            BeiYangOPOS.POS_FeedLine();
            BeiYangOPOS.POS_S_TextOut("桌台:1 @ 菜类：5454", 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
            BeiYangOPOS.POS_S_TextOut("-----------------------", 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();

                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_CutPaper(1, 50);
            opos.ClosePrinterPort();
            #endregion

        }
        public  void testlpt(string printname)
        {
            string State = "正在打印...";

            try
            {
                LPTControls lpt = new LPTControls();
                lpt.Open(printname);
               
                 
            
              
                    // usbStatus(f);
                    //UsbPrinterResolver.USBDataRead(sh);
                    // Read from and write to the stream f  
                    StringBuilder sb = new StringBuilder("菜单 【455】\n");
                    sb.Append("-----------------------------------------\n");
                    sb.Append("厨房:123 送单人:44\n");
                    sb.Append("桌台:1 @ 菜类：5454\n");
                    sb.Append("-----------------------------------------\n");
                    //WriteData(f, sb.ToString());
                    lpt.OnWriteData("菜单 【455】\n", true, true, false, 2);

                lpt.OnWriteData( "- - - - - - - - - - - - - - - -\n", false, false, false, 0);

                lpt.OnWriteData( "So You Want？\n", false, false, false, 2);

                lpt.OnWriteData("- - - - - - - - - - - - - - - -\n", true, false, false, 0);
                lpt.OnWriteData("厨房:123\n", true, true, false, 1);
                lpt.OnWriteData("我找到了哈哈哈" + (char)(10), false, false, false, 3);
                lpt.OnWriteData("ok test" + (char)(10), false, false, false, 2);
                //OnWriteData(f, "菜单 【455】\n", true, true, false, 2);
                lpt.CutPaper();
                lpt.Close();



            }
            catch (Exception Ex)
            {
                State = "打印出错...";             
                string errormsg = "打印出错:" + Ex.Message;
                Console.WriteLine(errormsg);
                utils.ShowTip("警告", errormsg, 5000);
                 
            }
        }
        public  void WriteLog(string str)
        {
            LogHelper.Log.Error( str);
        }
        //划单小票的打印主要用于餐饮行业2
        public void huadanPrint(string printname)
        {
            lock (thisLock)
            {
                Console.WriteLine("档口小票的打印:" + printname);
                int vindex = printname.IndexOf("ip");
                int cindex = printname.IndexOf("COM");
                int uindex = printname.IndexOf("USB\\VID");
                int lindex = printname.IndexOf("LPT");
                BizPrinter dao = new BizPrinter();

                dd_printers printer = dao.QueryPrinters(2, printname).FirstOrDefault();
                
                if (vindex >= 0)
                {
                    printname = printname.Substring(vindex + 2);
                    //打印的IP一定要预先设置好
                    bool b = opos.OpenNetPort(printname);//"192.168.1.254"
                    if (!b)
                    {
                        Console.WriteLine("初始化'"+ printname + "'的打印机参数失败。请检测打印机配置");
                        b = opos.OpenNetPort(printname);//"192.168.1.254"
                        Thread.Sleep(1000);
                        if (!b)
                        {
                            Console.WriteLine("初始化'" + printname + "'的打印机参数失败。请检测打印机配置");
                            utils.ShowTip("警告", "初始化'" + printname + "'的打印机参数失败。请检测打印机配置", 5000);
                            //form.showmsg("初始化'" + printname + "'的打印机参数失败。请检测打印机配置");
                            return;
                        }

                    }



                    Byte res = new Byte();
                    int ret = BeiYangOPOS.POS_NETQueryStatus(printname, out res);
                    StringBuilder sb = new StringBuilder();
                    #region 检测打印机状态
                    if ((res & 0x10) == 0x10)
                    {
                        sb.AppendLine("打印机出错！");
                    }
                    if ((res & 0x02) == 0x02)
                    {
                        sb.AppendLine("打印机脱机！");
                    }
                    if ((res & 0x04) == 0x04)
                    {
                        sb.AppendLine("上盖打开！");
                    }
                    if ((res & 0x20) == 0x20)
                    {
                        sb.AppendLine("切刀出错！");
                    }
                    if ((res & 0x40) == 0x40)
                    {
                        sb.AppendLine("纸将尽！");
                    }
                    if ((res & 0x80) == 0x80)
                    {
                        sb.AppendLine("缺纸！");
                    }
                    #endregion
                    if (sb.Length > 0)
                    {
                        Console.WriteLine("Error",
                                   string.Format("'" + printname + "'的打印机处于非正常状态：" + sb.ToString() + "。请检测打印机配置。",
                                              printname, sb.ToString()));
                        return;
                    }
                    huadanPrintPage(printname, printer);
                    return;
                }
                else if (cindex >= 0)
                {

                    SerialPort com = new SerialPort();
                    int pbites = printer.pbites.Value;
                    com.BaudRate = pbites;
                    com.PortName = printname;
                    com.DataBits = 8;
                    bool b = opos.OpenComPort(ref com);
                    if (!b)
                    {
                        string errormsg = string.Format("初始化'{0}'的打印机参数失败。请检测打印机配置",
                                            printname);
                        Console.WriteLine(errormsg);
                        utils.ShowTip("警告", errormsg, 5000);
                        return;
                    }
                    huadanPrintPage(printname, printer);
                    return;
                }
                else if (uindex >= 0)
                {
                    huadanUsbPrintPage(printname, printer);
                    return;
                }
                else if (lindex >= 0)
                {
                    huadanLptPrintPage(printname, printer);
                    return;
                }
                Console.WriteLine("划单小票的打印:" + printname);
                if (printname == null || "".Equals(printname) || "default".Equals(printname))
                {
                    //this.printDocument1.PrinterSettings.PrinterName = printname;
                }
                else
                {
                    this.printDocument1.PrinterSettings.PrinterName = printname;
                }
                this.printDocument1.DocumentName = "test划单" + DateTime.Now.TimeOfDay.ToString();
                printDocument1.PrintPage +=

                new PrintPageEventHandler(this.printDocument1_huadanPrintPage);
                this.printDocument1.Print();
            }
        }
        private void huadanPrintPage(string printname, dd_printers printer)
        {

                if (pcontent != null)
                {
                    JObject main = (JObject)pcontent["main"];
                    #region 执行指令打印
                    uint width = 2;
               
               
                int psize = printer.psize.Value;
                int pwidth = psize;
                string linestr = "------------------------------------------------";
                if (psize == 58)
                {
                    BeiYangOPOS.POS_SetLineSpacing(30);
                    width = 1;
                    pwidth =190;
                    linestr = "-------------------------------";
                }
                else if (psize == 70)
                {
                    width = 2;
                    pwidth = 250;
                    linestr = "-----------------------------------------";
                }
                else if (psize == 76)
                {
                    width =2;
                    pwidth = 270;
                    linestr = "---------------------------------------------";
                }
                else if (psize == 80)
                {
                    width = 2;
                    pwidth = 286;
                }
                //BeiYangOPOS.POS_SetRightSpacing(0);
                    BeiYangOPOS.POS_SetLineSpacing(30);

                int tlength = getStringLength("点点菜单");
                BeiYangOPOS.POS_S_TextOut("点点菜单", (uint)(pwidth - tlength * 6) - 12 * width, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();

                string tablename = main["tablename"].ToString();
                bool _actual1 = HasChinese(tablename);
                string serialno = main["serialno"].ToString();
                try
                {
                    int no = Convert.ToInt32(serialno);
                    serialno = string.Format("{0,-10:D3}", no);
                }
                catch (Exception es)
                {

                    Console.WriteLine("结款小票的打印:" + es.Message);
                }
                if (_actual1)
                {
                    tablename = tablename + " " + serialno;
                }
                else
                {
                    tablename = tablename + "号桌 " + serialno;
                }

                int talength = getStringLength(tablename);
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut(tablename, (uint)(pwidth - tlength * 12) - 12 * width, width, 2, opos.POS_FONT_TYPE_CHINESE, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut("订单编号", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                int olength = getStringLength(main["orderno"].ToString());
                BeiYangOPOS.POS_S_TextOut(main["orderno"].ToString(), (uint)(pwidth * 2 - olength * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut("点餐时间", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                int dclength = getStringLength(main["addtime"].ToString());
                BeiYangOPOS.POS_S_TextOut(main["addtime"].ToString(), (uint)(pwidth * 2 - dclength * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut("用餐时间", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                int sclength = getStringLength(main["sctime"].ToString());
                BeiYangOPOS.POS_S_TextOut(main["sctime"].ToString(), (uint)(pwidth * 2 - sclength * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                   
                if (main["payway"] != null && !"".Equals(main["payway"].ToString()))
                {
                    string payway = main["payway"].ToString();
                    BeiYangOPOS.POS_FeedLine();
                    int paywaylength = getStringLength(payway);
                    BeiYangOPOS.POS_S_TextOut(payway + "买单\n", (uint)(pwidth - paywaylength * 12 )-12* width, width, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                     
                }




                BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_S_TextOut("项目", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    //e.Graphics.DrawString("数量", cfont, System.Drawing.Brushes.Black, pwidth - 89 + (49 - e.Graphics.MeasureString("数量", cfont).Width) / 2, 110);
                    BeiYangOPOS.POS_S_TextOut("数量", (uint)(pwidth - 10* 12) * 2-24, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_S_TextOut("金额", (uint)(pwidth - getpos(1, "金额") * 12) * 2-24, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();
                    List<JToken> zitems = (List<JToken>)pcontent["items"];
                    if (zitems.Count > 0)
                    {

                            for (int i = 0; i < zitems.Count; i++)
                        {
                            //菜品打印h20
                            string num = zitems[i]["num"].ToString();
                        string price = zitems[i]["price"].ToString();

                        if (zitems[i]["weight"] != null)
                        {
                            string weight = zitems[i]["weight"].ToString();
                            float weig = float.Parse(weight);
                            if (weig > 0)
                            {
                                num = weig + "";
                               
                                price = float.Parse(price) / weig+"";
                            }
                        }
                        string name = zitems[i]["name"].ToString();
                       
                        string zuofa = zitems[i]["zuofa"].ToString();
                        string agioprice = "0";
                        if (zitems[i]["agioprice"]!=null)
                            agioprice = zitems[i]["agioprice"].ToString();
                        if (zitems[i]["isexception"] != null && !zitems[i]["isexception"].ToString().Equals("1"))
                        {
                            float mprice = float.Parse(price);
                            if (mprice == 0)
                            {

                                name = "(赠)" + name;

                            }
                            else
                            {

                                if (agioprice.Equals(price))
                                {
                                    name = "(折)" + name;
                                }
                            }
                        }
                        else {
                            name = "(退)" + name;
                        }
                          
                       
                        if (zitems[i]["istaocan"] != null) {
                            string istaocan = zitems[i]["istaocan"].ToString();
                            if (istaocan.Equals("0"))
                            {//如果套餐不打印套餐菜品子选项
                                continue;
                            }
                        }
                       
                        if (zitems[i]["guigename"] != null && !zitems[i]["guigename"].ToString().Equals(""))
                            {
                                name += "(" + zitems[i]["guigename"].ToString() + ")";
                            }
                        if (zitems[i]["unit"] != null && !"".Equals(zitems[i]["unit"].ToString()))
                        {
                            name = name + "/" + zitems[i]["unit"];
                        }
                        string ename = "";//为了菜名过长设定，截断字符串
                        int hznums = GetHanNumFromString(name);//数字个数
                        int onums = (int)Math.Ceiling((name.Length - hznums) / 2.0);
                        int zlength = hznums + onums;
                      
                        if (zlength > 6)
                        {

                            ename = name.Substring(6, name.Length -6);
                            name = name.Substring(0,6);
                        }
                        BeiYangOPOS.POS_S_TextOut(name, 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                            BeiYangOPOS.POS_S_TextOut(num, (uint)(pwidth - 10 * 12) * 2, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                           // string fprice=decimal.Round(decimal.Parse(price + ""), 2)+"";
                        string fprice = float.Parse(price).ToString("F2");
                        if (zitems[i]["isexception"] != null && !zitems[i]["isexception"].ToString().Equals("1"))
                        {
                            // fprice = string.Format("{0,6}",fprice);
                            fprice = string.Format("{0,6}", fprice);
                            int flength = getStringLength(fprice);
                            BeiYangOPOS.POS_S_TextOut(fprice, (uint)(pwidth * 2 - flength * 12 * width), width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);//(uint)(pwidth - getpos(1, fprice) * 24) * 2 + 24

                        }
                        else {
                            //fprice = string.Format("{0,7}", "-" + fprice);
                            //fprice = "-" + fprice;
                            fprice = string.Format("{0,6}",fprice);
                            int flength = getStringLength(fprice);
                            BeiYangOPOS.POS_S_TextOut(fprice, (uint)(pwidth * 2 - flength * 12* width), width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);//(uint)(pwidth - getpos(1, fprice) * 24)*2+48

                        }

                        BeiYangOPOS.POS_FeedLine();
                        if (!ename.Equals("")) {
                            BeiYangOPOS.POS_S_TextOut(ename, 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                            BeiYangOPOS.POS_FeedLine();
                        }
                        if (zuofa!=null&&!zuofa.Equals(""))
                        {
                            BeiYangOPOS.POS_S_TextOut("    <"+zuofa+">", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                            BeiYangOPOS.POS_FeedLine();
                        }
                    }
                    }
                    BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();

                    // 横线 

                    BeiYangOPOS.POS_S_TextOut("总金额", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                int alength = getStringLength(main["amount"].ToString() + "元");
                    BeiYangOPOS.POS_S_TextOut(main["amount"].ToString() + "元", (uint)(pwidth*2 - alength * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();
                    float exceptionamount = float.Parse(main["exceptionamount"].ToString());
                    if (exceptionamount > 0) {
                        BeiYangOPOS.POS_S_TextOut("异常金额", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                        BeiYangOPOS.POS_S_TextOut(main["exceptionamount"].ToString() + "元", (uint)(pwidth - getpos(1, main["exceptionamount"].ToString() + "元") * 12) * 2 + 12, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                        BeiYangOPOS.POS_FeedLine();
                    }
                    float youhui = float.Parse(main["youhui"].ToString());
                    if (youhui > 0)
                    {
                        BeiYangOPOS.POS_S_TextOut("优惠", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                        BeiYangOPOS.POS_S_TextOut(main["youhui"].ToString() + "元", (uint)(pwidth - getpos(1, main["youhui"].ToString() + "元") * 12) * 2 + 12, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                        BeiYangOPOS.POS_FeedLine();
                    }
                    if (youhui + exceptionamount > 0) {
                        BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                        BeiYangOPOS.POS_FeedLine();
                        BeiYangOPOS.POS_S_TextOut("实收", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                        float amount = float.Parse(main["amount"].ToString());
                        string shishou = Math.Round(amount - exceptionamount - youhui, 2)+"元";
                        BeiYangOPOS.POS_S_TextOut(shishou, (uint)(pwidth - getpos(1, shishou) * 12) * 2 , 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                        BeiYangOPOS.POS_FeedLine();
                    }
                    BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();
                    // 横线 
                    string beizhu = "备注:" + main["remark"] + main["customremark"];
                    BeiYangOPOS.POS_S_TextOut(beizhu, 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                if (pcontent.Keys.Contains("comm"))
                {
                    BeiYangOPOS.POS_FeedLine();
                    string comm = "\n"+(string)pcontent["comm"];
                    BeiYangOPOS.POS_S_TextOut(comm, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                }

                BeiYangOPOS.POS_S_TextOut("\n\n", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_CutPaper(1,50);
                    opos.ClosePrinterPort();
                #endregion
            }

            

        }
        private void huadanUsbPrintPage(string printname, dd_printers printer)
        {

            if (pcontent != null)
            {
                JObject main = (JObject)pcontent["main"];
                #region 执行指令打印
                uint width = 2;


                int psize = printer.psize.Value;
                int pwidth = psize;
                string linestr = "------------------------------------------------";
                if (psize == 58)
                {
                    width = 1;
                    pwidth = 190;
                    linestr = "--------------------------------";
                }
                else if (psize == 70)
                {
                    width = 2;
                    pwidth = 250;
                    linestr = "-----------------------------------------";
                }
                else if (psize == 76)
                {
                    width = 2;
                    pwidth = 260;
                    linestr = "----------------------------------------";
                }
                else if (psize == 80)
                {
                    width = 2;
                    pwidth = 260;
                }
                using (var sh = UsbPrinterResolver.OpenUSBPrinterBydeviceId(printname))
                {
                    using (var f = new System.IO.FileStream(sh, System.IO.FileAccess.ReadWrite))
                    {
                        OnWriteData(f, "点点菜单\n\n", false, false, false, 2);

                        string tablename = main["tablename"].ToString();
                        bool _actual1 = HasChinese(tablename);
                        string serialno = main["serialno"].ToString();
                        try
                        {
                            int no = Convert.ToInt32(serialno);
                            serialno = string.Format("{0,-10:D3}", no);
                        }
                        catch (Exception es)
                        {

                            Console.WriteLine("结款小票的打印:" + es.Message);
                        }
                        if (_actual1)
                        {
                            tablename = tablename + " " + serialno;
                        }
                        else
                        {
                            tablename = tablename + "号桌 " + serialno;
                        }
                        OnWriteData(f, "      " + tablename + "\n\n", true, true, false, 2);
                        //OnWriteData(f, "订单编号", false, false, false, 1);
                        string orderno = main["orderno"].ToString();
                        orderno = string.Format("{0,19}", orderno);
                        
                        OnWriteData(f, "订单编号     " + orderno + "\n", false, false, false, 1);
                       
                      
                        //OnWriteData(f, "点餐时间", false, false, false, 1);
                        OnWriteData(f, "点餐时间             " + main["addtime"].ToString() + "\n", false, false, false, 1);
                        //OnWriteData(f, "用餐时间", false, false, false, 1);
                        
                        if (main["sctime"].ToString().Length <3)
                            OnWriteData(f, "用餐时间                    " + main["sctime"].ToString() + "\n", false, false, false, 1);
                        else
                            OnWriteData(f, "用餐时间                " + main["sctime"].ToString() + "\n", false, false, false, 1);

                     
                        
                        if (main["payway"]!=null&&!"".Equals(main["payway"].ToString())) {
                            string payway = main["payway"].ToString();
                            OnWriteData(f, linestr + "\n", false, false, false, 1);
                           
                            OnWriteData(f, payway+"买单\n", false, false, false,2);
                        }
                    
                        OnWriteData(f, linestr + "\n", false, false, false, 1);
                        //OnWriteData(f, "项目        ", false, false, false, 1);
                        OnWriteData(f, "项目              数量      金额\n", false, false, false, 1);
                        //OnWriteData(f, "金额\n", false, false, false, 1);
                        OnWriteData(f, linestr + "\n", false, false, false, 1);
                        
                        List<JToken> zitems = (List<JToken>)pcontent["items"];
                        if (zitems.Count > 0)
                        {

                            for (int i = 0; i < zitems.Count; i++)
                            {
                                //菜品打印h20
                                string num = zitems[i]["num"].ToString();
                                string price = zitems[i]["price"].ToString();
                                if (zitems[i]["weight"] != null)
                                {
                                    string weight = zitems[i]["weight"].ToString();
                                    float weig = float.Parse(weight);
                                    if (weig > 0)
                                    {
                                        num = weig + "";

                                        price = float.Parse(price) / weig + "";
                                    }
                                }
                                string name = zitems[i]["name"].ToString();
                               
                                string zuofa = zitems[i]["zuofa"].ToString();
                                string agioprice ="0";
                                if (zitems[i]["agioprice"] != null) {
                                    agioprice = zitems[i]["agioprice"].ToString();
                                }
                                string kongge = "";
                                if (zitems[i]["isexception"] != null && !zitems[i]["isexception"].ToString().Equals("1"))
                                {
                                    float mprice = float.Parse(price);
                                    if (mprice == 0)
                                    {

                                        name = "(赠)" + name;
                                        kongge = " ";

                                    }
                                    else
                                    {

                                        if (agioprice.Equals(price))
                                        {
                                            name = "(折)" + name;
                                            kongge = " ";
                                        }
                                    }
                                }
                                else
                                {
                                    name = "(退)" + name;
                                    kongge = " ";
                                }

                                if (zitems[i]["istaocan"] != null)
                                {
                                    string istaocan = zitems[i]["istaocan"].ToString();
                                    if (istaocan.Equals("0"))
                                    {//如果套餐不打印套餐菜品子选项
                                        continue;
                                    }
                                }
                                if (zitems[i]["guigename"] != null && !zitems[i]["guigename"].ToString().Equals(""))
                                {
                                    name += "(" + zitems[i]["guigename"].ToString() + ")";

                                }
                                if (zitems[i]["unit"] != null && !"".Equals(zitems[i]["unit"].ToString()))
                                {
                                    name = name + "/" + zitems[i]["unit"];
                                }
                                string ename = "";//为了菜名过长设定，截断字符串
                               
                                var slen = getStringLength(name);
                                if (slen >18)
                                {

                                    ename = name.Substring(9, name.Length -9);
                                    name = name.Substring(0,9);
                                    
                                }
                                int chakong =18 - getStringLength(name);
                                if (chakong > 0)
                                {
                                    string skongge = string.Format("{0,-" + chakong + "}", "");
                                    name = name + skongge;
                                }
                                num = string.Format("{0,3}", num);
                               
                                // OnWriteData(f, name, false, false, false, 1);
                                // OnWriteData(f, name+num + "      ", false, false, false, 1);
                                string fprice = float.Parse(price).ToString("F2");

                                if (zitems[i]["isexception"] != null && !zitems[i]["isexception"].ToString().Equals("1"))
                                {
                                    fprice = string.Format("{0,6}", fprice);
                                    if (i == zitems.Count - 1)
                                        OnWriteData(f, name + num + "     " + fprice + ename + "\n", false, true, false, 1);
                                    else
                                        OnWriteData(f, name + num + "     " + fprice + ename + "\n\n", false, true, false, 1);
                                }
                                else
                                {
                                    fprice = string.Format("{0,6}", fprice);// "-" +
                                    if (i == zitems.Count - 1)
                                        OnWriteData(f, name + num + "     " + fprice + ename + "\n", false, true, false, 1);
                                    else
                                        OnWriteData(f, name + num + "     " + fprice + ename + "\n\n", false, true, false, 1);
                                }
                                if (zuofa != null && !"".Equals(zuofa)) {                                   
                                    OnWriteData(f, "    <"+zuofa+">\n\n", false, false, false, 1);
                                }
                            }
                        }
                        OnWriteData(f, linestr + "\n", false, false, false, 1);
                        // 横线 
                        
                        string amountstr = string.Format("{0,7}", main["amount"].ToString() + "元");
                        OnWriteData(f, "总金额                  " + amountstr + "\n", false, false, false, 1);
                        float exceptionamount = float.Parse(main["exceptionamount"].ToString());
                        if (exceptionamount > 0)
                        { 
                            //OnWriteData(f, "异常金额                  ", false, false, false, 1);
                            //OnWriteData(f, "异常金额                  "+main["exceptionamount"].ToString() + "元\n", false, false, false,1);
                            string exeamount = main["exceptionamount"].ToString();
                            exeamount = string.Format("{0,7}", exeamount + "元");
                            //OnWriteData(f, "异常金额                   ", false, false, false, 1);
                            OnWriteData(f, "\n-异常金额               " + exeamount + "\n", false, false, false, 1);
                        }
                        float youhui = float.Parse(main["youhui"].ToString());
                        if (youhui > 0)
                        {
                            //OnWriteData(f, "优惠                    ", false, false, false, 1);
                            //OnWriteData(f, "优惠                    "+main["youhui"].ToString() + "元\n", false, false, false,1);
                            string youhuistr = string.Format("{0,7}", youhui + "元");
                            // OnWriteData(f, "优惠                    ", false, false, false, 1);
                            OnWriteData(f, "\n优惠                    " + youhuistr + "\n", false, false, false, 1);

                        }
                        if (youhui + exceptionamount > 0)
                        {
                            OnWriteData(f, linestr + "\n", false, false, false, 1);
                            
                            float amount = float.Parse(main["amount"].ToString());
                            string shishou = Math.Round(amount - exceptionamount - youhui, 2) + "元";
                         
                            shishou = string.Format("{0,7}", shishou);
                            // OnWriteData(f, "实收                       ", false, false, false, 1);
                            OnWriteData(f, "实收                    " + shishou + "\n", false, false, false, 1);
                        }
                        OnWriteData(f, linestr + "\n", false, false, false, 1);
                        // 横线 
                        string beizhu = "备注:" + main["remark"] + main["customremark"] + "\n\n";
                        OnWriteData(f, beizhu, true, true, false, 1);

                        if (pcontent.Keys.Contains("comm"))
                        {
                            
                            string comm = (string)pcontent["comm"] + "\n\n";
                            OnWriteData(f, comm, false, false, false, 1);
                        }

                        cutPage(f);
                        #endregion
                    }
                }
            }


        }
        private void huadanLptPrintPage(string printname, dd_printers printer)
        {
          
            if (pcontent != null)
            {
                LPTControls lpt = new LPTControls();
                lpt.Open(printname);
                JObject main = (JObject)pcontent["main"];
                #region 执行指令打印
                uint width = 2;
                int psize = printer.psize.Value;
                int pwidth = psize;
                string linestr = "------------------------------------------------";
                string kongestr = "";
                if (psize == 58)
                {
                    width = 1;
                    pwidth = 190;
                    linestr = "--------------------------------";
                }
                else if (psize == 70)
                {
                    width = 2;
                    pwidth = 250;
                    linestr = "-----------------------------------------";
                }
                else if (psize == 76)
                {
                    width = 2;
                    pwidth = 260;
                    linestr = "----------------------------------------";
                }
                else if (psize == 80)
                {
                    kongestr = "                ";
                    width = 2;
                    pwidth = 260;
                }

                lpt.OnWriteData("点点菜单\n\n", false, false, false, 2);

                        string tablename = main["tablename"].ToString();
                        bool _actual1 = HasChinese(tablename);
                        string serialno = main["serialno"].ToString();
                        try
                        {
                            int no = Convert.ToInt32(serialno);
                            serialno = string.Format("{0,-10:D3}", no);
                        }
                        catch (Exception es)
                        {

                            Console.WriteLine("结款小票的打印:" + es.Message);
                        }
                        if (_actual1)
                        {
                            tablename = tablename + " " + serialno;
                        }
                        else
                        {
                            tablename = tablename + "号桌 " + serialno;
                        }
                         lpt.OnWriteData( "      " + tablename + "\n\n", true, true, false, 2);
                        //OnWriteData(f, "订单编号", false, false, false, 1);
                        string orderno = main["orderno"].ToString();
                        if (orderno.Length > 14)
                        {
                    lpt.OnWriteData( "订单编号       " + kongestr+ main["orderno"].ToString() + "\n", false, false, false, 1);
                        }
                        else
                        {
                    lpt.OnWriteData("订单编号          " + kongestr + main["orderno"].ToString() + "\n", false, false, false, 1);
                        }
                //OnWriteData(f, "点餐时间", false, false, false, 1);
                lpt.OnWriteData( "点餐时间             " + kongestr + main["addtime"].ToString() + "\n", false, false, false, 1);
                        //OnWriteData(f, "用餐时间", false, false, false, 1);

                        if (main["sctime"].ToString().Length < 3)
                    lpt.OnWriteData( "用餐时间                    " + kongestr + main["sctime"].ToString() + "\n", false, false, false, 1);
                        else
                    lpt.OnWriteData( "用餐时间                " + kongestr + main["sctime"].ToString() + "\n", false, false, false, 1);

                if (main["payway"] != null && !"".Equals(main["payway"].ToString()))
                {
                    string payway = main["payway"].ToString();
                    lpt.OnWriteData( linestr + "\n", false, false, false, 1);

                    lpt.OnWriteData( payway + "买单\n", false, false, false, 2);
                }


                lpt.OnWriteData(linestr + "\n", false, false, false, 1);
                //OnWriteData(f, "项目        ", false, false, false, 1);
                lpt.OnWriteData( "项目              "+ kongestr+"数量      金额\n", false, false, false, 1);
                //OnWriteData(f, "金额\n", false, false, false, 1);
                lpt.OnWriteData(linestr + "\n", false, false, false, 1);

                        List<JToken> zitems = (List<JToken>)pcontent["items"];
                        if (zitems.Count > 0)
                        {

                            for (int i = 0; i < zitems.Count; i++)
                            {
                                //菜品打印h20
                                string num = zitems[i]["num"].ToString();
                                string price = zitems[i]["price"].ToString();
                        if (zitems[i]["weight"] != null)
                        {
                            string weight = zitems[i]["weight"].ToString();
                            float weig = float.Parse(weight);
                            if (weig > 0)
                            {
                                num = weig + "";

                                price = float.Parse(price) / weig + "";
                            }
                        }
                        string name = zitems[i]["name"].ToString();
                        
                        string zuofa = zitems[i]["zuofa"].ToString();
                        string agioprice = "0";
                        if (zitems[i]["agioprice"] != null)
                            agioprice = zitems[i]["agioprice"].ToString();
                        string kongge = "";
                        if (zitems[i]["isexception"] != null && !zitems[i]["isexception"].ToString().Equals("1"))
                        {
                            float mprice = float.Parse(price);
                            if (mprice == 0)
                            {

                                name = "(赠)" + name;
                                kongge = " ";

                            }
                            else
                            {

                                if (agioprice.Equals(price))
                                {
                                    name = "(折)" + name;
                                    kongge = " ";
                                }
                            }
                        }
                        else {
                            name = "(退)" + name;
                            kongge = " ";
                        }
                            

                        if (zitems[i]["istaocan"] != null)
                                {
                                    string istaocan = zitems[i]["istaocan"].ToString();
                                    if (istaocan.Equals("0"))
                                    {//如果套餐不打印套餐菜品子选项
                                        continue;
                                    }
                                }
                                if (zitems[i]["guigename"] != null && !zitems[i]["guigename"].ToString().Equals(""))
                                {
                                    name += "(" + zitems[i]["guigename"].ToString() + ")";

                                }
                        if (zitems[i]["unit"] != null && !"".Equals(zitems[i]["unit"].ToString()))
                        {
                            name = name + "/" + zitems[i]["unit"];
                        }
                        string ename = "";//为了菜名过长设定，截断字符串
                        var slen = getStringLength(name);
                        if (slen > 18)
                        {

                            ename = name.Substring(9, name.Length - 9);
                            name = name.Substring(0, 9);

                        }
                        int chakong = 18 - getStringLength(name);
                        if (chakong > 0)
                        {
                            string skongge = string.Format("{0,-" + chakong + "}", "");
                            name = name + skongge;
                        }

                        num = string.Format("{0,3}", num);
                                
                                // OnWriteData(f, name, false, false, false, 1);
                                // OnWriteData(f, name+num + "      ", false, false, false, 1);
                        string fprice = float.Parse(price).ToString("F2");

                                if (zitems[i]["isexception"] != null && !zitems[i]["isexception"].ToString().Equals("1"))
                                {
                                    fprice = string.Format("{0,6}", fprice);
                                    if (i == zitems.Count - 1)
                                lpt.OnWriteData(name + kongestr+ num + "     " + fprice + ename + "\n", false, true, false, 1);
                                    else
                                lpt.OnWriteData(name + kongestr+ num + "     " + fprice + ename + "\n\n", false, true, false, 1);
                                }
                                else
                                {
                                    fprice = string.Format("{0,6}", fprice);// "-" +
                            if (i == zitems.Count - 1)
                                lpt.OnWriteData( name + kongestr + num + "     " + fprice + ename + "\n", false, true, false, 1);
                                    else
                                lpt.OnWriteData(name + kongestr + num + "     " + fprice + ename + "\n\n", false, true, false, 1);
                                }
                                if (zuofa != null && !"".Equals(zuofa))
                                {
                                    lpt.OnWriteData("    <" + zuofa + ">\n\n", false, false, false, 1);
                                }
                    }
                        }
                lpt.OnWriteData(linestr + "\n", false, false, false, 1);
                        // 横线 

                        string amountstr = string.Format("{0,7}", main["amount"].ToString() + "元");
                lpt.OnWriteData( "总金额                  " + kongestr + amountstr + "\n", false, false, false, 1);
                        float exceptionamount = float.Parse(main["exceptionamount"].ToString());
                        if (exceptionamount > 0)
                        {
                            //OnWriteData(f, "异常金额                  ", false, false, false, 1);
                            //OnWriteData(f, "异常金额                  "+main["exceptionamount"].ToString() + "元\n", false, false, false,1);
                            string exeamount = main["exceptionamount"].ToString();
                            exeamount = string.Format("{0,7}", exeamount + "元");
                    //OnWriteData(f, "异常金额                   ", false, false, false, 1);
                    lpt.OnWriteData( "\n-异常金额               " + kongestr + exeamount + "\n", false, false, false, 1);
                        }
                        float youhui = float.Parse(main["youhui"].ToString());
                        if (youhui > 0)
                        {
                            //OnWriteData(f, "优惠                    ", false, false, false, 1);
                            //OnWriteData(f, "优惠                    "+main["youhui"].ToString() + "元\n", false, false, false,1);
                            string youhuistr = string.Format("{0,7}", youhui + "元");
                    // OnWriteData(f, "优惠                    ", false, false, false, 1);
                    lpt.OnWriteData( "\n优惠                    " + kongestr + youhuistr + "\n", false, false, false, 1);

                        }
                        if (youhui + exceptionamount > 0)
                        {
                    lpt.OnWriteData(linestr + "\n", false, false, false, 1);

                            float amount = float.Parse(main["amount"].ToString());
                            string shishou = Math.Round(amount - exceptionamount - youhui, 2) + "元";

                            shishou = string.Format("{0,7}", shishou);
                    // OnWriteData(f, "实收                       ", false, false, false, 1);
                    lpt.OnWriteData( "实收                    " + kongestr + shishou + "\n", false, false, false, 1);
                        }
                lpt.OnWriteData( linestr + "\n", false, false, false, 1);
                        // 横线 
                        string beizhu = "备注:" + main["remark"] + main["customremark"] + "\n\n";
                lpt.OnWriteData( beizhu, true, true, false, 1);
                if (pcontent.Keys.Contains("comm"))
                {

                    string comm = (string)pcontent["comm"] + "\n\n";
                    lpt.OnWriteData(comm, false, false, false, 1);
                }
                lpt.CutPaper();
                lpt.Close();
                        #endregion
            }
         


        }
        private void printDocument1_huadanPrintPage(object sender, PrintPageEventArgs e)
        {
            if (pcontent != null)
            {
                JObject main = (JObject)pcontent["main"];
                e.Graphics.Clear(Color.White);
                // 开始绘制文档  
                // 默认为横排文字  
                Graphics g = form.CreateGraphics();
                Font cfont = new Font(new FontFamily("宋体"), 9, FontStyle.Bold);
                Font tfont = new Font(new FontFamily("宋体"), 16, FontStyle.Bold);
                Font cpfont = new Font(new FontFamily("宋体"), 13, FontStyle.Bold);
                int pwidth = e.PageBounds.Width;//纸张宽度
                Pen pen1 = new Pen(Color.Black);
                pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                StringFormat SF = new StringFormat();
                SF.LineAlignment = StringAlignment.Center;                                        //设置属性为水平居中
                SF.Alignment = StringAlignment.Center;                                               //设置属性为垂直居中
                RectangleF rect = new RectangleF(0, 0, pwidth, e.Graphics.MeasureString("点点菜单", cfont).Height);    //其中e.PageBounds属性表示页面全部区域的矩形区域
                                                                                                                   //e.Graphics.MeasureString("点点菜单", new Font("Times New Roman", 20)).Height;
                                                                                                                   //表示获取你要打印字符串的高度
                e.Graphics.DrawString("点点菜单", cfont, Brushes.Black, rect, SF);
                string tablename = main["tablename"].ToString();
                bool _actual1 = HasChinese(tablename);
                string serialno = main["serialno"].ToString();
                try
                {
                    int no = Convert.ToInt32(serialno);
                    serialno = string.Format("{0,-10:D3}", no);
                }
                catch (Exception es)
                {
                  
                    Console.WriteLine("结款小票的打印:" + es.Message);
                }
                if (_actual1)
                {
                    tablename = tablename + " " + serialno;
                }
                else
                {
                    tablename = tablename + "号桌 " + serialno;
                }
                RectangleF rect1 = new RectangleF(0, 35, pwidth, e.Graphics.MeasureString(tablename, tfont).Height);
                e.Graphics.DrawString(tablename, tfont, Brushes.Black, rect1, SF);

                e.Graphics.DrawString("订单编号", cfont, System.Drawing.Brushes.Black, 0, 70);
                e.Graphics.DrawString(main["orderno"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(main["orderno"].ToString(), cfont).Width, 70);

                e.Graphics.DrawString("点餐时间", cfont, System.Drawing.Brushes.Black, 0, 85);
                e.Graphics.DrawString(main["addtime"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(main["addtime"].ToString(), cfont).Width, 85);
                int cheight = 105;
                // 横线  
                e.Graphics.DrawLine(pen1, 0, cheight, pwidth, cheight);
                if (main["payway"] != null && !"".Equals(main["payway"].ToString()))
                {

                    cheight = cheight + 5;
                    string payway = main["payway"].ToString();
                    SF.LineAlignment = StringAlignment.Center;                                        //设置属性为水平居中
                    SF.Alignment = StringAlignment.Center;                                               //设置属性为垂直居中
                    payway = payway + "买单";
                     RectangleF rect2 = new RectangleF(0, cheight, pwidth, e.Graphics.MeasureString(payway, cfont).Height);    //其中e.PageBounds属性表示页面全部区域的矩形区域
                                                                                                                       //表示获取你要打印字符串的高度
                    e.Graphics.DrawString(payway, cfont, Brushes.Black, rect2, SF);
                    cheight = cheight + 20;
                    e.Graphics.DrawLine(pen1, 0, cheight, pwidth, cheight);
                } 
                cheight = cheight + 5;
                e.Graphics.DrawString("项目", cfont, System.Drawing.Brushes.Black, 0, cheight);
                e.Graphics.DrawString("数量", cfont, System.Drawing.Brushes.Black, pwidth - 89 + (49 - e.Graphics.MeasureString("数量", cfont).Width) / 2, cheight);
                e.Graphics.DrawString("金额", cfont, System.Drawing.Brushes.Black, pwidth - 40 + (40 - e.Graphics.MeasureString("金额", cfont).Width) / 2, cheight);
                cheight = cheight + 20;
                // 横线  
                e.Graphics.DrawLine(pen1, 0, cheight, pwidth, cheight);
                cheight = cheight + 5;
                List<JToken> zitems = (List<JToken>)pcontent["items"];
                if (zitems.Count > 0)
                {
                   
                        for (int i = 0; i < zitems.Count; i++)
                        {
                            //菜品打印h20
                            string num = zitems[i]["num"].ToString();
                            string price = zitems[i]["price"].ToString();
                        if (zitems[i]["weight"] != null)
                        {
                            string weight = zitems[i]["weight"].ToString();
                            float weig = float.Parse(weight);
                            if (weig > 0)
                            {
                                num = weig + "";

                                price = float.Parse(price) / weig + "";
                            }
                        }
                        string name = zitems[i]["name"].ToString();
                       
                        string zuofa = zitems[i]["zuofa"].ToString();
                        string agioprice = "0";
                        if (zitems[i]["agioprice"] != null)
                            agioprice = zitems[i]["agioprice"].ToString();
                        if (agioprice.Equals(price))
                        {
                            name = "(折)" + name;
                        }
                        if (zitems[i]["istaocan"] != null)
                        {
                            string istaocan = zitems[i]["istaocan"].ToString();
                            if (istaocan.Equals("0"))
                            {//如果套餐不打印套餐菜品子选项
                                continue;
                            }
                        }
                        if (zitems[i]["guigename"] != null && !zitems[i]["guigename"].ToString().Equals(""))
                            {
                                name += "(" + zitems[i]["guigename"].ToString() + ")";
                            }
                        if (zitems[i]["unit"] != null && !"".Equals(zitems[i]["unit"].ToString()))
                        {
                            name = name + "/" + zitems[i]["unit"];
                        }
                        int hznums= GetHanNumFromString(name);//数字个数
                        int onums = (int)Math.Ceiling((name.Length - hznums) / 2.0);
                        double sheight = 25;
                        int nlength = getStringLength(name);
                        //int zlength = hznums + onums;
                        //MessageBox.Show("纸张宽度 " + pwidth);
                        double tail = 12;
                        if (pwidth > 200) {
                            tail = 16;
                        }
                        if (nlength > tail)
                        {
                            sheight = sheight * Math.Ceiling(nlength / tail);
                        }
                        StringFormat fmt = new StringFormat();
                        fmt.LineAlignment = StringAlignment.Near;//左对齐
                        fmt.FormatFlags = StringFormatFlags.LineLimit;//自动换行
                        Point b = new Point(0, cheight);
                        Rectangle r = new Rectangle(b, new Size(pwidth - 89, 25 * 2));
                        //e.Graphics.DrawString(name, cpfont, System.Drawing.Brushes.Black, 0, cheight);
                        e.Graphics.DrawString(name, cpfont, new SolidBrush(Color.Black), r, fmt);

                        //e.Graphics.DrawString(name, cpfont, System.Drawing.Brushes.Black, 0, cheight);
                            e.Graphics.DrawString(num, cpfont, System.Drawing.Brushes.Black, pwidth - 89 + (49 - e.Graphics.MeasureString(num, cpfont).Width) / 2, cheight);
                            e.Graphics.DrawString(price, cpfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(price, cpfont).Width, cheight);
                            cheight +=(int) sheight;
                        if (zuofa != null && !"".Equals(zuofa)) {
                            zuofa = "    <"+zuofa + ">\n\n";
                            zuofa = AutomaticLine(zuofa, 8, pwidth);//8, 36  
                            RectangleF drawRect2 = new RectangleF(0, cheight, pwidth, e.Graphics.MeasureString(zuofa, cfont).Height); //设定这个就行了
                            e.Graphics.DrawString(zuofa, cfont, Brushes.Black, drawRect2, null);
                            cheight += 20;
                        }
                        

                    }
                   
                }
                cheight += 5;
                // 横线 
                e.Graphics.DrawLine(pen1, 0, cheight, pwidth, cheight);
                cheight += 5;
                e.Graphics.DrawString("总金额", cfont, System.Drawing.Brushes.Black, 0, cheight);
                e.Graphics.DrawString(main["amount"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(main["amount"].ToString(), cfont).Width, cheight);
                cheight += 20;
                // 横线 
                e.Graphics.DrawLine(pen1, 0, cheight, pwidth, cheight);
                cheight += 5;
                string beizhu = "备注:" + main["remark"] + main["customremark"];
                beizhu = AutomaticLine(beizhu, 8, pwidth);//8, 36  
                double csheight = 25;
                if (beizhu.Length > 7)
                {
                    csheight = csheight * Math.Ceiling(beizhu.Length / 7.0);//
                }
                RectangleF drawRect = new RectangleF(0, cheight, pwidth, (int)csheight); //设定这个就行了
                e.Graphics.DrawString(beizhu, tfont, Brushes.Black, drawRect, null);
                if (pcontent.Keys.Contains("comm"))
                {

                    string comm = (string)pcontent["comm"] + "\n\n";
                    
                    cheight += (int)csheight+5;

                    comm = AutomaticLine(comm, 8, pwidth);//8, 36  
                    RectangleF drawRect2 = new RectangleF(0, cheight, pwidth, e.Graphics.MeasureString(comm, cfont).Height); //设定这个就行了
                    e.Graphics.DrawString(comm, cfont, Brushes.Black, drawRect2, null);
                }
            }

        }
        //打印结单小票的打印主要用于餐饮行业2
        public void jiedanPrint(string printname)
        {
            lock (thisLock)
            {
                Console.WriteLine("档口小票的打印:" + printname);
                int vindex = printname.IndexOf("ip");
                int cindex = printname.IndexOf("COM");
                int uindex = printname.IndexOf("USB\\VID");
                int lindex = printname.IndexOf("LPT");
                BizPrinter dao = new BizPrinter();

                dd_printers printer = dao.QueryPrinters(2, printname).FirstOrDefault();
                if (vindex >= 0)
                {
                    printname = printname.Substring(vindex + 2);
                    //打印的IP一定要预先设置好
                   

                    bool b = opos.OpenNetPort(printname);//"192.168.1.254"
                    if (!b)
                    {
                        Console.WriteLine("初始化'" + printname + "'的打印机参数失败。请检测打印机配置");
                        b = opos.OpenNetPort(printname);//"192.168.1.254"
                        Thread.Sleep(1000);
                        if (!b)
                        {
                            Console.WriteLine("初始化'" + printname + "'的打印机参数失败。请检测打印机配置");
                            utils.ShowTip("警告", "初始化'" + printname + "'的打印机参数失败。请检测打印机配置",5000);
                            //form.showmsg("初始化'" + printname + "'的打印机参数失败。请检测打印机配置");
                            return;
                        }

                    }

                    Byte res = new Byte();
                    int ret = BeiYangOPOS.POS_NETQueryStatus(printname, out res);
                    StringBuilder sb = new StringBuilder();
                    #region 检测打印机状态
                    if ((res & 0x10) == 0x10)
                    {
                        sb.AppendLine("打印机出错！");
                    }
                    if ((res & 0x02) == 0x02)
                    {
                        sb.AppendLine("打印机脱机！");
                    }
                    if ((res & 0x04) == 0x04)
                    {
                        sb.AppendLine("上盖打开！");
                    }
                    if ((res & 0x20) == 0x20)
                    {
                        sb.AppendLine("切刀出错！");
                    }
                    if ((res & 0x40) == 0x40)
                    {
                        sb.AppendLine("纸将尽！");
                    }
                    if ((res & 0x80) == 0x80)
                    {
                        sb.AppendLine("缺纸！");
                    }
                    #endregion
                    if (sb.Length > 0)
                    {
                        Console.WriteLine("Error",
                                   string.Format("'{0}'的打印机处于非正常状态：{1}。请检测打印机配置。",
                                              printname, sb.ToString()));
                        return;
                    }
                    jiedanPrintPage(printname, printer);
                    return;
                }
                else if (cindex >= 0)
                {

                    SerialPort com = new SerialPort();
                    int pbites = printer.pbites.Value;
                    com.BaudRate = pbites;
                    com.PortName = printname;
                    com.DataBits = 8;
                    bool b = opos.OpenComPort(ref com);
                    if (!b)
                    {
                        string errormsg = string.Format("初始化'{0}'的打印机参数失败。请检测打印机配置",
                                            printname);
                        Console.WriteLine(errormsg);
                        utils.ShowTip("警告", errormsg, 5000);
                        return;
                    }
                    //BeiYangOPOS.POS_QueryStatus
                    jiedanPrintPage(printname, printer);
                    return;
                }
                else if (uindex >= 0)
                {
                    jiedanUsbPrintPage(printname, printer);
                    return;
                }
                else if (lindex >= 0)
                {

                    jiedanLptPrintPage(printname, printer);
                    return;
                }
                Console.WriteLine("划单小票的打印:" + printname);
                if (printname == null || "".Equals(printname) || "default".Equals(printname))
                {
                    //this.printDocument1.PrinterSettings.PrinterName = printname;
                }
                else
                {
                    this.printDocument1.PrinterSettings.PrinterName = printname;
                }
                this.printDocument1.DocumentName = "test";
                printDocument1.PrintPage +=

                new PrintPageEventHandler(this.printDocument1_jiedanPrintPage);
                this.printDocument1.Print();
            }
        }
        private void jiedanPrintPage(string printname,dd_printers printer)
        {



           
                if (pcontent != null)
                {
                    JObject main = (JObject)pcontent["main"];
                #region 执行指令打印
                uint width = 2;
               
                int psize = printer.psize.Value;
                int pwidth = psize;
                string linestr = "------------------------------------------------";
                if (psize == 58)
                {
                    width =1;
                    BeiYangOPOS.POS_SetLineSpacing(30);
                    pwidth = 190;
                    linestr = "--------------------------------";
                }
                else if (psize == 70)
                {
                    width = 2;
                    pwidth = 250;
                    linestr = "-----------------------------------------";
                }
                else if (psize == 76)
                {
                    width = 2;
                    pwidth = 270;
                    linestr = "---------------------------------------------";
                }
                else if (psize == 80)
                {
                    width = 2;
                    pwidth = 286;
                }
                BeiYangOPOS.POS_SetRightSpacing(0);
                    BeiYangOPOS.POS_SetLineSpacing(30);
                int tlength = getStringLength("点点菜单");
                BeiYangOPOS.POS_S_TextOut("点点菜单", (uint)(pwidth - tlength * 6) - 12 * width, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();

                string tablename = main["tablename"].ToString();
                bool _actual1 = HasChinese(tablename);
                string serialno = "";// main["serialno"].ToString();
                //try
                //{
                //    int no = Convert.ToInt32(serialno);
                //     serialno = string.Format("{0,-10:D3}", no);
                //}
                //catch (Exception es)
                //{
                //
                //   Console.WriteLine("结款小票的打印:" + es.Message);
                //}
                if (_actual1)
                {
                    tablename = tablename + " " + serialno;
                }
                else
                {
                    tablename = tablename + "号桌 " + serialno;
                }

                int talength = getStringLength(tablename);
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut(tablename, (uint)(pwidth - tlength * 12) - 12 * width, width, 2, opos.POS_FONT_TYPE_CHINESE, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut("订单编号", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                int olength = getStringLength(main["orderno"].ToString());
                BeiYangOPOS.POS_S_TextOut(main["orderno"].ToString(), (uint)(pwidth * 2 - olength * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut("点餐时间", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                int dclength = getStringLength(main["addtime"].ToString());
                BeiYangOPOS.POS_S_TextOut(main["addtime"].ToString(), (uint)(pwidth * 2 - dclength * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                BeiYangOPOS.POS_FeedLine();

                BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();

                    string amount = main["amount"].ToString();
                    string weifuamount = main["weifuamount"].ToString();
                    double samount = Double.Parse(amount);
                    string yifuamount = main["yifuamount"].ToString();
                    double syifuamount = Double.Parse(yifuamount);
                    string amountException = main["amountexception"].ToString();
                    double samountException = Double.Parse(amountException);
                double sweifuamount = Double.Parse(weifuamount);
                if (sweifuamount > 0)
                    {
                        BeiYangOPOS.POS_S_TextOut("未支付", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                    }
                    else
                    {
                        BeiYangOPOS.POS_S_TextOut("已支付", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                    }
                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_S_TextOut("项目", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    //e.Graphics.DrawString("数量", cfont, System.Drawing.Brushes.Black, pwidth - 89 + (49 - e.Graphics.MeasureString("数量", cfont).Width) / 2, 110);
                    BeiYangOPOS.POS_S_TextOut("数量", (uint)(pwidth -9* 12) * 2, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_S_TextOut("小计", (uint)(pwidth - getpos(1, "小计") * 12) * 2-24, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();
                    JArray zitems = (JArray)main["items"];
                    if (zitems.Count > 0)
                    {

                        for (int i = 0; i < zitems.Count; i++)
                        {
                            //菜品打印h20
                            string num = zitems[i]["num"].ToString();
                            string price = zitems[i]["price"].ToString();
                        if (zitems[i]["weight"] != null)
                        {
                            string weight = zitems[i]["weight"].ToString();
                            float weig = float.Parse(weight);
                            if (weig > 0)
                            {
                                num = weig + "";

                                // price = float.Parse(price) / weig + "";
                                double snum = Double.Parse(num);
                                double sprice = Double.Parse(price);
                                price = sprice + "";
                            }
                            else
                            {
                                double snum = Double.Parse(num);
                                double sprice = Double.Parse(price);
                                price = sprice * snum + "";
                            }
                        }
                        else
                        {
                            double snum = Double.Parse(num);
                            double sprice = Double.Parse(price);
                            price = sprice * snum + "";
                        }
                        string name = zitems[i]["name"].ToString();
                      
                        string agioprice = "0";
                        if (zitems[i]["agioprice"] != null)
                            agioprice = zitems[i]["agioprice"].ToString();
                        string discountprice = zitems[i]["discountprice"].ToString();
                        string isexception = zitems[i]["isexception"].ToString();
                        double sagioprice = Double.Parse(agioprice);
                        if (zitems[i]["guigename"] != null && !zitems[i]["guigename"].ToString().Equals(""))
                            {
                                name += "(" + zitems[i]["guigename"].ToString() + ")";
                            }
                        // if (isexception.Equals("1")) {
                        //      
                        //  }
                        if (zitems[i]["unit"] != null && !"".Equals(zitems[i]["unit"].ToString()))
                        {
                            name = name + "/" + zitems[i]["unit"];
                        }
                        string ename = "";//为了菜名过长设定，截断字符串
                      
                        string fprice = float.Parse(price).ToString("F2");
                        
                        if (zitems[i]["isexception"] != null && !zitems[i]["isexception"].ToString().Equals("1"))
                        {
                            if (sagioprice>0 && !agioprice.Equals(discountprice))
                            {
                                name = "(折)" + name;
                            }
                        }
                        else {
                            //fprice = "-" + fprice;
                            name = "(退)"+name;
                           
                        }
                        if (psize == 80)
                        {
                            if (name.Length > 7)
                            {

                                ename = name.Substring(7, name.Length - 7);
                                name = name.Substring(0, 7);
                            }
                        }
                        else {
                            if (name.Length > 6)
                            {

                                ename = name.Substring(6, name.Length - 6);
                                name = name.Substring(0, 6);
                            }
                        }
                       
                        BeiYangOPOS.POS_S_TextOut(name, 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                            BeiYangOPOS.POS_S_TextOut(num, (uint)(pwidth -9* 12) * 2, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                        fprice = string.Format("{0,6}", fprice);
                        int flength = getStringLength(fprice);
                        BeiYangOPOS.POS_S_TextOut(fprice, (uint)(pwidth * 2 - flength * 12 * width), width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                      

                        BeiYangOPOS.POS_FeedLine();
                        if (!ename.Equals(""))
                        {
                            BeiYangOPOS.POS_S_TextOut(ename, 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                            BeiYangOPOS.POS_FeedLine();
                        }
                    }
                    }
                    BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();

                    // 横线 

                    BeiYangOPOS.POS_S_TextOut("总金额", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                int alength = getStringLength(main["amount"].ToString() + "元");
                BeiYangOPOS.POS_S_TextOut(main["amount"].ToString() + "元", (uint)(pwidth * 2 - alength * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
               
                    BeiYangOPOS.POS_FeedLine();
                   
                    if (samountException > 0)
                    {
                        BeiYangOPOS.POS_S_TextOut("异常金额", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    int yclength = getStringLength(amountException + "元");
                    BeiYangOPOS.POS_S_TextOut(amountException + "元", (uint)(pwidth * 2 - yclength * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                        BeiYangOPOS.POS_FeedLine();
                    }
                    float youhui = 0;
                    if (main["youhui"]!=null)
                        youhui = float.Parse(main["youhui"].ToString());
                    if (youhui > 0)
                    {
                        BeiYangOPOS.POS_S_TextOut("优惠", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    int yhlength = getStringLength(main["youhui"].ToString() + "元");
                    BeiYangOPOS.POS_S_TextOut(main["youhui"].ToString() + "元", (uint)(pwidth * 2 - yhlength * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                        BeiYangOPOS.POS_FeedLine();
                    }
                float fuwufee = 0;
                if (main["fuwufei"] != null)
                    fuwufee = float.Parse(main["fuwufei"].ToString());
                if (fuwufee > 0)
                {
                    string fuwufeestr = string.Format("{0,7}", fuwufee + "元");
                    //OnWriteData(f, "异常金额                   ", false, false, false, 1);
                    BeiYangOPOS.POS_S_TextOut("服务费", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    int fwlength = getStringLength(main["fuwufei"].ToString() + "元");
                    BeiYangOPOS.POS_S_TextOut(main["fuwufei"].ToString() + "元", (uint)(pwidth * 2 - fwlength * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();
                }
                if (sweifuamount > 0)
                {


                   
                        if (syifuamount > 0)
                        {
                           
                            BeiYangOPOS.POS_S_TextOut("已付", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                        int yflength = getStringLength(yifuamount + "元");
                        BeiYangOPOS.POS_S_TextOut(yifuamount + "元", (uint)(pwidth * 2 - yflength * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                            BeiYangOPOS.POS_FeedLine();
                        }
                        
                            BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                            BeiYangOPOS.POS_FeedLine();
                            BeiYangOPOS.POS_S_TextOut("需收", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                            string shishou = Math.Round(sweifuamount, 2) + "元";
                    int sslength = getStringLength(shishou);
                    BeiYangOPOS.POS_S_TextOut(shishou, (uint)(pwidth * 2 - sslength * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                            BeiYangOPOS.POS_FeedLine();

                             
                       
                   
                    
                }
                else
                {

                    
                    if (youhui + samountException + syifuamount > 0)
                    {
                        BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                        BeiYangOPOS.POS_FeedLine();
                        JArray pays = (JArray)main["pays"];
                        if (pays.Count > 0)
                        {
                            for (int k = 0; k < pays.Count; k++)
                            {
                                string money = pays[k]["money"].ToString() + "元";
                                
                                string payway = pays[k]["payway"].ToString() + "支付";
                               
                                BeiYangOPOS.POS_S_TextOut(payway, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                                
                                int sslength = getStringLength(money);
                                BeiYangOPOS.POS_S_TextOut(money, (uint)(pwidth * 2 - sslength * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                                BeiYangOPOS.POS_FeedLine();
                                //OnWriteData(f, payway + money + "\n", false, false, false, 1);
                            }
                            JArray reorders = (JArray)main["reorders"];
                            if (reorders!=null&&reorders.Count > 0)
                            {
                                for (int m = 0; m < reorders.Count; m++)
                                {
                                    string money = reorders[m]["refund_fee"].ToString() + "元";
                                    
                                    string payway = "-" + reorders[m]["payway"].ToString() + "退款";
                                    
                                    BeiYangOPOS.POS_S_TextOut(payway, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                                    int sslength = getStringLength(money);
                                    BeiYangOPOS.POS_S_TextOut(money, (uint)(pwidth * 2 - sslength * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                                    BeiYangOPOS.POS_FeedLine();
                                }
                            }
                        }
                        else
                        {
                            BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                            BeiYangOPOS.POS_FeedLine();
                            BeiYangOPOS.POS_S_TextOut("实收", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                            yifuamount = yifuamount + "元";
                            int sslength = getStringLength(yifuamount);
                            BeiYangOPOS.POS_S_TextOut(yifuamount, (uint)(pwidth * 2 - sslength * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                            BeiYangOPOS.POS_FeedLine();
                        }

                    }
                }
              
                    BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();
                    // 横线 
                    string beizhu = "备注:" + main["remark"] + main["customremark"]+"\n";
                    BeiYangOPOS.POS_S_TextOut(beizhu, 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    if (pcontent.Keys.Contains("comm"))
                    {
                    
                    string comm = "\n"+(string)pcontent["comm"] + "\n\n";
                    BeiYangOPOS.POS_S_TextOut(comm, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                }
               
                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_CutPaper(1, 50);
                opos.ClosePrinterPort();
                #endregion
            }

            

        }
        private void jiedanUsbPrintPage(string printname, dd_printers printer)
        {
            if (pcontent != null)
            {
                JObject main = (JObject)pcontent["main"];
                #region 执行指令打印
                uint width = 2;
                if (printer == null) {
                    //打印失败打印机未连接
                    return;
                }
                int psize = printer.psize.Value;
                int pwidth = psize;
                string linestr = "------------------------------------------------";
                if (psize == 58)
                {
                    width = 1;

                    pwidth = 190;
                    linestr = "--------------------------------";
                }
                else if (psize == 70)
                {
                    width = 2;
                    pwidth = 250;
                    linestr = "-----------------------------------------";
                }
                else if (psize == 76)
                {
                    width = 2;
                    pwidth = 260;
                    linestr = "----------------------------------------";
                }
                else if (psize == 80)
                {
                    width = 2;
                    pwidth = 260;
                }
                using (var sh = UsbPrinterResolver.OpenUSBPrinterBydeviceId(printname))
                {
                    using (var f = new System.IO.FileStream(sh, System.IO.FileAccess.ReadWrite))
                    {
                        //BeiYangOPOS.POS_SetLineSpacing(50);
                         
                        OnWriteData(f, "点点菜单\n"+(char)(10), false, false, false, 2);
                        string tablename = main["tablename"].ToString();
                        bool _actual1 = HasChinese(tablename);
                        string serialno = "";// main["serialno"].ToString();
                        //try
                        //{
                        //    int no = Convert.ToInt32(serialno);
                       //     serialno = string.Format("{0,-10:D3}", no);
                       // }
                       // catch (Exception es)
                       // {

                      //      Console.WriteLine("结款小票的打印:" + es.Message);
                      //  }
                        if (_actual1)
                        {
                            tablename = tablename + " " + serialno;
                        }
                        else
                        {
                            tablename = tablename + "号桌 " + serialno;
                        }

                        OnWriteData(f, "      " + tablename + "\n\n", true, true, false, 2);

                        //OnWriteData(f, "订单编号", false, false, false, 1);
                        string orderno = main["orderno"].ToString();
                       
                        orderno = string.Format("{0,19}", orderno);
                       
                      
                            OnWriteData(f, "订单编号     " + orderno + "\n", false, false, false, 1);
                         
                       
                        //OnWriteData(f, "点餐时间", false, false, false, 1);
                        OnWriteData(f, "点餐时间             " + main["addtime"].ToString() + "\n", false, false, false, 1);

                        OnWriteData(f, linestr + "\n", false, false, false, 1);

                        string amount = main["amount"].ToString();
                        string weifuamount = main["weifuamount"].ToString();
                        double samount = Double.Parse(amount);
                        double sweifuamount = Double.Parse(weifuamount);
                        string yifuamount = main["yifuamount"].ToString();
                        double syifuamount = Double.Parse(yifuamount);
                        string amountException = main["amountexception"].ToString();
                        double samountException = Double.Parse(amountException);

                        if (sweifuamount > 0)
                        {
                            OnWriteData(f, "未支付\n", false, false, false, 1);
                        }
                        else
                        {
                            OnWriteData(f, "已支付\n", false, false, false, 1);
                        }
                        OnWriteData(f, linestr + "\n", false, false, false, 1);
                       // OnWriteData(f, "项目        ", false, false, false, 1);
                       // OnWriteData(f, "项目              数量      ", false, false, false, 1);
                        OnWriteData(f, "项目              数量      小计\n", false, false, false, 1);
                        OnWriteData(f, linestr + "\n", false, false, false, 1);

                        JArray zitems = (JArray)main["items"];
                        if (zitems.Count > 0)
                        {

                            for (int i = 0; i < zitems.Count; i++)
                            {
                                //菜品打印h20
                                string num = zitems[i]["num"].ToString();
                                string price = zitems[i]["price"].ToString();
                                if (zitems[i]["weight"] != null)
                                {
                                    string weight = zitems[i]["weight"].ToString();
                                    float weig = float.Parse(weight);
                                    if (weig > 0)
                                    {
                                        num = weig + "";

                                        // price = float.Parse(price) / weig + "";
                                        double snum = Double.Parse(num);
                                        double sprice = Double.Parse(price);
                                        price = sprice+ "";
                                    }
                                    else {
                                        double snum = Double.Parse(num);
                                        double sprice = Double.Parse(price);
                                        price = sprice * snum + "";
                                    }
                                }
                                else {
                                    double snum = Double.Parse(num);
                                    double sprice = Double.Parse(price);
                                    price = sprice * snum + "";
                                }



                                string name = zitems[i]["name"].ToString();
                              
                                string agioprice ="0";
                                if (zitems[i]["agioprice"] != null&&!"".Equals(zitems[i]["agioprice"].ToString())) {
                                    agioprice = zitems[i]["agioprice"].ToString();
                                }
                                string discountprice ="0";
                                if (zitems[i]["discountprice"] != null && !"".Equals(zitems[i]["discountprice"].ToString()))
                                {
                                    discountprice = zitems[i]["discountprice"].ToString();
                                }
                                string kongge = "";
                                double sagioprice = Double.Parse(agioprice);
                                // string isexception = zitems[i]["isexception"].ToString();

                                if (zitems[i]["guigename"] != null && !zitems[i]["guigename"].ToString().Equals(""))
                                {
                                    name += "（" + zitems[i]["guigename"].ToString() + "）";
                                    
                                }
                                if (zitems[i]["unit"] != null && !"".Equals(zitems[i]["unit"].ToString()))
                                {
                                    name = name + "/" + zitems[i]["unit"];
                                }
                                string fprice = float.Parse(price).ToString("F2");
                               
                                if (zitems[i]["isexception"] != null && !zitems[i]["isexception"].ToString().Equals("1"))
                                {
                                    fprice = string.Format("{0,6}", fprice);
                                    if (sagioprice >0&& !agioprice.Equals(discountprice))
                                    {
                                        name = "(折)" + name;
                                        kongge += "  ";
                                    }
                                }
                                else {
                                    fprice = string.Format("{0,6}", fprice);
                                    name = "(退)"+name;
                                    kongge+= "  ";
                                }
                              
                                string ename = "";//为了菜名过长设定，截断字符串

                                var slen = getStringLength(name);
                                if (slen > 18)
                                {

                                    ename = name.Substring(9, name.Length - 9);
                                    name = name.Substring(0, 9);

                                }
                                int chakong = 18 - getStringLength(name);
                                if (chakong > 0)
                                {
                                    string skongge = string.Format("{0,-" + chakong + "}", "");
                                    name = name + skongge;
                                }
                                num = string.Format("{0,3}", num);
                               
                                // OnWriteData(f, name, false, false, false, 1);
                                // OnWriteData(f, name+num + "      ", false, false, false, 1);
                                if (i == zitems.Count - 1)
                                        OnWriteData(f, name + num + "     " + fprice+ ename + "\n", false, true, false, 1);
                                    else
                                        OnWriteData(f, name + num + "     " + fprice+ ename + "\n\n", false, true, false, 1);
                                }
                                 
                            
                        }
                        OnWriteData(f, linestr + "\n", false, false, false, 1);
                        // 横线
                        //OnWriteData(f, "总金额                    ", false, false, false, 1);
                        string amountstr = string.Format("{0,7}", main["amount"].ToString()+ "元");
                        OnWriteData(f, "总金额                  "+ amountstr + "\n", false, false, false, 1);
                      
                        if (samountException > 0)
                        {
                            amountException = string.Format("{0,7}", amountException + "元");
                            //OnWriteData(f, "异常金额                   ", false, false, false, 1);
                            OnWriteData(f, "\n异常金额                " + amountException + "\n", false, false, false, 1);
                        }
                        float youhui = 0;
                        if (main["youhui"] != null)
                            youhui = float.Parse(main["youhui"].ToString());
                      
                        if (youhui > 0)
                        {
                            string youhuistr = string.Format("{0,7}", youhui + "元");
                            // OnWriteData(f, "优惠                    ", false, false, false, 1);
                            OnWriteData(f, "\n优惠                    " + youhuistr + "\n", false, false, false, 1);
                        }
                        float fuwufee = 0;
                        if (main["fuwufei"] != null)
                            fuwufee = float.Parse(main["fuwufei"].ToString());
                        if (fuwufee > 0)
                        {
                            string fuwufeestr = string.Format("{0,7}", fuwufee + "元");
                            //OnWriteData(f, "异常金额                   ", false, false, false, 1);
                            OnWriteData(f, "\n服务费                  " + fuwufeestr + "\n", false, false, false, 1);
                        }
                        if (sweifuamount > 0)
                        {


                          
                           if (syifuamount > 0)
                                {
                                    yifuamount = string.Format("{0,7}", yifuamount + "元");
                                    // OnWriteData(f, "-已付                   ", false, false, false, 1);
                                    OnWriteData(f, "\n已付                    " + yifuamount + "\n", false, false, false, 1);
                                }
                                
                                    OnWriteData(f, linestr + "\n", false, false, false, 1);
                                    string shishou = Math.Round(sweifuamount, 2) + "元";
                                    shishou = string.Format("{0,7}", shishou);
                                    // OnWriteData(f, "实收                       ", false, false, false, 1);
                                    OnWriteData(f, "需收                    " + shishou + "\n", false, false, false, 1);
                               
                            
                             
                        }
                        else {
                           
                            if (syifuamount > 0)
                            {
                                OnWriteData(f, linestr + "\n", false, false, false, 1);
                                JArray pays = (JArray)main["pays"];
                                if (pays.Count > 0)
                                {
                                    for (int k = 0; k < pays.Count; k++)
                                    {
                                        string money = pays[k]["money"].ToString() + "元";
                                        money = string.Format("{0,7}", money);
                                        string payway = pays[k]["payway"].ToString()+"支付";
                                        int chakong = 24 - getStringLength(payway);
                                        if (chakong > 0)
                                        {
                                            string skongge = string.Format("{0,-" + chakong + "}", "");
                                            payway = payway + skongge;
                                        }
                                        OnWriteData(f, payway + money + "\n", false, false, false, 1);
                                    }
                                    JArray reorders = (JArray)main["reorders"];
                                    if (reorders!=null&&reorders.Count > 0)
                                    {
                                        for (int m = 0; m < reorders.Count; m++)
                                        {
                                            string money = reorders[m]["refund_fee"].ToString() + "元";
                                            money = string.Format("{0,7}", money);
                                            string payway = "-"+reorders[m]["payway"].ToString() + "退款";
                                            int chakong = 24 - getStringLength(payway);
                                            if (chakong > 0)
                                            {
                                                string skongge = string.Format("{0,-" + chakong + "}", "");
                                                payway = payway + skongge;
                                            }
                                            OnWriteData(f, payway + money + "\n", false, false, false, 1);
                                        }
                                    }
                                }
                                else {
                                    //string shishou = Math.Round(syifuamount, 2) + "元";
                                    //shishou = string.Format("{0,7}", shishou);
                                    yifuamount = string.Format("{0,7}", yifuamount + "元");                                 
                                    OnWriteData(f, "实收                    " + yifuamount + "\n", false, false, false, 1);
                                }
 
                            }

                        }
                      
                        OnWriteData(f, linestr + "\n", false, false, false, 1);
                        // 横线 
                        string beizhu = "备注:" + main["remark"] + main["customremark"];
                        OnWriteData(f, beizhu , true, true, false, 1);
                        if (pcontent.Keys.Contains("comm"))
                        {

                            string comm = "\n\n" + (string)pcontent["comm"];
                            OnWriteData(f, comm, false, false, false, 1);
                        }
                        cutPage(f);
                        #endregion
                    }
                }
            }


        }
        private void jiedanLptPrintPage(string printname, dd_printers printer)
        {
            if (pcontent != null)
            {
                JObject main = (JObject)pcontent["main"];
                #region 执行指令打印
                uint width = 2;
                if (printer == null)
                {
                    //打印失败打印机未连接
                    return;
                }
                int psize = printer.psize.Value;
                int pwidth = psize;
                string linestr = "------------------------------------------------";
                string kongestr = "";
                if (psize == 58)
                {
                    width = 1;

                    pwidth = 190;
                    linestr = "--------------------------------";
                }
                else if (psize == 70)
                {
                    width = 2;
                    pwidth = 250;
                    linestr = "-----------------------------------------";
                }
                else if (psize == 76)
                {
                    width = 2;
                    pwidth = 260;
                    linestr = "----------------------------------------";
                }
                else if (psize == 80)
                {
                    kongestr = "                ";
                    width = 2;
                    pwidth = 260;
                }

                //BeiYangOPOS.POS_SetLineSpacing(50);
                LPTControls lpt = new LPTControls();
                lpt.Open(printname);
                lpt.OnWriteData("点点菜单\n" + (char)(10), false, false, false, 2);
                        string tablename = main["tablename"].ToString();
                        bool _actual1 = HasChinese(tablename);
                string serialno = "";// main["serialno"].ToString();
                        //try
                        //{
                        //    int no = Convert.ToInt32(serialno);
                        //    serialno = string.Format("{0,-10:D3}", no);
                       // }
                       // catch (Exception es)
                       // {

                       //     Console.WriteLine("结款小票的打印:" + es.Message);
                       // }
                        if (_actual1)
                        {
                            tablename = tablename + " " + serialno;
                        }
                        else
                        {
                            tablename = tablename + "号桌 " + serialno;
                        }

                lpt.OnWriteData( "      " + tablename + "\n\n", true, true, false, 2);

                        //OnWriteData(f, "订单编号", false, false, false, 1);
                        string orderno = main["orderno"].ToString();
                        if (orderno.Length > 14)
                        {
                    lpt.OnWriteData( "订单编号       " + kongestr + main["orderno"].ToString() + "\n", false, false, false, 1);
                        }
                        else
                        {
                    lpt.OnWriteData("订单编号          " + kongestr + main["orderno"].ToString() + "\n", false, false, false, 1);
                        }

                //OnWriteData(f, "点餐时间", false, false, false, 1);
                lpt.OnWriteData( "点餐时间             " + kongestr + main["addtime"].ToString() + "\n", false, false, false, 1);

                lpt.OnWriteData(linestr + "\n", false, false, false, 1);

                        string amount = main["amount"].ToString();
                        string weifuamount = main["weifuamount"].ToString();
                        double samount = Double.Parse(amount);
                        string yifuamount = main["yifuamount"].ToString();
                double sweifuamount = Double.Parse(weifuamount);
                double syifuamount = Double.Parse(yifuamount);
                        string amountException = main["amountexception"].ToString();
                        double samountException = Double.Parse(amountException);

                if (sweifuamount > 0)
                {
                    lpt.OnWriteData("未支付\n", false, false, false, 1);
                }
                else
                {
                    lpt.OnWriteData("已支付\n", false, false, false, 1);
                }
               
                lpt.OnWriteData( linestr + "\n", false, false, false, 1);
                // OnWriteData(f, "项目        ", false, false, false, 1);
                // OnWriteData(f, "项目              数量      ", false, false, false, 1);
                lpt.OnWriteData( "项目              " + kongestr+"数量      小计\n", false, false, false, 1);
                lpt.OnWriteData( linestr + "\n", false, false, false, 1);

                        JArray zitems = (JArray)main["items"];
                        if (zitems.Count > 0)
                        {

                            for (int i = 0; i < zitems.Count; i++)
                            {
                                //菜品打印h20
                                string num = zitems[i]["num"].ToString();
                                string price = zitems[i]["price"].ToString();
                      
                        if (zitems[i]["weight"] != null)
                        {
                            string weight = zitems[i]["weight"].ToString();
                            float weig = float.Parse(weight);
                            if (weig > 0)
                            {
                                num = weig + "";

                                // price = float.Parse(price) / weig + "";
                                double snum = Double.Parse(num);
                                double sprice = Double.Parse(price);
                                price = sprice + "";
                            }
                            else
                            {
                                double snum = Double.Parse(num);
                                double sprice = Double.Parse(price);
                                price = sprice * snum + "";
                            }
                        }
                        else
                        {
                            double snum = Double.Parse(num);
                            double sprice = Double.Parse(price);
                            price = sprice * snum + "";
                        }


                        string name = zitems[i]["name"].ToString();
                       
                        string agioprice = "0";
                        if (zitems[i]["agioprice"] != null)
                            agioprice = zitems[i]["agioprice"].ToString();
                        string discountprice = zitems[i]["discountprice"].ToString();
                        string kongge = "";
                        double sagioprice = Double.Parse(agioprice);
                        string isexception = zitems[i]["isexception"].ToString();

                                if (zitems[i]["guigename"] != null && !zitems[i]["guigename"].ToString().Equals(""))
                                {
                                    name += "（" + zitems[i]["guigename"].ToString() + "）";

                                }

                        if (zitems[i]["unit"] != null && !"".Equals(zitems[i]["unit"].ToString()))
                        {
                            name = name + "/" + zitems[i]["unit"];
                        }
                        string fprice = float.Parse(price).ToString("F2");
                        fprice = string.Format("{0,6}", fprice);
                        
                        if (zitems[i]["isexception"] != null && !zitems[i]["isexception"].ToString().Equals("1"))
                        {
                            if (sagioprice>0 && !agioprice.Equals(discountprice))
                            {
                                name = "(折)" + name;
                                kongge += "  ";
                            }
                        }
                        else {
                            name = "(退)" + name;
                            kongge += "  ";
                        }
                     
                        string ename = "";//为了菜名过长设定，截断字符串
                        var slen = getStringLength(name);
                        if (slen > 18)
                        {

                            ename = name.Substring(9, name.Length - 9);
                            name = name.Substring(0, 9);

                        }
                        int chakong = 18 - getStringLength(name);
                        if (chakong > 0)
                        {
                            string skongge = string.Format("{0,-" + chakong + "}", "");
                            name = name + skongge;
                        }
                        num = string.Format("{0,3}", num);
                         
                                // OnWriteData(f, name, false, false, false, 1);
                                // OnWriteData(f, name+num + "      ", false, false, false, 1);
                                
                                    if (i == zitems.Count - 1)
                                lpt.OnWriteData( name + kongestr + num + "     " + fprice + ename + "\n", false, true, false, 1);
                                    else
                                lpt.OnWriteData( name + kongestr + num + "     " + fprice + ename + "\n\n", false, true, false, 1);
                               

                            }
                        }
                lpt.OnWriteData( linestr + "\n", false, false, false, 1);
                        // 横线
                        //OnWriteData(f, "总金额                    ", false, false, false, 1);
                        string amountstr = string.Format("{0,6}", main["amount"].ToString() + "元");
                lpt.OnWriteData( "总金额                   " + kongestr + amountstr + "\n", false, false, false, 1);

                        if (samountException > 0)
                        {
                            amountException = string.Format("{0,6}", amountException + "元");
                    //OnWriteData(f, "异常金额                   ", false, false, false, 1);
                    lpt.OnWriteData( "\n-异常金额                " + kongestr + amountException + "\n", false, false, false, 1);
                        }
                        float youhui = 0;
                        if (main["youhui"] != null)
                            youhui = float.Parse(main["youhui"].ToString());

                        if (youhui > 0)
                        {
                            string youhuistr = string.Format("{0,6}", youhui + "元");
                    // OnWriteData(f, "优惠                    ", false, false, false, 1);
                    lpt.OnWriteData( "\n优惠                  " + kongestr + youhuistr + "\n", false, false, false, 1);
                        }

                float fuwufee = 0;
                if (main["fuwufei"] != null)
                    fuwufee = float.Parse(main["fuwufei"].ToString());
                if (fuwufee > 0)
                {
                    string fuwufeestr = string.Format("{0,7}", fuwufee + "元");
                    //OnWriteData(f, "异常金额                   ", false, false, false, 1);
                    lpt.OnWriteData( "\n服务费                  " + fuwufeestr + "\n", false, false, false, 1);
                }
                if (sweifuamount > 0)
                {



                    if (syifuamount > 0)
                    {
                        yifuamount = string.Format("{0,7}", yifuamount + "元");
                        // OnWriteData(f, "-已付                   ", false, false, false, 1);
                        lpt.OnWriteData( "\n已付                    " + yifuamount + "\n", false, false, false, 1);
                    }

                    lpt.OnWriteData(linestr + "\n", false, false, false, 1);
                    string shishou = Math.Round(sweifuamount, 2) + "元";
                    shishou = string.Format("{0,7}", shishou);
                    // OnWriteData(f, "实收                       ", false, false, false, 1);
                    lpt.OnWriteData( "需收                    " + shishou + "\n", false, false, false, 1);



                }
                else
                {

                    
                    if (syifuamount > 0)
                    {
                        lpt.OnWriteData(linestr + "\n", false, false, false, 1);
                        JArray pays = (JArray)main["pays"];
                        if (pays.Count > 0)
                        {
                            for (int k = 0; k < pays.Count; k++)
                            {
                                string money = pays[k]["money"].ToString() + "元";
                                money = string.Format("{0,7}", money);
                                string payway = pays[k]["payway"].ToString() + "支付";
                                int chakong = 24 - getStringLength(payway);
                                if (chakong > 0)
                                {
                                    string skongge = string.Format("{0,-" + chakong + "}", "");
                                    payway = payway + skongge;
                                }
                                lpt.OnWriteData(payway + money + "\n", false, false, false, 1);
                            }
                            JArray reorders = (JArray)main["reorders"];
                            if (reorders!=null&&reorders.Count > 0)
                            {
                                for (int m = 0; m < reorders.Count; m++)
                                {
                                    string money = reorders[m]["refund_fee"].ToString() + "元";
                                    money = string.Format("{0,7}", money);
                                    string payway = "-" + reorders[m]["payway"].ToString() + "退款";
                                    int chakong = 24 - getStringLength(payway);
                                    if (chakong > 0)
                                    {
                                        string skongge = string.Format("{0,-" + chakong + "}", "");
                                        payway = payway + skongge;
                                    }
                                    lpt.OnWriteData(payway + money + "\n", false, false, false, 1);
                                }
                            }
                        }
                        else
                        {
                            
                            //yifuamount = string.Format("{0,7}", yifuamount + "元");
                            //lpt.OnWriteData( "实收                    " + yifuamount + "\n", false, false, false, 1);

                            lpt.OnWriteData(linestr + "\n", false, false, false, 1);
                            string shishou = Math.Round(syifuamount, 2) + "元";
                            shishou = string.Format("{0,7}", shishou);
                            yifuamount = string.Format("{0,7}", yifuamount + "元");
                            // OnWriteData(f, "实收                       ", false, false, false, 1);
                            lpt.OnWriteData("实收                    " + yifuamount + "\n", false, false, false, 1);
                        }

                    }

                }
                lpt.OnWriteData(linestr + "\n", false, false, false, 1);
                        // 横线 
                        string beizhu = "备注:" + main["remark"] + main["customremark"];
                lpt.OnWriteData(beizhu, true, true, false, 1);
                if (pcontent.Keys.Contains("comm"))
                {

                    string comm = "\n\n"+(string)pcontent["comm"] ;
                    lpt.OnWriteData( comm, false, false, false, 1);
                }
                lpt.CutPaper();
                lpt.Close();
                        #endregion
                    }
             


        }
        //打印结单小票
        private void printDocument1_jiedanPrintPage(object sender, PrintPageEventArgs e)
        {
            if (pcontent != null)
            {
                JObject main = (JObject)pcontent["main"];
                e.Graphics.Clear(Color.White);
                // 开始绘制文档  
                // 默认为横排文字  
                Graphics g = form.CreateGraphics();
                Font cfont = new Font(new FontFamily("宋体"), 9, FontStyle.Bold);
                Font tfont = new Font(new FontFamily("宋体"), 16, FontStyle.Bold);
                Font cpfont = new Font(new FontFamily("宋体"), 13, FontStyle.Bold);
                int pwidth = e.PageBounds.Width;//纸张宽度
                Pen pen1 = new Pen(Color.Black);
                pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                StringFormat SF = new StringFormat();
                SF.LineAlignment = StringAlignment.Center;                                        //设置属性为水平居中
                SF.Alignment = StringAlignment.Center;                                               //设置属性为垂直居中
                string shopname = "点点菜单";
                if (main["name"] != null && !"".Equals(main["name"].ToString())) {
                    shopname = main["name"].ToString();
                }
                RectangleF rect = new RectangleF(0, 0, pwidth, e.Graphics.MeasureString(shopname, cfont).Height);    //其中e.PageBounds属性表示页面全部区域的矩形区域
                                                                                                                   //e.Graphics.MeasureString("点点菜单", new Font("Times New Roman", 20)).Height;
                                                                                                                   //表示获取你要打印字符串的高度
                e.Graphics.DrawString(shopname, cfont, Brushes.Black, rect, SF);
                string tablename = main["tablename"].ToString();// + main["serialno"].ToString();
                bool _actual1 = HasChinese(tablename);
                if (_actual1)
                {
                    
                }
                else
                {
                    tablename = tablename + "号桌  ";
                }
                RectangleF rect1 = new RectangleF(0, 32, pwidth, e.Graphics.MeasureString(tablename, tfont).Height);
                e.Graphics.DrawString(tablename, tfont, Brushes.Black, rect1, SF);

                e.Graphics.DrawString("订单编号", cfont, System.Drawing.Brushes.Black, 0, 70);
                e.Graphics.DrawString(main["orderno"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(main["orderno"].ToString(), cfont).Width, 70);

                e.Graphics.DrawString("点餐时间", cfont, System.Drawing.Brushes.Black, 0, 85);
                e.Graphics.DrawString(main["addtime"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(main["addtime"].ToString(), cfont).Width, 85);
                int cheight = 105;
                //e.Graphics.DrawString("点餐时间", cfont, System.Drawing.Brushes.Black, 0, cheight);
               // e.Graphics.DrawString(main["addtime"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(main["addtime"].ToString(), cfont).Width, cheight);
                // 横线  
                e.Graphics.DrawLine(pen1, 0, cheight, pwidth, cheight);
                cheight += 5;
                string amount = main["amount"].ToString();
                string weifuamount= main["weifuamount"].ToString();
                double samount = Double.Parse(amount);

                string yifuamount = main["yifuamount"].ToString();
                double syifuamount = Double.Parse(yifuamount);
                string amountException = main["amountexception"].ToString();
                double samountException = Double.Parse(amountException);
                double sweifuamount = Double.Parse(weifuamount);
                if (sweifuamount > 0)
                {
                    e.Graphics.DrawString("未支付", cfont, System.Drawing.Brushes.Black, 0, cheight);
                    cheight += 20;

                } else {
                    e.Graphics.DrawString("已支付", cfont, System.Drawing.Brushes.Black, 0, cheight);
                    cheight += 20;

                }
                // 横线  
                e.Graphics.DrawLine(pen1, 0, cheight, pwidth, cheight);
                cheight += 5;
                e.Graphics.DrawString("项目", cfont, System.Drawing.Brushes.Black, 0, cheight);
                e.Graphics.DrawString("数量", cfont, System.Drawing.Brushes.Black, pwidth - 89 + (49 - e.Graphics.MeasureString("数量", cfont).Width) / 2, cheight);
                e.Graphics.DrawString("小计", cfont, System.Drawing.Brushes.Black, pwidth - 40 + (40 - e.Graphics.MeasureString("金额", cfont).Width) / 2, cheight);
                cheight += 20;
                // 横线  
                e.Graphics.DrawLine(pen1, 0, cheight, pwidth, cheight);
                cheight += 5;
                JArray zitems = (JArray)main["items"];
                if (zitems.Count > 0)
                {
                     for (int i = 0; i < zitems.Count; i++)
                        {
                            //菜品打印h20
                            string num = zitems[i]["num"].ToString();
                            
                            string price = zitems[i]["price"].ToString();
                        string tprice = price;
                        if (zitems[i]["weight"] != null)
                        {
                            string weight = zitems[i]["weight"].ToString();
                            float weig = float.Parse(weight);
                            if (weig > 0)
                            {
                                num = weig + "";

                                //price = float.Parse(price) / weig + "";
                                double sprice = Double.Parse(price);
                                double snum = Double.Parse(num);
                                tprice = sprice  + "";
                            }
                            else {
                                double sprice = Double.Parse(price);
                                double snum = Double.Parse(num);
                                 tprice = sprice * snum + "";
                            }
                        }
                        else {
                            double sprice = Double.Parse(price);
                            double snum = Double.Parse(num);
                             tprice = sprice * snum + "";
                        }
                        string dyouhui = zitems[i]["youhui"].ToString();



                        string name = zitems[i]["name"].ToString();
                       
                        string agioprice = "0";
                        if (zitems[i]["agioprice"] != null)
                            agioprice = zitems[i]["agioprice"].ToString();
                        double sagioprice = Double.Parse(agioprice);
                        string discountprice = zitems[i]["discountprice"].ToString();

                      
                       
                        if (zitems[i]["guigename"] != null && !zitems[i]["guigename"].ToString().Equals(""))
                            {
                                name += "(" + zitems[i]["guigename"].ToString() + ")";
                            }

                        if (zitems[i]["unit"] != null && !"".Equals(zitems[i]["unit"].ToString()))
                        {
                            name = name + "/" + zitems[i]["unit"];
                        }
                        if (zitems[i]["isexception"] != null && !zitems[i]["isexception"].ToString().Equals("1"))
                        {
                            if (sagioprice > 0 && !agioprice.Equals(discountprice))
                            {
                                name = "(折)" + name;
                            }
                        }
                        else
                        {
                            // tprice = "-" + tprice;
                            name = "(退)" + name;
                        }
                        StringFormat fmt = new StringFormat();
                        fmt.LineAlignment = StringAlignment.Near;//左对齐
                        fmt.FormatFlags = StringFormatFlags.LineLimit;//自动换行

                        //设定文本打印区域 b是左上角坐标，Size是打印区域（矩形） float mmtopt = 2.835f;
                        //Rectangle r = new Rectangle();
                        double sheight = 25;
                        int hznums = GetHanNumFromString(name);//数字个数
                        int onums = (int)Math.Ceiling((name.Length - hznums) / 2.0);
                        int nlength= getStringLength(name);
                        //int zlength = hznums + onums;
                        double tail = 12;
                        if (pwidth > 200)
                        {
                            tail = 16;
                        }
                        if (nlength > tail)
                        {
                            sheight = sheight * Math.Ceiling(nlength / tail);
                        }
                        Point b = new Point(0, cheight);
                        Rectangle r = new Rectangle(b, new Size(pwidth - 89, 25 * 2));
                        e.Graphics.DrawString(name, cpfont, new SolidBrush(Color.Black), r, fmt);
                        //e.Graphics.DrawString(name, cfont, System.Drawing.Brushes.Black, 0, cheight);
                            e.Graphics.DrawString(num, cfont, System.Drawing.Brushes.Black, pwidth - 89 + (49 - e.Graphics.MeasureString(num, cfont).Width) / 2, cheight);
                            e.Graphics.DrawString(tprice, cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(tprice, cfont).Width, cheight);
                            cheight += (int)sheight;
                        }
                   
                }
                cheight += 5;
                // 横线 
                e.Graphics.DrawLine(pen1, 0, cheight, pwidth, cheight);
                cheight += 5;
                e.Graphics.DrawString("总金额", cfont, System.Drawing.Brushes.Black, 0, cheight);
                double zongjia = samount + samountException;
                e.Graphics.DrawString(amount, cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(amount, cfont).Width, cheight);
                float youhui = 0;
                if (main["youhui"] != null)
                    youhui = float.Parse(main["youhui"].ToString());
                
                    if (youhui > 0)
                {
                    cheight += 20;
                    string youhuistr = string.Format("{0,6}", youhui + "元");
                    e.Graphics.DrawString("优惠", cfont, System.Drawing.Brushes.Black, 0, cheight);
                    e.Graphics.DrawString(youhuistr, cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(youhuistr, cfont).Width, cheight);

                }
                float fuwufee = 0;
                if (main["fuwufei"] != null)
                    fuwufee = float.Parse(main["fuwufei"].ToString());
                if (fuwufee > 0)
                {
                    cheight += 20;
                    string fuwufeestr = string.Format("{0,6}", fuwufee + "元");
                    e.Graphics.DrawString("服务费", cfont, System.Drawing.Brushes.Black, 0, cheight);
                    e.Graphics.DrawString(fuwufeestr, cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(fuwufeestr, cfont).Width, cheight);
                }
                if (sweifuamount > 0)
                {


                   
                        if (syifuamount > 0)
                        {
                            cheight += 20;
                            yifuamount = yifuamount + "元";
                            e.Graphics.DrawString("已付", cfont, System.Drawing.Brushes.Black, 0, cheight);
                            e.Graphics.DrawString(yifuamount, cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(yifuamount, cfont).Width, cheight);
                        }
                            cheight += 20;
                            e.Graphics.DrawLine(pen1, 0, cheight, pwidth, cheight);
                            cheight += 5;
                            string shishou = Math.Round(sweifuamount, 2) + "元";
                            e.Graphics.DrawString("需收", cfont, System.Drawing.Brushes.Black, 0, cheight);
                            e.Graphics.DrawString(shishou, cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(shishou, cfont).Width, cheight);
                       
                     
                }
                else
                {

                    if (syifuamount > 0)
                    {
                       



                        cheight += 20;
                        e.Graphics.DrawLine(pen1, 0, cheight, pwidth, cheight);
                        cheight += 5;
                        JArray pays = (JArray)main["pays"];
                        if (pays.Count > 0)
                        {
                            for (int k = 0; k < pays.Count; k++)
                            {
                                string money = pays[k]["money"].ToString() + "元";
                                
                                string payway = pays[k]["payway"].ToString() + "支付";
                                
                                 
                                e.Graphics.DrawString(payway, cfont, System.Drawing.Brushes.Black, 0, cheight);
                                e.Graphics.DrawString(money, cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(money, cfont).Width, cheight);
                                cheight += 20;
                            }
                            JArray reorders = (JArray)main["reorders"];
                            if (reorders!=null&&reorders.Count > 0)
                            {
                                for (int m = 0; m < reorders.Count; m++)
                                {
                                    string money = reorders[m]["refund_fee"].ToString() + "元";
                                    //money = string.Format("{0,7}", money);
                                    string payway = "-" + reorders[m]["payway"].ToString() + "退款";
                                    e.Graphics.DrawString(payway, cfont, System.Drawing.Brushes.Black, 0, cheight);
                                    e.Graphics.DrawString(money, cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(money, cfont).Width, cheight);
                                    cheight += 20;
                                }
                            }
                        }
                        else
                        {

                            //yifuamount = string.Format("{0,7}", yifuamount + "元");
                            //lpt.OnWriteData( "实收                    " + yifuamount + "\n", false, false, false, 1);
                            cheight += 20;
                            e.Graphics.DrawLine(pen1, 0, cheight, pwidth, cheight);
                            cheight += 5;

                            yifuamount = yifuamount + "元";
                            // OnWriteData(f, "实收                       ", false, false, false, 1);
                            e.Graphics.DrawString("实收", cfont, System.Drawing.Brushes.Black, 0, cheight);
                            e.Graphics.DrawString(yifuamount, cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(yifuamount, cfont).Width, cheight);
                            cheight += 20;
                        }
                    }

                }
               
 
                if (pcontent.Keys.Contains("comm"))
                {
                  
                    e.Graphics.DrawLine(pen1, 0, cheight, pwidth, cheight);
                    cheight +=5;
                    string comm = (string)pcontent["comm"] + "\n\n";

                    comm = AutomaticLine(comm, 8, pwidth);//8, 36  
                    RectangleF drawRect2 = new RectangleF(0, cheight, pwidth, e.Graphics.MeasureString(comm, cfont).Height); //设定这个就行了
                    e.Graphics.DrawString(comm, cfont, Brushes.Black, drawRect2, null);
                }
            }

        }
        /// <summary>    
        /// 返回字符的实际截取位置    
        /// </summary>    
        /// <param name="bytes">UCS2码</param>    
        /// <param name="intLength">要截取的字节长度</param>    
        /// <returns></returns>    
        /// <remarks></remarks>    
        public int RealCutPos(byte[] bytes, int intLength)
        {
            //获取UCS2编码    
            int intCountB = 0;
            // 统计当前的字节数     
            int intCutPos = 0;
            //记录要截取字节的位置      

            while ((intCutPos < bytes.GetLength(0) && intCountB < intLength))
            {
                // 偶数位置，如0、2、4等，为UCS2编码中两个字节的第一个字节    
                if (intCutPos % 2 == 0)
                {
                    // 在UCS2第一个字节时，字节数加1    
                    intCountB += 1;
                }
                else
                {
                    // 当UCS2编码的第二个字节大于0时，该UCS2字符为汉字，一个汉字算两个字节    
                    if (bytes[intCutPos] > 0)
                    {
                        intCountB += 1;
                    }
                }
                intCutPos += 1;
            }

            // 如果intCutPos为奇数时，处理成偶数      
            if (intCutPos % 2 == 1)
            {
                // 该UCS2字符是汉字时，去掉这个截一半的汉字    
                if (bytes[intCutPos] > 0)
                {
                    intCutPos = intCutPos - 1;
                }
                else
                {
                    // 该UCS2字符是字母或数字，则保留该字符    
                    intCutPos = intCutPos + 1;
                }
            }

            return intCutPos / 2;
        }

        /// <summary>    
        /// 处理字符串自动换行问题。最短为intLenMin，最长为intLenMax，最后一行用空格补齐到intLenMin长度。http://blog.csdn.net/xiaoxian8023/article/details/7276220    
        /// </summary>    
        /// <param name="strOldText">原字符串</param>    
        /// <param name="intLenMin">最短字节长度</param>    
        /// <param name="intLenMax">最长字节长度</param>    
        /// <returns>string</returns>    
        /// <remarks></remarks>    
        public string AutomaticLine(string strOldText, int intLenMin, int intLenMax)
        {

            int intLength = 0;
            string strResult = "";

            //获取原字符串的字节长度    
            intLength = System.Text.Encoding.GetEncoding("gb2312").GetByteCount(strOldText);

            if (intLength > intLenMax)
            {
                //总字节数> 最长截取的最长字节数，    
                //则截取最长字节数, 然后对剩余字符串再处理    

                //获取字符串的UCS2码    
                byte[] bytes = System.Text.Encoding.Unicode.GetBytes(strOldText);
                //获取字符的实际截取位置    
                int intCutPos = RealCutPos(bytes, intLenMax);
                //采用递归调用    
                strResult = System.Text.Encoding.Unicode.GetString(bytes, 0, intCutPos * 2) + "\r\n" + AutomaticLine(Strings.Mid(strOldText, intCutPos + 1), intLenMin, intLenMax);

            }
            else if (intLength > intLenMin)
            {
                //如果 最长字节数 >总字节数 > 最短字节数，则 换行，并补齐空格到最短字节数位置    
                strResult = strOldText + "\r\n" + Strings.Space(intLenMin);

            }
            else
            {
                //如果 总字节数 < 最短字节数，则直接补齐空格到最短字节数的位置    
                strResult = strOldText + Strings.Space(intLenMin - intLength);
            }
            return strResult;
        }
        public void testUsbPrint(string printname)
        {

            try
            {
                using (var sh = UsbPrinterResolver.OpenUSBPrinterBydeviceId(printname))
                {
                    MessageBox.Show("开始打印2  " + DateTime.Now.ToString()+":"+ sh);
                    using (var f = new System.IO.FileStream(sh, System.IO.FileAccess.ReadWrite))
                    {
                        MessageBox.Show("开始打印  " + DateTime.Now.ToString()+":"+f.ToString());
                        // usbStatus(f);
                        //UsbPrinterResolver.USBDataRead(sh);
                        // Read from and write to the stream f  
                        StringBuilder sb = new StringBuilder("菜单 【455】");
                        sb.Append("-----------------------------------------\n");
                        sb.Append("厨房:123 送单人:44");
                        sb.Append("桌台:1 @ 菜类：5454\n");
                        sb.Append("-----------------------------------------\n");
                        //WriteData(f, sb.ToString());
                        OnWriteData(f, "菜单 【455】\n", true, true, false, 2);

                        OnWriteData(f, "- - - - - - - - - - - - - - - -\n", false, false, false, 0);

                        OnWriteData(f, "So You Want？\n", false, false, false, 2);

                        OnWriteData(f, "- - - - - - - - - - - - - - - -\n", true, false, false, 0);
                        OnWriteData(f, "厨房:123\n", true, true, false, 1);
                        OnWriteData(f, "我找到了哈哈哈" + (char)(10), false, false, false, 3);
                        OnWriteData(f, "ok test" + (char)(10), false, false, false, 2);
                        //OnWriteData(f, "菜单 【455】\n", true, true, false, 2);

                        cutPage(f);

                        f.Close();
                        MessageBox.Show("结束打印  " + DateTime.Now.ToString());
                    }
                }
            }
            catch (Exception e) {
                MessageBox.Show("打印异常 " +e.ToString());
            }
            
        }
        //打印输出
        public  void WriteData(FileStream fs, string msg)
        {
            
            //StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);

            //开钱箱指令
            // sw.Write(((char)27).ToString() + "p" + ((char)0).ToString() + ((char)60).ToString() + ((char)255).ToString());
            //打印小票头
            // sw.Write(send);
            // 格式：　 ASCII：　GS V m
            // 十进制：　29 86 m
            // 十六进制：　1D 56 m
            // send = "" + (char)(29) + (char)(86) + (char)(49);//切纸命令
            Encoding encoder = Encoding.Default;
            byte[] bytes = encoder.GetBytes(msg);
            fs.Write(bytes, 0, bytes.Length);
            fs.Flush();
           // sw.Write(msg);
           // sw.WriteLine("");
           //sw.Close();



        }
        public void cutPage(FileStream fs) {
            Encoding encoder = Encoding.Default;
            byte[] bytes = encoder.GetBytes(""+(char)(10) + (char)(10) + (char)(10) + (char)(10) + (char)(10) + (char)(27) + (char)(109));//切纸//+ (char)(10)+ (char)(10) + (char)(10) 
            fs.Write(bytes, 0, bytes.Length);

        }
        public void usbStatus(FileStream fs)
        {
            Encoding encoder = Encoding.Default;
            byte[] bytes = encoder.GetBytes("" + (char)(16) + (char)(04) + (char)(1));//切纸
            fs.Write(bytes, 0, bytes.Length);
           
                int fsLen = (int)fs.Length;
                byte[] heByte = new byte[fsLen];
                int r = fs.Read(heByte, 0, heByte.Length);
                string myStr = System.Text.Encoding.UTF8.GetString(heByte);
                Console.WriteLine(myStr);
                //Console.ReadKey();
            
        }
        //打印数据，meg打印字符串，bBold=true粗体，nZoom=2大一号字体， nHAil=2居中对齐，nHAil=3右对齐。部分打印机可能中文字体设置无效，请加上FS ！命令设置中文字体。
        public bool OnWriteData(FileStream fs, string meg, bool bBold, bool bDTall, bool bDWide, int nHAil) {
            string fmt = "" + (char)(27) + (char)(64) ;
            WriteData(fs, fmt);//回复默认字体
                               // fmt = "" + (char)(27) + (char)(50);//调整行距
                               // WriteData(fs, fmt);//回复默认字体
                               // fmt = "" + (char)(29) + (char)(73) + (char)(69);
                               // WriteData(fs, fmt);//回复默认字体
            meg = "" +(char)(27) + (char)(51) + (char)(15) + meg;//调整行距

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

           WriteData(fs,meg);

         
            //strcpy(s, "\x1B\x21\x00");
            meg = "" + (char)(27) + (char)(97) + (char)(0)+"" + (char)(27) + (char)(33) + (char)(0) ;
            WriteData(fs,meg);//回复默认字体
           // meg = "" + (char)(13);//打印回车
            //WriteData(fs, meg);//回复默认字体
            return true;
        }



        //预定订单小票的打印主要用于餐饮行业2
        public void bookPrint(string printname)
        {
            lock (thisLock)
            {
                Console.WriteLine("档口小票的打印:" + printname);
                int vindex = printname.IndexOf("ip");
                int cindex = printname.IndexOf("COM");
                int uindex = printname.IndexOf("USB\\VID");
                int lindex = printname.IndexOf("LPT");

                BizPrinter dao = new BizPrinter();

                dd_printers printer = dao.QueryPrinters(2, printname).FirstOrDefault();
                if (vindex >= 0)
                {
                    printname = printname.Substring(vindex + 2);
                    //打印的IP一定要预先设置好
                    bool b = opos.OpenNetPort(printname);//"192.168.1.254"
                    if (!b)
                    {
                        Console.WriteLine("初始化'" + printname + "'的打印机参数失败。请检测打印机配置");
                        b = opos.OpenNetPort(printname);//"192.168.1.254"
                        Thread.Sleep(1000);
                        if (!b)
                        {
                            Console.WriteLine("初始化'" + printname + "'的打印机参数失败。请检测打印机配置");
                            utils.ShowTip("警告", "初始化'" + printname + "'的打印机参数失败。请检测打印机配置", 5000);
                            //form.showmsg("初始化'" + printname + "'的打印机参数失败。请检测打印机配置");
                            return;
                        }

                    }



                    Byte res = new Byte();
                    int ret = BeiYangOPOS.POS_NETQueryStatus(printname, out res);
                    StringBuilder sb = new StringBuilder();
                    #region 检测打印机状态
                    if ((res & 0x10) == 0x10)
                    {
                        sb.AppendLine("打印机出错！");
                    }
                    if ((res & 0x02) == 0x02)
                    {
                        sb.AppendLine("打印机脱机！");
                    }
                    if ((res & 0x04) == 0x04)
                    {
                        sb.AppendLine("上盖打开！");
                    }
                    if ((res & 0x20) == 0x20)
                    {
                        sb.AppendLine("切刀出错！");
                    }
                    if ((res & 0x40) == 0x40)
                    {
                        sb.AppendLine("纸将尽！");
                    }
                    if ((res & 0x80) == 0x80)
                    {
                        sb.AppendLine("缺纸！");
                    }
                    #endregion
                    if (sb.Length > 0)
                    {
                        Console.WriteLine("Error",
                                   string.Format("'" + printname + "'的打印机处于非正常状态：" + sb.ToString() + "。请检测打印机配置。",
                                              printname, sb.ToString()));
                        return;
                    }
                    bookPrintPage(printname, printer);
                    return;
                }
                else if (cindex >= 0)
                {

                    SerialPort com = new SerialPort();
                    int pbites = printer.pbites.Value;
                    com.BaudRate = pbites;
                    com.PortName = printname;
                    com.DataBits = 8;
                    bool b = opos.OpenComPort(ref com);
                    if (!b)
                    {
                        string errormsg = string.Format("初始化'{0}'的打印机参数失败。请检测打印机配置",
                                            printname);
                        Console.WriteLine(errormsg);
                        utils.ShowTip("警告", errormsg, 5000);
                        //form.showmsg(errormsg);
                        return;
                    }
                    bookPrintPage(printname, printer);
                    return;
                }
                else if (uindex >= 0)
                {
                    bookUsbPrintPage(printname, printer);
                    return;
                }
                else if (lindex >= 0)
                {
                    bookLptPrintPage(printname, printer);
                    return;
                }
                Console.WriteLine("划单小票的打印:" + printname);
                if (printname == null || "".Equals(printname) || "default".Equals(printname))
                {
                    //this.printDocument1.PrinterSettings.PrinterName = printname;
                }
                else
                {
                    this.printDocument1.PrinterSettings.PrinterName = printname;
                }
                this.printDocument1.DocumentName = "test划单" + DateTime.Now.TimeOfDay.ToString();
                printDocument1.PrintPage +=

                new PrintPageEventHandler(this.printDocument1_bookPrintPage);
                this.printDocument1.Print();
            }
        }
        private void bookPrintPage(string printname, dd_printers printer)
        {

            if (pcontent != null)
            {
                Dictionary<string, object> main = pcontent;
                #region 执行指令打印
                uint width = 2;


                int psize = printer.psize.Value;
                int pwidth = psize;
                string linestr = "------------------------------------------------";
                if (psize == 58)
                {
                    BeiYangOPOS.POS_SetLineSpacing(30);
                    width = 1;
                    pwidth = 190;
                    linestr = "-------------------------------";
                }
                else if (psize == 70)
                {
                    width = 2;
                    pwidth = 250;
                    linestr = "-----------------------------------------";
                }
                else if (psize == 76)
                {
                    width = 2;
                    pwidth = 260;
                    linestr = "----------------------------------------";
                }
                else if (psize == 80)
                {
                    width = 2;
                    pwidth = 280;
                }
                //BeiYangOPOS.POS_SetRightSpacing(0);
                BeiYangOPOS.POS_SetLineSpacing(30);
               
                int nPos = getpos(0, "点点菜单"); //一行宽度为42个字符
                BeiYangOPOS.POS_S_TextOut("点点菜单", (uint)(pwidth - nPos * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();
                string type= main["type"].ToString();
                string otype = main["otype"].ToString();
                string noticetype = main["noticetype"].ToString();
                string  tname ="";
                if (otype != null && "meituan".Equals(otype)) {
                    tname += "（美团）";
                }
                else if (otype != null && "eleme".Equals(otype))
                {
                    tname += "（饿了么）";
                }
                if (type != null && "0".Equals(type))
                {
                   
                   tname = "预定订单"+ tname;
                }
                else {
                    tname = "外卖订单"+ tname;
                }

                if (noticetype != null && "14".Equals(noticetype))
                {//退
                    tname = tname+"（退）";
                }
                bool _actual1 = HasChinese(tname);
               // string serialno = main["serialno"].ToString();
               
                nPos = getpos(0, tname); //一行宽度为42个字符


                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut(tname, (uint)(pwidth - nPos * 12 * 2), width, 2, opos.POS_FONT_TYPE_CHINESE, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut("订单编号", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_S_TextOut(main["orderno"].ToString(), (uint)(pwidth - getpos(1, main["orderno"].ToString()) * 12) * 2, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut("点餐时间", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_S_TextOut(main["addtime"].ToString(), (uint)(pwidth - getpos(1, main["addtime"].ToString()) * 12) * 2 + 12, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut("用餐时间", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_S_TextOut(main["jctime"].ToString(), (uint)(pwidth - getpos(1, main["jctime"].ToString()) * 12) * 2 - 24, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut("项目", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                //e.Graphics.DrawString("数量", cfont, System.Drawing.Brushes.Black, pwidth - 89 + (49 - e.Graphics.MeasureString("数量", cfont).Width) / 2, 110);
                BeiYangOPOS.POS_S_TextOut("数量", (uint)(pwidth - 10 * 12) * 2 - 24, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_S_TextOut("金额", (uint)(pwidth - getpos(1, "金额") * 12) * 2 - 24, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();
                JArray zitems = (JArray)pcontent["items"];
                if (zitems.Count > 0)
                {

                    for (int i = 0; i < zitems.Count; i++)
                    {
                        //菜品打印h20
                        string num = zitems[i]["num"].ToString();
                        string price = zitems[i]["price"].ToString();
                        if (zitems[i]["weight"] != null)
                        {
                            string weight = zitems[i]["weight"].ToString();
                            float weig = float.Parse(weight);
                            if (weig > 0)
                            {
                                num = weig + "";

                                price = float.Parse(price) / weig + "";
                            }
                        }
                        string name = zitems[i]["name"].ToString();
                        
                        if (zitems[i]["istaocan"] != null)
                        {
                            string istaocan = zitems[i]["istaocan"].ToString();
                            if (istaocan.Equals("0"))
                            {//如果套餐不打印套餐菜品子选项
                                continue;
                            }
                        }
                        if (zitems[i]["unit"] != null && !"".Equals(zitems[i]["unit"].ToString()))
                        {
                            name = name + "/" + zitems[i]["unit"];
                        }


                        string ename = "";//为了菜名过长设定，截断字符串
                        if (name.Length > 6)
                        {

                            ename = name.Substring(6, name.Length - 6);
                            name = name.Substring(0, 6);
                        }
                        BeiYangOPOS.POS_S_TextOut(name, 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                        BeiYangOPOS.POS_S_TextOut(num, (uint)(pwidth - 10 * 12) * 2, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                        // string fprice=decimal.Round(decimal.Parse(price + ""), 2)+"";
                        string fprice = float.Parse(price).ToString("F2");
                       
                            // fprice = string.Format("{0,6}",fprice);
                        BeiYangOPOS.POS_S_TextOut(fprice, (uint)(pwidth - getpos(1, fprice) * 24) * 2 + 24, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
 

                        BeiYangOPOS.POS_FeedLine();
                        if (!ename.Equals(""))
                        {
                            BeiYangOPOS.POS_S_TextOut(ename, 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                            BeiYangOPOS.POS_FeedLine();
                        }
                        
                    }
                }
                BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();

                // 横线 

                BeiYangOPOS.POS_S_TextOut("总金额", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_S_TextOut(main["realamount"].ToString() + "元", (uint)(pwidth - getpos(1, main["realamount"].ToString() + "元") * 12) * 2 + 12, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();
                
                BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();
                // 横线 
                string beizhu = "备注:" + main["comm"] ;
                BeiYangOPOS.POS_S_TextOut(beizhu+"\n", 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                string ftype = main["type"].ToString();
                if ("1".Equals(ftype))
                {
                    string address = "" + main["address"] + "\n";
                    BeiYangOPOS.POS_S_TextOut(address, 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    string linkman = "" + main["linkman"] + "  " + main["sex"] + "\n";
                    BeiYangOPOS.POS_S_TextOut(linkman, 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    string linktel = "" + main["linktel"];
                    BeiYangOPOS.POS_S_TextOut(linktel, 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                }
                BeiYangOPOS.POS_S_TextOut("\n\n", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                  
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_CutPaper(1, 50);
                opos.ClosePrinterPort();
                #endregion
            }



        }
        private void bookUsbPrintPage(string printname, dd_printers printer)
        {

            if (pcontent != null)
            {
                Dictionary<string, object> main = pcontent;
                #region 执行指令打印
                uint width = 2;


                int psize = printer.psize.Value;
                int pwidth = psize;
                string linestr = "------------------------------------------------";
                if (psize == 58)
                {
                    width = 1;
                    pwidth = 190;
                    linestr = "--------------------------------";
                }
                else if (psize == 70)
                {
                    width = 2;
                    pwidth = 250;
                    linestr = "-----------------------------------------";
                }
                else if (psize == 76)
                {
                    width = 2;
                    pwidth = 260;
                    linestr = "----------------------------------------";
                }
                else if (psize == 80)
                {
                    width = 2;
                    pwidth = 260;
                }
                using (var sh = UsbPrinterResolver.OpenUSBPrinterBydeviceId(printname))
                {
                    using (var f = new System.IO.FileStream(sh, System.IO.FileAccess.ReadWrite))
                    {
                        OnWriteData(f, "点点菜单\n\n", false, false, false, 2);

                        string type = main["type"].ToString();
                        string tname = "";
                        string otype = main["otype"].ToString();
                        string noticetype = main["noticetype"].ToString();
                        
                        if (otype != null && "meituan".Equals(otype))
                        {
                            tname += "（美团）";
                        }
                        else if (otype != null && "eleme".Equals(otype))
                        {
                            tname += "（饿了么）";
                        }
                        if (type != null && "0".Equals(type))
                        {

                            tname = "预定订单" + tname;
                        }
                        else
                        {
                            tname = "外卖订单" + tname;
                        }

                        if (noticetype != null && "14".Equals(noticetype))
                        {//退
                            tname = tname + "（退）";
                        }
                        bool _actual1 = HasChinese(tname);
                         
                       
                        OnWriteData(f, "" + tname + "\n\n", true, true, false, 2);
                        //OnWriteData(f, "订单编号", false, false, false, 1);
                        string orderno = main["orderno"].ToString();
                        if (orderno.Length > 14)
                        {
                            OnWriteData(f, "订单编号       " + main["orderno"].ToString() + "\n", false, false, false, 1);
                        }
                        else
                        {
                            OnWriteData(f, "订单编号          " + main["orderno"].ToString() + "\n", false, false, false, 1);
                        }
                        //OnWriteData(f, "点餐时间", false, false, false, 1);
                        OnWriteData(f, "点餐时间             " + main["addtime"].ToString() + "\n", false, false, false, 1);
                        //OnWriteData(f, "用餐时间", false, false, false, 1);

                        if (main["jctime"].ToString().Length < 3)
                            OnWriteData(f, "用餐时间                    " + main["jctime"].ToString() + "\n", false, false, false, 1);
                        else
                            OnWriteData(f, "用餐时间                " + main["jctime"].ToString() + "\n", false, false, false, 1);
                        OnWriteData(f, linestr + "\n", false, false, false, 1);
                        //OnWriteData(f, "项目        ", false, false, false, 1);
                        OnWriteData(f, "项目              数量      金额\n", false, false, false, 1);
                        //OnWriteData(f, "金额\n", false, false, false, 1);
                        OnWriteData(f, linestr + "\n", false, false, false, 1);

                        JArray zitems = (JArray)pcontent["items"];
                        if (zitems.Count > 0)
                        {

                            for (int i = 0; i < zitems.Count; i++)
                            {
                                //菜品打印h20
                                string num = zitems[i]["num"].ToString();
                                string price = zitems[i]["price"].ToString();
                                if (zitems[i]["weight"] != null)
                                {
                                    string weight = zitems[i]["weight"].ToString();
                                    float weig = float.Parse(weight);
                                    if (weig > 0)
                                    {
                                        num = weig + "";

                                        price = float.Parse(price) / weig + "";
                                    }
                                }
                                string name = zitems[i]["name"].ToString();
                                if (zitems[i]["unit"] != null && !"".Equals(zitems[i]["unit"].ToString()))
                                {
                                    name = name + "/" + zitems[i]["unit"];
                                }
                                string kongge = "";
                               
                                
                                string ename = "";//为了菜名过长设定，截断字符串
                                if (name.Length > 9)
                                {

                                    ename = name.Substring(9, name.Length - 9);
                                    name = name.Substring(0, 9);
                                }
                                name = string.Format("{0,-9}", name);
                                num = string.Format("{0,3}", num);
                                name = name.Replace(" ", "  ");
                                name = name + kongge;
                                // OnWriteData(f, name, false, false, false, 1);
                                // OnWriteData(f, name+num + "      ", false, false, false, 1);
                                string fprice = float.Parse(price).ToString("F2");

                                
                                    fprice = string.Format("{0,6}", fprice);
                                    if (i == zitems.Count - 1)
                                        OnWriteData(f, name + num + "     " + fprice + ename + "\n", false, true, false, 1);
                                    else
                                        OnWriteData(f, name + num + "     " + fprice + ename + "\n\n", false, true, false, 1);
                                
                            }
                        }
                        OnWriteData(f, linestr + "\n", false, false, false, 1);
                        // 横线 

                        string amountstr = string.Format("{0,7}", main["amount"].ToString() + "元");
                        OnWriteData(f, "总金额                  " + amountstr + "\n", false, false, false, 1);
                        OnWriteData(f, linestr + "\n", false, false, false, 1);
                        // 横线 
                        string beizhu = "备注:" + main["comm"]  + "\n\n";
                        OnWriteData(f, beizhu, true, true, false, 1);
                        string ftype = main["type"].ToString();
                        if ("1".Equals(ftype))
                        {
                            string address = main["address"] + "\n";
                            OnWriteData(f, address, true, true, false, 1);
                            string linkman = "" + main["linkman"] + "  " + main["sex"] + "\n";
                            OnWriteData(f, linkman, true, true, false, 1);
                            string linktel = "" + main["linktel"] + "\n\n";
                            OnWriteData(f, linktel, true, true, false, 1);
                        }
                        cutPage(f);
                        #endregion
                    }
                }
            }


        }
        private void bookLptPrintPage(string printname, dd_printers printer)
        {

            if (pcontent != null)
            {
                LPTControls lpt = new LPTControls();
                lpt.Open(printname);
                Dictionary<string, object> main = pcontent;
                #region 执行指令打印
                uint width = 2;
                int psize = printer.psize.Value;
                int pwidth = psize;
                string linestr = "------------------------------------------------";
                string kongestr = "";
                if (psize == 58)
                {
                    width = 1;
                    pwidth = 190;
                    linestr = "--------------------------------";
                }
                else if (psize == 70)
                {
                    width = 2;
                    pwidth = 250;
                    linestr = "-----------------------------------------";
                }
                else if (psize == 76)
                {
                    width = 2;
                    pwidth = 260;
                    linestr = "----------------------------------------";
                }
                else if (psize == 80)
                {
                    kongestr = "                ";
                    width = 2;
                    pwidth = 260;
                }

                lpt.OnWriteData("点点菜单\n\n", false, false, false, 2);

                string type = main["type"].ToString();
                string tname = "";
                string otype = main["otype"].ToString();
                string noticetype = main["noticetype"].ToString();

                if (otype != null && "meituan".Equals(otype))
                {
                    tname += "（美团）";
                }
                else if (otype != null && "eleme".Equals(otype))
                {
                    tname += "（饿了么）";
                }
                if (type != null && "0".Equals(type))
                {

                    tname = "预定订单" + tname;
                }
                else
                {
                    tname = "外卖订单" + tname;
                }

                if (noticetype != null && "14".Equals(noticetype))
                {//退
                    tname = tname + "（退）";
                }

                lpt.OnWriteData("      " + tname + "\n\n", true, true, false, 2);
                //OnWriteData(f, "订单编号", false, false, false, 1);
                string orderno = main["orderno"].ToString();
                if (orderno.Length > 14)
                {
                    lpt.OnWriteData("订单编号       " + kongestr + main["orderno"].ToString() + "\n", false, false, false, 1);
                }
                else
                {
                    lpt.OnWriteData("订单编号          " + kongestr + main["orderno"].ToString() + "\n", false, false, false, 1);
                }
                //OnWriteData(f, "点餐时间", false, false, false, 1);
                lpt.OnWriteData("点餐时间             " + kongestr + main["addtime"].ToString() + "\n", false, false, false, 1);
                //OnWriteData(f, "用餐时间", false, false, false, 1);

                if (main["jctime"].ToString().Length < 3)
                    lpt.OnWriteData("用餐时间                    " + kongestr + main["jctime"].ToString() + "\n", false, false, false, 1);
                else
                    lpt.OnWriteData("用餐时间                " + kongestr + main["jctime"].ToString() + "\n", false, false, false, 1);
                lpt.OnWriteData(linestr + "\n", false, false, false, 1);
                //OnWriteData(f, "项目        ", false, false, false, 1);
                lpt.OnWriteData("项目              " + kongestr + "数量      金额\n", false, false, false, 1);
                //OnWriteData(f, "金额\n", false, false, false, 1);
                lpt.OnWriteData(linestr + "\n", false, false, false, 1);

                JArray zitems = (JArray)pcontent["items"];
                if (zitems.Count > 0)
                {

                    for (int i = 0; i < zitems.Count; i++)
                    {
                       
                        //菜品打印h20
                        string num = zitems[i]["num"].ToString();
                        string price = zitems[i]["price"].ToString();
                        if (zitems[i]["weight"] != null)
                        {
                            string weight = zitems[i]["weight"].ToString();
                            float weig = float.Parse(weight);
                            if (weig > 0)
                            {
                                num = weig + "";

                                price = float.Parse(price) / weig + "";
                            }
                        }
                        string name = zitems[i]["name"].ToString();
                        if (zitems[i]["unit"] != null && !"".Equals(zitems[i]["unit"].ToString()))
                        {
                            name = name + "/" + zitems[i]["unit"];
                        }
                        string kongge = "";


                        string ename = "";//为了菜名过长设定，截断字符串
                        if (name.Length > 9)
                        {

                            ename = name.Substring(9, name.Length - 9);
                            name = name.Substring(0, 9);
                        }
                        name = string.Format("{0,-9}", name);
                        num = string.Format("{0,3}", num);
                        name = name.Replace(" ", "  ");
                        name = name + kongge;
                        // OnWriteData(f, name, false, false, false, 1);
                        // OnWriteData(f, name+num + "      ", false, false, false, 1);
                        string fprice = float.Parse(price).ToString("F2");


                        fprice = string.Format("{0,6}", fprice);
                        if (i == zitems.Count - 1)
                            lpt.OnWriteData( name + num + "     " + fprice + ename + "\n", false, true, false, 1);
                        else
                            lpt.OnWriteData( name + num + "     " + fprice + ename + "\n\n", false, true, false, 1);
                    }
                }
                lpt.OnWriteData(linestr + "\n", false, false, false, 1);
                // 横线 

                
                string amountstr = string.Format("{0,7}", main["amount"].ToString() + "元");
                lpt.OnWriteData( "总金额                  " + amountstr + "\n", false, false, false, 1);
                lpt.OnWriteData( linestr + "\n", false, false, false, 1);
                // 横线 
                string beizhu = "备注:" + main["comm"] + "\n\n";
                lpt.OnWriteData(beizhu, true, true, false, 1);
                string ftype = main["type"].ToString();
                if ("1".Equals(ftype))
                {
                    string address = main["address"] + "\n";
                    lpt.OnWriteData(address, true, true, false, 1);
                    string linkman = "" + main["linkman"] + "  " + main["sex"] + "\n";
                    lpt.OnWriteData(linkman, true, true, false, 1);
                    string linktel = "" + main["linktel"] + "\n\n";
                    lpt.OnWriteData(linktel, true, true, false, 1);
                }
                lpt.CutPaper();
                lpt.Close();
                #endregion
            }



        }
        private void printDocument1_bookPrintPage(object sender, PrintPageEventArgs e)
        {
            if (pcontent != null)
            {
                Dictionary<string, object> main = pcontent;
                e.Graphics.Clear(Color.White);
                // 开始绘制文档  
                // 默认为横排文字  
                Graphics g = form.CreateGraphics();
                Font cfont = new Font(new FontFamily("宋体"), 9, FontStyle.Bold);
                Font tfont = new Font(new FontFamily("宋体"), 16, FontStyle.Bold);
                Font cpfont = new Font(new FontFamily("宋体"), 13, FontStyle.Bold);
                int pwidth = e.PageBounds.Width;//纸张宽度
                Pen pen1 = new Pen(Color.Black);
                pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                StringFormat SF = new StringFormat();
                SF.LineAlignment = StringAlignment.Center;                                        //设置属性为水平居中
                SF.Alignment = StringAlignment.Center;                                               //设置属性为垂直居中
                RectangleF rect = new RectangleF(0, 0, pwidth, e.Graphics.MeasureString("点点菜单", cfont).Height);    //其中e.PageBounds属性表示页面全部区域的矩形区域
                                                                                                                   //e.Graphics.MeasureString("点点菜单", new Font("Times New Roman", 20)).Height;
                                                                                                                   //表示获取你要打印字符串的高度
                e.Graphics.DrawString("点点菜单", cfont, Brushes.Black, rect, SF);
                string type = main["type"].ToString();
                string tname = "";
                string otype = main["otype"].ToString();
                string noticetype = main["noticetype"].ToString();

                if (otype != null && "meituan".Equals(otype))
                {
                    tname += "（美团）";
                }
                else if (otype != null && "eleme".Equals(otype))
                {
                    tname += "（饿了么）";
                }
                if (type != null && "0".Equals(type))
                {

                    tname = "预定订单" + tname;
                }
                else
                {
                    tname = "外卖订单" + tname;
                }

                if (noticetype != null && "14".Equals(noticetype))
                {//退
                    tname = tname + "（退）";
                }
                RectangleF rect1 = new RectangleF(0, 35, pwidth, e.Graphics.MeasureString(tname, tfont).Height);
                e.Graphics.DrawString(tname, tfont, Brushes.Black, rect1, SF);

                e.Graphics.DrawString("订单编号", cfont, System.Drawing.Brushes.Black, 0, 70);
                e.Graphics.DrawString(main["orderno"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(main["orderno"].ToString(), cfont).Width, 70);

                e.Graphics.DrawString("点餐时间", cfont, System.Drawing.Brushes.Black, 0, 85);
                e.Graphics.DrawString(main["addtime"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(main["addtime"].ToString(), cfont).Width, 85);

                // 横线  
                e.Graphics.DrawLine(pen1, 0, 105, pwidth, 105);

                e.Graphics.DrawString("项目", cfont, System.Drawing.Brushes.Black, 0, 110);
                e.Graphics.DrawString("数量", cfont, System.Drawing.Brushes.Black, pwidth - 89 + (49 - e.Graphics.MeasureString("数量", cfont).Width) / 2, 110);
                e.Graphics.DrawString("金额", cfont, System.Drawing.Brushes.Black, pwidth - 40 + (40 - e.Graphics.MeasureString("金额", cfont).Width) / 2, 110);
                // 横线  
                e.Graphics.DrawLine(pen1, 0, 130, pwidth, 130);
                int cheight = 135;
                JArray zitems = (JArray)pcontent["items"];
                if (zitems.Count > 0)
                {

                    for (int i = 0; i < zitems.Count; i++)
                    {
                        //菜品打印h20
                        string num = zitems[i]["num"].ToString();
                        string price = zitems[i]["price"].ToString();
                        if (zitems[i]["weight"] != null)
                        {
                            string weight = zitems[i]["weight"].ToString();
                            float weig = float.Parse(weight);
                            if (weig > 0)
                            {
                                num = weig + "";

                                price = float.Parse(price) / weig + "";
                            }
                        }
                        string name = zitems[i]["name"].ToString();
                        if (zitems[i]["unit"] != null && !"".Equals(zitems[i]["unit"].ToString()))
                        {
                            name = name + "/" + zitems[i]["unit"];
                        }

                        double sheight = 25;
                        if (name.Length > 7)
                        {
                            sheight = sheight * Math.Ceiling(name.Length / 7.0);//
                        }
                        StringFormat fmt = new StringFormat();
                        fmt.LineAlignment = StringAlignment.Near;//左对齐
                        fmt.FormatFlags = StringFormatFlags.LineLimit;//自动换行
                        Point b = new Point(0, cheight);
                        Rectangle r = new Rectangle(b, new Size(pwidth - 89, 25 * 2));
                        //e.Graphics.DrawString(name, cpfont, System.Drawing.Brushes.Black, 0, cheight);
                        e.Graphics.DrawString(name, cpfont, new SolidBrush(Color.Black), r, fmt);

                        //e.Graphics.DrawString(name, cpfont, System.Drawing.Brushes.Black, 0, cheight);
                        e.Graphics.DrawString(num, cpfont, System.Drawing.Brushes.Black, pwidth - 89 + (49 - e.Graphics.MeasureString(num, cpfont).Width) / 2, cheight);
                        e.Graphics.DrawString(price, cpfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(price, cpfont).Width, cheight);
                        cheight += (int)sheight;
                        


                    }

                }
                cheight += 5;
                // 横线 
                e.Graphics.DrawLine(pen1, 0, cheight, pwidth, cheight);
                cheight += 5;
                e.Graphics.DrawString("总金额", cfont, System.Drawing.Brushes.Black, 0, cheight);
                e.Graphics.DrawString(main["realamount"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(main["realamount"].ToString(), cfont).Width, cheight);
                cheight += 20;
                // 横线 
                e.Graphics.DrawLine(pen1, 0, cheight, pwidth, cheight);
                cheight += 5;
                string beizhu = "备注:" + main["comm"] ;
                beizhu = AutomaticLine(beizhu, 8, pwidth);//8, 36  
                double csheight = 25;
                if (beizhu.Length > 7)
                {
                    csheight = csheight * Math.Ceiling(beizhu.Length / 7.0);//
                }
                RectangleF drawRect = new RectangleF(0, cheight, pwidth, (int)csheight); //设定这个就行了
                e.Graphics.DrawString(beizhu, tfont, Brushes.Black, drawRect, null);
                cheight += (int)csheight + 5;
                string ftype = main["type"].ToString();
                if ("1".Equals(ftype)) {
                    string address = main["address"] + "\n" + main["linkman"] + "  " + main["sex"] + "\n" + main["linktel"] + "\n\n";
                    e.Graphics.DrawString(address, tfont, System.Drawing.Brushes.Black, 0, cheight);
                }
               
            }

        }
        //打印结单小票的打印主要用于餐饮行业2
        public void yingyePrint(string printname)
        {
            lock (thisLock)
            {
                Console.WriteLine("档口小票的打印:" + printname);
                int vindex = printname.IndexOf("ip");
                int cindex = printname.IndexOf("COM");
                int uindex = printname.IndexOf("USB\\VID");
                int lindex = printname.IndexOf("LPT");
                BizPrinter dao = new BizPrinter();

                dd_printers printer = dao.QueryPrinters(2, printname).FirstOrDefault();
                if (vindex >= 0)
                {
                    printname = printname.Substring(vindex + 2);
                    //打印的IP一定要预先设置好


                    bool b = opos.OpenNetPort(printname);//"192.168.1.254"
                    if (!b)
                    {
                        Console.WriteLine("初始化'" + printname + "'的打印机参数失败。请检测打印机配置");
                        b = opos.OpenNetPort(printname);//"192.168.1.254"
                        Thread.Sleep(1000);
                        if (!b)
                        {
                            Console.WriteLine("初始化'" + printname + "'的打印机参数失败。请检测打印机配置");
                            utils.ShowTip("警告", "初始化'" + printname + "'的打印机参数失败。请检测打印机配置", 5000);
                            //form.showmsg("初始化'" + printname + "'的打印机参数失败。请检测打印机配置");
                            return;
                        }

                    }

                    Byte res = new Byte();
                    int ret = BeiYangOPOS.POS_NETQueryStatus(printname, out res);
                    StringBuilder sb = new StringBuilder();
                    #region 检测打印机状态
                    if ((res & 0x10) == 0x10)
                    {
                        sb.AppendLine("打印机出错！");
                    }
                    if ((res & 0x02) == 0x02)
                    {
                        sb.AppendLine("打印机脱机！");
                    }
                    if ((res & 0x04) == 0x04)
                    {
                        sb.AppendLine("上盖打开！");
                    }
                    if ((res & 0x20) == 0x20)
                    {
                        sb.AppendLine("切刀出错！");
                    }
                    if ((res & 0x40) == 0x40)
                    {
                        sb.AppendLine("纸将尽！");
                    }
                    if ((res & 0x80) == 0x80)
                    {
                        sb.AppendLine("缺纸！");
                    }
                    #endregion
                    if (sb.Length > 0)
                    {
                        Console.WriteLine("Error",
                                   string.Format("'{0}'的打印机处于非正常状态：{1}。请检测打印机配置。",
                                              printname, sb.ToString()));
                        return;
                    }
                    yingyePrintPage(printname, printer);
                    return;
                }
                else if (cindex >= 0)
                {

                    SerialPort com = new SerialPort();
                    int pbites = printer.pbites.Value;
                    com.BaudRate = pbites;
                    com.PortName = printname;
                    com.DataBits = 8;
                    bool b = opos.OpenComPort(ref com);
                    if (!b)
                    {
                        string errormsg = string.Format("初始化'{0}'的打印机参数失败。请检测打印机配置",
                                            printname);
                        Console.WriteLine(errormsg);
                        utils.ShowTip("警告", errormsg, 5000);
                        return;
                    }
                    //BeiYangOPOS.POS_QueryStatus
                    yingyePrintPage(printname, printer);
                    return;
                }
                else if (uindex >= 0)
                {
                    yingyeUsbPrintPage(printname, printer);
                    return;
                }
                else if (lindex >= 0)
                {

                    yingyeLptPrintPage(printname, printer);
                    return;
                }
                Console.WriteLine("划单小票的打印:" + printname);
                if (printname == null || "".Equals(printname) || "default".Equals(printname))
                {
                    //this.printDocument1.PrinterSettings.PrinterName = printname;
                }
                else
                {
                    this.printDocument1.PrinterSettings.PrinterName = printname;
                }
                this.printDocument1.DocumentName = "test";
                printDocument1.PrintPage +=

                new PrintPageEventHandler(this.printDocument1_yingyePrintPage);
                this.printDocument1.Print();
            }
        }
        //打印结单小票
        private void printDocument1_yingyePrintPage(object sender, PrintPageEventArgs e)
        {
            if (pcontent != null)
            {
                Dictionary<string, object> main = pcontent;
                e.Graphics.Clear(Color.White);
                // 开始绘制文档  
                // 默认为横排文字  
                Graphics g = form.CreateGraphics();
                Font cfont = new Font(new FontFamily("宋体"), 9, FontStyle.Bold);
                Font tfont = new Font(new FontFamily("宋体"), 16, FontStyle.Bold);
                Font cpfont = new Font(new FontFamily("宋体"), 13, FontStyle.Bold);
                int pwidth = e.PageBounds.Width;//纸张宽度
                Pen pen1 = new Pen(Color.Black);
                pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                StringFormat SF = new StringFormat();
                SF.LineAlignment = StringAlignment.Center;                                        //设置属性为水平居中
                SF.Alignment = StringAlignment.Center;                                               //设置属性为垂直居中
                string shopname = "点点菜单";

                RectangleF rect = new RectangleF(0, 0, pwidth, e.Graphics.MeasureString(shopname, cfont).Height);    //其中e.PageBounds属性表示页面全部区域的矩形区域
                                                                                                                     //e.Graphics.MeasureString("点点菜单", new Font("Times New Roman", 20)).Height;
                                                                                                                     //表示获取你要打印字符串的高度
                e.Graphics.DrawString(shopname, cfont, Brushes.Black, rect, SF);
                string tablename = "营业明细";// + main["serialno"].ToString();

                RectangleF rect1 = new RectangleF(0, 32, pwidth, e.Graphics.MeasureString(tablename, tfont).Height);
                e.Graphics.DrawString(tablename, tfont, Brushes.Black, rect1, SF);
               
                e.Graphics.DrawString("项目 ", cfont, System.Drawing.Brushes.Black, 0, 70);
               
                //e.Graphics.DrawString("数量", cfont, System.Drawing.Brushes.Black, pwidth - 40 + (40 - e.Graphics.MeasureString("数量", cfont).Width) / 2, 110);
                e.Graphics.DrawString("总营业笔数", cfont, System.Drawing.Brushes.Black, pwidth - 130 + (80 - e.Graphics.MeasureString("总营业笔数", cfont).Width) / 2, 70);
                e.Graphics.DrawString("总营业额", cfont, System.Drawing.Brushes.Black, pwidth - 55 + (55 - e.Graphics.MeasureString("总营业额", cfont).Width) / 2, 70);               
                
                int cheight = 90;              
                e.Graphics.DrawString("总营业额", cfont, System.Drawing.Brushes.Black, 0, cheight);
                
                string bishu =  pcontent["yingyenums"].ToString();
              
                string yfprice = float.Parse(pcontent["yingyee"] + "").ToString("F2");
                 
                e.Graphics.DrawString(bishu, cfont, System.Drawing.Brushes.Black, pwidth - 89 + (49 - e.Graphics.MeasureString(bishu, cfont).Width) / 2, cheight);

                e.Graphics.DrawString(yfprice, cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(yfprice, cfont).Width, cheight);
                cheight += 20;

                e.Graphics.DrawString("(不包含会员消费)", cfont, System.Drawing.Brushes.Black, 0, cheight);
                cheight += 20;

                JArray lines = (JArray)pcontent["lines"];
               
                if (lines.Count > 0)
                {
                    for (int i = 0; i < lines.Count; i++)
                    {
                        JToken line = lines[i];
                        
                            string name = "—" + line["name"].ToString();

                            string price = line["svalue"].ToString();

                            string fprice = float.Parse(price).ToString("F2");

                            StringFormat fmt = new StringFormat();
                            fmt.LineAlignment = StringAlignment.Near;//左对齐
                            fmt.FormatFlags = StringFormatFlags.LineLimit;//自动换行

                            //设定文本打印区域 b是左上角坐标，Size是打印区域（矩形） float mmtopt = 2.835f;
                            //Rectangle r = new Rectangle();
                            double sheight = 20;
                           
                            Point b = new Point(0, cheight);
                            Rectangle r = new Rectangle(b, new Size(pwidth - 89, 25 * 2));
                            e.Graphics.DrawString(name, cfont, new SolidBrush(Color.Black), r, fmt);
                            //e.Graphics.DrawString(name, cfont, System.Drawing.Brushes.Black, 0, cheight);
                            string num = line["nums"].ToString();                       
                            e.Graphics.DrawString(num, cfont, System.Drawing.Brushes.Black, pwidth - 89 + (49 - e.Graphics.MeasureString(num, cfont).Width) / 2, cheight);
                            e.Graphics.DrawString(fprice, cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(fprice, cfont).Width, cheight);
                            cheight += (int)sheight;
                            string tnum = line["tnums"].ToString();
                            float tnums = float.Parse(tnum);
                            float cvalue = float.Parse(line["cvalue"].ToString());
                        if (cvalue > 0)
                        {

                            // e.Graphics.DrawString("——" + line["name"].ToString() + "退款", cfont, new SolidBrush(Color.Black), r, fmt);
                            e.Graphics.DrawString("——其中" + line["name"].ToString() + "充值", cfont, System.Drawing.Brushes.Black, 0, cheight);

                            e.Graphics.DrawString(line["cnums"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - 89 + (49 - e.Graphics.MeasureString(line["cnums"].ToString(), cfont).Width) / 2, cheight);
                            string fcvaluee = cvalue.ToString("F2");
                            e.Graphics.DrawString(fcvaluee, cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(fcvaluee, cfont).Width, cheight);
                            cheight = cheight + 20;
                        }
                        if (tnums > 0)
                            {

                           // e.Graphics.DrawString("——" + line["name"].ToString() + "退款", cfont, new SolidBrush(Color.Black), r, fmt);
                            e.Graphics.DrawString("——" + line["name"].ToString() + "退款", cfont, System.Drawing.Brushes.Black, 0, cheight);
                             
                            e.Graphics.DrawString(tnum, cfont, System.Drawing.Brushes.Black, pwidth - 89 + (49 - e.Graphics.MeasureString(tnum, cfont).Width) / 2, cheight);
                            e.Graphics.DrawString(line["tvalue"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(line["tvalue"].ToString(), cfont).Width, cheight);
                            cheight = cheight + 20;
                        }
                    }
                     
                    e.Graphics.DrawLine(pen1, 0, cheight, pwidth, cheight);
                    cheight = cheight + 5;

                    e.Graphics.DrawString("—会员消费", cfont, System.Drawing.Brushes.Black, 0, cheight);                   
                    e.Graphics.DrawString(pcontent["xiaofeinums"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - 89 + (49 - e.Graphics.MeasureString(pcontent["xiaofeinums"].ToString(), cfont).Width) / 2, cheight);
                    e.Graphics.DrawString(pcontent["xiaofei"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(pcontent["xiaofei"].ToString(), cfont).Width, cheight);
                    cheight = cheight + 20;

                    e.Graphics.DrawString("—会员充值", cfont, System.Drawing.Brushes.Black, 0, cheight);
                    e.Graphics.DrawString(pcontent["chongzhiunms"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - 89 + (49 - e.Graphics.MeasureString(pcontent["chongzhiunms"].ToString(), cfont).Width) / 2, cheight);
                    e.Graphics.DrawString(pcontent["chongzhi"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(pcontent["chongzhi"].ToString(), cfont).Width, cheight);
                    cheight = cheight + 20;

                    e.Graphics.DrawString("—优惠", cfont, System.Drawing.Brushes.Black, 0, cheight);
                    e.Graphics.DrawString(pcontent["yhnums"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - 89 + (49 - e.Graphics.MeasureString(pcontent["yhnums"].ToString(), cfont).Width) / 2, cheight);
                    e.Graphics.DrawString(pcontent["youhui"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(pcontent["youhui"].ToString(), cfont).Width, cheight);
                    


                }


            }

        }
        private void yingyePrintPage(string printname, dd_printers printer)
        {




            if (pcontent != null)
            {
                 
                #region 执行指令打印
                uint width = 2;

                int psize = printer.psize.Value;
                int pwidth = psize;
                string linestr = "------------------------------------------------";
                if (psize == 58)
                {
                    width = 1;
                    BeiYangOPOS.POS_SetLineSpacing(30);
                    pwidth = 190;
                    linestr = "--------------------------------";
                }
                else if (psize == 70)
                {
                    width = 2;
                    pwidth = 250;
                    linestr = "-----------------------------------------";
                }
                else if (psize == 76)
                {
                    width = 2;
                    pwidth = 270;
                    linestr = "---------------------------------------------";
                }
                else if (psize == 80)
                {
                    width = 2;
                    pwidth = 286;
                }
                BeiYangOPOS.POS_SetRightSpacing(0);
                BeiYangOPOS.POS_SetLineSpacing(30);
                int tlength = getStringLength("点点菜单");
                BeiYangOPOS.POS_S_TextOut("点点菜单", (uint)(pwidth - tlength * 6) , 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();

                string tablename = "营业明细\n";                
                int talength = getStringLength(tablename);
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut(tablename, (uint)(pwidth - tlength * 12), width, 2, opos.POS_FONT_TYPE_CHINESE, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();

                BeiYangOPOS.POS_S_TextOut("项目", 0, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_BOLD);
                
                BeiYangOPOS.POS_S_TextOut("总营业笔数", (uint)pwidth, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_BOLD);
                
                BeiYangOPOS.POS_S_TextOut("总营业额\n", (uint)(pwidth - 8 *6) * 2 , 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_BOLD);
                BeiYangOPOS.POS_FeedLine();

                BeiYangOPOS.POS_S_TextOut("总营业额(不含会员消费)", 0, 1, 2, opos.POS_FONT_TYPE_CHINESE, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_S_TextOut(pcontent["yingyenums"]+"", (uint)pwidth, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                
                BeiYangOPOS.POS_S_TextOut(pcontent["yingyee"] + "", (uint)(pwidth -8* 6) * 2, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();
                JArray lines = (JArray)pcontent["lines"];
                if (lines.Count > 0)
                {

                    for (int i = 0; i < lines.Count; i++)
                    {
                        JToken line=lines[i];
                        BeiYangOPOS.POS_S_TextOut(line["name"].ToString(), 0, 1, 2, opos.POS_FONT_TYPE_CHINESE, opos.POS_FONT_STYLE_NORMAL);

                        BeiYangOPOS.POS_S_TextOut(line["nums"].ToString(), (uint)pwidth, 1,2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                        BeiYangOPOS.POS_S_TextOut(line["svalue"].ToString()+"\n", (uint)(pwidth - 8 * 6) * 2,1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                        BeiYangOPOS.POS_FeedLine();
                        string tnum = line["tnums"].ToString();
                        float tnums = float.Parse(tnum);
                        float cvalue = float.Parse(line["cvalue"].ToString());
                        if (cvalue > 0)
                        {

                           
                            string fcvaluee = cvalue.ToString("F2");
                            
                            BeiYangOPOS.POS_S_TextOut("——其中" + line["name"].ToString() + "充值", 0, 1, 2, opos.POS_FONT_TYPE_CHINESE, opos.POS_FONT_STYLE_NORMAL);

                            BeiYangOPOS.POS_S_TextOut(line["cnums"].ToString(), (uint)pwidth, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                            BeiYangOPOS.POS_S_TextOut(fcvaluee + "\n", (uint)(pwidth - 8 * 6) * 2, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                        }
                        if (tnums > 0)
                        {

                           
                            BeiYangOPOS.POS_S_TextOut("——" + line["name"].ToString()+"退款", 0, 1, 2, opos.POS_FONT_TYPE_CHINESE, opos.POS_FONT_STYLE_NORMAL);

                            BeiYangOPOS.POS_S_TextOut(line["tnums"].ToString(), (uint)pwidth, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                            BeiYangOPOS.POS_S_TextOut(line["tvalue"].ToString() + "\n", (uint)(pwidth - 8 * 6) * 2, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                        }

                    }
                }
                 
              

               
                //BeiYangOPOS.POS_S_TextOut("——退款", 0, 1, 2, opos.POS_FONT_TYPE_CHINESE, opos.POS_FONT_STYLE_NORMAL);
                //BeiYangOPOS.POS_S_TextOut(pcontent["tuikuannums"].ToString(), (uint)pwidth, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                //BeiYangOPOS.POS_S_TextOut(pcontent["tuikuane"].ToString(), (uint)(pwidth - 8* 6) * 2, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                //BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut(linestr+"\n", 0, 1, 2, opos.POS_FONT_TYPE_CHINESE, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_S_TextOut("会员消费", 0, 1, 2, opos.POS_FONT_TYPE_CHINESE, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_S_TextOut(pcontent["xiaofeinums"].ToString(), (uint)pwidth, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_S_TextOut(pcontent["xiaofei"].ToString(), (uint)(pwidth - 8 * 6) * 2, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();

                BeiYangOPOS.POS_S_TextOut("——会员充值", 0, 1, 2, opos.POS_FONT_TYPE_CHINESE, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_S_TextOut(pcontent["chongzhiunms"].ToString(), (uint)pwidth, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_S_TextOut(pcontent["chongzhi"].ToString(), (uint)(pwidth - 8 * 6) * 2, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut("——优惠", 0, 1, 2, opos.POS_FONT_TYPE_CHINESE, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_S_TextOut(pcontent["yhnums"].ToString(), (uint)pwidth, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_S_TextOut(pcontent["youhui"].ToString(), (uint)(pwidth - 8 * 6) * 2, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();

                BeiYangOPOS.POS_S_TextOut("\n\n", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                
 
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_CutPaper(0, 100);
                opos.ClosePrinterPort();
                #endregion
            }



        }
  
        private void yingyeUsbPrintPage(string printname, dd_printers printer)
        {

            if (pcontent != null)
            {
                Dictionary<string, object> main = pcontent;
                #region 执行指令打印
                uint width = 2;


                int psize = printer.psize.Value;
                int pwidth = psize;
                string linestr = "------------------------------------------------";
                if (psize == 58)
                {
                    width = 1;
                    pwidth = 190;
                    linestr = "--------------------------------";
                }
                else if (psize == 70)
                {
                    width = 2;
                    pwidth = 250;
                    linestr = "-----------------------------------------";
                }
                else if (psize == 76)
                {
                    width = 2;
                    pwidth = 260;
                    linestr = "----------------------------------------";
                }
                else if (psize == 80)
                {
                    width = 2;
                    pwidth = 260;
                }
                using (var sh = UsbPrinterResolver.OpenUSBPrinterBydeviceId(printname))
                {
                    using (var f = new System.IO.FileStream(sh, System.IO.FileAccess.ReadWrite))
                    {
                        OnWriteData(f, "点点菜单\n\n", false, false, false, 2);
 
                        OnWriteData(f, "营业明细\n\n", true, true, false, 2);
                        
                        //OnWriteData(f, "项目        ", false, false, false, 1);
                        OnWriteData(f, "项目        总营业笔数  总营业额\n", false, false, false, 1);
                        //OnWriteData(f, "金额\n", false, false, false, 1);
                        OnWriteData(f, linestr + "\n", false, false, false, 1);

                        string title = string.Format("{0,-8}", "总营业额");
                        string bishu = string.Format("{0,4}", pcontent["yingyenums"]);
                        title = title.Replace(" ", "  ");

                        string yfprice = float.Parse(pcontent["yingyee"]+"").ToString("F2");
                        string yingyee = string.Format("{0,8}", yfprice + "");

                        OnWriteData(f, title + bishu + "    " + yingyee + "\n", false, true, false, 1);
                        OnWriteData(f, "(不含会员消费)\n", false, true, false, 1);

                        JArray lines = (JArray)pcontent["lines"];
                        if (lines.Count > 0)
                        {

                            for (int i = 0; i < lines.Count; i++)
                            {
                                JToken line = lines[i];
                                string name = line["name"].ToString();
                                string num = line["nums"].ToString();
                                string price = line["svalue"].ToString();

                                var len = getStringLength(name);
                                if (len < 16) {
                                    int chakong =16 - len;
                                    if (chakong > 0)
                                    {
                                        string kongge = string.Format("{0,-" + chakong + "}", "");
                                        name = name + kongge;
                                    }
                                    //name = string.Format("{0,-8}", name);
                                }
                                
                                num = string.Format("{0,4}", num);
                                
                                
                                string fprice = float.Parse(price).ToString("F2");
                                price = string.Format("{0,8}", fprice+"");
                                OnWriteData(f,  name + num + "    " + price + "\n", false, true, false, 1);
                                string tnum = line["tnums"].ToString();
                                float   tnums = float.Parse(tnum);
                                float cvalue = float.Parse(line["cvalue"].ToString());
                                if (cvalue > 0)
                                {

                                    name = "——其中" + line["name"].ToString() + "充值";
                                    len = getStringLength(name);
                                    if (len < 16)
                                    {
                                        int chakong = 16 - len;
                                        if (chakong > 0)
                                        {
                                            string kongge = string.Format("{0,-" + chakong + "}", "");
                                            name = name + kongge;
                                        }
                                        //name = string.Format("{0,-8}", name);
                                    }

                                    string fcvaluee = cvalue.ToString("F2");
                                    fcvaluee = string.Format("{0,8}", fcvaluee + "");
                                    string cnums = string.Format("{0,4}", line["cnums"].ToString());
                                    OnWriteData(f, name + cnums + "    " + fcvaluee + "\n", false, false, false, 1);
 
                                }
                                if (tnums > 0) {

                                    name = "—" + line["name"].ToString()+"退款";
                                    len = getStringLength(name);
                                    if (len < 16)
                                    {
                                        int chakong = 16 - len;
                                        if (chakong > 0)
                                        {
                                            string kongge = string.Format("{0,-" + chakong + "}", "");
                                            name = name + kongge;
                                        }
                                        //name = string.Format("{0,-8}", name);
                                    }
                                    price = line["tvalue"].ToString();
                                     fprice = float.Parse(price).ToString("F2");
                                     price = string.Format("{0,8}", fprice + "");
                                     num = string.Format("{0,4}", tnum);
                                     OnWriteData(f, name + num + "    " + price + "\n", false, true, false, 1);
                                }
                            }
                        }
                        OnWriteData(f, linestr + "\n", false, false, false, 1);
                        string sprice = pcontent["xiaofei"].ToString();
                        string sfprice = float.Parse(sprice).ToString("F2");
                        sprice = string.Format("{0,8}", sfprice + "");
                        string snum = pcontent["xiaofeinums"].ToString();
                        snum = string.Format("{0,4}", snum);
                        OnWriteData(f, "—会员消费      " + snum + "    " + sprice + "\n", false, true, false, 1);
                        sprice = pcontent["tuikuane"].ToString();
                        sfprice = float.Parse(sprice).ToString("F2");
                        sprice = string.Format("{0,8}", sfprice + "");
                        snum = pcontent["tuikuannums"].ToString();
                        snum = string.Format("{0,4}", snum);
                        //OnWriteData(f, "—退款          "+ snum + "    " + sprice + "\n", false, true, false, 1);
                    
                         sprice = pcontent["chongzhi"].ToString();
                        sfprice = float.Parse(sprice).ToString("F2");
                        sprice = string.Format("{0,8}", sfprice + "");
                        snum = pcontent["chongzhiunms"].ToString();
                        snum = string.Format("{0,4}", snum);
                        OnWriteData(f, "—会员充值      " + snum + "    " + sprice + "\n", false, true, false, 1);
                        sprice = pcontent["youhui"].ToString();
                        sfprice = float.Parse(sprice).ToString("F2");
                        sprice = string.Format("{0,8}", sfprice + "");
                        snum = pcontent["yhnums"].ToString();
                        snum = string.Format("{0,4}", snum);
                        OnWriteData(f, "—优惠          " + snum + "    " + sprice + "\n\n\n\n", false, true, false, 1);
                        
                        cutPage(f);
                        #endregion
                    }
                }
            }


        }
        private void yingyeLptPrintPage(string printname, dd_printers printer)
        {

            if (pcontent != null)
            {
                LPTControls lpt = new LPTControls();
                lpt.Open(printname);
                Dictionary<string, object> main = pcontent;
                #region 执行指令打印
                uint width = 2;
                int psize = printer.psize.Value;
                int pwidth = psize;
                string linestr = "------------------------------------------------";
                string kongestr = "";
                if (psize == 58)
                {
                    width = 1;
                    pwidth = 190;
                    linestr = "--------------------------------";
                }
                else if (psize == 70)
                {
                    width = 2;
                    pwidth = 250;
                    linestr = "-----------------------------------------";
                }
                else if (psize == 76)
                {
                    width = 2;
                    pwidth = 260;
                    linestr = "----------------------------------------";
                }
                else if (psize == 80)
                {
                    kongestr = "                ";
                    width = 2;
                    pwidth = 260;
                }

                lpt.OnWriteData( "点点菜单\n\n", false, false, false, 2);

                lpt.OnWriteData("营业明细\n\n", true, true, false, 2);

                //OnWriteData(f, "项目        ", false, false, false, 1);
                lpt.OnWriteData("项目        总营业笔数  总营业额\n", false, false, false, 1);
                //OnWriteData(f, "金额\n", false, false, false, 1);
                lpt.OnWriteData(linestr + "\n", false, false, false, 1);

                string title = string.Format("{0,-8}", "总营业额(不含会员消费)");
                string bishu = string.Format("{0,4}", pcontent["yingyenums"]);
                title = title.Replace(" ", "  ");

                string yfprice = float.Parse(pcontent["yingyee"] + "").ToString("F2");
                string yingyee = string.Format("{0,8}", yfprice + "");

                lpt.OnWriteData(title + bishu + "    " + yingyee + "\n", false, true, false, 1);


                JArray lines = (JArray)pcontent["lines"];
                if (lines.Count > 0)
                {

                    for (int i = 0; i < lines.Count; i++)
                    {
                        JToken line = lines[i];
                        string name = line["name"].ToString();
                        string num = line["nums"].ToString();
                        string price = line["svalue"].ToString();
                        var len = getStringLength(name);
                        if (len < 16)
                        {
                            int chakong = 16 - len;
                            if (chakong > 0)
                            {
                                string kongge = string.Format("{0,-" + chakong + "}", "");
                                name = name + kongge;
                            }
                            //name = string.Format("{0,-8}", name);
                        }

                        num = string.Format("{0,4}", num);
                        

                        string fprice = float.Parse(price).ToString("F2");
                        price = string.Format("{0,8}", fprice + "");
                        lpt.OnWriteData( "—" + name + num + "    " + price + "\n", false, true, false, 1);
                        string tnum = line["tnums"].ToString();
                        float tnums = float.Parse(tnum);
                        float cvalue = float.Parse(line["cvalue"].ToString());
                        if (cvalue > 0)
                        {

                            name = "——其中" + line["name"].ToString() + "充值";
                            len = getStringLength(name);
                            if (len < 16)
                            {
                                int chakong = 16 - len;
                                if (chakong > 0)
                                {
                                    string kongge = string.Format("{0,-" + chakong + "}", "");
                                    name = name + kongge;
                                }
                                //name = string.Format("{0,-8}", name);
                            }

                            string fcvaluee = cvalue.ToString("F2");
                            fcvaluee = string.Format("{0,8}", fcvaluee + "");
                            string cnums = string.Format("{0,4}", line["cnums"].ToString());
                            lpt.OnWriteData( name + cnums + "    " + fcvaluee + "\n", false, false, false, 1);

                        }
                        if (tnums > 0)
                        {
                            name = "—" + line["name"].ToString() + "退款";
                            len = getStringLength(name);
                            if (len < 16)
                            {
                                int chakong = 16 - len;
                                if (chakong > 0)
                                {
                                    string kongge = string.Format("{0,-" + chakong + "}", "");
                                    name = name + kongge;
                                }
                                //name = string.Format("{0,-8}", name);
                            }
                            price = line["tvalue"].ToString();
                            fprice = float.Parse(price).ToString("F2");
                            price = string.Format("{0,8}", fprice + "");
                            num = string.Format("{0,4}", tnum);
                            lpt.OnWriteData( name + num + "    " + price + "\n", false, true, false, 1);
                        }
                    }
                }

              
                //lpt.OnWriteData( "—退款          " + snum + "    " + sprice + "\n\n\n\n", false, true, false, 1);
                lpt.OnWriteData(linestr + "\n", false, false, false, 1);
                string sprice = pcontent["xiaofei"].ToString();
                string sfprice = float.Parse(sprice).ToString("F2");
                sprice = string.Format("{0,8}", sfprice + "");
                string snum = pcontent["xiaofeinums"].ToString();
                snum = string.Format("{0,4}", snum);
                lpt.OnWriteData("—会员消费        " + snum + "    " + sprice + "\n", false, true, false, 1);
                sprice = pcontent["tuikuane"].ToString();
                sfprice = float.Parse(sprice).ToString("F2");
                sprice = string.Format("{0,8}", sfprice + "");
                snum = pcontent["tuikuannums"].ToString();
                snum = string.Format("{0,4}", snum);

                sprice = pcontent["chongzhi"].ToString();
                 sfprice = float.Parse(sprice).ToString("F2");
                sprice = string.Format("{0,8}", sfprice + "");
                 snum = pcontent["chongzhiunms"].ToString();
                snum = string.Format("{0,4}", snum);
                lpt.OnWriteData("—会员充值      " + snum + "    " + sprice + "\n", false, true, false, 1);
                sprice = pcontent["youhui"].ToString();
                sfprice = float.Parse(sprice).ToString("F2");
                sprice = string.Format("{0,8}", sfprice + "");
                snum = pcontent["yhnums"].ToString();
                snum = string.Format("{0,4}", snum);
                lpt.OnWriteData( "—优惠          " + snum + "    " + sprice + "\n\n\n\n", false, true, false, 1);

                lpt.CutPaper();
                lpt.Close();
                #endregion
            }

        }


        //打印交班小票的打印主要用于餐饮行业2
        public void jiaobanPrint(string printname)
        {
            lock (thisLock)
            {
                Console.WriteLine("档口小票的打印:" + printname);
                int vindex = printname.IndexOf("ip");
                int cindex = printname.IndexOf("COM");
                int uindex = printname.IndexOf("USB\\VID");
                int lindex = printname.IndexOf("LPT");
                BizPrinter dao = new BizPrinter();
                dd_printers printer = dao.QueryPrinters(2, printname).FirstOrDefault();
                if (vindex >= 0)
                {
                    printname = printname.Substring(vindex + 2);
                    //打印的IP一定要预先设置好


                    bool b = opos.OpenNetPort(printname);//"192.168.1.254"
                    if (!b)
                    {
                        Console.WriteLine("初始化'" + printname + "'的打印机参数失败。请检测打印机配置");
                        b = opos.OpenNetPort(printname);//"192.168.1.254"
                        Thread.Sleep(1000);
                        if (!b)
                        {
                            Console.WriteLine("初始化'" + printname + "'的打印机参数失败。请检测打印机配置");
                            utils.ShowTip("警告", "初始化'" + printname + "'的打印机参数失败。请检测打印机配置", 5000);
                            //form.showmsg("初始化'" + printname + "'的打印机参数失败。请检测打印机配置");
                            return;
                        }

                    }

                    Byte res = new Byte();
                    int ret = BeiYangOPOS.POS_NETQueryStatus(printname, out res);
                    StringBuilder sb = new StringBuilder();
                    #region 检测打印机状态
                    if ((res & 0x10) == 0x10)
                    {
                        sb.AppendLine("打印机出错！");
                    }
                    if ((res & 0x02) == 0x02)
                    {
                        sb.AppendLine("打印机脱机！");
                    }
                    if ((res & 0x04) == 0x04)
                    {
                        sb.AppendLine("上盖打开！");
                    }
                    if ((res & 0x20) == 0x20)
                    {
                        sb.AppendLine("切刀出错！");
                    }
                    if ((res & 0x40) == 0x40)
                    {
                        sb.AppendLine("纸将尽！");
                    }
                    if ((res & 0x80) == 0x80)
                    {
                        sb.AppendLine("缺纸！");
                    }
                    #endregion
                    if (sb.Length > 0)
                    {
                        Console.WriteLine("Error",
                                   string.Format("'{0}'的打印机处于非正常状态：{1}。请检测打印机配置。",
                                              printname, sb.ToString()));
                        return;
                    }
                    jiaobanPrintPage(printname, printer);
                    return;
                }
                else if (cindex >= 0)
                {

                    SerialPort com = new SerialPort();
                    int pbites = printer.pbites.Value;
                    com.BaudRate = pbites;
                    com.PortName = printname;
                    com.DataBits = 8;
                    bool b = opos.OpenComPort(ref com);
                    if (!b)
                    {
                        string errormsg = string.Format("初始化'{0}'的打印机参数失败。请检测打印机配置",
                                            printname);
                        Console.WriteLine(errormsg);
                        //form.showmsg(errormsg);
                        return;
                    }
                    //BeiYangOPOS.POS_QueryStatus
                    jiaobanPrintPage(printname, printer);
                    return;
                }
                else if (uindex >= 0)
                {
                    jiaobanUsbPrintPage(printname, printer);
                    return;
                }
                else if (lindex >= 0)
                {

                    jiaobanLptPrintPage(printname, printer);
                    return;
                }
                Console.WriteLine("划单小票的打印:" + printname);
                if (printname == null || "".Equals(printname) || "default".Equals(printname))
                {
                    //this.printDocument1.PrinterSettings.PrinterName = printname;
                }
                else
                {
                    this.printDocument1.PrinterSettings.PrinterName = printname;
                }
                this.printDocument1.DocumentName = "test";
                printDocument1.PrintPage +=

                new PrintPageEventHandler(this.printDocument1_jiaobanPrintPage);
                this.printDocument1.Print();
            }
        }
        private void jiaobanPrintPage(string printname, dd_printers printer)
        {




            if (pcontent != null)
            {

                #region 执行指令打印
                uint width = 2;

                int psize = printer.psize.Value;
                int pwidth = psize;
                string linestr = "------------------------------------------------";
                if (psize == 58)
                {
                    width = 1;
                    BeiYangOPOS.POS_SetLineSpacing(30);
                    pwidth = 190;
                    linestr = "--------------------------------";
                }
                else if (psize == 70)
                {
                    width = 2;
                    pwidth = 250;
                    linestr = "-----------------------------------------";
                }
                else if (psize == 76)
                {
                    width = 2;
                    pwidth = 270;
                    linestr = "---------------------------------------------";
                }
                else if (psize == 80)
                {
                    width = 2;
                    pwidth = 286;
                }
                BeiYangOPOS.POS_SetRightSpacing(0);
                BeiYangOPOS.POS_SetLineSpacing(30);
                int tlength = getStringLength("点点菜单");
                BeiYangOPOS.POS_S_TextOut("点点菜单", (uint)(pwidth - tlength * 6), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();

                string tablename = "交班小票\n";
                int talength = getStringLength(tablename);
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut(tablename, (uint)(pwidth - tlength * 12), width, 2, opos.POS_FONT_TYPE_CHINESE, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();

                BeiYangOPOS.POS_S_TextOut("班前现金金额", 0, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_BOLD);
                string overplus = float.Parse(pcontent["overplus"] + "").ToString("F2");
                BeiYangOPOS.POS_S_TextOut(overplus + "元\n", (uint)(pwidth - 8 * 6) * 2, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_BOLD);
                BeiYangOPOS.POS_S_TextOut("备用金", 0, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_BOLD);
                string pettycash = float.Parse(pcontent["pettycash"] + "").ToString("F2");
                BeiYangOPOS.POS_S_TextOut(pettycash + "元\n", (uint)(pwidth - 8 * 6) * 2, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_BOLD);
                BeiYangOPOS.POS_S_TextOut("本班现金余额", 0, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_BOLD);

                string xianjin = float.Parse(pcontent["xianjin"]+"").ToString("F2");

                BeiYangOPOS.POS_S_TextOut(xianjin + "元\n", (uint)(pwidth - 8 * 6) * 2, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_BOLD);
                BeiYangOPOS.POS_S_TextOut("本班营收（不包含会员卡消费）\n", 0, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_BOLD);
                //BeiYangOPOS.POS_FeedLine();
                
                string totalmoney = float.Parse(pcontent["totalmoney"] + "").ToString("F2");
                BeiYangOPOS.POS_S_TextOut(totalmoney + "元\n", (uint)(pwidth - 8 * 6) * 2, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_BOLD);
                BeiYangOPOS.POS_FeedLine();
                JArray lines = (JArray)pcontent["payways"];
                string cname = "";
                string cprice = "";
                if (lines.Count > 0)
                {

                    for (int i = 0; i < lines.Count; i++)
                    {
                        JToken line = lines[i];
                        if (line["id"].ToString() != "3")
                        {
                            BeiYangOPOS.POS_S_TextOut("——" + line["payway"].ToString(), 0, 1, 2, opos.POS_FONT_TYPE_CHINESE, opos.POS_FONT_STYLE_NORMAL);

                        string money = float.Parse(line["money"] + "").ToString("F2");

                        BeiYangOPOS.POS_S_TextOut(money+ "元", (uint)(pwidth - 8 * 6) * 2, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                        BeiYangOPOS.POS_FeedLine();
                        }
                        else
                        {
                            cname = "—" + line["payway"].ToString();
                            string price = line["money"].ToString();

                            cprice = float.Parse(price).ToString("F2");
                        }
                    }
                }
                if (cname != "")
                {
                    BeiYangOPOS.POS_S_TextOut(linestr + "\n", 0, 1, 2, opos.POS_FONT_TYPE_CHINESE, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_S_TextOut("—" + cname+"消费", 0, 1, 2, opos.POS_FONT_TYPE_CHINESE, opos.POS_FONT_STYLE_NORMAL);                    
                    BeiYangOPOS.POS_S_TextOut(cprice + "元\n", (uint)(pwidth - 8 * 6) * 2, 1, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                }



                BeiYangOPOS.POS_S_TextOut("\n\n", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);


                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_CutPaper(0, 100);
                opos.ClosePrinterPort();
                #endregion
            }



        }
        private void jiaobanUsbPrintPage(string printname, dd_printers printer)
        {

            if (pcontent != null)
            {
                Dictionary<string, object> main = pcontent;
                #region 执行指令打印
                uint width = 2;


                int psize = printer.psize.Value;
                int pwidth = psize;
                string linestr = "------------------------------------------------";
                if (psize == 58)
                {
                    width = 1;
                    pwidth = 190;
                    linestr = "--------------------------------";
                }
                else if (psize == 70)
                {
                    width = 2;
                    pwidth = 250;
                    linestr = "-----------------------------------------";
                }
                else if (psize == 76)
                {
                    width = 2;
                    pwidth = 260;
                    linestr = "----------------------------------------";
                }
                else if (psize == 80)
                {
                    width = 2;
                    pwidth = 260;
                }
                using (var sh = UsbPrinterResolver.OpenUSBPrinterBydeviceId(printname))
                {
                    using (var f = new System.IO.FileStream(sh, System.IO.FileAccess.ReadWrite))
                    {
                        OnWriteData(f, "点点菜单\n\n", false, false, false, 2);

                        OnWriteData(f, "交班小票\n\n", true, true, false, 2);
                        string overplus = float.Parse(pcontent["overplus"] + "").ToString("F2");
                        overplus = string.Format("{0,8}", overplus + "");
                        string pettycash = float.Parse(pcontent["pettycash"] + "").ToString("F2");
                        pettycash = string.Format("{0,8}", pettycash + "");
                        OnWriteData(f, "班前现金金额            " + overplus + "\n", false, true, false, 1);
                        OnWriteData(f, "备用金                  " + pettycash + "\n", false, true, false, 1);
                        string xianjin = float.Parse(pcontent["xianjin"] + "").ToString("F2");
                        xianjin = string.Format("{0,8}", xianjin);
                        OnWriteData(f, "本班现金余额            " + xianjin + "\n", false, true, false, 1);

                         

                        OnWriteData(f, "本班营收（不包含会员卡消费）\n", false, true, false, 1);
                        string totalmoney = float.Parse(pcontent["totalmoney"] + "").ToString("F2");
                        OnWriteData(f, totalmoney+ "\n", false, true, false, 3);

                        string cname = "";
                        string cprice = "";
                        JArray lines = (JArray)pcontent["payways"];
                        if (lines.Count > 0)
                        {

                            for (int i = 0; i < lines.Count; i++)
                            {
                                JToken line = lines[i];

                                if (line["id"].ToString() != "3")
                                {
                                    string name = "—" + line["payway"].ToString();

                                    string price = line["money"].ToString();
                                    var len = getStringLength(name);
                                    if (len < 16)
                                    {
                                        int chakong = 16 - len;
                                        if (chakong > 0)
                                        {
                                            string kongge = string.Format("{0,-" + chakong + "}", "");
                                            name = name + kongge;
                                        }
                                        //name = string.Format("{0,-8}", name);
                                    }
                                    // OnWriteData(f, name, false, false, false, 1);
                                    // OnWriteData(f, name+num + "      ", false, false, false, 1);
                                    string fprice = float.Parse(price).ToString("F2");
                                    price = string.Format("{0,8}", fprice + "");
                                    OnWriteData(f, name + "        " + price + "\n", false, true, false, 1);
                                }
                                else {
                                    cname = "—" + line["payway"].ToString();
                                    string price = line["money"].ToString();

                                    cprice = float.Parse(price).ToString("F2");
                                }
                            }
                        }
                        if (cname != "")
                        {
                            OnWriteData(f, linestr + "\n", false, false, false, 1);
                            cname = cname + "消费";
                            var len = getStringLength(cname);
                            if (len < 16)
                            {
                                int chakong = 16 - len;
                                if (chakong > 0)
                                {
                                    string kongge = string.Format("{0,-" + chakong + "}", "");
                                    cname = cname + kongge;
                                }
                                //name = string.Format("{0,-8}", name);
                            }
                            // OnWriteData(f, name, false, false, false, 1);
                            // OnWriteData(f, name+num + "      ", false, false, false, 1);
                            string fprice = float.Parse(cprice).ToString("F2");
                            cprice = string.Format("{0,8}", fprice + "");
                            OnWriteData(f, cname + "        " + cprice + "\n", false, true, false, 1);
                        }
                        OnWriteData(f, "\n\n\n\n", false, true, false, 1);

                        cutPage(f);
                        #endregion
                    }
                }
            }


        }
        private void jiaobanLptPrintPage(string printname, dd_printers printer)
        {

            if (pcontent != null)
            {
                LPTControls lpt = new LPTControls();
                lpt.Open(printname);
                Dictionary<string, object> main = pcontent;
                #region 执行指令打印
                uint width = 2;
                   

                int psize = printer.psize.Value;
                int pwidth = psize;
                string linestr = "------------------------------------------------";
                if (psize == 58)
                {
                    width = 1;
                    pwidth = 190;
                    linestr = "--------------------------------";
                }
                else if (psize == 70)
                {
                    width = 2;
                    pwidth = 250;
                    linestr = "-----------------------------------------";
                }
                else if (psize == 76)
                {
                    width = 2;
                    pwidth = 260;
                    linestr = "----------------------------------------";
                }
                else if (psize == 80)
                {
                    width = 2;
                    pwidth = 260;
                }

                lpt.OnWriteData( "点点菜单\n\n", false, false, false, 2);

                lpt.OnWriteData( "交班小票\n\n", true, true, false, 2);
                        string overplus = float.Parse(pcontent["overplus"] + "").ToString("F2");
                        overplus = string.Format("{0,8}", overplus + "");
                        string pettycash = float.Parse(pcontent["pettycash"] + "").ToString("F2");
                        pettycash = string.Format("{0,8}", pettycash + "");
                lpt.OnWriteData( "班前现金金额            " + overplus + "\n", false, true, false, 1);
                lpt.OnWriteData( "备用金                  " + pettycash + "\n", false, true, false, 1);
                        string xianjin = float.Parse(pcontent["xianjin"] + "").ToString("F2");
                xianjin = string.Format("{0,8}", xianjin);
                lpt.OnWriteData("本班现金余额            " + xianjin + "\n", false, true, false, 1);
                //lpt.OnWriteData( "本班现金余额                " + xianjin + "\n", false, true, false, 1);
                lpt.OnWriteData( "本班营收（不包含会员消费）\n", false, true, false, 1);
                        string totalmoney = float.Parse(pcontent["totalmoney"] + "").ToString("F2");
                lpt.OnWriteData( totalmoney + "\n", false, true, false, 3);


                        JArray lines = (JArray)pcontent["payways"];
                string cname = "";
                string cprice = "";
                if (lines.Count > 0)
                        {

                            for (int i = 0; i < lines.Count; i++)
                            {
                                JToken line = lines[i];
                        if (line["id"].ToString() != "3")
                        {
                            string name = "—" + line["payway"].ToString();

                            string price = line["money"].ToString();
                            var len = getStringLength(name);
                            if (len < 16)
                            {
                                int chakong = 16 - len;
                                if (chakong > 0)
                                {
                                    string kongge = string.Format("{0,-" + chakong + "}", "");
                                    name = name + kongge;
                                }
                                //name = string.Format("{0,-8}", name);
                            }
                            // OnWriteData(f, name, false, false, false, 1);
                            // OnWriteData(f, name+num + "      ", false, false, false, 1);
                            string fprice = float.Parse(price).ToString("F2");
                            price = string.Format("{0,8}", fprice + "");
                            lpt.OnWriteData("—" + name + "        " + price + "\n", false, true, false, 1);
                        }
                        else {
                            cname = "—" + line["payway"].ToString();
                            string price = line["money"].ToString();

                            cprice = float.Parse(price).ToString("F2");
                        }
                            }
                        }

                if (cname != "")
                {
                    lpt.OnWriteData( linestr + "\n", false, false, false, 1);
                    cname = cname + "消费";
                    var len = getStringLength(cname);
                    if (len < 16)
                    {
                        int chakong = 16 - len;
                        if (chakong > 0)
                        {
                            string kongge = string.Format("{0,-" + chakong + "}", "");
                            cname = cname + kongge;
                        }
                        //name = string.Format("{0,-8}", name);
                    }
                    // OnWriteData(f, name, false, false, false, 1);
                    // OnWriteData(f, name+num + "      ", false, false, false, 1);
                    string fprice = float.Parse(cprice).ToString("F2");
                    cprice = string.Format("{0,8}", fprice + "");
                    lpt.OnWriteData( cname + "        " + cprice + "\n", false, true, false, 1);
                }
                    lpt.OnWriteData( "\n\n\n\n", false, true, false, 1);

                lpt.CutPaper();
                lpt.Close();
                #endregion

            }


        }
        //打印结单小票
        private void printDocument1_jiaobanPrintPage(object sender, PrintPageEventArgs e)
        {
            if (pcontent != null)
            {
                Dictionary<string, object> main = pcontent;
                e.Graphics.Clear(Color.White);
                // 开始绘制文档  
                // 默认为横排文字  
                Graphics g = form.CreateGraphics();
                Font cfont = new Font(new FontFamily("宋体"), 9, FontStyle.Bold);
                Font tfont = new Font(new FontFamily("宋体"), 16, FontStyle.Bold);
                Font cpfont = new Font(new FontFamily("宋体"), 13, FontStyle.Bold);
                int pwidth = e.PageBounds.Width;//纸张宽度
                Pen pen1 = new Pen(Color.Black);
                pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                StringFormat SF = new StringFormat();
                SF.LineAlignment = StringAlignment.Center;                                        //设置属性为水平居中
                SF.Alignment = StringAlignment.Center;                                               //设置属性为垂直居中
                string shopname = "点点菜单";
                 
                RectangleF rect = new RectangleF(0, 0, pwidth, e.Graphics.MeasureString(shopname, cfont).Height);    //其中e.PageBounds属性表示页面全部区域的矩形区域
                                                                                                                     //e.Graphics.MeasureString("点点菜单", new Font("Times New Roman", 20)).Height;
                                                                                                                     //表示获取你要打印字符串的高度
                e.Graphics.DrawString(shopname, cfont, Brushes.Black, rect, SF);
                string tablename = "交班小票";// + main["serialno"].ToString();
                
                RectangleF rect1 = new RectangleF(0, 32, pwidth, e.Graphics.MeasureString(tablename, tfont).Height);
                e.Graphics.DrawString(tablename, tfont, Brushes.Black, rect1, SF);
                string overplus = float.Parse(pcontent["overplus"] + "").ToString("F2");
                e.Graphics.DrawString("班前现金金额 ", cpfont, System.Drawing.Brushes.Black, 0, 70);
                e.Graphics.DrawString(overplus, cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(overplus, cfont).Width, 70);
                string pettycash = float.Parse(pcontent["pettycash"] + "").ToString("F2");
                int cheight = 90;
                e.Graphics.DrawString("备用金", cpfont, System.Drawing.Brushes.Black, 0, cheight);
                e.Graphics.DrawString(pettycash, cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(pettycash, cfont).Width, cheight);
                cheight += 20;
                string xianjin = float.Parse(pcontent["xianjin"] + "").ToString("F2");
                e.Graphics.DrawString("本班现金余额", cpfont, System.Drawing.Brushes.Black, 0, cheight);
                e.Graphics.DrawString(xianjin, cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(xianjin, cfont).Width, cheight);
                cheight +=20;
                string totalmoney = float.Parse(pcontent["totalmoney"] + "").ToString("F2");
                 
                e.Graphics.DrawString("本班营收", cpfont, System.Drawing.Brushes.Black, 0, cheight);
                
                cheight += 20;
                e.Graphics.DrawString("(不包含会员消费)", cfont, System.Drawing.Brushes.Black, 0, cheight);
                e.Graphics.DrawString(totalmoney, cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(totalmoney, cfont).Width, cheight);
                cheight += 20;
                JArray lines = (JArray)pcontent["payways"];
                string cname = "";
                string cprice = "";
                if (lines.Count > 0)
                {
                    for (int i = 0; i < lines.Count; i++)
                    {
                        JToken line = lines[i];
                        if (line["id"].ToString() != "3")
                        {
                            string name = "—" + line["payway"].ToString();

                            string price = line["money"].ToString();

                            string fprice = float.Parse(price).ToString("F2");

                            StringFormat fmt = new StringFormat();
                            fmt.LineAlignment = StringAlignment.Near;//左对齐
                            fmt.FormatFlags = StringFormatFlags.LineLimit;//自动换行

                            //设定文本打印区域 b是左上角坐标，Size是打印区域（矩形） float mmtopt = 2.835f;
                            //Rectangle r = new Rectangle();
                            double sheight = 25;
                            int nlength = getStringLength(name);
                            //int zlength = hznums + onums;
                            //MessageBox.Show("纸张宽度 " + pwidth);
                            double tail = 12;
                            if (pwidth > 200)
                            {
                                tail = 16;
                            }
                            if (nlength > tail)
                            {
                                sheight = sheight * Math.Ceiling(nlength / tail);
                            }
                            Point b = new Point(0, cheight);
                            Rectangle r = new Rectangle(b, new Size(pwidth - 89, 25 * 2));
                            e.Graphics.DrawString(name, cfont, new SolidBrush(Color.Black), r, fmt);
                            //e.Graphics.DrawString(name, cfont, System.Drawing.Brushes.Black, 0, cheight);
                            // e.Graphics.DrawString(num, cfont, System.Drawing.Brushes.Black, pwidth - 89 + (49 - e.Graphics.MeasureString(num, cfont).Width) / 2, cheight);
                            e.Graphics.DrawString(fprice, cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(fprice, cfont).Width, cheight);
                            cheight += (int)sheight;
                        }
                        else {
                            cname= "—" + line["payway"].ToString();
                            string price = line["money"].ToString();

                            cprice = float.Parse(price).ToString("F2");
                        }
                           
                    }
                    if (cname != "") {
                        e.Graphics.DrawLine(pen1, 0, cheight, pwidth, cheight);
                        cheight = cheight + 5;
                        Point b = new Point(0, cheight);
                        Rectangle r = new Rectangle(b, new Size(pwidth - 89, 25 * 2));
                        StringFormat fmt = new StringFormat();
                        fmt.LineAlignment = StringAlignment.Near;//左对齐
                        fmt.FormatFlags = StringFormatFlags.LineLimit;//自动换行
                        cname = cname + "消费";
                        e.Graphics.DrawString(cname, cfont, new SolidBrush(Color.Black), r, fmt);
                        //e.Graphics.DrawString(name, cfont, System.Drawing.Brushes.Black, 0, cheight);
                        // e.Graphics.DrawString(num, cfont, System.Drawing.Brushes.Black, pwidth - 89 + (49 - e.Graphics.MeasureString(num, cfont).Width) / 2, cheight);
                        e.Graphics.DrawString(cprice, cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(cprice, cfont).Width, cheight);
                    }
                   
                }
                
               
            }

        }
        //档口小票的打印0
        public void tuiPrint(string printname)
        {
            lock (thisLock)
            {
                Console.WriteLine("档口小票的打印:" + printname);
                int vindex = printname.IndexOf("ip");
                int cindex = printname.IndexOf("COM");
                int uindex = printname.IndexOf("USB\\VID");
                int lindex = printname.IndexOf("LPT");

                BizPrinter dao = new BizPrinter();

                dd_printers printer = dao.QueryPrinters(2, printname).FirstOrDefault();
                if (vindex >= 0)
                {
                    printname = printname.Substring(vindex + 2);
                    //打印的IP一定要预先设置好

                    bool b = opos.OpenNetPort(printname);//"192.168.1.254"
                    if (!b)
                    {
                        Console.WriteLine("初始化'" + printname + "'的打印机参数失败。请检测打印机配置");
                        b = opos.OpenNetPort(printname);//"192.168.1.254"
                        Thread.Sleep(1000);
                        if (!b)
                        {
                            Console.WriteLine("初始化'" + printname + "'的打印机参数失败。请检测打印机配置");
                            utils.ShowTip("警告", "初始化'" + printname + "'的打印机参数失败。请检测打印机配置", 5000);
                            //form.showmsg("初始化'" + printname + "'的打印机参数失败。请检测打印机配置");
                            return;
                        }

                    }


                    Byte res = new Byte();
                    int ret = BeiYangOPOS.POS_NETQueryStatus(printname, out res);
                    StringBuilder sb = new StringBuilder();
                    #region 检测打印机状态
                    if ((res & 0x10) == 0x10)
                    {
                        sb.AppendLine("打印机出错！");
                    }
                    if ((res & 0x02) == 0x02)
                    {
                        sb.AppendLine("打印机脱机！");
                    }
                    if ((res & 0x04) == 0x04)
                    {
                        sb.AppendLine("上盖打开！");
                    }
                    if ((res & 0x20) == 0x20)
                    {
                        sb.AppendLine("切刀出错！");
                    }
                    if ((res & 0x40) == 0x40)
                    {
                        sb.AppendLine("纸将尽！");
                    }
                    if ((res & 0x80) == 0x80)
                    {
                        sb.AppendLine("缺纸！");
                    }
                    #endregion
                    if (sb.Length > 0)
                    {
                        string errormsg = ("'" + printname + "'的打印机处于非正常状态：" + sb.ToString() + "。请检测打印机配置。");
                        Console.WriteLine("Error", errormsg);
                        if (pcontent != null)
                        {
                            JObject main = (JObject)pcontent["main"];
                            try
                            {
                                dao.addPrintQueue(printname, JsonUtils.ObjectToJson(pcontent), pcontent["cfmainkey"].ToString(), 0);//0档口小票1，l划单小票
                            }
                            catch (Exception e)
                            {
                                log.Error(errormsg);
                            }

                        }
                        utils.ShowTip("警告", errormsg, 5000);
                        return;
                    }
                    tuiPrintPage(printname, printer);
                    return;
                }
                else if (cindex >= 0)
                {
                    //printname = printname.Substring(cindex + 4);
                    SerialPort com = new SerialPort();
                    int pbites = printer.pbites.Value;
                    com.BaudRate = pbites;
                    com.PortName = printname;
                    com.DataBits = 8;
                    bool b = opos.OpenComPort(ref com);
                    if (!b)
                    {
                        Console.WriteLine("初始化'{0}'的打印机参数失败。请检测打印机配置1");
                        b = opos.OpenComPort(ref com);
                        if (!b)
                        {
                            string errormsg = string.Format("初始化'{0}'的打印机参数失败。请检测打印机配置2",
                                            printname);
                            Console.WriteLine(errormsg); utils.ShowTip("警告", errormsg, 5000);
                            return;
                        }
                    }

                    tuiPrintPage(printname, printer);
                    return;
                }
                else if (uindex >= 0)
                {
                    tuiUsbPrintPage(printname, printer);
                    return;
                }
                else if (lindex >= 0)
                {
                    tuiLptPrintPage(printname, printer);
                    return;
                }
                //string status=PrintUtil.GetPrinterStatus(printname);
                // Console.WriteLine(printname+"结款小票的打印:" + status);
                if (printname == null || "".Equals(printname) || "default".Equals(printname))
                {
                    //this.printDocument1.PrinterSettings.PrinterName = printname;
                }
                else
                {
                    this.printDocument1.PrinterSettings.PrinterName = printname;
                }
                // SPrinterStatus status = PrintUtil.getStatus(printname);
                //PrintQueue pq = LocalPrintServer.GetDefaultPrintQueue();
                // if (status.IndexOf("打印纸用完") >= 0) {
                //     Console.WriteLine("打印纸用完");
                //    return;
                // }
                this.printDocument1.DocumentName = "test窗口票" + DateTime.Now.TimeOfDay.ToString();
                printDocument1.PrintPage +=

                new PrintPageEventHandler(this.printDocument1_tuiPrintPage);
                this.printDocument1.Print();
            }
        }
        private void tuiUsbPrintPage(string printname, dd_printers printer)
        {


            if (pcontent != null)
            {
                
                #region 执行指令打印

                int psize = printer.psize.Value;
                int pwidth = psize;
                string linestr = "------------------------------------------------";
                if (psize == 58)
                {
                    pwidth = 190;
                    linestr = "--------------------------------";
                }
                else if (psize == 70)
                {
                    
                    pwidth = 250;
                    linestr = "-----------------------------------------";
                }
                else if (psize == 76)
                {

                    pwidth = 260;
                    linestr = "----------------------------------------";
                }
                else if (psize == 80)
                {

                    pwidth = 260;
                }
                using (var sh = UsbPrinterResolver.OpenUSBPrinterBydeviceId(printname))
                {
                    using (var f = new System.IO.FileStream(sh, System.IO.FileAccess.ReadWrite))
                    {

 
                            OnWriteData(f, "【退菜】\n\n", true, true, false, 2);

                            string tablename = pcontent["tablename"].ToString();
                            bool _actual1 = HasChinese(tablename);
                            
                            if (_actual1)
                            {
                                tablename = tablename + " " ;
                            }
                            else
                            {
                                tablename = tablename + "号桌 " ;
                            }

                            OnWriteData(f, "   " + tablename + "\n\n", true, true, false, 2);
                             
                            OnWriteData(f, "下单时间     " + pcontent["createdate"].ToString() + "\n", false, false, false, 1);
                            OnWriteData(f, "退菜时间     " + pcontent["tctime"].ToString() + "\n", false, false, false, 1);
                         
                        OnWriteData(f, "项目                        " + "数量\n", false, false, false, 1);
                            OnWriteData(f, linestr + "\n", false, false, false, 1);
                         
                                    //菜品打印h20
                                    string num = pcontent["num"].ToString();
                                    string price = pcontent["price"].ToString();
                                     
                                    string name = pcontent["name"].ToString()+"(退)";

                                   
                                    string ename = "";//为了菜名过长设定，截断字符串
                                     
                                    
                                    var slen = getStringLength(name);
                                    if (slen > 24)
                                    {

                                        ename = name.Substring(12, name.Length - 12);
                                        name = name.Substring(0, 12);
                                        int chakong = 24 - getStringLength(name);
                                        //name = string.Format("{0,-12}", name);
                                        //name = name.Replace(" ", "  ");

                                        if (chakong > 0)
                                        {
                                            string kongge = string.Format("{0,-" + chakong + "}", "");
                                            name = name + kongge;
                                        }
                                    }
                                    else
                                    {

                                        int chakong = 24 - getStringLength(name);
                                        //name = string.Format("{0,-12}", name);
                                        //name = name.Replace(" ", "  ");

                                        if (chakong > 0)
                                        {
                                            string kongge = string.Format("{0,-" + chakong + "}", "");
                                            name = name + kongge;
                                        }
                                    }
                                    num = string.Format("{0,3}", num);
                                    // OnWriteData(f, name, true, true, false, 1);

                         
                                        OnWriteData(f, name + "    " + num + ename + "\n", false, true, false, 1);
                                    
                              
                            OnWriteData(f, linestr + "\n", false, false, false, 1);
                        string beizhu = "备注:" + pcontent["mark"]+ "\n\n\n\n";//
                        OnWriteData(f, beizhu, true, true, false, 1);

                            cutPage(f);

                        


                        #endregion
                    }
                }
            }


        }
        private void tuiLptPrintPage(string printname, dd_printers printer)
        {


            if (pcontent != null)
            {

                #region 执行指令打印

                LPTControls lpt = new LPTControls();
                lpt.Open(printname);
                Dictionary<string, object> main = pcontent;
                 
                //uint width = 2;
                int psize = printer.psize.Value;
                int pwidth = psize;
                string linestr = "------------------------------------------------";
                if (psize == 58)
                {
                    pwidth = 190;
                    linestr = "--------------------------------";
                }
                else if (psize == 70)
                {
                    
                    pwidth = 250;
                    linestr = "-----------------------------------------";
                }
                else if (psize == 76)
                {

                    pwidth = 260;
                    linestr = "----------------------------------------";
                }
                else if (psize == 80)
                {

                    pwidth = 260;
                }


                lpt.OnWriteData("【退菜】\n\n", true, true, false, 2);

                string tablename = pcontent["tablename"].ToString();
                bool _actual1 = HasChinese(tablename);

                if (_actual1)
                {
                    tablename = tablename + " ";
                }
                else
                {
                    tablename = tablename + "号桌 ";
                }

                lpt.OnWriteData("   " + tablename + "\n\n", true, true, false, 2);

                lpt.OnWriteData("下单时间     " + pcontent["createdate"].ToString() + "\n", false, false, false, 1);
                lpt.OnWriteData("退菜时间     " + pcontent["tctime"].ToString() + "\n", false, false, false, 1);

                lpt.OnWriteData("项目                        " + "数量\n", false, false, false, 1);
                lpt.OnWriteData(linestr + "\n", false, false, false, 1);

                //菜品打印h20
                string num = pcontent["num"].ToString();
                string price = pcontent["price"].ToString();
                string name = pcontent["name"].ToString() + "(退)";
                string ename = "";//为了菜名过长设定，截断字符串
                var slen = getStringLength(name);
                if (slen > 24)
                {
                    ename = name.Substring(12, name.Length - 12);
                    name = name.Substring(0, 12);
                    int chakong = 24 - getStringLength(name);
                    //name = string.Format("{0,-12}", name);
                    //name = name.Replace(" ", "  ");

                    if (chakong > 0)
                    {
                        string kongge = string.Format("{0,-" + chakong + "}", "");
                        name = name + kongge;
                    }
                }
                else
                {
                    int chakong = 24 - getStringLength(name);
                    //name = string.Format("{0,-12}", name);
                    //name = name.Replace(" ", "  ");

                    if (chakong > 0)
                    {
                        string kongge = string.Format("{0,-" + chakong + "}", "");
                        name = name + kongge;
                    }
                }
                num = string.Format("{0,3}", num);
                // OnWriteData(f, name, true, true, false, 1);
                lpt.OnWriteData(name + "    " + num + ename + "\n", false, true, false, 1);
                lpt.OnWriteData(linestr + "\n", false, false, false, 1);
                string beizhu = "备注:" + pcontent["mark"] + "\n\n\n\n";//
                lpt.OnWriteData(beizhu, true, true, false, 1);
                lpt.CutPaper();
                lpt.Close();
                #endregion
            }
           
        }

        private void tuiPrintPage(string printname, dd_printers printer)
        {


            if (pcontent != null)
            {
               
                #region 执行指令打印
                uint width = 2;

                int psize = printer.psize.Value;
                int pwidth = psize;
                string linestr = "------------------------------------------------";
                if (psize == 58)
                {
                    BeiYangOPOS.POS_SetLineSpacing(30);
                    width = 1;
                    pwidth = 190;
                    linestr = "--------------------------------";
                }
                else if (psize == 70)
                {
                    width = 2;
                    pwidth = 250;
                    linestr = "-----------------------------------------";
                }
                else if (psize == 76)
                {
                    width = 2;
                    pwidth = 270;
                    linestr = "---------------------------------------------";
                }
                else if (psize == 80)
                {
                    width = 2;
                    pwidth = 286;
                }

                BeiYangOPOS.POS_SetRightSpacing(0);
                BeiYangOPOS.POS_SetLineSpacing(30);
                 
                    int tlength = getStringLength("【退】");
                    BeiYangOPOS.POS_S_TextOut("【退】", (uint)(pwidth - tlength * 6) - 12 * width, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();

                    string tablename = pcontent["tablename"].ToString();
                    bool _actual1 = HasChinese(tablename);                   
                    if (_actual1)
                    {
                        tablename = tablename + " " ;
                    }
                    else
                    {
                        tablename = tablename + "号桌 " ;
                    }

                    int talength = getStringLength(tablename);
                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_S_TextOut(tablename, (uint)(pwidth - tlength * 12) - 12 * width, width, 2, opos.POS_FONT_TYPE_CHINESE, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();
                   
                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_S_TextOut("点餐时间", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    int dclength = getStringLength(pcontent["createdate"].ToString());
                    BeiYangOPOS.POS_S_TextOut(pcontent["createdate"].ToString(), (uint)(pwidth * 2 - dclength * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_S_TextOut("退菜时间", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    int sclength = getStringLength(pcontent["tctime"].ToString());
                    BeiYangOPOS.POS_S_TextOut(pcontent["tctime"].ToString(), (uint)(pwidth * 2 - sclength * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_S_TextOut("项目", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_S_TextOut("数量", (uint)(pwidth - getpos(1, "数量") * 12) * 2, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();

                    string num = pcontent["num"].ToString();
                    string price = pcontent["price"].ToString();
                    string name = pcontent["name"].ToString() + "(退)";
                    string ename = "";//为了菜名过长设定，截断字符串
                
                if (name.Length > 10)
                {

                    ename = name.Substring(10, name.Length - 10);
                    name = name.Substring(0, 10);
                }
                BeiYangOPOS.POS_S_TextOut(name, 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                num = string.Format("{0,6}", num);
                int flength = getStringLength(num);
                BeiYangOPOS.POS_S_TextOut(num, (uint)(pwidth * 2 - flength * 12 * width), width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
 

                BeiYangOPOS.POS_FeedLine();
                if (!ename.Equals(""))
                {
                    BeiYangOPOS.POS_S_TextOut(ename, 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();
                }

                BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();
                    string beizhu = "备注:" + pcontent["mark"]; 
                    BeiYangOPOS.POS_S_TextOut(beizhu, 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                    BeiYangOPOS.POS_S_TextOut("\n\n", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                    
                    BeiYangOPOS.POS_FeedLine();
                    BeiYangOPOS.POS_CutPaper(1,50);
               
                 

                opos.ClosePrinterPort();
                #endregion
            }

        }
        private void printDocument1_tuiPrintPage(object sender, PrintPageEventArgs e)
        {
            if (pcontent != null)
            {
              
                e.Graphics.Clear(Color.White);
                // 开始绘制文档  
                // 默认为横排文字  
                Graphics g = form.CreateGraphics();
                Font cfont = new Font(new FontFamily("宋体"), 9, FontStyle.Bold);
                Font tfont = new Font(new FontFamily("宋体"), 16, FontStyle.Bold);
                Font cpfont = new Font(new FontFamily("宋体"), 13, FontStyle.Bold);
                int pwidth = e.PageBounds.Width;//纸张宽度
                Pen pen1 = new Pen(Color.Black);
                pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                StringFormat SF = new StringFormat();
                SF.LineAlignment = StringAlignment.Center;                                        //设置属性为水平居中
                SF.Alignment = StringAlignment.Center;                                               //设置属性为垂直居中
                                                                                                     //其中e.PageBounds属性表示页面全部区域的矩形区域
               
                    RectangleF rect = new RectangleF(0, 0, pwidth, e.Graphics.MeasureString("【退】", tfont).Height);
                    e.Graphics.DrawString("【退】", tfont, Brushes.Black, rect, SF);
                    string tablename = pcontent["tablename"].ToString();
                    bool _actual1 = HasChinese(tablename);
                    
                    
                    if (_actual1)
                    {
                        tablename = tablename + " " ;
                    }
                    else
                    {
                        tablename = tablename + "号桌 " ;
                    }
                    RectangleF rect1 = new RectangleF(0, 35, pwidth, e.Graphics.MeasureString(tablename, tfont).Height);
                    e.Graphics.DrawString(tablename, tfont, Brushes.Black, rect1, SF);
                 
                    e.Graphics.DrawString("点餐时间", cfont, System.Drawing.Brushes.Black, 0, 70);
                    e.Graphics.DrawString(pcontent["createdate"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(pcontent["createdate"].ToString(), cfont).Width, 70);
                    e.Graphics.DrawString("退菜时间", cfont, System.Drawing.Brushes.Black, 0, 85);
                    e.Graphics.DrawString(pcontent["tctime"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(pcontent["tctime"].ToString(), cfont).Width, 85);
                    // 横线  
                    e.Graphics.DrawLine(pen1, 0, 105, pwidth, 105);

                    e.Graphics.DrawString("项目", cfont, System.Drawing.Brushes.Black, 0, 110);
                    e.Graphics.DrawString("数量", cfont, System.Drawing.Brushes.Black, pwidth - 40 + (40 - e.Graphics.MeasureString("数量", cfont).Width) / 2, 110);
                    // 横线  
                    e.Graphics.DrawLine(pen1, 0, 130, pwidth, 130);
                    int cheight = 135;
          

                string num = pcontent["num"].ToString();
                string price = pcontent["price"].ToString();
                string name = pcontent["name"].ToString() + "(退)";
                
                            StringFormat fmt = new StringFormat();
                            fmt.LineAlignment = StringAlignment.Near;//左对齐
                            fmt.FormatFlags = StringFormatFlags.LineLimit;//自动换行

                            //设定文本打印区域 b是左上角坐标，Size是打印区域（矩形） float mmtopt = 2.835f;
                            //Rectangle r = new Rectangle();
                            double sheight = 25;

                            var slen = getStringLength(name);
                            if (slen > 12)
                            {
                                sheight = sheight * Math.Ceiling(slen / 12.0);
                            }
                            Point b = new Point(0, cheight);
                            Rectangle r = new Rectangle(b, new Size(pwidth - 40, 25 * 2));
                            //e.Graphics.DrawString(name, cpfont, System.Drawing.Brushes.Black, 0, cheight);
                            e.Graphics.DrawString(name, cpfont, new SolidBrush(Color.Black), r, fmt);
                            e.Graphics.DrawString(num, cpfont, System.Drawing.Brushes.Black, pwidth - 40 + (40 - e.Graphics.MeasureString(num, cpfont).Width) / 2, cheight);
                            cheight += (int)sheight;
                            

 
                    cheight += 2;
                    // 横线 
                    e.Graphics.DrawLine(pen1, 0, cheight, pwidth, cheight);
                    string beizhu = "备注:" + pcontent["mark"] ;
                    beizhu = AutomaticLine(beizhu, 8, pwidth);//8, 36  
                    RectangleF drawRect = new RectangleF(0, cheight + 5, pwidth, e.Graphics.MeasureString(beizhu, tfont).Height); //设定这个就行了
                    e.Graphics.DrawString(beizhu, tfont, Brushes.Black, drawRect, null);
                                                                                                               //e.Graphics.MeasureString("点点菜单", new Font("Times New Roman", 20)).Height;
                                                                                                                   //表示获取你要打印字符串的高度

            }
        }

        //档口小票的打印0
        public void qtuiPrint(string printname)
        {
            lock (thisLock)
            {
                Console.WriteLine("档口小票的打印:" + printname);
                int vindex = printname.IndexOf("ip");
                int cindex = printname.IndexOf("COM");
                int uindex = printname.IndexOf("USB\\VID");
                int lindex = printname.IndexOf("LPT");
                BizPrinter dao = new BizPrinter();

                dd_printers printer = dao.QueryPrinters(2, printname).FirstOrDefault();
                if (vindex >= 0)
                {
                    printname = printname.Substring(vindex + 2);
                    //打印的IP一定要预先设置好

                    bool b = opos.OpenNetPort(printname);//"192.168.1.254"
                    if (!b)
                    {
                        Console.WriteLine("初始化'" + printname + "'的打印机参数失败。请检测打印机配置");
                        b = opos.OpenNetPort(printname);//"192.168.1.254"
                        Thread.Sleep(1000);
                        if (!b)
                        {
                            Console.WriteLine("初始化'" + printname + "'的打印机参数失败。请检测打印机配置");
                            utils.ShowTip("警告", "初始化'" + printname + "'的打印机参数失败。请检测打印机配置", 5000);
                            return;
                        }

                    }


                    Byte res = new Byte();
                    int ret = BeiYangOPOS.POS_NETQueryStatus(printname, out res);
                    StringBuilder sb = new StringBuilder();
                    #region 检测打印机状态
                    if ((res & 0x10) == 0x10)
                    {
                        sb.AppendLine("打印机出错！");
                    }
                    if ((res & 0x02) == 0x02)
                    {
                        sb.AppendLine("打印机脱机！");
                    }
                    if ((res & 0x04) == 0x04)
                    {
                        sb.AppendLine("上盖打开！");
                    }
                    if ((res & 0x20) == 0x20)
                    {
                        sb.AppendLine("切刀出错！");
                    }
                    if ((res & 0x40) == 0x40)
                    {
                        sb.AppendLine("纸将尽！");
                    }
                    if ((res & 0x80) == 0x80)
                    {
                        sb.AppendLine("缺纸！");
                    }
                    #endregion
                    if (sb.Length > 0)
                    {
                        string errormsg = ("'" + printname + "'的打印机处于非正常状态：" + sb.ToString() + "。请检测打印机配置。");
                        Console.WriteLine("Error", errormsg);
                        if (pcontent != null)
                        {
                            JObject main = (JObject)pcontent["main"];
                            try
                            {
                                dao.addPrintQueue(printname, JsonUtils.ObjectToJson(pcontent), pcontent["cfmainkey"].ToString(), 0);//0档口小票1，l划单小票
                            }
                            catch (Exception e)
                            {
                                log.Error(errormsg);
                            }

                        }
                        utils.ShowTip("警告", errormsg, 5000);
                        return;
                    }
                    qtuiPrintPage(printname, printer);
                    return;
                }
                else if (cindex >= 0)
                {
                    //printname = printname.Substring(cindex + 4);
                    SerialPort com = new SerialPort();
                    int pbites = printer.pbites.Value;
                    com.BaudRate = pbites;
                    com.PortName = printname;
                    com.DataBits = 8;
                    bool b = opos.OpenComPort(ref com);
                    if (!b)
                    {
                        Console.WriteLine("初始化'{0}'的打印机参数失败。请检测打印机配置1");
                        b = opos.OpenComPort(ref com);
                        if (!b)
                        {
                            string errormsg = string.Format("初始化'{0}'的打印机参数失败。请检测打印机配置2",
                                            printname);
                            Console.WriteLine(errormsg);
                            utils.ShowTip("警告", errormsg, 5000);
                            return;
                        }
                    }

                    qtuiPrintPage(printname, printer);
                    return;
                }
                else if (uindex >= 0)
                {
                    qtuiUsbPrintPage(printname, printer);
                    return;
                }
                else if (lindex >= 0)
                {
                    qtuiLptPrintPage(printname, printer);
                    return;
                }
                //string status=PrintUtil.GetPrinterStatus(printname);
                // Console.WriteLine(printname+"结款小票的打印:" + status);
                if (printname == null || "".Equals(printname) || "default".Equals(printname))
                {
                    //this.printDocument1.PrinterSettings.PrinterName = printname;
                }
                else
                {
                    this.printDocument1.PrinterSettings.PrinterName = printname;
                }
                // SPrinterStatus status = PrintUtil.getStatus(printname);
                //PrintQueue pq = LocalPrintServer.GetDefaultPrintQueue();
                // if (status.IndexOf("打印纸用完") >= 0) {
                //     Console.WriteLine("打印纸用完");
                //    return;
                // }
                this.printDocument1.DocumentName = "test窗口票" + DateTime.Now.TimeOfDay.ToString();
                printDocument1.PrintPage +=

                new PrintPageEventHandler(this.printDocument1_qtuiPrintPage);
                this.printDocument1.Print();
            }
        }
        private void qtuiUsbPrintPage(string printname, dd_printers printer)
        {


            if (pcontent != null)
            {

                #region 执行指令打印

                int psize = printer.psize.Value;
                int pwidth = psize;
                string linestr = "------------------------------------------------";
                if (psize == 58)
                {
                    pwidth = 190;
                    linestr = "--------------------------------";
                }
                else if (psize == 70)
                {
                    
                    pwidth = 250;
                    linestr = "-----------------------------------------";
                }
                else if (psize == 76)
                {

                    pwidth = 260;
                    linestr = "----------------------------------------";
                }
                else if (psize == 80)
                {

                    pwidth = 260;
                }
                using (var sh = UsbPrinterResolver.OpenUSBPrinterBydeviceId(printname))
                {
                    using (var f = new System.IO.FileStream(sh, System.IO.FileAccess.ReadWrite))
                    {


                        OnWriteData(f, "【退菜】\n\n", true, true, false, 2);

                        string tablename = pcontent["tablename"].ToString();
                        bool _actual1 = HasChinese(tablename);

                        if (_actual1)
                        {
                            tablename = tablename + " ";
                        }
                        else
                        {
                            tablename = tablename + "号桌 ";
                        }

                        OnWriteData(f, "   " + tablename + "\n\n", true, true, false, 2);

                        OnWriteData(f, "下单时间     " + pcontent["createdate"].ToString() + "\n", false, false, false, 1);
                        OnWriteData(f, "退菜时间     " + pcontent["tctime"].ToString() + "\n", false, false, false, 1);

                        OnWriteData(f, "项目              数量      金额\n", false, false, false, 1);
                        OnWriteData(f, linestr + "\n", false, false, false, 1);

                        //菜品打印h20
                        string num = pcontent["num"].ToString();
                        string price = pcontent["price"].ToString();

                        string name = pcontent["name"].ToString() + "(退)";


                        string ename = "";//为了菜名过长设定，截断字符串

                        var slen = getStringLength(name);
                        if (slen > 18)
                        {

                            ename = name.Substring(9, name.Length - 9);
                            name = name.Substring(0, 9);

                        }
                        int chakong = 18 - getStringLength(name);
                        if (chakong > 0)
                        {
                            string skongge = string.Format("{0,-" + chakong + "}", "");
                            name = name + skongge;
                        }
                        num = string.Format("{0,3}", num);
                        // OnWriteData(f, name, true, true, false, 1);

                        string fprice = string.Format("{0,6}", price);

                        OnWriteData(f,name + num + "     " + fprice + ename + "\n\n", false, true, false, 1);

                        OnWriteData(f, linestr + "\n", false, false, false, 1);
                        string beizhu = "备注:" + pcontent["mark"] + "\n\n\n\n";//
                        OnWriteData(f, beizhu, true, true, false, 1);

                        cutPage(f);




                        #endregion
                    }
                }
            }


        }
        private void qtuiLptPrintPage(string printname, dd_printers printer)
        {


            if (pcontent != null)
            {

                #region 执行指令打印

                LPTControls lpt = new LPTControls();
                lpt.Open(printname);
                Dictionary<string, object> main = pcontent;

                //uint width = 2;
                int psize = printer.psize.Value;
                int pwidth = psize;
                string linestr = "------------------------------------------------";
                if (psize == 58)
                {
                    pwidth = 190;
                    linestr = "--------------------------------";
                }
                else if (psize == 70)
                {
                  
                    pwidth = 250;
                    linestr = "-----------------------------------------";
                }
                else if (psize == 76)
                {

                    pwidth = 260;
                    linestr = "----------------------------------------";
                }
                else if (psize == 80)
                {

                    pwidth = 260;
                }


                lpt.OnWriteData("【退菜】\n\n", true, true, false, 2);

                string tablename = pcontent["tablename"].ToString();
                bool _actual1 = HasChinese(tablename);

                if (_actual1)
                {
                    tablename = tablename + " ";
                }
                else
                {
                    tablename = tablename + "号桌 ";
                }

                lpt.OnWriteData("   " + tablename + "\n\n", true, true, false, 2);

                lpt.OnWriteData("下单时间     " + pcontent["createdate"].ToString() + "\n", false, false, false, 1);
                lpt.OnWriteData("退菜时间     " + pcontent["tctime"].ToString() + "\n", false, false, false, 1);

               lpt.OnWriteData( "项目              数量      金额\n", false, false, false, 1);
                lpt.OnWriteData(linestr + "\n", false, false, false, 1);

                //菜品打印h20
                string num = pcontent["num"].ToString();
                string price = pcontent["price"].ToString();
                string name = pcontent["name"].ToString() + "(退)";
                string ename = "";//为了菜名过长设定，截断字符串
                var slen = getStringLength(name);
                if (slen > 18)
                {

                    ename = name.Substring(9, name.Length - 9);
                    name = name.Substring(0, 9);

                }
                int chakong = 18 - getStringLength(name);
                if (chakong > 0)
                {
                    string skongge = string.Format("{0,-" + chakong + "}", "");
                    name = name + skongge;
                }
                num = string.Format("{0,3}", num);
                // OnWriteData(f, name, true, true, false, 1);

                string fprice = string.Format("{0,6}",   price);

                lpt.OnWriteData(  name + num + "     " + fprice + ename + "\n\n", false, true, false, 1);

                //lpt.OnWriteData(name + "    " + num + ename + "\n", false, true, false, 1);
                lpt.OnWriteData(linestr + "\n", false, false, false, 1);
                string beizhu = "备注:" + pcontent["mark"] + "\n\n\n\n";//
                lpt.OnWriteData(beizhu, true, true, false, 1);
                lpt.CutPaper();
                lpt.Close();
                #endregion
            }

        }

        private void qtuiPrintPage(string printname, dd_printers printer)
        {


            if (pcontent != null)
            {

                #region 执行指令打印
                uint width = 2;

                int psize = printer.psize.Value;
                int pwidth = psize;
                string linestr = "------------------------------------------------";
                if (psize == 58)
                {
                    BeiYangOPOS.POS_SetLineSpacing(30);
                    width = 1;
                    pwidth = 190;
                    linestr = "--------------------------------";
                }
                else if (psize == 70)
                {
                    width = 2;
                    pwidth = 250;
                    linestr = "-----------------------------------------";
                }
                else if (psize == 76)
                {
                    width = 2;
                    pwidth = 270;
                    linestr = "---------------------------------------------";
                }
                else if (psize == 80)
                {
                    width = 2;
                    pwidth = 286;
                }

                BeiYangOPOS.POS_SetRightSpacing(0);
                BeiYangOPOS.POS_SetLineSpacing(30);

                int tlength = getStringLength("【退】");
                BeiYangOPOS.POS_S_TextOut("【退】", (uint)(pwidth - tlength * 6) - 12 * width, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();

                string tablename = pcontent["tablename"].ToString();
                bool _actual1 = HasChinese(tablename);
                if (_actual1)
                {
                    tablename = tablename + " ";
                }
                else
                {
                    tablename = tablename + "号桌 ";
                }

                int talength = getStringLength(tablename);
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut(tablename, (uint)(pwidth - tlength * 12) - 12 * width, width, 2, opos.POS_FONT_TYPE_CHINESE, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();

                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut("点餐时间", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                int dclength = getStringLength(pcontent["createdate"].ToString());
                BeiYangOPOS.POS_S_TextOut(pcontent["createdate"].ToString(), (uint)(pwidth * 2 - dclength * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut("退菜时间", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                int sclength = getStringLength(pcontent["tctime"].ToString());
                BeiYangOPOS.POS_S_TextOut(pcontent["tctime"].ToString(), (uint)(pwidth * 2 - sclength * 12), 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut("项目", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                //e.Graphics.DrawString("数量", cfont, System.Drawing.Brushes.Black, pwidth - 89 + (49 - e.Graphics.MeasureString("数量", cfont).Width) / 2, 110);
                BeiYangOPOS.POS_S_TextOut("数量", (uint)(pwidth - 10 * 12) * 2 - 24, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_S_TextOut("金额", (uint)(pwidth - getpos(1, "金额") * 12) * 2 - 24, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();

                string num = pcontent["num"].ToString();
                string price = pcontent["price"].ToString();
                string name = pcontent["name"].ToString() + "(退)";
                string ename = "";//为了菜名过长设定，截断字符串
                num = string.Format("{0,3}", num);

                // OnWriteData(f, name, false, false, false, 1);
                // OnWriteData(f, name+num + "      ", false, false, false, 1);
                string fprice = float.Parse(price).ToString("F2");

                
                int hznums = GetHanNumFromString(name);//数字个数
                int onums = (int)Math.Ceiling((name.Length - hznums) / 2.0);
                int zlength = hznums + onums;

                if (zlength > 6)
                {

                    ename = name.Substring(6, name.Length - 6);
                    name = name.Substring(0, 6);
                }
                BeiYangOPOS.POS_S_TextOut(name, 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_S_TextOut(num, (uint)(pwidth - 10 * 12) * 2, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                // string fprice=decimal.Round(decimal.Parse(price + ""), 2)+"";
               
                
                    //fprice = string.Format("{0,7}", "-" + fprice);
                    fprice = "-" + fprice;
                    fprice = string.Format("{0,6}", fprice);
                    int flength = getStringLength(fprice);
                    BeiYangOPOS.POS_S_TextOut(fprice, (uint)(pwidth * 2 - flength * 12 * width), width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);//(uint)(pwidth - getpos(1, fprice) * 24)*2+48

              

                BeiYangOPOS.POS_FeedLine();
                if (!ename.Equals(""))
                {
                    BeiYangOPOS.POS_S_TextOut(ename, 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                    BeiYangOPOS.POS_FeedLine();
                }

                BeiYangOPOS.POS_S_TextOut(linestr, 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
                BeiYangOPOS.POS_FeedLine();
                string beizhu = "备注:" + pcontent["mark"];
                BeiYangOPOS.POS_S_TextOut(beizhu, 0, width, 2, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);

                BeiYangOPOS.POS_S_TextOut("\n\n", 0, 1, 1, opos.POS_FONT_TYPE_STANDARD, opos.POS_FONT_STYLE_NORMAL);
 
                BeiYangOPOS.POS_FeedLine();
                BeiYangOPOS.POS_CutPaper(1, 50);



                opos.ClosePrinterPort();
                #endregion
            }

        }
        private void printDocument1_qtuiPrintPage(object sender, PrintPageEventArgs e)
        {
            if (pcontent != null)
            {

                e.Graphics.Clear(Color.White);
                // 开始绘制文档  
                // 默认为横排文字  
                Graphics g = form.CreateGraphics();
                Font cfont = new Font(new FontFamily("宋体"), 9, FontStyle.Bold);
                Font tfont = new Font(new FontFamily("宋体"), 16, FontStyle.Bold);
                Font cpfont = new Font(new FontFamily("宋体"), 13, FontStyle.Bold);
                int pwidth = e.PageBounds.Width;//纸张宽度
                Pen pen1 = new Pen(Color.Black);
                pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                StringFormat SF = new StringFormat();
                SF.LineAlignment = StringAlignment.Center;                                        //设置属性为水平居中
                SF.Alignment = StringAlignment.Center;                                               //设置属性为垂直居中
                                                                                                     //其中e.PageBounds属性表示页面全部区域的矩形区域

                RectangleF rect = new RectangleF(0, 0, pwidth, e.Graphics.MeasureString("【退】", tfont).Height);
                e.Graphics.DrawString("【退】", tfont, Brushes.Black, rect, SF);
                string tablename = pcontent["tablename"].ToString();
                bool _actual1 = HasChinese(tablename);


                if (_actual1)
                {
                    tablename = tablename + " ";
                }
                else
                {
                    tablename = tablename + "号桌 ";
                }
                RectangleF rect1 = new RectangleF(0, 35, pwidth, e.Graphics.MeasureString(tablename, tfont).Height);
                e.Graphics.DrawString(tablename, tfont, Brushes.Black, rect1, SF);

                e.Graphics.DrawString("点餐时间", cfont, System.Drawing.Brushes.Black, 0, 70);
                e.Graphics.DrawString(pcontent["createdate"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(pcontent["createdate"].ToString(), cfont).Width, 70);
                e.Graphics.DrawString("退菜时间", cfont, System.Drawing.Brushes.Black, 0, 85);
                e.Graphics.DrawString(pcontent["tctime"].ToString(), cfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(pcontent["tctime"].ToString(), cfont).Width, 85);
                // 横线  
                e.Graphics.DrawLine(pen1, 0, 105, pwidth, 105);

                e.Graphics.DrawString("项目", cfont, System.Drawing.Brushes.Black, 0, 110);
                //e.Graphics.DrawString("数量", cfont, System.Drawing.Brushes.Black, pwidth - 40 + (40 - e.Graphics.MeasureString("数量", cfont).Width) / 2, 110);
                e.Graphics.DrawString("数量", cfont, System.Drawing.Brushes.Black, pwidth - 89 + (49 - e.Graphics.MeasureString("数量", cfont).Width) / 2, 110);
                e.Graphics.DrawString("金额", cfont, System.Drawing.Brushes.Black, pwidth - 40 + (40 - e.Graphics.MeasureString("金额", cfont).Width) / 2, 110);
                // 横线  
                e.Graphics.DrawLine(pen1, 0, 130, pwidth, 130);
                int cheight = 135;


                string num = pcontent["num"].ToString();
                string price = pcontent["price"].ToString();
                string name = pcontent["name"].ToString() + "(退)";

               
                //设定文本打印区域 b是左上角坐标，Size是打印区域（矩形） float mmtopt = 2.835f;
                //Rectangle r = new Rectangle();
                
                int hznums = GetHanNumFromString(name);//数字个数
                int onums = (int)Math.Ceiling((name.Length - hznums) / 2.0);
                double sheight = 25;
                int nlength = getStringLength(name);
                //int zlength = hznums + onums;
                //MessageBox.Show("纸张宽度 " + pwidth);
                double tail = 12;
                if (pwidth > 200)
                {
                    tail = 16;
                }
                if (nlength > tail)
                {
                    sheight = sheight * Math.Ceiling(nlength / tail);
                }
                StringFormat fmt = new StringFormat();
                fmt.LineAlignment = StringAlignment.Near;//左对齐
                fmt.FormatFlags = StringFormatFlags.LineLimit;//自动换行
                Point b = new Point(0, cheight);
                Rectangle r = new Rectangle(b, new Size(pwidth - 89, 25 * 2));
                //e.Graphics.DrawString(name, cpfont, System.Drawing.Brushes.Black, 0, cheight);
                e.Graphics.DrawString(name, cpfont, new SolidBrush(Color.Black), r, fmt);

                //e.Graphics.DrawString(name, cpfont, System.Drawing.Brushes.Black, 0, cheight);
                e.Graphics.DrawString(num, cpfont, System.Drawing.Brushes.Black, pwidth - 89 + (49 - e.Graphics.MeasureString(num, cpfont).Width) / 2, cheight);
                e.Graphics.DrawString(price, cpfont, System.Drawing.Brushes.Black, pwidth - e.Graphics.MeasureString(price, cpfont).Width, cheight);
                cheight += (int)sheight;



                cheight += 2;
                // 横线 
                e.Graphics.DrawLine(pen1, 0, cheight, pwidth, cheight);
                string beizhu = "备注:" + pcontent["mark"];
                beizhu = AutomaticLine(beizhu, 8, pwidth);//8, 36  
                RectangleF drawRect = new RectangleF(0, cheight + 5, pwidth, e.Graphics.MeasureString(beizhu, tfont).Height); //设定这个就行了
                e.Graphics.DrawString(beizhu, tfont, Brushes.Black, drawRect, null);
                //e.Graphics.MeasureString("点点菜单", new Font("Times New Roman", 20)).Height;
                //表示获取你要打印字符串的高度

            }
        }

    }
}
