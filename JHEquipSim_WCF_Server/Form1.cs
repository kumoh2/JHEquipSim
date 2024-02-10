using System;
using System.ServiceModel;
using System.Windows.Forms;

namespace JHEquipSim_WCF_Server
{
    public partial class Form1 : Form
    {
        private ServiceHost host;

        public Form1()
        {
            InitializeComponent();
            StartWcfService();

            // WCF 서비스에 현재 폼의 인스턴스를 전달
            XmlReceiverService.MainForm = this;
        }

        private void StartWcfService()
        {
            host = new ServiceHost(typeof(XmlReceiverService), new Uri("http://localhost:8000/XmlReceiverService"));
            //host.AddServiceEndpoint(typeof(IXmlReceiverService), new BasicHttpBinding(), "");
            host.Open();
            // 콘솔 출력 대신 UI 업데이트 메서드 호출
            UpdateUI("WCF Service started. Listening at http://localhost:8000/XmlReceiverService");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            host?.Close();
        }

        public void UpdateUI(string message)
        {
            // UI 컨트롤 업데이트는 UI 스레드에서만 수행되어야 합니다.
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateUI), message);
            }
            else
            {
                // RichTextBox에 메시지 추가
                richTextBox1.AppendText(message + Environment.NewLine);
            }
        }
    }
}
