using System.ServiceModel;

namespace JHEquipSim_WCF_Server
{
    [ServiceContract]
    public interface IXmlReceiverService
    {
        [OperationContract]
        void ReceiveXml(string xmlData);
    }
}