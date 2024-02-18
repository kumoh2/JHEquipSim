// GlobalVariableForm.cs

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace JHEquipSim.Views
{
    public partial class GlobalVariableForm : UserControl
    {
        public Dictionary<string, string> GlobalVariables { get; private set; }

        public GlobalVariableForm()
        {
            InitializeComponent();
            GlobalVariables = new Dictionary<string, string>();
        }

        private void btnAddVariable_Click(object sender, EventArgs e)
        {
        }
    }
}