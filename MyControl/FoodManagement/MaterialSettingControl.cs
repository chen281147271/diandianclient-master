﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DianDianClient.MyControl.FoodManagement
{
    public partial class MaterialSetting : UserControl
    {
        public string strSelected = ""; //选择了什么

        public int curPage = 1;
        public int pageSize = 10;
        public int allcount = 0;
        DataTable dt;
        public List<Models.v_item_crude> list_itemcrude;
        Biz.BizStorage BizStorage = new Biz.BizStorage();
        List<Models.v_crude_genre> list;
        public delegate void MyDelegate();
        public event MyDelegate MyEvent;
        public delegate void _MyDelegate();
        public event _MyDelegate _MyEvent;
        public MaterialSetting(List<Models.v_item_crude> list_itemcrude)
        {
            this.list_itemcrude = list_itemcrude;
            InitializeComponent();
            IniData();
        }
        #region IniData
        private void IniData()
        {
            //Demo 数据 字段名请不要改变
            dt = new DataTable();
            dt.Columns.Add("IsCheck", typeof(Boolean));
            dt.Columns.Add("MaterialName", typeof(String));
            dt.Columns.Add("MaterialType", typeof(String));
            dt.Columns.Add("MaterialNumber", typeof(int));
            dt.Columns.Add("crudeid", typeof(int));
            //for (int i = 0; i < 200; i++)
            //{
            //    string strprice = "¥" + i + new Random().Next(1, 10);
            //    string strNumber = "菜名" + i;
            //    int MaterialNumber = i;
            //    dt.Rows.Add(new object[] { false, strNumber, strprice, MaterialNumber });
            //}
            list = BizStorage.QueryCrude("", 0, "", "", 0);
            foreach(var a in list)
            {
                Models.v_item_crude v_Item_Crude = null;
                if (list_itemcrude != null)
                {
                    v_Item_Crude = list_itemcrude.Where(o => o.crudeid.Value == a.crudeid).FirstOrDefault();
                }
                bool IsCheck = false;
                int MaterialNumber = 0;
                if (v_Item_Crude != null)
                {
                    IsCheck = true;
                    MaterialNumber = v_Item_Crude.num.Value;
                }
                dt.Rows.Add(new object[] { IsCheck, a.crudename, a.genrename, MaterialNumber,a.crudeid });
            }
            gridView1.RowHeight = 70;
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
            //分页
            mgncPager1.myPagerEvents += MyPagerEvents; //new MgncPager.MyPagerEvents(MyPagerEvents);
            mgncPager1.exportEvents += ExportEvents;// new MgncPager.ExportEvents(ExportEvents);
            //必须更新allcount！！！！！！！！！！！！！！！！！！！
            allcount = dt.Rows.Count;
            mgncPager1.RefreshPager(pageSize, allcount, curPage);//更新分页控件显示。
        }
        #endregion

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
            var q = from tb in dt.AsEnumerable()
                    where tb.Field<string>("MaterialName").Contains(this.textEdit1.Text) &&
                    tb.Field<string>("MaterialType").Contains(this.textEdit2.Text)
                    select new
                    {
                        MaterialName = tb.Field<string>("MaterialName"),
                        MaterialType = tb.Field<string>("MaterialType"),
                        IsCheck = tb.Field<Boolean>("IsCheck"),
                        MaterialNumber = tb.Field<int>("MaterialNumber"),
                        crudeid = tb.Field<int>("crudeid")
                    };
            if (singlePage)
            {
                this.gridControl1.DataSource = Utils.utils.CopyToDataTable(q.Skip((curPage - 1) * pageSize).Take(pageSize).ToList());
            }
            else
            {
                this.gridControl1.DataSource = Utils.utils.CopyToDataTable(q.ToList());
            }
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
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.curPage = 1;
            RefreshGridList();
        }
        #endregion

        #region event function 
        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string value = "";
            //for (int i = 0; i < gridView1.RowCount; i++)
            //{   //   获取选中行的check的值         
            //    value = gridView1.GetDataRow(i)["IsCheck"].ToString();
            //    if (value == "True")
            //    {               
            //        strSelected += gridView1.GetRowCellValue(i, "MaterialName");
            //        strSelected += ",";

            //        int crudeid=Convert.ToInt32( gridView1.GetRowCellValue(i, "crudeid"));
            //        int num= Convert.ToInt32(gridView1.GetRowCellValue(i, "MaterialNumber"));
            //        // List<Models.v_item_crude> list_itemcrude
            //        this.list_itemcrude.Clear();
            //    }
            //    if (strSelected.Length > 0)
            //    {
            //        strSelected = strSelected.Substring(0, strSelected.Length - 1);
            //    }
            //}
            if (this.list_itemcrude != null)
            {
                this.list_itemcrude.Clear();
            }
            else
            {
                this.list_itemcrude = new List<Models.v_item_crude>();
            }
            foreach (DataRow dr in dt.Rows)
            {
                value = dr["IsCheck"].ToString();
                if (value == "True")
                {
                    strSelected += dr["MaterialName"].ToString();
                    strSelected += ",";

                    int crudeid = Convert.ToInt32(dr["crudeid"]);
                    int num = Convert.ToInt32(dr["MaterialNumber"]);
                    Models.v_item_crude v_Item_Crude = new Models.v_item_crude();
                    v_Item_Crude.crudeid = crudeid;
                    v_Item_Crude.num = num;
                    this.list_itemcrude.Add(v_Item_Crude);
                }
            }
            if (strSelected.Length > 0)
            {
                strSelected = strSelected.Substring(0, strSelected.Length - 1);
            }
            MyEvent();
        }
        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            this.curPage = 1;
            RefreshGridList();
        }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {
            this.curPage = 1;
            RefreshGridList();
        }
        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            int crudeid = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "crudeid"));
            bool IsCheck = Convert.ToBoolean(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "IsCheck"));
            var a=dt.AsEnumerable().Where(o => o.Field<int>("crudeid") == crudeid).FirstOrDefault();
            a.SetField("IsCheck", !IsCheck);
        }


        #endregion

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this._MyEvent();
        }
    }
}
