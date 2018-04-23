using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraCharts;

namespace DianDianClient.MyControl.BusinessDetails
{
    public partial class BusinessAnalysisControl : UserControl
    {
        ToolTipController toolTipController = new ToolTipController();
        Biz.BizBusinessAnalysis BizBusiness = new Biz.BizBusinessAnalysis();
        List<Biz.BizBusinessAnalysis.StatisticBean> list = new List<Biz.BizBusinessAnalysis.StatisticBean>();
        List<Biz.BizBusinessAnalysis.StatisticMonthResult> list2=new List<Biz.BizBusinessAnalysis.StatisticMonthResult>();
        List<Biz.BizBusinessAnalysis.StatisticMonthResult> list3=new List<Biz.BizBusinessAnalysis.StatisticMonthResult>();
        Biz.BizBusinessAnalysis.StatisticGlobalBean statisticGlobalBean;
        int yearORmonth = 1;//1按年 2按月
        bool first = true;//首次启动
        decimal sum1 = 0;
        decimal sum2 = 0;
        List<Biz.BizBusinessAnalysis.StatisticActivityBean> list4;
        public int curPage = 1;
        public int pageSize = 10;
        public int allcount = 0;
        public BusinessAnalysisControl()
        {
            InitializeComponent();
            dateEdit3.Text = DateTime.Now.ToString();
            s_date.Text= DateTime.Now.ToShortDateString();
            e_date.Text = DateTime.Now.ToShortDateString();
            _sdate.Text = DateTime.Now.ToShortDateString();
            _edate.Text = DateTime.Now.ToShortDateString();
            //IniPie();
            //InichartControl1();
            ToYearStyle(dateEdit3);
            first = false;

            list4 = BizBusiness.QueryStatisticCoupons();
            inigrid();
        }
        public class _Chart
        {
            public string Name { get; set; }
            public decimal Value { get; set; }
        }
        public class _Chart2
        {
            public string Name { get; set; }
            public decimal Value { get; set; }
            public int iorder { get; set; }
        }
        private object CreateChartData()
        {
            List<_Chart> chart1 = new List<_Chart>();
            chart1.Clear();
            foreach (var a in list)
            {
                _Chart chart = new _Chart();
                string name = a.itemname + "(" + a.name + ")";
                Decimal Value = a.sum;
                chart.Name = name;
                chart.Value = Value;
                chart1.Add(chart);
            }
            sum1 = chart1.Sum(o => o.Value);
            return chart1;
        }
        private object CreateChartData2()
        {
            List<_Chart> chart2 = new List<_Chart>();
            chart2.Clear();
            var b = from p in list
                    group p by p.name into g
                    select new
                    {
                        g.Key,
                        sum_cnt = g.Sum(p => p.cnt)
                    };

            foreach (var a in b)
            {
                _Chart chart = new _Chart();
                string name =  a.Key ;
                Decimal Value = a.sum_cnt;
                chart.Name = name;
                chart.Value = Value;
                chart2.Add(chart);
            }
            sum2 = chart2.Sum(o => o.Value);
            return chart2;
        }
        private object CreateChartData(int type)
        {
            List<_Chart2> _list = new List<_Chart2>();
            _list.Clear();
            List<Biz.BizBusinessAnalysis.StatisticMonthResult> temp = new List<Biz.BizBusinessAnalysis.StatisticMonthResult>();
            temp = (yearORmonth == 1) ? list2 : list3;
            foreach (var a in temp.Where(o=>o.type== type))
            {
                _Chart2 chart = new _Chart2();
                string name = a.month.ToString();
                string day = a.day.ToString();
                Decimal Value = a.summoney;
                string _name = (yearORmonth == 1) ? a.name.ToString() : a.day.ToString();
                chart.Name =(yearORmonth==1)? name: day;
                chart.Value = Value;
                _list.Add(chart);
            }
            if (yearORmonth == 1)
            {
                for (int i = 1; i < 13; i++)
                {
                    var a = _list.Find(o => o.Name == i.ToString());
                    if (a != null)
                    {
                        a.iorder = Convert.ToInt32(a.Name);
                        a.Name += "月份";
                    }
                    else
                    {
                        _Chart2 chart = new _Chart2();
                        string name = i.ToString();
                        Decimal Value = 0;
                        chart.Name = name + "月份";
                        chart.Value = Value;
                        chart.iorder = i;
                        _list.Add(chart);
                    }
                }
            }
            else
            {
                for (int i = 1; i < 32; i++)
                {
                    var a = _list.Find(o => o.Name == i.ToString());
                    if (a != null)
                    {
                        a.iorder = Convert.ToInt32(a.Name);
                    }
                    else
                    {
                        _Chart2 chart = new _Chart2();
                        string name = i.ToString();
                        Decimal Value = 0;
                        chart.Name = name;
                        chart.Value = Value;
                        chart.iorder = i;
                        _list.Add(chart);
                    }
                }

                }
            return _list.OrderBy(o=>o.iorder);
        }
        private List<_Chart> CreateChartData3(Biz.BizBusinessAnalysis.StatisticGlobalBean statisticGlobalBean)
        {
            List<_Chart> chart1 = new List<_Chart>();
            chart1.Clear();
            string name="";
            decimal vaule=0;
            for (int i = 0; i < 6; i++)
            {
                _Chart chart = new _Chart();
                switch (i)
                {
                    case 0:
                        name = "日均客流量";
                        vaule =Convert.ToDecimal(statisticGlobalBean.avgdaypeople);
                        break;
                    case 1:
                        name = "人均价";
                        vaule = Convert.ToDecimal(statisticGlobalBean.avgsummoney);
                        break;
                    case 2:
                        name = "菜品数量";
                        vaule = Convert.ToDecimal(statisticGlobalBean.itemNum);
                        break;
                    case 3:
                        name = "会员占比";
                        vaule = Convert.ToDecimal(statisticGlobalBean.memuserpercent);
                        break;
                    case 4:
                        name = "老顾客占比";
                        vaule = Convert.ToDecimal(statisticGlobalBean.olduserpercent);
                        break;
                    case 5:
                        name = "进店周期";
                        vaule = Convert.ToDecimal(statisticGlobalBean.periodnum);
                        break;
                }
                chart.Name = name;
                chart.Value = vaule;
                chart1.Add(chart);
            }
            return chart1;
        }
        private void InichartControl3()
        {
            //chartControl3.Series[0].DataSource
            statisticGlobalBean = BizBusiness.getStatisticGlabolInfo(_sdate.DateTime, _edate.DateTime);
            chartControl3.Series[0].Points.Clear();
            foreach (var a in CreateChartData3(statisticGlobalBean))
            {
                string name = a.Name;
                decimal vaule = a.Value;
                DevExpress.XtraCharts.SeriesPoint seriesPoint = new DevExpress.XtraCharts.SeriesPoint(name, new object[] {
            vaule});
                chartControl3.Series[0].Points.Add(seriesPoint);
            }

        }
        private void InichartControl2()
        {
            if (yearORmonth == 1)
            {
                list2 = BizBusiness.getStatisticLineInfo(this.dateEdit3.DateTime.Year, -1);
            }
            else
            {
                list3 = BizBusiness.getStatisticLineInfo(this.dateEdit3.DateTime.Year, this.dateEdit3.DateTime.Month);
            }

            chartControl2.Series[0].DataSource = CreateChartData(1);
            chartControl2.Series[0].ArgumentDataMember = "Name";
            chartControl2.Series[0].ValueDataMembers[0] = "Value";
            chartControl2.Series[0].Name = "新顾客";
            //chartControl1.Series[0].ColorDataMember = "Color";

            chartControl2.Series[1].DataSource = CreateChartData(2);
            chartControl2.Series[1].ArgumentDataMember = "Name";
            chartControl2.Series[1].ValueDataMembers[0] = "Value";
            chartControl2.Series[1].Name = "老顾客";
            // chartControl1.Series[1].ColorDataMember = "Color";

            chartControl2.Series[2].DataSource = CreateChartData(3);
            chartControl2.Series[2].ArgumentDataMember = "Name";
            chartControl2.Series[2].ValueDataMembers[0] = "Value";
            chartControl2.Series[2].Name = "会员";
            // chartControl1.Series[1].ColorDataMember = "Color";
        }
        Color GetPaletteColor(int i)
        {
            return this.chartControl1.PaletteRepository["Chameleon"][i].Color;

        }
        void ToYearStyle(DevExpress.XtraEditors.DateEdit dateEdit, bool touchUI = false)
        {
            if (touchUI)
            {
                dateEdit.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
            }
            else
                dateEdit.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dateEdit.Properties.ShowToday = false;
            dateEdit.Properties.ShowMonthHeaders = false;
            dateEdit.Properties.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.YearsGroupView;
            dateEdit.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView;
            dateEdit.Properties.Mask.EditMask = "yyyy";
            dateEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        private void chartControl1_MouseMove(object sender, MouseEventArgs e)
        {

            ChartHitInfo hitInfo = chartControl1.CalcHitInfo(e.Location);
            StringBuilder builder = new StringBuilder();
            if (hitInfo.SeriesPoint != null)
            {
                if (hitInfo.InSeries)
                {
                    string SeriesName = ((Series)hitInfo.Series).Name;
                    if (SeriesName == "Series 1")
                    {
                        decimal Values = Convert.ToDecimal(hitInfo.SeriesPoint.Values[0].ToString());
                        decimal precent = Math.Round((Values / sum1 * 100), 2);
                        string str = "对应用户群菜品销量\n";
                        str += hitInfo.SeriesPoint.Argument.ToString();
                        str += ":";
                        str += hitInfo.SeriesPoint.Values[0].ToString();
                        str += "(";
                        str += precent.ToString();
                        str += "%";
                        str += ")";
                        toolTipController.ShowHint(str, chartControl1.PointToScreen(e.Location));
                    }
                    else if (SeriesName == "Series 2")
                    {
                        decimal Values = Convert.ToDecimal(hitInfo.SeriesPoint.Values[0].ToString());
                        decimal precent = Math.Round((Values / sum2 * 100), 2);
                        string str = "顾客占比\n";
                        str += hitInfo.SeriesPoint.Argument.ToString();
                        str += ":";
                        str += hitInfo.SeriesPoint.Values[0].ToString();
                        str += "(";
                        str += precent.ToString();
                        str += "%";
                        str += ")";
                        toolTipController.ShowHint(str, chartControl1.PointToScreen(e.Location));
                    }
                }
            }
        }

        private void chartControl1_MouseLeave(object sender, EventArgs e)
        {
            toolTipController.HideHint();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //IniPie();
        }

        private void btn_year_Click(object sender, EventArgs e)
        {
            yearORmonth = 1;
            ToYearStyle(dateEdit3);
            InichartControl2();
        }

        private void btn_month_Click(object sender, EventArgs e)
        {
            yearORmonth = 2;
            this.dateEdit3.Properties.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.YearView;
            this.dateEdit3.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            this.dateEdit3.Properties.Mask.EditMask = "yyyy-MM";
            this.dateEdit3.Properties.EditFormat.FormatString = "yyyy-MM";
            InichartControl2();

        }

        private void dateEdit3_EditValueChanged(object sender, EventArgs e)
        {
            InichartControl2();
        }
        private void InichartControl1()
        {
            if (e_date.DateTime >= s_date.DateTime)
            {
                list = BizBusiness.getStatisticInfo(s_date.DateTime, e_date.DateTime);
            }
            else
            {
                return;
            }
            chartControl1.Series[0].DataSource = CreateChartData();
            chartControl1.Series[0].ArgumentDataMember = "Name";
            chartControl1.Series[0].ValueDataMembers[0] = "Value";
            //chartControl1.Series[0].ColorDataMember = "Color";

            chartControl1.Series[1].DataSource = CreateChartData2();
            chartControl1.Series[1].ArgumentDataMember = "Name";
            chartControl1.Series[1].ValueDataMembers[0] = "Value";
            // chartControl1.Series[1].ColorDataMember = "Color";
        }
        private void sdate_EditValueChanged(object sender, EventArgs e)
        {
            if (!first)
            {
                InichartControl1();
            }
        }

        private void edate_EditValueChanged(object sender, EventArgs e)
        {
            if (!first)
            {
                InichartControl1();
            }
        }

        private void _sdate_EditValueChanged(object sender, EventArgs e)
        {
            if (!first)
            {
                InichartControl3();
            }
        }

        private void _edate_EditValueChanged(object sender, EventArgs e)
        {
            if (!first)
            {
                InichartControl3();
            }
        }

        private void chartControl3_MouseLeave(object sender, EventArgs e)
        {
            toolTipController.HideHint();
        }

        private void chartControl3_MouseMove(object sender, MouseEventArgs e)
        {
            ChartHitInfo hitInfo = chartControl3.CalcHitInfo(e.Location);
            StringBuilder builder = new StringBuilder();
            string str = "多角度分析图\n";
            if (hitInfo.SeriesPoint != null)
            {
                if (hitInfo.InSeries)
                {
                    DevExpress.XtraCharts.SeriesPointCollection seriesPoint = ((Series)hitInfo.Series).Points;
                    for(int i = 0; i < seriesPoint.Count; i++)
                    {
                        str += seriesPoint[i].Argument;
                        str += ":";
                        str += seriesPoint[i].Values[0].ToString();
                        str += "\n";
                    }
                    toolTipController.ShowHint(str, chartControl3.PointToScreen(e.Location));
                }
            }
        }
        private void inigrid()
        {
            this.gridControl1.DataSource = (list4).Take(10);
            this.gridView1.RowHeight = 50;
            this.gridView1.ColumnPanelRowHeight = 50;

            foreach (DevExpress.XtraGrid.Columns.GridColumn gc in this.gridView1.Columns)
            {
                gc.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                gc.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
            //分页
            mgncPager1.myPagerEvents += MyPagerEvents; //new MgncPager.MyPagerEvents(MyPagerEvents);
            mgncPager1.exportEvents += ExportEvents;// new MgncPager.ExportEvents(ExportEvents);
            //必须更新allcount！！！！！！！！！！！！！！！！！！！
            allcount = list4.Count;
            mgncPager1.RefreshPager(pageSize, allcount, curPage);//更新分页控件显示。
        }
        #region MgncPager 实现
        /// <summary>
        /// 导出EXCEL
        /// </summary>
        /// <param name="singlePage"></param>
        public void ExportEvents(bool singlePage)//单页，所有      
        {
            //导出GridControl代码写在这。
            if (singlePage)
            {
                ExportToXls();
            }
            else
            {
                BindGrid(false);
                ExportToXls();
                RefreshGridList();
            }
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
                gridControl1.ExportToXls(saveFileDialog.FileName, options);
                DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //分页相关
        public void RefreshGridList()
        {
            FillGridListCtrlQuery(curPage);
        }

        private void FillGridListCtrlQuery(int curPage = 1)   //更新控件
        {
            BindGrid();
            mgncPager1.RefreshPager(pageSize, allcount, curPage);//更新分页控件显示。
        }
        private void BindGrid(bool singlePage = true)//单页，所有     
        {
            list4 = BizBusiness.QueryStatisticCoupons();
            var q = (list4);
            if (singlePage)
            {
                this.gridControl1.DataSource = (q.Skip((curPage - 1) * pageSize).Take(pageSize)).ToList();
            }
            else
            {
                this.gridControl1.DataSource = q.ToList();
            }
            this.gridView1.FocusedRowHandle = 0;
            this.allcount = q.Count();
        }

        private void MyPagerEvents(int curPage, int pageSize)
        {
            this.curPage = curPage;
            this.pageSize = pageSize;
            FillGridListCtrlQuery(curPage);

        }
        /// <summary>
        /// 搜索按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_query_Click(object sender, EventArgs e)
        {
            this.curPage = 1;
            RefreshGridList();
        }
        #endregion

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if(e.Column.Name== "ifmoney")
            {
                string okjian = this.gridView1.GetRowCellValue(e.ListSourceRowIndex, "okjian").ToString();
                string ifmoney = this.gridView1.GetRowCellValue(e.ListSourceRowIndex, "ifmoney").ToString();
                string str = "满" + ifmoney + "减" + okjian;
                e.DisplayText = str;
            }
        }
    }
}
