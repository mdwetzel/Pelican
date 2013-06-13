#region Using
using System;
using System.Windows.Forms;
#endregion

namespace Server
{
    public partial class BroadcastForm : Form
    {
        #region Fields/Properties
        public string Message
        {
            get { return rchTxtMessage.Text; }
        }
        #endregion

        #region Constructors
        public BroadcastForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Event Handlers
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        #endregion
    }
}
