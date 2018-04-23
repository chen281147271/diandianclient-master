using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DianDianClient.MyModels;

namespace DianDianClient.MyControl.BusinessDetails
{
    public partial class BusinessDetailControl : UserControl
    {
        MyControl.BusinessDetails.MyPrintControl Print = new MyControl.BusinessDetails.MyPrintControl();
        Biz.BizBusinessAnalysis bizBusinessAnalysis = new Biz.BizBusinessAnalysis();
        Biz.BizBillController BillController = new Biz.BizBillController();
        DevExpress.XtraCharts.ChartControl chart_detail = new ChartControl();
        DevExpress.XtraCharts.ChartControl chart_type = new ChartControl();
        DataTable table;//业务笔数统计
        DataTable table_detail_Pay;//支付方式统计
        DataTable table_detail_Type;//类别统计
        DataTable table_detail_PayDate;//日期支付统计
        Point mousepoint = new Point(0, 0);//控件BUG 点一次鼠标触发两次事件 现在通过位置屏蔽误触
        DateTime sdate;
        DateTime edate;
        public BusinessDetailControl()
        {
            InitializeComponent();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            IniDate();
            RefreshChart();
            IniPie();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
           // tableLayoutPanel1.Dock = DockStyle.Fill;
        }
        //class v_cfmainaccount2 : Models.v_cfmainaccount
        //{  
        //    public string str_state { get; set; }
        //    public string str_type { get; set; }
        //}
        private void IniDate()
        {
            dateEdit1.Text = "2017 - 02 - 27";
            dateEdit1.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dateEdit2.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        private void RefreshChart()
        {
            int i = 0;
            //顺序不能反
            table_detail_Type = GetChartData(bizBusinessAnalysis.QueryRecordGroupByType(sdate, edate));
            table_detail_PayDate = table.Clone();
            foreach (Models.RecordGroupTotleBean recordGroupTotle in bizBusinessAnalysis.QueryRecordGroupByDateByPayType(sdate, edate))
            {

                foreach(DataRow dr in GetChartData(recordGroupTotle).Rows){
                    table_detail_PayDate.Rows.Add(dr.ItemArray);
                }
            }
            table_detail_Pay = GetChartData(bizBusinessAnalysis.QueryRecordGroupByPayType(sdate, edate));
        }
        private DataTable GetChartData(Models.RecordGroupTotleBean recordGroupTotle)//type 1 支付方式 2类型
        {
            DataTable table_detail=null;
            try
            {
                Models.RecordGroupTotleBean recordGroupTotleBean= recordGroupTotle;
                List<Models.RecordGroupBean> recordGroupBean = recordGroupTotleBean.groupList;
                string ssumMoney = recordGroupTotle.sumMoney.ToString();
                string totleCount=recordGroupTotle.totleCount.ToString();
                this.chartControl1.Series[0].Name ="营业额:"+ ssumMoney + "\r\n 营业笔数:"+totleCount;
                table = new DataTable("Table1");
                table.Columns.Add("Name", typeof(String));
                table.Columns.Add("Value", typeof(Decimal));
                table.Columns.Add("Count", typeof(Decimal));
                table.Columns.Add("Color", typeof(Color));
                table.Columns.Add("DSA", typeof(List<v_cfmainaccount2>));
                table.Columns.Add("createdate", typeof(String));
                table_detail = new DataTable();
                table_detail = table.Clone();
                for (int i = 0; i < recordGroupBean.Count; i++)
                {
                    decimal sumMoney = recordGroupBean[i].sumMoney;
                    if (sumMoney >= 0)
                    {
                        table.Rows.Add(new object[] { recordGroupBean[i].keyName, sumMoney, recordGroupBean[i].recList.Count, GetPaletteColor(i),null,null });
                    }
                    string strdate = "";
                    try
                    {
                        strdate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(recordGroupTotle.createdate));
                    }
                    catch { }

                    table_detail.Rows.Add(new object[] { recordGroupBean[i].keyName, sumMoney, recordGroupBean[i].recList.Count, GetPaletteColor(i), translate(recordGroupBean[i].recList), strdate });
                }
                return table_detail;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return table_detail;
            }
        }
        private List<v_cfmainaccount2> translate(List<Models.v_cfmainaccount> cfmainaccount)
        {
            List<v_cfmainaccount2> list_temp= new List<v_cfmainaccount2>();
            foreach (Models.v_cfmainaccount item in cfmainaccount)
            {
                v_cfmainaccount2 temp=new v_cfmainaccount2();
                temp.tableNo = item.tableNo;
                temp.createdate = item.createdate;
                temp.billNo = item.billNo;
                temp.payway = item.payway;
                temp.money = item.money;
                temp.waiter = item.waiter;
                temp.youhui = item.youhui;
                temp.realPay = item.realPay;
                temp.cfmainkey = item.cfmainkey;
                if (item.state == 5)
                {
                    temp.str_state="异常";
                }
                else
                {
                    temp.str_state = "正常";
                }
                temp.str_type = bizBusinessAnalysis.BillType2Name(item.type);
                list_temp.Add(temp);
            }
            return list_temp;
        }
        private DataTable CreateChartTypeData()
        {
            table = new DataTable("Table1");
            table.Columns.Add("Name", typeof(String));
            table.Columns.Add("Value", typeof(Double));
            table.Columns.Add("Count", typeof(Double));
            table.Columns.Add("Color", typeof(Color));

            for (int i = 0; i < 5; i++)
            {
                table.Rows.Add(new object[] { i + "号类别", i + new Random().Next(1, 10), i + new Random().Next(10, 20), Color.White });
            }
            return table;
        }
        Color GetPaletteColor(int i)
        {
           return this.chartControl1.PaletteRepository["Chameleon"][i].Color;

        }
        private void IniPie()
        {
            try
            {
                chartControl1.Series[0].DataSource = table;
                chartControl1.Series[0].ArgumentDataMember = "Name";
                chartControl1.Series[0].ValueDataMembers[0] = "Value";
                chartControl1.Series[0].ColorDataMember = "Color";
                IniPie(chart_detail, table_detail_Pay, chartControl_MouseClick, "Chameleon");
                this.tableLayoutPanel1.Controls.Add(chart_detail, 0, 1);
                IniPie(chart_type, table_detail_Type, chartTypeControl_MouseClick, "");
                this.tableLayoutPanel1.Controls.Add(chart_type, 0, 2);
                Print.gridControl1.DataSource = table_detail_PayDate.DefaultView;
            }
            catch { }

        }
        private void IniPie(ChartControl chartControl,DataTable table,MouseEventHandler mouseEventHandler,string PaletteColorName)
        {
            try
            {
                int iCount = table.Rows.Count;
                int i = 0;
                Series[] ArraySeries = new Series[iCount];
                foreach (DataRow dr in table.Rows)
                {
                    DevExpress.XtraCharts.Series series = new DevExpress.XtraCharts.Series();
                    DevExpress.XtraCharts.PieSeriesView pieSeriesView = new DevExpress.XtraCharts.PieSeriesView();
                    DevExpress.XtraCharts.PieSeriesLabel pieSeriesLabel1 = new DevExpress.XtraCharts.PieSeriesLabel();
                    DevExpress.XtraCharts.SeriesTitle seriesTitle1 = new DevExpress.XtraCharts.SeriesTitle();
                    DevExpress.XtraCharts.PieWidenAnimation pieWidenAnimation1 = new DevExpress.XtraCharts.PieWidenAnimation();
                    //
                    ((System.ComponentModel.ISupportInitialize)(chartControl)).BeginInit();
                    ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).BeginInit();
                    ((System.ComponentModel.ISupportInitialize)(series)).BeginInit();
                    ((System.ComponentModel.ISupportInitialize)(pieSeriesView)).BeginInit();
                    //
                    chartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                    pieSeriesLabel1.BackColor = System.Drawing.Color.Transparent;
                    pieSeriesLabel1.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
                    pieSeriesLabel1.ColumnIndent = 20;
                    pieSeriesLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
                    pieSeriesLabel1.LineLength = 30;
                    pieSeriesLabel1.Position = DevExpress.XtraCharts.PieSeriesLabelPosition.Inside;
                    pieSeriesLabel1.TextPattern = "{A}\n{V}";
                    pieSeriesLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //
                    seriesTitle1.Dock = DevExpress.XtraCharts.ChartTitleDockStyle.Bottom;
                    pieSeriesView.Titles.AddRange(new DevExpress.XtraCharts.SeriesTitle[] {
                seriesTitle1});
                    pieSeriesView.Animation = pieWidenAnimation1;
                    //
                    series.Label = pieSeriesLabel1;
                    series.View = pieSeriesView;
                    series.Name = dr["Name"].ToString();
                    series.Tag = dr["DSA"];

                    DevExpress.XtraCharts.SeriesPoint seriesPoint = new DevExpress.XtraCharts.SeriesPoint(dr["Value"], dr["Count"], 0);
                    seriesPoint.Tag = dr["Name"].ToString();
                    if (PaletteColorName.Length > 0)
                        seriesPoint.ColorSerializable = ToHexColor(this.chartControl1.PaletteRepository["Chameleon"][i].Color);
                    else
                        seriesPoint.ColorSerializable = ToHexColor(Color.Wheat);
                    series.Points.Add(seriesPoint);
                    ArraySeries[i++] = series;

                    //
                    ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).EndInit();
                    ((System.ComponentModel.ISupportInitialize)(series)).EndInit();
                    ((System.ComponentModel.ISupportInitialize)(pieSeriesView)).EndInit();
                    ((System.ComponentModel.ISupportInitialize)(chartControl)).EndInit();
                    //
                }
                chartControl.AnimationStartMode = DevExpress.XtraCharts.ChartAnimationMode.OnDataChanged;
                chartControl.SelectionMode = ElementSelectionMode.Single;
                chartControl.SeriesSerializable = ArraySeries;
                chartControl.Dock = DockStyle.Fill;
                chartControl.MouseClick += mouseEventHandler;
                chartControl.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            }
            catch { }

        }
        private void chartControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (mousepoint != e.Location)
            {
                mousepoint = e.Location;
                ChartHitInfo hitInfo = chart_detail.CalcHitInfo(e.Location);
                if (hitInfo.SeriesPoint != null)
                {
                    // MessageBox.Show(hitInfo.SeriesPoint.Tag.ToString());
                    //MyForm.BusinessDetails.OrderListForm orderListForm = new MyForm.BusinessDetails.OrderListForm();
                    //orderListForm.orderListControl1.gridControl1.DataSource = dsa;
                    //orderListForm.StartPosition = FormStartPosition.CenterScreen;
                    //orderListForm.ShowDialog();
                    MyForm.BusinessDetails.OrderDatilForm orderDatil = new MyForm.BusinessDetails.OrderDatilForm(hitInfo.Series.Tag);
                    orderDatil.StartPosition = FormStartPosition.CenterScreen;
                    orderDatil.WindowState = FormWindowState.Maximized;
                    orderDatil.ShowDialog();
                }
            }
        }
        private void chartTypeControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (mousepoint != e.Location)
            {
                mousepoint = e.Location;
                ChartHitInfo hitInfo = this.chart_type.CalcHitInfo(e.Location);
                if (hitInfo.SeriesPoint != null)
                {
                    // MessageBox.Show(hitInfo.SeriesPoint.Tag.ToString());
                    //MyForm.BusinessDetails.OrderListForm orderListForm = new MyForm.BusinessDetails.OrderListForm();
                    //orderListForm.orderListControl1.gridControl1.DataSource = dsa;
                    //orderListForm.StartPosition = FormStartPosition.CenterScreen;
                    //orderListForm.ShowDialog();
                    MyForm.BusinessDetails.OrderDatilForm orderDatil = new MyForm.BusinessDetails.OrderDatilForm(hitInfo.Series.Tag);
                    orderDatil.StartPosition = FormStartPosition.CenterScreen;
                    orderDatil.WindowState = FormWindowState.Maximized;
                    orderDatil.ShowDialog();
                }
            }
        }
        private static string ToHexColor(Color color)
        {
            if (color.IsEmpty)
                return "#000000";
            string R = Convert.ToString(color.R, 16);
            if (R == "0")
                R = "00";
            string G = Convert.ToString(color.G, 16);
            if (G == "0")
                G = "00";
            string B = Convert.ToString(color.B, 16);
            if (B == "0")
                B = "00";
            string HexColor = "#" + R + G + B;
            return HexColor.ToUpper();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SeriesPointCollection seriesPoint = chartControl1.Series[0].Points;
            int aaa = seriesPoint.Count();
            string Argument = seriesPoint[1].Argument;
            double[] Values = seriesPoint[1].Values;
            // Color color = this.chartControl1.PaletteRepository
            Color color = this.chartControl1.PaletteRepository["Chameleon"][0].Color;
        }

        private void tableLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            tableLayoutPanel1.VerticalScroll.Value = 0;
        }

        private void dateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (dateEdit1.Text.Length > 0 && dateEdit2.Text.Length > 0)
            {
                sdate = Convert.ToDateTime(dateEdit1.Text);
                edate = Convert.ToDateTime(dateEdit2.Text);
                RefreshChart();
                IniPie();
            }
        }

        private void dateEdit2_EditValueChanged(object sender, EventArgs e)
        {
            if (dateEdit1.Text.Length > 0 && dateEdit2.Text.Length > 0)
            {
                sdate = Convert.ToDateTime(dateEdit1.Text);
                edate = Convert.ToDateTime(dateEdit2.Text);
                RefreshChart();
                IniPie();
            }
        }

        private void Btn_Export_Click(object sender, EventArgs e)
        {
            //MyForm.BusinessDetails.printForm printForm = new MyForm.BusinessDetails.printForm();
            //printForm.printControl21.gridControl1.DataSource = table_detail_PayDate;
            //printForm.Dock = DockStyle.Fill;
            //printForm.Show();
            ExportToXls();
        }
        public void ExportToXls()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            DialogResult dialogResult = saveFileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                Print.gridControl1.ExportToXls(saveFileDialog.FileName, options);
                DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
