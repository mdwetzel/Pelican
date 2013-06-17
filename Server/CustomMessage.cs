#region Using
using System;
using System.Windows.Forms;

#endregion
namespace Server
{
    public partial class CustomMessage : Form
    {
        #region Fields/Properties
        public string Message
        {
            get { return rchTxtMessage.Text; }
        }
        #endregion

        #region Constructors
        public CustomMessage()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Event Handlers
        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        #endregion
    }
}
