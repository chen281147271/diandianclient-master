using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
namespace DianDianClient.MyControl
{
    public partial class EditGroupControl : UserControl
    {
        private string GroupName = "";
        private GridEditorCollection gridEditors;
        public EditGroupControl()
        {
            InitializeComponent();
            InitInplaceEditors();
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
        #endregion

        #region rowHandle定义
        /// <summary>
        /// rowHandle_GroupName 分类名称
        /// </summary>
        public int rowHandle_GroupName = 0;
        /// <summary>
        /// rowHandle_GroupNo 分类编码
        /// </summary>
        public int rowHandle_GroupNo = 1;
        /// <summary>
        /// rowHandle_GroupOrder 排列顺序
        /// </summary>
        public int rowHandle_GroupOrder = 2;
        /// <summary>
        /// rowHandle_GroupName 是否有效
        /// </summary>
        public int rowHandle_GroupvalidYesNo = 3;
        /// <summary>
        /// rowHandle_GroupName 是否打印
        /// </summary>
        public int rowHandle_GroupPrintYesNo = 4;
        /// <summary>
        /// rowHandle_GroupName 是否打印
        /// </summary>
        public int rowHandle_GroupSaleYesNo = 5;

        #endregion

        #region IniData
        void InitInplaceEditors()
        {
            this.gridEditors = new GridEditorCollection();

            this.gridEditors.Add(this.repositoryItemTextEdit1, "分类名称:", GroupName);
            this.gridEditors.Add(this.repositoryItemTextEdit1, "分类编码:", "001");
            this.gridEditors.Add(this.repositoryItemTextEdit2, "排列顺序:", "121");
            this.gridEditors.Add(this.repositoryItemToggleSwitch1, "是否有效:", true);
            this.gridEditors.Add(this.repositoryItemToggleSwitch1, "是否打印:", true);
            this.gridEditors.Add(this.repositoryItemToggleSwitch1, "是否打折:", true);

            this.gridControl1.DataSource = gridEditors;
        }

        #endregion

        #region evet function
        private void btn_save_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
