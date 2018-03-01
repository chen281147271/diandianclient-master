using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianDianClient.Utils
{
    class CommControl

    {

        /// <summary>

        /// 打印位置

        /// Left 居左打印

        /// Center 居中打印

        /// Right 居右打印

        /// </summary>

        public enum HorPos { Left, Center, Right }

        private int ColWidth = 32;

        private System.IO.Ports.SerialPort serialPort;

        /// <summary>

        /// 构造函数

        /// </summary>

        /// <param name="PortName">打印机所在的串口</param>

        public CommControl(string PortName)

        {

            try

            {

                serialPort = new System.IO.Ports.SerialPort(PortName);

                serialPort.BaudRate = 9600;

                serialPort.DataBits = 8;

                serialPort.StopBits = System.IO.Ports.StopBits.One;

                serialPort.Parity = System.IO.Ports.Parity.None;

                serialPort.Open();

                SetNormalFont();

            }

            catch { }

        }

        /// <summary>

        /// 析构函数，不调用此函数，不能释放串口

        /// </summary>

        /// <returns>是否释放成功</returns>

        public bool Dispose()

        {

            bool Result = true;

            if (serialPort != null)

            {

                if (serialPort.IsOpen)

                {

                    try

                    {

                        serialPort.Close();

                        serialPort.Dispose();

                    }

                    catch { Result = false; }

                }

            }

            return Result;

        }

        /// <summary>

        /// 打印机所在串口是否打开

        /// </summary>

        /// <returns>true 串口打开成功；false串口打开失败</returns>

        public bool IsOpen()

        {

            bool Result = false;

            if (serialPort != null && serialPort.IsOpen) Result = true;

            return Result;

        }

        /// <summary>

        /// 像打印机发送byte类型的数据

        /// </summary>

        /// <param name="bdata">要打印的数据</param>

        /// <returns>打印是否成功</returns>

        public bool Write(byte[] bdata)

        {

            try

            {

                if (IsOpen())

                {

                    serialPort.Write(bdata, 0, bdata.Length);

                    return true;

                }

                else

                {

                    return false;

                }

            }

            catch { return false; }

        }

        /// <summary>

        /// 向打印机发送字符串类型的数据

        /// </summary>

        /// <param name="Data">要打印的数据</param>

        /// <returns>打印是否成功</returns>

        public bool Write(string Data)

        {

            try

            {

                if (IsOpen())

                {

                    byte[] bData = System.Text.Encoding.Default.GetBytes(Data);

                    Write(bData);

                    return true;

                }

                else

                {

                    return false;

                }

            }

            catch { return false; }

        }

        /// <summary>

        /// 发送数据到打印机,打印完成后,自动跳到下一行

        /// </summary>

        /// <param name="Data">要打印的数据</param>

        /// <returns>是否打印成功</returns>

        public bool WriteLine(string Data)

        {

            bool Result = Write(Data);

            if (Result) Result = NewRow();

            return Result;

        }

        /// <summary>

        /// 发送数据到打印机,打印完成后,自动跳到下一行,并可指定打印位置

        /// </summary>

        /// <param name="Data">要打印的数据</param>

        /// <returns>是否打印成功</returns>

        public bool WriteLine(string Data, HorPos horpos)

        {

            int Length = Encoding.Default.GetBytes(Data).Length;

            if (Length > ColWidth || HorPos.Left == horpos) return WriteLine(Data);

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

            return WriteLine(Data);

        }

        /// <summary>

        /// 打印一行====

        /// </summary>

        /// <returns>是否打印成功</returns>

        public bool PrintLine()

        {

            return WriteLine("================================");

        }

        /// <summary>

        /// 打印日期 格式:2009-01-01 01:01:01

        /// </summary>

        /// <returns>是否打印成功</returns>

        public bool PrintDate()

        {

            return WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

        }

        /// <summary>

        /// 打印头移动到下一行

        /// </summary>

        /// <returns>是否移动成功</returns>

        public bool NewRow()

        {

            byte[] temp = new byte[] { 0x0A };

            return Write(temp);

        }

        /// <summary>

        /// 打印头移动多行

        /// </summary>

        /// <param name="iRow">要移动的行数</param>

        /// <returns>是否移动成功</returns>

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

        /// <summary>

        /// 切纸

        /// </summary>

        /// <returns>切纸是否成功</returns>

        public bool CutPaper()

        {

            byte[] temp = new byte[] { 0x1D, 0x56, 0x00, 0x05 };

            return Write(temp);

        }

        /// <summary>

        /// 设置打印机初始化状态

        /// </summary>

        /// <returns></returns>

        public bool SetNormalFont()

        {

            if (!IsOpen()) return false;

            byte[] temp;

            try

            {

                //1D, 50 设置横向和纵向移动单位

                temp = new byte[] { 0x1D, 0x50, 0xB4, 0xB4 };

                serialPort.Write(temp, 0, temp.Length);

                //1B, 53 选择标准模式

                temp = new byte[] { 0x1B, 0x53 };

                serialPort.Write(temp, 0, temp.Length);

                //1B, 20 设置字符右间距

                temp = new byte[] { 0x1B, 0x20, 0x00 };

                serialPort.Write(temp, 0, temp.Length);

                //设置汉字字符左右间距

                temp = new byte[] { 0x1C, 0x53, 0x00, 0x00 };

                serialPort.Write(temp, 0, temp.Length);

                //1D 42 是否反选打印 01反选/00取消

                temp = new byte[] { 0x1D, 0x42, 0x00 };

                serialPort.Write(temp, 0, temp.Length);

                //1B 45 选择/取消加粗模式 01选择/00取消

                temp = new byte[] { 0x1B, 0x45, 0x00 };

                serialPort.Write(temp, 0, temp.Length);

                //1B 7B 选择/取消倒置打印模式 01选择/00取消

                temp = new byte[] { 0x1B, 0x7B, 0x00 };

                serialPort.Write(temp, 0, temp.Length);

                //1B 2D 设置/取消下划线 01设置/00取消

                temp = new byte[] { 0x1B, 0x2D, 0x00 };

                serialPort.Write(temp, 0, temp.Length);

                //1B 2D 设置/取消汉字下划线 01设置/00取消

                temp = new byte[] { 0x1C, 0x2D, 0x00 };

                serialPort.Write(temp, 0, temp.Length);

                //选择取消顺时针旋转90度 01选择 00取消

                temp = new byte[] { 0x1B, 0x56, 0x00 };

                serialPort.Write(temp, 0, temp.Length);

                //1B 45 选择/取消加粗模式 01选择/00取消

                temp = new byte[] { 0x1B, 0x45, 0x00 };

                serialPort.Write(temp, 0, temp.Length);

                //1B 45 设置绝对打印位置

                temp = new byte[] { 0x1B, 0x24, 0x00, 0x00 };

                serialPort.Write(temp, 0, temp.Length);

                //1B, 33 设置行高, 18个像素

                temp = new byte[] { 0x1B, 0x33, 0x20 };

                serialPort.Write(temp, 0, temp.Length);

                //1B 4D 选择字体 03为汉字字体

                temp = new byte[] { 0x1B, 0x4D, 0x03 };

                serialPort.Write(temp, 0

                , temp.Length);

                //1D 21 选择字体大小,默认

                temp = new byte[] { 0x1D, 0x21, 0x00 };

                serialPort.Write(temp, 0, temp.Length);

                return true;

            }

            catch { return false; }

        }

        /// <summary>

        /// 以大一倍的字体打印数据

        /// </summary>

        /// <param name="Data">需要打印的数据</param>

        /// <returns>是否打印成功</returns>

        public bool WriteBig(string Data)

        {

            bool Result = false;

            Result = SetNormalFont();

            if (!Result) return Result;

            try

            {

                byte[] temp;

                //1B, 33 设置行高, 54个像素

                temp = new byte[] { 0x1B, 0x33, 0x48 };

                serialPort.Write(temp, 0, temp.Length);

                //1B 4D 选择字体 03为汉字字体

                temp = new byte[] { 0x1B, 0x4D, 0x03 };

                serialPort.Write(temp, 0, temp.Length);

                //横向放大和纵向放大不可同时作用

                //1D 21 选择字体大小,横向放大1倍

                temp = new byte[] { 0x1D, 0x21, 0x10 };

                serialPort.Write(temp, 0, temp.Length);

                //1D 21 选择字体大小,纵向放大1倍

                //temp = new byte[] { 0x1D, 0x21, 0x01 };

                //serialPort.Write(temp, 0, temp.Length);

                //1B 45 选择/取消加粗模式 01选择/00取消

                temp = new byte[] { 0x1B, 0x45, 0x01 };

                serialPort.Write(temp, 0, temp.Length);

                Write(Data);

                Result = true;

            }

            catch { Result = false; }

            Result = SetNormalFont();

            return Result;

        }

        /// <summary>

        /// 以大一倍的字体打印数据，打印完成换行

        /// </summary>

        /// <param name="Data">需要打印的数据</param>

        /// <returns>是否打印成功</returns>

        public bool WriteBigLine(string Data)

        {

            bool Result = false;

            Result = SetNormalFont();

            if (!Result) return Result;

            try

            {

                byte[] temp;

                //1B, 33 设置行高, 54个像素

                temp = new byte[] { 0x1B, 0x33, 0x48 };

                serialPort.Write(temp, 0, temp.Length);

                //1B 4D 选择字体 03为汉字字体

                temp = new byte[] { 0x1B, 0x4D, 0x03 };

                serialPort.Write(temp, 0, temp.Length);

                //横向放大和纵向放大不可同时作用

                //1D 21 选择字体大小,横向放大1倍

                temp = new byte[] { 0x1D, 0x21, 0x10 };

                serialPort.Write(temp, 0, temp.Length);

//1D 21 选择字体大小,纵向放大1倍

//temp = new byte[] { 0x1D, 0x21, 0x01 };

                //serialPort.Write(temp, 0, temp.Length);

                //1B 45 选择/取消加粗模式 01选择/00取消

                temp = new byte[] { 0x1B, 0x45, 0x01 };

                serialPort.Write(temp, 0, temp.Length);

                Write(Data);

                Result = true;

            }

            catch { Result = false; }

            Result = SetNormalFont();

            if (Result) Result = NewRow();

            return Result;

        }

        /// <summary>

        /// 设置带下划线的行

        /// </summary>

        /// <returns>是否设置成功</returns>

        public bool SetUnderLine()

        {

            bool Result = false;

            //Result = SetNormalFont();

            // if (!Result) return Result;

            try

            {

                byte[] temp;

                //1B 2D 设置/取消下划线 01设置/00取消

                temp = new byte[] { 0x1B, 0x2D, 0x02 };

                serialPort.Write(temp, 0, temp.Length);

                //1B 2D 设置/取消汉字下划线 01设置/00取消

                temp = new byte[] { 0x1C, 0x2D, 0x02 };

                serialPort.Write(temp, 0, temp.Length);

                Write(" ");

                Result = true;

            }

            catch { Result = false; }

            Result = SetNormalFont();

            return Result;

        }

    }
}
