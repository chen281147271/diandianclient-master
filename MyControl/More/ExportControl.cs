using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using ZipOneCode.ZipProvider;
namespace DianDianClient.MyControl.More
{
    public partial class ExportControl : UserControl
    {
        DataTable dt=new DataTable();
        int whocheck = 1;
        int zhuotieOrzhuopai = 1;//1桌贴2桌牌
        public ExportControl(DataTable dt)
        {
            InitializeComponent();
            this.dt = dt;
            this.Pic_zhuotie1.EditValue = global::DianDianClient.Properties.Resources.zhuotie1;
            this.Pic_zhuotie2.EditValue = global::DianDianClient.Properties.Resources.zhuotie2;
            this.Pic_zhuotie3.EditValue = global::DianDianClient.Properties.Resources.zhuotie3;
            (this.Pic_zhuotie1.Properties.ContextButtons[0] as CheckContextButton).Checked = true;
        }
        private void AllNoChecked()
        {
            (this.Pic_zhuotie1.Properties.ContextButtons[0] as CheckContextButton).Checked = false;
            (this.Pic_zhuotie2.Properties.ContextButtons[0] as CheckContextButton).Checked = false;
            (this.Pic_zhuotie3.Properties.ContextButtons[0] as CheckContextButton).Checked = false;
        }
        private void Pic_zhuotie1_Click(object sender, EventArgs e)
        {
            AllNoChecked();
           (this.Pic_zhuotie1.Properties.ContextButtons[0] as CheckContextButton).Checked=true;
            whocheck = 1;
        }

        private void Pic_zhuotie2_Click(object sender, EventArgs e)
        {
            AllNoChecked();
            (this.Pic_zhuotie2.Properties.ContextButtons[0] as CheckContextButton).Checked = true;
            whocheck = 2;
        }

        private void Pic_zhuotie3_Click(object sender, EventArgs e)
        {
            AllNoChecked();
            (this.Pic_zhuotie3.Properties.ContextButtons[0] as CheckContextButton).Checked = true;
            whocheck = 3;
        }

        private void Pic_zhuotie1_ContextButtonClick(object sender, DevExpress.Utils.ContextItemClickEventArgs e)
        {
            AllNoChecked();
            (this.Pic_zhuotie1.Properties.ContextButtons[0] as CheckContextButton).Checked = true;
            whocheck = 1;
        }

        private void Pic_zhuotie2_ContextButtonClick(object sender, DevExpress.Utils.ContextItemClickEventArgs e)
        {
            AllNoChecked();
            (this.Pic_zhuotie2.Properties.ContextButtons[0] as CheckContextButton).Checked = true;
            whocheck = 2;
        }

        private void Pic_zhuotie3_ContextButtonClick(object sender, DevExpress.Utils.ContextItemClickEventArgs e)
        {
            AllNoChecked();
            (this.Pic_zhuotie3.Properties.ContextButtons[0] as CheckContextButton).Checked = true;
            whocheck = 3;
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.radioGroup1.SelectedIndex == 0)
            {
                this.Pic_zhuotie1.EditValue = global::DianDianClient.Properties.Resources.zhuotie1;
                this.Pic_zhuotie2.EditValue = global::DianDianClient.Properties.Resources.zhuotie2;
                this.Pic_zhuotie3.EditValue = global::DianDianClient.Properties.Resources.zhuotie3;
                zhuotieOrzhuopai = 1;
            }
            else
            {
                this.Pic_zhuotie1.EditValue = global::DianDianClient.Properties.Resources.zhuopai1;
                this.Pic_zhuotie2.EditValue = global::DianDianClient.Properties.Resources.zhuopai2;
                this.Pic_zhuotie3.EditValue = global::DianDianClient.Properties.Resources.zhuopai3;
                zhuotieOrzhuopai = 2;
            }
        }

        private void Btn_Export_Click(object sender, EventArgs e)
        {
            MyEvent.More.MoreEvent.ShowWait();
            foreach (DataRow dr in dt.Rows)
            {
                string tableName = dr["tableName"].ToString();
                string qrCode = dr["qrCode"].ToString();
                string tableposkey = dr["tableposkey"].ToString();
                Image bg = null;
                switch (whocheck)
                {
                    case 1:
                        if(zhuotieOrzhuopai == 1)
                        {
                            bg = global::DianDianClient.Properties.Resources.zhuotiebg1;
                            SvaeImage(bg, tableName, qrCode, 394, 427, 20, 980, tableposkey,392,395,75,Color.White);
                        }
                        else
                        {
                            bg = global::DianDianClient.Properties.Resources.zhuopaibg1;
                            SvaeImage(bg, tableName, qrCode, 175, 600, 700, 1570, tableposkey, 800, 805, 95, Color.FromArgb(244, 120, 50));
                        }
                        break;
                    case 2:
                        if (zhuotieOrzhuopai == 1)
                        {
                            bg = global::DianDianClient.Properties.Resources.zhuotiebg2;
                            SvaeImage(bg, tableName, qrCode, 800, 100, 20, 1200, tableposkey, 392, 395, 75, Color.White);
                        }
                        else
                        {
                            bg = global::DianDianClient.Properties.Resources.zhuopaibg2;
                            SvaeImage(bg, tableName, qrCode, 275, 550, 300, 100, tableposkey, 392, 395, 95, Color.FromArgb(244, 120, 50));
                        }
                        break;
                    case 3:
                        if (zhuotieOrzhuopai == 1)
                        {
                            bg = global::DianDianClient.Properties.Resources.zhuotiebg3;
                            SvaeImage(bg, tableName, qrCode, 750, 140, 20, 580, tableposkey, 330, 332, 75, Color.White);
                        }
                        else
                        {
                            bg = global::DianDianClient.Properties.Resources.zhuopaibg3;
                            SvaeImage(bg, tableName, qrCode, 60, 300, 600, 330, tableposkey, 500, 503, 110, Color.FromArgb(244, 120, 50));
                        }
                        break;

                }
                bg.Dispose();
            }
            MyEvent.More.MoreEvent.EndShowWait();
            SaveZip();
        }
        #region 画图
        private void SaveZip()
        {
            string CurrentDirectory = System.Environment.CurrentDirectory;
            string path = CurrentDirectory + "\\temp";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "压缩（*.zip）|*.zip";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string ZipName = saveFileDialog.FileName;
                ZipHelper.CreateZip(@path, @ZipName);
                DeleteDir(@path);
                Utils.utils.ShowMessageBox("导出完成！");
            }
        }
        private void SvaeImage(Image bg, string tableName, string qrCode, int qrCodeX, int qrCodeY,int tableNameX,int tableNameY,string tableposkey, int barCodewidth, int barCodeheight, int fontSize, Color fontColor)
        {
            string CurrentDirectory = System.Environment.CurrentDirectory;
            this.barCodeControl1.Text = qrCode;
            this.barCodeControl1.Size = new System.Drawing.Size(barCodewidth, barCodeheight);
            string bgfilePath = "";
            using (Bitmap QRCode = new Bitmap(this.barCodeControl1.ExportToImage()))
            {
                string QRCodefilePath = CurrentDirectory + "\\QRCode.png";
                QRCode.Save(QRCodefilePath, ImageFormat.Png);
                QRCode.Dispose();
            }
            string tableNamefilePath = CurrentDirectory + "\\tableName.png";
            CreateImage(tableName, tableNamefilePath, fontSize,fontColor);

               Bitmap bm_bp = new Bitmap(bg);
            
                bgfilePath = CurrentDirectory + "\\bg.png";
                bm_bp.Save(bgfilePath, ImageFormat.Png);
                bm_bp.Dispose();
                bg.Dispose();
            

            favoriteImage[] FaImage = new favoriteImage[2];

            FaImage[0].x = qrCodeX;
            FaImage[0].y = qrCodeY;
            FaImage[0].imagePath = CurrentDirectory + "\\QRCode.png";

            FaImage[1].x = tableNameX;
            FaImage[1].y = tableNameY;
            FaImage[1].imagePath = CurrentDirectory + "\\tableName.png";

            generateWinterMark(CurrentDirectory, bgfilePath, FaImage,tableposkey);
        }
        public struct favoriteImage
        {
            private string _imagePath;
            private int _x;
            private int _y;

            public int x
            {
                get
                {
                    return _x;
                }
                set
                {
                    _x = value;
                }
            }

            public int y
            {
                get
                {
                    return _y;
                }
                set
                {
                    _y = value;
                }
            }

            public string imagePath
            {
                get
                {
                    return _imagePath;
                }
                set
                {
                    _imagePath = value;
                }
            }
        }
        public static void CreateImage(string name, string filePath,int fontSize,Color fontColor)
        {
            int wid = 400;
            int high = 200;
            Font font = new Font("微软雅黑", fontSize, FontStyle.Bold);
            //绘笔颜色  
            SolidBrush brush = new SolidBrush(fontColor);
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);


            Bitmap image = new Bitmap(wid, high);
            Graphics g = Graphics.FromImage(image);
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            SizeF sizef = g.MeasureString(name, font, PointF.Empty, format);
            int width = (int)(sizef.Width + 1);
            int height = (int)(sizef.Height + 1);
            image.Dispose();
            image = new Bitmap(width, height);
            //g.Clear(ColorTranslator.FromHtml("#f0f0f0"));  
            g = Graphics.FromImage(image);
            g.SmoothingMode = SmoothingMode.AntiAlias;//清除锯齿的呈现
            g.Clear(Color.Transparent);//Transparent  


            RectangleF rect = new RectangleF(0, 0, width, height);
            // RectangleF rect = new RectangleF(5, 2, wid, high);  
            //绘制图片  
            g.DrawString(name, font, brush, rect);
            //保存图片  
            image.Save(filePath, ImageFormat.Png);
            //释放对象  
            g.Dispose();
            image.Dispose();
        }
        private static string generateWinterMark(string savePath, string body_path, favoriteImage[] favorite,string saveImageName)
        {
            //create a image object containing the photograph to watermark
            // Image imgPhoto = Image.FromFile(body_path);
            System.Drawing.Image img = System.Drawing.Image.FromFile(body_path);
            System.Drawing.Image imgPhoto = new System.Drawing.Bitmap(img);
            img.Dispose();
            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;

            //create a Bitmap the Size of the original photograph
            Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);

            //设置此 Bitmap 的分辨率。 
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            //load the Bitmap into a Graphics object 
            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            //Set the rendering quality for this Graphics object
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;//清除锯齿的呈现
            Bitmap bmWatermark=null;                                       //haix
            //Bitmap bmWatermark = new Bitmap(bmPhoto);
            for (int i = 0; i < favorite.Length; i++)
            {
                //Draws the photo Image object at original size to the graphics object.
                grPhoto.DrawImage(
                    imgPhoto,                               // Photo Image object
                    new Rectangle(0, 0, phWidth, phHeight), // Rectangle structure
                    0,                                      // x-coordinate of the portion of the source image to draw. 
                    0,                                      // y-coordinate of the portion of the source image to draw. 
                    phWidth,                                // Width of the portion of the source image to draw. 
                    phHeight,                               // Height of the portion of the source image to draw. 
                    GraphicsUnit.Pixel);                    // Units of measure 


                //------------------------------------------------------------
                //Step #2 - Insert Property image,For example:hair,skirt,shoes etc.
                //------------------------------------------------------------
                //create a image object containing the watermark
                Image imgWatermark = new Bitmap(favorite[i].imagePath);
                int wmWidth = imgWatermark.Width;
                int wmHeight = imgWatermark.Height;


                //Create a Bitmap based on the previously modified photograph Bitmap
                bmWatermark = new Bitmap(bmPhoto);
                //bmWatermark.MakeTransparent(); //使默认的透明颜色对此 Bitmap 透明。

                //bmWatermark.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
                //Load this Bitmap into a new Graphic Object
                Graphics grWatermark = Graphics.FromImage(bmWatermark);


                int xPosOfWm = favorite[i].x;
                int yPosOfWm = favorite[i].y;

                //叠加
                grWatermark.DrawImage(imgWatermark, new Rectangle(xPosOfWm, yPosOfWm, wmWidth, wmHeight),  //Set the detination Position
                0,                  // x-coordinate of the portion of the source image to draw. 
                0,                  // y-coordinate of the portion of the source image to draw. 
                wmWidth,            // Watermark Width
                wmHeight,		    // Watermark Height
                GraphicsUnit.Pixel, // Unit of measurment
                null);   //ImageAttributes Object


                //Replace the original photgraphs bitmap with the new Bitmap
                imgPhoto = bmWatermark;
                grWatermark.Dispose();
                imgWatermark.Dispose();
               // bmWatermark.Dispose();
            }
            grPhoto.Dispose();
            //haix

            string nowTime = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
            nowTime += DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

            string saveImagePath = savePath +"\\temp\\"+ saveImageName + ".png";
            checkDir(savePath + "\\temp");
            //save new image to file system.
            imgPhoto.Save(saveImagePath, ImageFormat.Png);
            imgPhoto.Dispose();
            bmWatermark.Dispose();
            bmPhoto.Dispose();

            return saveImagePath;
        }
        /// <summary>  
        /// 检查指定目录是否存在,如不存在则创建  
        /// </summary>  
        /// <param name="url"></param>  
        /// <returns></returns>  
        public static bool checkDir(string url)
        {
            try
            {
                if (!Directory.Exists(url))//如果不存在就创建file文件夹　　             　　                
                    Directory.CreateDirectory(url);//创建该文件夹　　              
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #region 直接删除指定目录下的所有文件及文件夹(保留目录)
        /// <summary>
        ///直接删除指定目录下的所有文件及文件夹(保留目录)
        /// </summary>
        /// <param name="strPath">文件夹路径</param>
        /// <returns>执行结果</returns>
        public static bool DeleteDir(string strPath)
        {
            try
            {
                strPath = @strPath.Trim().ToString();// 清除空格
                if (System.IO.Directory.Exists(strPath))// 判断文件夹是否存在
                {
                    string[] strDirs = System.IO.Directory.GetDirectories(strPath);// 获得文件夹数组
                    string[] strFiles = System.IO.Directory.GetFiles(strPath);// 获得文件数组
                    foreach (string strFile in strFiles)// 遍历所有子文件夹
                    {
                        System.Diagnostics.Debug.Write(strFile + "-deleted");
                        System.IO.File.Delete(strFile);// 删除文件夹
                    }
                    foreach (string strdir in strDirs)// 遍历所有文件
                    {
                        System.Diagnostics.Debug.Write(strdir + "-deleted");
                        System.IO.Directory.Delete(strdir, true);// 删除文件
                    }
                }
                return true;// 成功
            }
            catch (Exception Exp) // 异常处理
            {
                System.Diagnostics.Debug.Write(Exp.Message.ToString());// 异常信息
                return false;// 失败
            }
        }
        #endregion
        #endregion
    }
}
