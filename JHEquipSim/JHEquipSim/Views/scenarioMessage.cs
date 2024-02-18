using JHEquipSim.Helpers;
using JHEquipSim.ServiceReference;
using System.ServiceModel;
using System.Xml;
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
            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns.Add("FileName", "File Name");
            dataGridView1.Columns.Add("FilePath", "File Path");
            dataGridView1.Columns.Add("RepeatCount", "Repeat Count");

            dataGridView1.Columns["FileName"].Width = 200;
            dataGridView1.Columns["FilePath"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["FilePath"].Visible = false;
            dataGridView1.Columns["RepeatCount"].Width = 100;

            dataGridView2.Columns.Add("ParamName", "ParamName");
            dataGridView2.Columns.Add("ParamValue", "ParamValue");
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

            // 기존 메시지 중 가장 높은 id 값을 찾습니다.
            var lastId = scenarioDoc.Descendants("Message")
                                    .Attributes("id").Select(a => (int)a)
                                    .DefaultIfEmpty(0)
                                    .Max();

            XElement newMessageElement = new XElement("Message",
                new XAttribute("id", ++lastId), // 마지막 id에서 1을 더해 새 id를 생성합니다.
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
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("전송할 XML 파일이 없습니다.", "전송 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var client = new XmlReceiverServiceClient();

            try
            {
                // 시나리오 XML을 읽습니다.
                XDocument scenarioXmlDoc = XDocument.Load(Path.Combine(scenarioPath, comboBox1.SelectedItem.ToString(), "scenario.xml"));

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (string.IsNullOrWhiteSpace(row.Cells["FilePath"].Value?.ToString()))
                    {
                        continue; // FilePath가 비어있다면 이 행을 건너뜁니다.
                    }

                    string originalFilePath = row.Cells["FilePath"].Value.ToString();
                    int messageId = int.Parse(row.Cells["id"].Value.ToString()); // 각 메시지의 ID를 가져옵니다.
                    int repeatCount = int.Parse(row.Cells["RepeatCount"].Value.ToString()); // 각 메시지의 리핏 카운트를 가져옵니다.

                    // 시나리오 XML에 대한 해당 메시지의 파라미터 값을 가져옵니다.
                    var parameterValues = GetParameterValuesFromScenarioXml(scenarioXmlDoc, messageId);
                    for (int i = 0; i < repeatCount; i++)
                    {
                        // 변경된 XML 내용을 문자열로 변환합니다.
                        string modifiedXmlContent = ModifyXmlWithScenarioParameters(originalFilePath, parameterValues);

                        // Send the modified XML content
                        await client.ReceiveXmlAsync(modifiedXmlContent);
                    }
                }

                MessageBox.Show("모든 XML 데이터가 서버로 전송되었습니다.", "전송 성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"서버로의 전송 중 오류가 발생했습니다: {ex.Message}", "전송 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (client.State == CommunicationState.Opened)
                {
                    await client.CloseAsync();
                }
            }
        }

        private List<KeyValuePair<string, string>> GetParameterValuesFromScenarioXml(XDocument scenarioXmlDoc, int messageId)
        {
            var parameterValues = new List<KeyValuePair<string, string>>();

            var messageElement = scenarioXmlDoc.Descendants("Message").FirstOrDefault(m => m.Attribute("id")?.Value == messageId.ToString());
            if (messageElement != null)
            {
                var parameters = messageElement.Element("Parameters")?.Elements()
                                                                .Select(x => new KeyValuePair<string, string>(x.Name.LocalName, x.Value))
                                                                .ToList();
                if (parameters != null)
                {
                    parameterValues.AddRange(parameters);
                }
            }

            return parameterValues;
        }

        private string ModifyXmlWithScenarioParameters(string originalFilePath, List<KeyValuePair<string, string>> parameterValues)
        {
            string xmlContent = File.ReadAllText(originalFilePath);
            XDocument xDocument = XDocument.Parse(xmlContent);

            int parameterIndex = 0; // 파라미터 값 리스트 내의 각 파라미터의 인덱스
            foreach (var parameterPair in parameterValues)
            {
                string parameterName = parameterPair.Key; // 파라미터의 이름
                string parameterValue = parameterPair.Value; // 파라미터의 값

                // 해당 파라미터 이름에 대한 파라미터 엘리먼트를 가져옵니다.
                var parameterElements = xDocument.Descendants().Where(d => d.Name.LocalName == parameterName).ToList();

                // 중복된 파라미터가 있을 경우에만 값을 변경합니다.
                if (parameterElements.Any())
                {
                    var paramElement = parameterElements[parameterIndex % parameterElements.Count]; // 중복된 파라미터의 경우 인덱스를 사용하여 순환합니다.
                    paramElement.Value = parameterValue; // 해당 파라미터 값을 시나리오 XML의 값으로 변경합니다.
                }

                parameterIndex++;
            }

            // 변경된 XML 내용을 문자열로 반환합니다.
            return xDocument.ToString();
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
                int messageId = message.Attribute("id") != null ? (int)message.Attribute("id") : -1;
                string messagePath = message.Element("Path").Value; // 'Path' 요소의 값을 가져옴
                string fileName = Path.GetFileName(messagePath); // 전체 경로에서 파일 이름만 추출
                string repeatCount = message.Element("RepeatCount").Value;

                // 파일 이름, 파일 경로, 반복 횟수를 DataGridView에 추가합니다.
                dataGridView1.Rows.Add(messageId, fileName, messagePath, repeatCount);
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
                    // 'FilePath'와 'id' 값을 얻습니다.
                    string selectedFilePath = dataGridView1.Rows[selectedIndex].Cells["FilePath"].Value.ToString();
                    int messageId = Convert.ToInt32(dataGridView1.Rows[selectedIndex].Cells["ID"].Value); // 가정: 'MessageId'는 메시지 ID를 저장하는 컬럼입니다.

                    RemoveMessageFromXml(selectedFilePath, messageId);
                    LoadScenarioFileToListBox(Path.Combine(scenarioPath, comboBox1.SelectedItem.ToString(), "scenario.xml"));
                }
            }
            else
            {
                MessageBox.Show("삭제할 항목을 먼저 선택하세요.", "선택 필요", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RemoveMessageFromXml(string filePath, int messageId)
        {
            string scenarioConfigPath = Path.Combine(scenarioPath, comboBox1.SelectedItem.ToString(), "scenario.xml");
            XDocument scenarioDoc = XDocument.Load(scenarioConfigPath);

            // 'id' 속성과 'Path' 엘리먼트의 값이 모두 일치하는 메시지를 찾습니다.
            // 'id' 속성과 'Path' 엘리먼트의 값이 모두 일치하는 메시지를 찾습니다.
            var messageToRemove = scenarioDoc.Descendants("Message")
                .FirstOrDefault(m => (int?)m.Attribute("id") == messageId && m.Element("Path").Value.Equals(filePath));

            // 해당 메시지가 있으면 삭제합니다.
            if (messageToRemove != null)
            {
                messageToRemove.Remove();
                // XML 문서를 저장합니다.
                scenarioDoc.Save(scenarioConfigPath);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // 'RepeatCount' 열에서 편집이 발생한 경우에만 처리
            if (dataGridView1.Columns[e.ColumnIndex].Name == "RepeatCount")
            {
                string filePath = dataGridView1.Rows[e.RowIndex].Cells["FilePath"].Value.ToString();
                string repeatCount = dataGridView1.Rows[e.RowIndex].Cells["RepeatCount"].Value.ToString();
                int messageId = GetSelectedMessageId(); // 선택된 행의 ID 가져오기

                UpdateRepeatCountAndIdInXml(filePath, repeatCount, messageId);
            }
        }

        private int GetSelectedMessageId()
        {
            // 선택된 행이 있는지 확인하고 ID를 가져옵니다.
            if (dataGridView1.SelectedRows.Count > 0)
            {
                return (int)dataGridView1.SelectedRows[0].Cells["id"].Value;
            }
            return -1; // 선택된 행이 없는 경우 -1 반환
        }

        private void UpdateRepeatCountAndIdInXml(string filePath, string repeatCount, int messageId)
        {
            string scenarioConfigPath = Path.Combine(scenarioPath, comboBox1.SelectedItem.ToString(), "scenario.xml");
            XDocument scenarioDoc = XDocument.Load(scenarioConfigPath);

            // 파일 경로가 일치하고 ID가 일치하는 'Message' 요소 찾기
            XElement messageElement = scenarioDoc.Descendants("Message")
                .FirstOrDefault(m => (int)m.Attribute("id") == messageId && m.Element("Path").Value.Equals(filePath));

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

                // ID를 업데이트합니다. (다시 로드할 필요 없음)
                messageElement.Attribute("id").Value = messageId.ToString();

                // XML을 저장하지 않고 정렬만 수행합니다.
                SortXmlById(scenarioDoc, scenarioConfigPath);
            }
        }

        private void SortXmlById(XDocument doc, string filePath)
        {
            // XML 문서에서 모든 Message 요소를 가져옵니다.
            var messageElements = doc.Root.Elements("Message").ToList();

            // 가져온 Message 요소 리스트가 비어 있는지 확인합니다.
            if (messageElements.Any())
            {
                // Message 요소를 id 속성을 기준으로 정렬합니다.
                var sortedElements = messageElements.OrderBy(e => (int)e.Attribute("id")).ToList();

                // 정렬된 요소를 문서에 다시 삽입합니다.
                doc.Root.ReplaceAll(sortedElements);
                doc.Save(filePath);
            }
            // 만약 정렬할 Message 요소가 없다면, 아무 작업도 수행하지 않습니다.
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                LoadParametersToDataGridView2();
            }
        }

        private void LoadParametersToDataGridView2()
        {
            dataGridView2.Rows.Clear();

            // 선택된 행이 있는지 확인
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // 선택된 행에서 ID 값을 가져옵니다.
                var idCell = dataGridView1.SelectedRows[0].Cells["id"].Value;
                if (idCell != null)
                {
                    int messageId = Convert.ToInt32(idCell);

                    // 시나리오 XML을 로드합니다.
                    XDocument scenarioDoc = XDocument.Load(Path.Combine(scenarioPath, comboBox1.SelectedItem.ToString(), "scenario.xml"));

                    // 선택된 메시지 ID에 해당하는 메시지를 찾습니다.
                    var selectedMessage = scenarioDoc.Descendants("Message")
                        .FirstOrDefault(m => m.Attribute("id")?.Value == messageId.ToString());

                    if (selectedMessage != null)
                    {
                        var parameters = selectedMessage.Element("Parameters").Elements();
                        foreach (var param in parameters)
                        {
                            dataGridView2.Rows.Add(param.Name.LocalName, param.Value); // param.Name.LocalName을 사용하여 요소의 이름만 가져옵니다.
                        }
                    }
                }
            }
        }

        private void paramadd_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // 선택된 행에서 ID 값을 가져옵니다.
                var idCell = dataGridView1.SelectedRows[0].Cells["id"].Value;
                if (idCell != null)
                {
                    int messageId = Convert.ToInt32(idCell);

                    XDocument scenarioDoc = XDocument.Load(Path.Combine(scenarioPath, comboBox1.SelectedItem.ToString(), "scenario.xml"));

                    var selectedMessage = scenarioDoc.Descendants("Message")
                        .FirstOrDefault(m => m.Attribute("id")?.Value == messageId.ToString());

                    if (selectedMessage != null)
                    {
                        // 새 파라미터 추가 예시. 실제 파라미터 이름과 값을 사용자 입력으로부터 받아와야 할 수 있습니다.
                        selectedMessage.Element("Parameters").Add(new XElement("NewParameter", "NewValue", "1"));
                        scenarioDoc.Save(Path.Combine(scenarioPath, comboBox1.SelectedItem.ToString(), "scenario.xml"));

                        LoadParametersToDataGridView2();
                    }
                }
            }
        }

        private void paramremove_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0 && dataGridView1.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("정말로 이 파라미터를 제거하시겠습니까?", "파라미터 제거", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string paramName = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                    var idCell = dataGridView1.SelectedRows[0].Cells["id"].Value;
                    if (idCell != null)
                    {
                        int messageId = Convert.ToInt32(idCell);

                        XDocument scenarioDoc = XDocument.Load(Path.Combine(scenarioPath, comboBox1.SelectedItem.ToString(), "scenario.xml"));

                        var selectedMessage = scenarioDoc.Descendants("Message")
                            .FirstOrDefault(m => m.Attribute("id")?.Value == messageId.ToString());

                        if (selectedMessage != null)
                        {
                            var param = selectedMessage.Element("Parameters").Elements().FirstOrDefault(p => p.Name.LocalName == paramName);
                            if (param != null)
                            {
                                param.Remove();
                                scenarioDoc.Save(Path.Combine(scenarioPath, comboBox1.SelectedItem.ToString(), "scenario.xml"));

                                LoadParametersToDataGridView2();
                            }
                        }
                    }
                }
            }
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var idCell = dataGridView1.SelectedRows[0].Cells["id"].Value;
                if (idCell != null)
                {
                    int messageId = Convert.ToInt32(idCell);

                    XDocument scenarioDoc = XDocument.Load(Path.Combine(scenarioPath, comboBox1.SelectedItem.ToString(), "scenario.xml"));
                    var selectedMessage = scenarioDoc.Descendants("Message")
                        .FirstOrDefault(m => m.Attribute("id")?.Value == messageId.ToString());

                    if (selectedMessage != null)
                    {
                        var parameters = selectedMessage.Element("Parameters").Elements().ToList();

                        if (e.RowIndex < parameters.Count)
                        {
                            // 파라미터 이름을 수정하는 경우
                            if (e.ColumnIndex == 0)
                            {
                                string newParamName = dataGridView2.Rows[e.RowIndex].Cells["ParamName"].Value?.ToString() ?? "";

                                // 새 파라미터 이름이 XML 요소 이름으로 유효한지 확인합니다.
                                if (!IsValidXmlName(newParamName))
                                {
                                    MessageBox.Show("파라미터 이름은 비어 있지 않은 문자로 시작해야 하며, 특수 문자나 숫자로 시작할 수 없습니다.", "유효하지 않은 이름", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    // 유효하지 않은 이름에 대한 처리를 합니다. 여기에서는 셀의 값을 이전 값으로 되돌립니다.
                                    ReloadDataGridView2ParameterNames(e.RowIndex);
                                    return;
                                }

                                XElement oldParam = parameters[e.RowIndex];
                                XElement newParam = new XElement(newParamName, oldParam.Value);
                                foreach (var attr in oldParam.Attributes())
                                {
                                    newParam.SetAttributeValue(attr.Name, attr.Value);
                                }
                                oldParam.ReplaceWith(newParam);
                            }
                            // 파라미터 값을 수정하는 경우
                            else if (e.ColumnIndex == 1) // 'ParamValue' 열
                            {
                                parameters[e.RowIndex].Value = dataGridView2.Rows[e.RowIndex].Cells["ParamValue"].Value.ToString();
                            }

                            // XML 파일을 저장합니다.
                            scenarioDoc.Save(Path.Combine(scenarioPath, comboBox1.SelectedItem.ToString(), "scenario.xml"));
                            // 변경 사항을 반영하기 위해 파라미터 목록을 새로고침합니다.
                            LoadParametersToDataGridView2();
                        }
                    }
                }
            }
        }

        // XML 요소 이름으로 유효한지 확인하는 메서드
        private bool IsValidXmlName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;

            try
            {
                XmlConvert.VerifyName(name);
                return true;
            }
            catch (XmlException)
            {
                return false;
            }
        }

        // 유효하지 않은 파라미터 이름이 입력된 경우 원래의 파라미터 이름으로 dataGridView2를 다시 로드하는 메서드
        private void ReloadDataGridView2ParameterNames(int rowIndex)
        {
            // 선택된 메시지의 파일 경로를 가져옵니다.
            string selectedMessagePath = dataGridView1.SelectedRows[0].Cells["FilePath"].Value.ToString();
            XDocument scenarioDoc = XDocument.Load(Path.Combine(scenarioPath, comboBox1.SelectedItem.ToString(), "scenario.xml"));

            // 선택된 메시지를 찾습니다.
            var selectedMessage = scenarioDoc.Descendants("Message")
                .FirstOrDefault(m => m.Element("Path").Value.Equals(selectedMessagePath));

            if (selectedMessage != null)
            {
                var parameters = selectedMessage.Element("Parameters").Elements().ToList();
                // rowIndex에 해당하는 파라미터 이름을 다시 로드합니다.
                if (rowIndex < parameters.Count)
                {
                    dataGridView2.Rows[rowIndex].Cells["ParamName"].Value = parameters[rowIndex].Name.LocalName;
                }
            }
        }

        private Dictionary<string, string> globalVariables = new Dictionary<string, string>();

        private void edit_g_various_Click(object sender, EventArgs e)
        {
            // 글로벌 변수 편집 폼을 엽니다.
            using (var form = new GlobalVariableForm())
            {
            }
        }

        private string ReplaceGlobalVariables(string originalXmlContent)
        {
            // originalXmlContent에서 globalVariables에 정의된 변수를 모두 찾아 교체합니다.
            foreach (var variable in globalVariables)
            {
                originalXmlContent = originalXmlContent.Replace($"${{{variable.Key}}}", variable.Value);
            }
            return originalXmlContent;
        }
    }
}