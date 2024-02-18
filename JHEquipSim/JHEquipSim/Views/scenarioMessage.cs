using JHEquipSim.Helpers;
using JHEquipSim.ServiceReference;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JHEquipSim.Views
{
    public partial class scenarioMessage : UserControl
    {
        string xmlPath = Path.Combine(Application.StartupPath, "xml");
        string scenarioPath = Path.Combine(Application.StartupPath, "scenario");

        public scenarioMessage()
        {
            InitializeComponent();
            InitializeDataGridView();
            TreeViewHelper.InitializeTreeView(treeView1, xmlPath);
            InitializeComboBox(scenarioPath);
        }

        private void InitializeDataGridView()
        {
            // Add two columns to the DataGridView: 'FileName' and 'FilePath'
            dataGridView1.Columns.Add("FileName", "File Name");
            dataGridView1.Columns.Add("FilePath", "File Path");
            dataGridView1.Columns.Add("RepeatCount", "Repeat Count");

            // Optional: customize columns, such as setting widths or formatting
            dataGridView1.Columns["FileName"].Width = 200; // Set column width
            dataGridView1.Columns["FilePath"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Fill the remaining space
            dataGridView1.Columns["FilePath"].Visible = false;
            dataGridView1.Columns["RepeatCount"].Width = 100; // 열 너비 설정
        }

        private void InitializeComboBox(string scenarioFolderPath)
        {
            try
            {
                // 'scenario' 폴더 내의 모든 하위 폴더를 가져옵니다.
                string[] subDirectories = Directory.GetDirectories(scenarioFolderPath);

                foreach (string subDirectory in subDirectories)
                {
                    string folderName = Path.GetFileName(subDirectory); // 전체 경로에서 폴더 이름만 추출
                    comboBox1.Items.Add(folderName); // 콤보박스에 폴더 이름 추가
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"폴더를 읽는 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // 콤보박스에서 아이템이 선택되었는지 확인
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("먼저 시나리오 폴더를 선택해주세요.", "선택 필요", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // 콤보박스에서 아무것도 선택되지 않았으면 여기서 함수 종료
            }
            if (e.Node.Tag != null && Path.GetExtension(e.Node.Tag.ToString()).Equals(".xml", StringComparison.OrdinalIgnoreCase))
            {
                string sourceFilePath = e.Node.Tag.ToString();
                string destinationFilePath = Path.Combine(scenarioPath, comboBox1.SelectedItem.ToString(), Path.GetFileName(sourceFilePath));

                // 파일을 시나리오 폴더로 복사
                File.Copy(sourceFilePath, destinationFilePath, true);

                // scenario.xml 파일 업데이트
                UpdateScenarioConfigFile(destinationFilePath);

                // 리스트뷰 새로 고침
                LoadScenarioFileToListBox(Path.Combine(scenarioPath, comboBox1.SelectedItem.ToString(), "scenario.xml"));
            }
        }

        private void UpdateScenarioConfigFile(string newFilePath)
        {
            string scenarioConfigPath = Path.Combine(scenarioPath, comboBox1.SelectedItem.ToString(), "scenario.xml");

            XDocument scenarioDoc;
            if (File.Exists(scenarioConfigPath))
            {
                scenarioDoc = XDocument.Load(scenarioConfigPath);
            }
            else
            {
                scenarioDoc = new XDocument(new XElement("SimulationConfig", new XElement("Messages")));
                CreateNewScenarioFile(scenarioConfigPath);
            }

            XElement messagesElement = scenarioDoc.Element("SimulationConfig").Element("Messages");

            XElement newMessageElement = new XElement("Message",
                new XElement("RepeatCount", "1"), // 'RepeatCount'의 기본값을 1로 설정
                new XElement("Path", newFilePath),
                new XElement("Parameters")
            // 여기에 필요한 파라미터를 추가할 수 있습니다.
            );

            messagesElement.Add(newMessageElement);
            scenarioDoc.Save(scenarioConfigPath);
        }

        private void CreateNewScenarioFile(string filePath)
        {
            // 새 scenario.xml 파일의 기본 구조
            string defaultContent = "<SimulationConfig>\n\t<Messages>\n\t\t<!-- Add your messages here -->\n\t</Messages>\n</SimulationConfig>";
            File.WriteAllText(filePath, defaultContent);
        }

        private async void sendXmlButton_Click(object sender, EventArgs e)
        {
        }

        private void btn_add_treexml_Click(object sender, EventArgs e)
        {
            // 새 그룹 폴더 추가 또는 선택된 그룹에 XML 파일 추가
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Parent == null)
            {
                TreeViewHelper.AddNewGroupFolder(treeView1, xmlPath);
            }
            else
            {
                TreeViewHelper.AddXmlFileToGroup(treeView1, xmlPath);
            }
        }

        private void btn_remove_treexml_Click(object sender, EventArgs e)
        {
            TreeViewHelper.RemoveSelectedItem(treeView1, xmlPath);
        }

        private void btn_rename_Click(object sender, EventArgs e)
        {
            TreeViewHelper.RenameSelectedNode(treeView1, xmlPath);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFolder = comboBox1.SelectedItem.ToString();
            string scenarioFilePath = Path.Combine(scenarioPath, selectedFolder, "scenario.xml");
            if (!File.Exists(scenarioFilePath))
            {
                // scenario.xml 파일이 없는 경우, 새로 생성
                CreateNewScenarioFile(scenarioFilePath);
            }

            // scenario.xml 파일 내용을 읽고 리스트박스에 항목 추가
            LoadScenarioFileToListBox(scenarioFilePath);
        }

        private void LoadScenarioFileToListBox(string filePath)
        {
            dataGridView1.Rows.Clear();

            XDocument scenarioDoc = XDocument.Load(filePath);

            // scenario.xml 파일 내의 각 'Message' 요소를 리스트뷰에 추가
            foreach (var message in scenarioDoc.Descendants("Message"))
            {
                string messagePath = message.Element("Path").Value; // 'Path' 요소의 값을 가져옴
                string fileName = Path.GetFileName(messagePath); // 전체 경로에서 파일 이름만 추출
                string repeatCount = message.Element("RepeatCount").Value; 

                // 파일 이름, 파일 경로, 반복 횟수를 DataGridView에 추가합니다.
                dataGridView1.Rows.Add(fileName, messagePath, repeatCount);
            }
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.ClearSelection(); // 기존의 선택을 모두 지웁니다.
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected = true; // 마지막 행을 선택합니다.
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1; // 마지막 행이 화면에 보이도록 스크롤을 조정합니다.
            }
        }

        private void remove_step_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("정말로 이 항목을 삭제하시겠습니까?", "항목 삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int selectedIndex = dataGridView1.SelectedRows[0].Index;
                    if (selectedIndex < dataGridView1.Rows.Count - 1) // 실제 데이터 행만 처리합니다.
                    {
                        string selectedFilePath = dataGridView1.Rows[selectedIndex].Cells["FilePath"].Value.ToString(); // 'FilePath' 셀에서 파일 경로 가져오기

                        RemoveMessageFromXml(selectedFilePath);
                        LoadScenarioFileToListBox(Path.Combine(scenarioPath, comboBox1.SelectedItem.ToString(), "scenario.xml"));
                    }
                }
            }
            else
            {
                MessageBox.Show("삭제할 항목을 먼저 선택하세요.", "선택 필요", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RemoveMessageFromXml(string filePath)
        {
            string scenarioConfigPath = Path.Combine(scenarioPath, comboBox1.SelectedItem.ToString(), "scenario.xml");
            XDocument scenarioDoc = XDocument.Load(scenarioConfigPath);

            var messages = scenarioDoc.Descendants("Message").Where(m => m.Element("Path").Value.Equals(filePath)).ToList();

            foreach (var message in messages)
            {
                message.Remove();
            }

            scenarioDoc.Save(scenarioConfigPath);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {    // 'RepeatCount' 열에서 편집이 발생한 경우에만 처리
            if (dataGridView1.Columns[e.ColumnIndex].Name == "RepeatCount")
            {
                string filePath = dataGridView1.Rows[e.RowIndex].Cells["FilePath"].Value.ToString();
                string repeatCount = dataGridView1.Rows[e.RowIndex].Cells["RepeatCount"].Value.ToString();

                UpdateRepeatCountInXml(filePath, repeatCount);
            }
        }

        private void UpdateRepeatCountInXml(string filePath, string repeatCount)
        {
            string scenarioConfigPath = Path.Combine(scenarioPath, comboBox1.SelectedItem.ToString(), "scenario.xml");
            XDocument scenarioDoc = XDocument.Load(scenarioConfigPath);

            // 파일 경로가 일치하는 'Message' 요소 찾기
            XElement messageElement = scenarioDoc.Descendants("Message")
                .FirstOrDefault(m => m.Element("Path").Value.Equals(filePath));

            if (messageElement != null)
            {
                // 'RepeatCount' 요소가 이미 존재하는지 확인
                XElement repeatCountElement = messageElement.Element("RepeatCount");
                if (repeatCountElement != null)
                {
                    // 요소가 존재하면 값을 업데이트
                    repeatCountElement.Value = repeatCount;
                }
                else
                {
                    // 요소가 없으면 새로 추가
                    messageElement.Add(new XElement("RepeatCount", repeatCount));
                }

                scenarioDoc.Save(scenarioConfigPath);
            }
        }

    }
}