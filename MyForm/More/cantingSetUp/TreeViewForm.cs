using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyForm.More.cantingSetUp
{
    public partial class TreeViewForm : Form
    {
        public string strSelected;
        public int dqcode;
        List<Models.dd_areas> list;
        Biz.BizSPInfoController BizSPInfo = new Biz.BizSPInfoController();
        public TreeViewForm()
        {
            InitializeComponent();
            list = BizSPInfo.QueryAreas();
            setTreeView();
        }
        private void setTreeView()
        {
            if (list.Count > 0)
            {
                int index = 0;
                foreach (var row in list.Where(o=>o.dqjibie=="2"))
                {
                    TreeNode node = new TreeNode();
                    node.Text = row.dqname;
                    node.Tag = row.dqcode;
                    this.treeView1.Nodes.Add(node);
                    foreach (var a in list.Where(o => o.fdqcode == row.dqcode))
                    {
                        TreeNode _node = new TreeNode();
                        _node.Text = a.dqname;
                        _node.Tag = row.dqcode;
                        this.treeView1.Nodes[index].Nodes.Add(_node);
                    }
                    index++;

                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.textEdit1.Text = e.Node.Text;
            this.dqcode = Convert.ToInt32(e.Node.Tag);
            this.strSelected = e.Node.Text;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
