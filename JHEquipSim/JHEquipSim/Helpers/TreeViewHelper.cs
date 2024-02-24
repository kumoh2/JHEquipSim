using System;
using System.IO;
using System.Windows.Forms;

namespace JHEquipSim.Helpers
{
    public static class TreeViewHelper
    {
        public static void InitializeTreeView(TreeView treeView, string rootPath)
        {
            treeView.Nodes.Clear();
            DirectoryInfo rootDir = new DirectoryInfo(rootPath);
            TreeNode rootNode = new TreeNode(rootDir.Name) { Tag = rootDir.FullName };
            treeView.Nodes.Add(rootNode);
            LoadXmlFiles(rootNode, rootDir);
            treeView.ExpandAll();
        }

        private static void LoadXmlFiles(TreeNode parentNode, DirectoryInfo dirInfo)
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

        public static string ShowDialog(string text, string caption, string defaultValue = "")
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            textBox.Text = defaultValue;
            Button confirmationButton = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmationButton.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmationButton);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmationButton;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : null;
        }

        public static void AddNewGroupFolder(TreeView treeView, string rootPath)
        {
            if (treeView.SelectedNode == null || treeView.SelectedNode.Parent != null)
            {
                MessageBox.Show("최상위 노드를 선택해주세요.", "선택 필요", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // string groupName = Microsoft.VisualBasic.Interaction.InputBox(...); 대신
            string groupName = ShowDialog("새 그룹 폴더 이름을 입력하세요:", "그룹 폴더 생성", "새 그룹");
            if (!string.IsNullOrWhiteSpace(groupName))
            {
                string newPath = ResolveFileNameConflict(treeView.SelectedNode.Tag.ToString(), groupName, true);

                if (newPath != null)
                {
                    Directory.CreateDirectory(newPath);
                    InitializeTreeView(treeView, rootPath);
                }
            }
        }

        public static void AddXmlFileToGroup(TreeView treeView, string rootPath)
        {
            if (treeView.SelectedNode == null)
            {
                MessageBox.Show("그룹 폴더 또는 XML 파일을 선택해주세요.", "선택 필요", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string folderPath = treeView.SelectedNode.Tag.ToString();
            // 선택된 노드가 XML 파일인 경우 해당 파일의 부모 디렉토리 경로를 사용
            if (Path.GetExtension(folderPath).Equals(".xml", StringComparison.OrdinalIgnoreCase))
            {
                folderPath = Path.GetDirectoryName(folderPath);
            }

            using (var fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "XML 파일 (*.xml)|*.xml";
                fileDialog.InitialDirectory = rootPath;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = fileDialog.FileName;
                    string destFileName = Path.GetFileName(selectedFilePath);
                    string destPath = ResolveFileNameConflict(folderPath, destFileName);

                    // ResolveFileNameConflict에서 null이 반환되었는지 확인
                    if (destPath != null)
                    {
                        File.Copy(selectedFilePath, destPath, true); // null이 아닐 때만 파일 복사 실행
                        InitializeTreeView(treeView, rootPath); // 트리뷰 초기화
                    }
                }
            }
        }

        private static string ResolveFileNameConflict(string folderPath, string itemName, bool isDirectory = false)
        {
            string destPath = Path.Combine(folderPath, itemName);
            string baseName = isDirectory ? itemName : Path.GetFileNameWithoutExtension(itemName);
            string extension = isDirectory ? "" : Path.GetExtension(itemName);
            int counter = 1;
            while (Directory.Exists(destPath) || (!isDirectory && File.Exists(destPath)))
            {
                string newFileName = $"{baseName}_{counter}{extension}";
                destPath = Path.Combine(folderPath, newFileName);
                counter++;
            }

            // 사용 가능한 파일 이름을 찾은 후 사용자에게 확인
            var result = MessageBox.Show($"'{itemName}' 파일이 이미 존재합니다. '{Path.GetFileName(destPath)}'(으)로 변경하시겠습니까?", "파일 이름 충돌", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                return destPath; // 사용자가 변경을 수락한 경우, 변경된 경로 반환
            }
            else
            {
                return null; // 사용자가 변경을 거부한 경우, null 반환
            }
        }

        public static void RemoveSelectedItem(TreeView treeView, string rootPath)
        {
            var selectedNode = treeView.SelectedNode;
            if (selectedNode != null && selectedNode.Tag != null && selectedNode.Parent != null)
            {
                string path = selectedNode.Tag.ToString();
                if (ConfirmDeletion(path))
                {
                    PerformDeletion(path);
                    treeView.Nodes.Remove(selectedNode);
                    InitializeTreeView(treeView, rootPath);
                }
            }
            else
            {
                MessageBox.Show("삭제할 항목을 선택해주세요.", "선택 필요", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static void RenameSelectedNode(TreeView treeView, string rootPath)
        {
            var selectedNode = treeView.SelectedNode;
            if (selectedNode != null && selectedNode.Tag != null)
            {
                string currentPath = selectedNode.Tag.ToString();
                bool isFile = File.Exists(currentPath);
                bool isDirectory = Directory.Exists(currentPath);

                // string newName = Microsoft.VisualBasic.Interaction.InputBox(...); 대신
                string newName = ShowDialog(
                    "새 이름을 입력하세요" + (isFile ? " (확장자 포함):" : ":"),
                    isFile ? "파일 이름 수정" : "폴더 이름 수정",
                    isFile ? Path.GetFileName(currentPath) : new DirectoryInfo(currentPath).Name
                );

                if (!string.IsNullOrWhiteSpace(newName))
                {
                    string newPath = Path.Combine(isFile ? Path.GetDirectoryName(currentPath) : new DirectoryInfo(currentPath).Parent.FullName, newName);
                    if (isFile) File.Move(currentPath, newPath);
                    else Directory.Move(currentPath, newPath);
                }
            }
            InitializeTreeView(treeView, rootPath);
        }

        private static bool ConfirmDeletion(string path)
        {
            string message = $"정말로 '{path}'를 삭제하시겠습니까?";
            return MessageBox.Show(message, "삭제 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        private static void PerformDeletion(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }
    }
}
