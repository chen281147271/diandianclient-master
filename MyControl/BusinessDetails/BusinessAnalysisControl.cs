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
        DataTable table;
        ToolTipController toolTipController = new ToolTipController();
        decimal sum;
        public BusinessAnalysisControl()
        {
            InitializeComponent();
            IniPie();
            ToYearStyle(dateEdit3);
        }
        private DataTable CreateChartData()
        {

            table = new DataTable("Table1");
            table.Columns.Add("Name", typeof(String));
            table.Columns.Add("Value", typeof(Decimal));
            table.Columns.Add("Count", typeof(Decimal));
            table.Columns.Add("Color", typeof(Color));
            for (int i = 0; i < 10; i++)
            {
                table.Rows.Add(new object[] { i + "号方式支付", i + new Random().Next(1, 10), i + new Random().Next(10, 20), GetPaletteColor(i) });
            }
            sum = Convert.ToDecimal(table.AsEnumerable().Sum(s => s.Field<Decimal>("Value")).ToString());
            return table;
        }
        private DataTable CreateChartData2()
        {

            table = new DataTable("Table1");
            table.Columns.Add("Name", typeof(String));
            table.Columns.Add("Value", typeof(Decimal));
            table.Columns.Add("Count", typeof(Decimal));
            table.Columns.Add("Color", typeof(Color));
            for (int i = 0; i < 3; i++)
            {
                table.Rows.Add(new object[] { i + "号会员", i + new Random().Next(1, 10), i + new Random().Next(10, 20), GetPaletteColor(i) });
            }
            sum = Convert.ToDecimal(table.AsEnumerable().Sum(s => s.Field<Decimal>("Value")).ToString());
            return table;
        }
        private DataTable CreateChartData3()
        {

            table = new DataTable("Table1");
            table.Columns.Add("Name", typeof(String));
            table.Columns.Add("Value", typeof(Decimal));
            table.Columns.Add("Count", typeof(Decimal));
            table.Columns.Add("Color", typeof(Color));
            for (int i = 0; i < 6; i++)
            {
                table.Rows.Add(new object[] { i + "号会员", i + new Random().Next(1, 10), i + new Random().Next(10, 20), GetPaletteColor(i) });
            }
            sum = Convert.ToDecimal(table.AsEnumerable().Sum(s => s.Field<Decimal>("Value")).ToString());
            return table;
        }
        private void IniPie()
        {
            chartControl1.Series[0].DataSource = CreateChartData();
            chartControl1.Series[0].ArgumentDataMember = "Name";
            chartControl1.Series[0].ValueDataMembers[0] = "Value";
            //chartControl1.Series[0].ColorDataMember = "Color";

            chartControl1.Series[1].DataSource = CreateChartData2();
            chartControl1.Series[1].ArgumentDataMember = "Name";
            chartControl1.Series[1].ValueDataMembers[0] = "Value";
            // chartControl1.Series[1].ColorDataMember = "Color";

            chartControl2.Series[0].DataSource = CreateChartData();
            chartControl2.Series[0].ArgumentDataMember = "Name";
            chartControl2.Series[0].ValueDataMembers[0] = "Value";
            //chartControl1.Series[0].ColorDataMember = "Color";

            chartControl2.Series[1].DataSource = CreateChartData2();
            chartControl2.Series[1].ArgumentDataMember = "Name";
            chartControl2.Series[1].ValueDataMembers[0] = "Value";
            // chartControl1.Series[1].ColorDataMember = "Color";

            chartControl2.Series[2].DataSource = CreateChartData2();
            chartControl2.Series[2].ArgumentDataMember = "Name";
            chartControl2.Series[2].ValueDataMembers[0] = "Value";
            // chartControl1.Series[1].ColorDataMember = "Color";

            chartControl3.Series[0].DataSource = CreateChartData3();
            chartControl3.Series[0].ArgumentDataMember = "Name";
            chartControl3.Series[0].ValueDataMembers[0] = "Value";

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
                        decimal precent = Math.Round((Values / sum * 100), 2);
                        string str = "对应用户群菜品销量\n";
                        str += hitInfo.SeriesPoint.Argument[0].ToString();
                        str += ":";
                        str += hitInfo.SeriesPoint.Values[0].ToString();
                        str += "(";
                        str += precent.ToString();
                        str += "%";
                        str += ")";
                        toolTipController.ShowHint(str, chartControl1.PointToScreen(e.Location));
                    }else if (SeriesName == "Series 2")
                    {
                        decimal Values = Convert.ToDecimal(hitInfo.SeriesPoint.Values[0].ToString());
                        decimal precent = Math.Round((Values / sum * 100), 2);
                        string str = "顾客占比\n";
                        str += hitInfo.SeriesPoint.Argument[0].ToString();
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

    }
}
