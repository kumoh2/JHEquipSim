using System.Xml.Linq;
using JHEquipSim.ServiceReference;

namespace JHEquipSim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            richTextBox1.Font = new Font("Consolas", 10); // 고정폭 글꼴 사용
        }

        private async void sendXmlButton_Click(object sender, EventArgs e)
        {
            // TextBox에서 XML 데이터를 읽어옵니다.
            string xmlData = "<example><message>Hello, WCF from .NET 8 Client!</message></example>";
            DisplayFormattedXml(xmlData);

            try
            {
                // 서비스 클라이언트 인스턴스를 생성합니다.
                var client = new XmlReceiverServiceClient();

                // 서비스의 메서드를 비동기적으로 호출합니다.
                await client.ReceiveXmlAsync(xmlData);

                MessageBox.Show("XML 데이터가 서버로 전송되었습니다.", "전송 성공", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 클라이언트를 닫습니다.
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
                string formattedXml = xDocument.ToString();

                // RichTextBox 내용을 지우고, 서식이 적용된 XML을 추가
                richTextBox1.Clear();
                richTextBox1.AppendText(formattedXml);

                // 추가적인 서식 적용이 필요한 경우 여기에 코드 추가
                // 예: 태그, 속성에 대한 색상 적용 등
            }
            catch (Exception ex)
            {
                MessageBox.Show($"XML 파싱 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
