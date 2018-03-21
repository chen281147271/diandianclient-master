using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DianDianClient.MyControl.More
{
    public partial class QRCodeControl : UserControl
    {
        int tableposkey;
        int QRCode;
        public QRCodeControl(int tableposkey,int QRCode)
        {
            InitializeComponent();
            this.QRCode = QRCode;
            this.tableposkey = tableposkey;
            this.textEdit1.Text = QRCode.ToString();
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            this.barCodeControl1.Text = textEdit1.Text;
        }

        private void btn_saveas_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "图片（*.png）|*.png";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = QRCode.ToString();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)

            {

                string pictureName = saveFileDialog.FileName;



                if (this.barCodeControl1.ExportToImage() != null)

                {

                    ////********************照片另存*********************************

                    using (MemoryStream mem = new MemoryStream())

                    {

                        //这句很重要，不然不能正确保存图片或出错（关键就这一句）

                        Bitmap bmp = new Bitmap(this.barCodeControl1.ExportToImage());

                        //保存到内存

                        //bmp.Save(mem, pictureBox1.Image.RawFormat );

                        //保存到磁盘文件

                        bmp.Save(@pictureName, this.barCodeControl1.ExportToImage().RawFormat);

                        bmp.Dispose();



                        MessageBox.Show("照片另存成功！", "系统提示");

                    }

                    ////********************照片另存*********************************

                }

            }

        }
    }
}
