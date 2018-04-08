using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using System.Collections;
using DianDianClient.Utils;
namespace DianDianClient.MyControl.More.cantingSetUp
{
    public partial class QuanXianControl : UserControl
    {
        private GridEditorCollection gridEditors;
        public QuanXianControl()
        {
            InitializeComponent();
            gridView1.OptionsMenu.ShowAutoFilterRowItem = false;
            IniData();
        }
        #region UI



        public class GridEditorItem
        {
            string fName;
            object fValue;
            RepositoryItem fRepositoryItem;

            public GridEditorItem(RepositoryItem fRepositoryItem, string fName, object fValue)
            {
                this.fRepositoryItem = fRepositoryItem;
                this.fName = fName;
                this.fValue = fValue;
            }
            public string Name { get { return this.fName; } }
            public object Value { get { return this.fValue; } set { this.fValue = value; } }
            public RepositoryItem RepositoryItem { get { return this.fRepositoryItem; } }
        }
        class GridEditorCollection : ArrayList
        {
            public GridEditorCollection()
            {
            }
            public new GridEditorItem this[int index] { get { return base[index] as GridEditorItem; } }
            public void Add(RepositoryItem fRepositoryItem, string fName, object fValue)
            {
                base.Add(new GridEditorItem(fRepositoryItem, fName, fValue));
            }
        }

        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == this.gridEditorValue)
            {
                GridEditorItem item = gridView1.GetRow(e.RowHandle) as GridEditorItem;
                if (item != null) e.RepositoryItem = item.RepositoryItem;
            }
        }
        private void repositoryItemPictureEdit1_MouseDown(object sender, MouseEventArgs e)
        {
            Io_Api ia = new Io_Api();
            ia.mouse_click("R");
        }
        #endregion
        #region IniData
        void IniData()
        {
            InitInplaceEditors();
        }
        void InitInplaceEditors()
        {
            //设置Demo 从数据库取值后这样设置
            //this.gridView1.SetRowCellValue(rowHandle_CommodityName, "Value","hahhahahahah");

            this.gridEditors = new GridEditorCollection();

            this.gridEditors.Add(this.repositoryItemRadioGroup1, "是否开启定位:", true);
            this.gridEditors.Add(this.repositoryItemRadioGroup1, "是否开启锁桌:", true);
            this.gridEditors.Add(this.repositoryItemRadioGroup1, "是否在线支付:", true);
            this.gridEditors.Add(this.repositoryItemRadioGroup1, "是否买单清桌:", true);
            this.gridEditors.Add(this.repositoryItemRadioGroup1, "未接订单提醒:", true);
            this.gridEditors.Add(this.repositoryItemRadioGroup1, "开启呼叫服务:", true);
            this.gridEditors.Add(this.repositoryItemToggleSwitch1, "关联美团外卖:", true);
            this.gridEditors.Add(this.repositoryItemToggleSwitch2, "关联饿了么外卖:", true);
            //
            this.gridControl1.DataSource = gridEditors;
        }
        #endregion

        private void repositoryItemToggleSwitch1_EditValueChanged(object sender, EventArgs e)
        {  
            System.Diagnostics.Process.Start("explorer.exe", "https://www.baidu.com/");
            //根据具体操作 设置值
            this.gridView1.SetRowCellValue(6, "Value", false);
        }

        private void repositoryItemToggleSwitch2_EditValueChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://www.baidu.com/");
            //根据具体操作 设置值
            this.gridView1.SetRowCellValue(7, "Value", false);
        }
    }
}
