using System;

namespace JHEquipSim_WCF_Server
{
    public class XmlReceiverService : IXmlReceiverService
    {
        // 폼의 인스턴스를 저장하기 위한 정적 변수
        public static Form1 MainForm { get; set; }

        public void ReceiveXml(string xmlData)
        {
            MainForm?.UpdateUI(xmlData);
        }
    }
}