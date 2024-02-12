using JHEquipSim.Helpers;
using JHEquipSim.ServiceReference;
using System.Xml.Linq;

namespace JHEquipSim.Views
{
    public partial class singleMessage : UserControl
    {
        string rootPath = Path.Combine(Application.StartupPath, "xml");

        public singleMessage()
        {
            InitializeComponent();
            richTextBox1.Font = new Font("Consolas", 10);
            richTextBox1.Text = "<example><message>Hello, WCF from .NET 8 Client!</message></example>";
            TreeViewHelper.InitializeTreeView(treeView1, rootPath);
        }

        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag != null && Path.GetExtension(e.Node.Tag.ToString()).Equals(".xml", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    // XDocument를 사용하여 XML 파일을 불러오고 서식을 지정합니다.
                    var xDocument = XDocument.Load(e.Node.Tag.ToString());
                    string formattedXml = xDocument.ToString();

                    // RichTextBox에 서식이 지정된 XML 문자열을 표시합니다.
                    richTextBox1.Text = formattedXml;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"XML 파일을 불러오는 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void sendXmlButton_Click(object sender, EventArgs e)
        {
            try
            {
                var client = new XmlReceiverServiceClient();
                await client.ReceiveXmlAsync(richTextBox1.Text);
                MessageBox.Show("XML 데이터가 서버로 전송되었습니다.", "전송 성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await client.CloseAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"서버로의 전송 중 오류가 발생했습니다: {ex.Message}", "전송 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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