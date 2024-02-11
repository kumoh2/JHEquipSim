using JHEquipSim.ServiceReference;
using System.Xml.Linq;

namespace JHEquipSim.Views
{
    public partial class singleMessage : UserControl
    {
        public singleMessage()
        {
            InitializeComponent();
            richTextBox1.Font = new Font("Consolas", 10); // 고정폭 글꼴 사용    
            richTextBox1.Text = "<example><message>Hello, WCF from .NET 8 Client!</message></example>";
            InitializeTreeView();
        }

        private void InitializeTreeView()
        {
            treeView1.Nodes.Clear();
            string rootPath = Path.Combine(Application.StartupPath, "xml");
            DirectoryInfo rootDir = new DirectoryInfo(rootPath);
            TreeNode rootNode = new TreeNode(rootDir.Name) { Tag = rootDir.FullName };
            treeView1.Nodes.Add(rootNode);
            LoadXmlFiles(rootNode, rootDir);
            treeView1.ExpandAll();
            treeView1.NodeMouseDoubleClick += TreeView1_NodeMouseDoubleClick;
        }

        private void LoadXmlFiles(TreeNode parentNode, DirectoryInfo dirInfo)
        {
            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            {
                TreeNode dirNode = new TreeNode(dir.Name) { Tag = dir.FullName };
                parentNode.Nodes.Add(dirNode);
                foreach (FileInfo file in dir.GetFiles("*.xml"))
                {
                    TreeNode fileNode = new TreeNode(file.Name) { Tag = file.FullName };
                    dirNode.Nodes.Add(fileNode);
                }
            }
        }

        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag != null && Path.GetExtension(e.Node.Tag.ToString()).Equals(".xml", StringComparison.OrdinalIgnoreCase))
            {
                DisplayXmlContent(e.Node.Tag.ToString());
            }
        }

        private void DisplayXmlContent(string filePath)
        {
            try
            {
                string xmlContent = File.ReadAllText(filePath);
                DisplayFormattedXml(xmlContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"파일 읽기 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void sendXmlButton_Click(object sender, EventArgs e)
        {
            string xmlData = richTextBox1.Text;
            DisplayFormattedXml(xmlData);

            try
            {
                var client = new XmlReceiverServiceClient();
                await client.ReceiveXmlAsync(xmlData);
                MessageBox.Show("XML 데이터가 서버로 전송되었습니다.", "전송 성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await client.CloseAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"서버로의 전송 중 오류가 발생했습니다: {ex.Message}", "전송 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayFormattedXml(string xmlData)
        {
            try
            {
                var xDocument = XDocument.Parse(xmlData);
                richTextBox1.Clear();
                richTextBox1.AppendText(xDocument.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"XML 파싱 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_remove_treexml_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Parent != null)
            {
                string path = treeView1.SelectedNode.Tag.ToString();
                if (ConfirmDeletion(path))
                {
                    PerformDeletion(path);
                    treeView1.Nodes.Remove(treeView1.SelectedNode);
                }
            }
        }

        private void btn_add_treexml_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                MessageBox.Show("폴더 또는 그룹을 선택해주세요.", "선택 필요", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (treeView1.SelectedNode.Parent == null)
            {
                CreateNewGroupFolder();
            }
            else
            {
                AddXmlFileToSelectedGroup();
            }
        }

        private void btn_rename_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Parent != null)
            {
                RenameSelectedItem();
            }
            else
            {
                MessageBox.Show("수정할 항목을 선택해주세요.", "선택 필요", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Helper methods for code simplification and reusability

        private bool ConfirmDeletion(string path)
        {
            string message = $"정말로 '{path}'를 삭제하시겠습니까?";
            return MessageBox.Show(message, "삭제 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        private void PerformDeletion(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    MessageBox.Show("파일이 삭제되었습니다.", "삭제 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                    MessageBox.Show("폴더가 삭제되었습니다.", "삭제 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"삭제 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateNewGroupFolder()
        {
            string groupName = Microsoft.VisualBasic.Interaction.InputBox("새 그룹 폴더 이름을 입력하세요:", "그룹 폴더 생성", "새 그룹");
            if (!string.IsNullOrWhiteSpace(groupName))
            {
                string newPath = Path.Combine(treeView1.SelectedNode.Tag.ToString(), groupName);
                Directory.CreateDirectory(newPath);
                InitializeTreeView();
            }
        }

        private void AddXmlFileToSelectedGroup()
        {
            using (var fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "XML 파일 (*.xml)|*.xml";
                fileDialog.InitialDirectory = Path.Combine(Application.StartupPath, "xml");

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string selectedFilePath = fileDialog.FileName;
                        string destFileName = Path.GetFileName(selectedFilePath);
                        string folderPath = GetFolderPathFromSelectedNode();
                        string destPath = ResolveFileNameConflict(folderPath, destFileName);

                        File.Copy(selectedFilePath, destPath, true);
                        InitializeTreeView();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"파일 추가 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void RenameSelectedItem()
        {
            string currentPath = treeView1.SelectedNode.Tag.ToString();
            bool isFile = File.Exists(currentPath);
            bool isDirectory = Directory.Exists(currentPath);
            string newName = Microsoft.VisualBasic.Interaction.InputBox(
                "새 이름을 입력하세요" + (isFile ? " (확장자 포함):" : ":"),
                isFile ? "파일 이름 수정" : "폴더 이름 수정",
                isFile ? Path.GetFileName(currentPath) : new DirectoryInfo(currentPath).Name
            );

            if (!string.IsNullOrWhiteSpace(newName))
            {
                try
                {
                    string newPath = Path.Combine(isFile ? Path.GetDirectoryName(currentPath) : new DirectoryInfo(currentPath).Parent.FullName, newName);
                    if (isFile) File.Move(currentPath, newPath);
                    else Directory.Move(currentPath, newPath);
                    InitializeTreeView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"이름 변경 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string GetFolderPathFromSelectedNode()
        {
            return treeView1.SelectedNode.Tag != null && Path.GetExtension(treeView1.SelectedNode.Tag.ToString()).Equals(".xml", StringComparison.OrdinalIgnoreCase)
                   ? Path.GetDirectoryName(treeView1.SelectedNode.Tag.ToString())
                   : treeView1.SelectedNode.Tag.ToString();
        }

        private string ResolveFileNameConflict(string folderPath, string fileName)
        {
            string destPath = Path.Combine(folderPath, fileName);
            int counter = 1;

            while (File.Exists(destPath))
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                string extension = Path.GetExtension(fileName);
                string newFileName = $"{fileNameWithoutExtension}_{counter}{extension}";
                destPath = Path.Combine(folderPath, newFileName);
                counter++;
            }

            return destPath;
        }
    }
}