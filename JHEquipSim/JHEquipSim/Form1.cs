using System.Xml.Linq;
using JHEquipSim.ServiceReference;

namespace JHEquipSim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            richTextBox1.Font = new Font("Consolas", 10); // ������ �۲� ���
        }

        private async void sendXmlButton_Click(object sender, EventArgs e)
        {
            // TextBox���� XML �����͸� �о�ɴϴ�.
            string xmlData = "<example><message>Hello, WCF from .NET 8 Client!</message></example>";
            DisplayFormattedXml(xmlData);

            try
            {
                // ���� Ŭ���̾�Ʈ �ν��Ͻ��� �����մϴ�.
                var client = new XmlReceiverServiceClient();

                // ������ �޼��带 �񵿱������� ȣ���մϴ�.
                await client.ReceiveXmlAsync(xmlData);

                MessageBox.Show("XML �����Ͱ� ������ ���۵Ǿ����ϴ�.", "���� ����", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Ŭ���̾�Ʈ�� �ݽ��ϴ�.
                await client.CloseAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"�������� ���� �� ������ �߻��߽��ϴ�: {ex.Message}", "���� ����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayFormattedXml(string xmlData)
        {
            try
            {
                var xDocument = XDocument.Parse(xmlData);
                string formattedXml = xDocument.ToString();

                // RichTextBox ������ �����, ������ ����� XML�� �߰�
                richTextBox1.Clear();
                richTextBox1.AppendText(formattedXml);

                // �߰����� ���� ������ �ʿ��� ��� ���⿡ �ڵ� �߰�
                // ��: �±�, �Ӽ��� ���� ���� ���� ��
            }
            catch (Exception ex)
            {
                MessageBox.Show($"XML �Ľ� ����: {ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
