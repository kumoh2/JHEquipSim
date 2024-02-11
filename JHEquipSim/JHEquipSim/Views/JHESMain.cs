using JHEquipSim.Views;
using System.Windows.Forms;

namespace JHEquipSim
{
    public partial class JHESMain : Form
    {
        public JHESMain()
        {
            InitializeComponent();

            singleMessage singleMessage = new singleMessage();
            tabPage1.Controls.Add(singleMessage);
            singleMessage.Dock = DockStyle.Fill;
        }
    }
}
