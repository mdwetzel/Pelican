#region Using
using System;
using System.Text;
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

        #region Event Handlers
        private void btnOk_Click(object sender, EventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(Message)) {
                errors.Append("Message cannot be blank.\n");
            } else if (Message.Length > 200) {
                errors.Append("Message length must be fewer than 200 characters.\n");
            }

            if (errors.Length > 0) {
                MessageBox.Show(errors.ToString());
            } else {
                DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        #endregion
    }
}
