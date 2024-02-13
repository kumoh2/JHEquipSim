using JHEquipSim.Helpers;
using JHEquipSim.ServiceReference;
using System.Xml.Linq;

namespace JHEquipSim.Views
{
    public partial class scenarioMessage : UserControl
    {
        string rootPath = Path.Combine(Application.StartupPath, "xml");

        public scenarioMessage()
        {
            InitializeComponent();
            TreeViewHelper.InitializeTreeView(treeView1, rootPath);
        }

        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
        }

        private async void sendXmlButton_Click(object sender, EventArgs e)
        {
        }

        private void btn_add_treexml_Click(object sender, EventArgs e)
        {
            // 새 그룹 폴더 추가 또는 선택된 그룹에 XML 파일 추가
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Parent == null)
            {
                TreeViewHelper.AddNewGroupFolder(treeView1, rootPath);
            }
            else
            {
                TreeViewHelper.AddXmlFileToGroup(treeView1, rootPath);
            }
        }

        private void btn_remove_treexml_Click(object sender, EventArgs e)
        {
            TreeViewHelper.RemoveSelectedItem(treeView1, rootPath);
        }

        private void btn_rename_Click(object sender, EventArgs e)
        {
            TreeViewHelper.RenameSelectedNode(treeView1, rootPath);
        }
    }
}