#region Using
using System;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Client.Properties;

#endregion

namespace Client
{
    public partial class OptionsForm : Form
    {
        #region Fields/Properties
        public string IpAddress
        {
            get { return txtIpAddress.Text; }
            private set { txtIpAddress.Text = value; }
        }

        public string Port
        {
            get { return txtPort.Text; }
            private set { txtPort.Text = value; }
        }

        public string Username
        {
            get { return txtUsername.Text; }
            private set { txtUsername.Text = value; }
        }
        #endregion

        #region Constructors
        public OptionsForm()
        {
            InitializeComponent();

            IpAddress = Settings.Default.IPAddress;
            Port = Settings.Default.Port;
            Username = Settings.Default.Username;

            txtUsername.Select();
        }
        #endregion

        #region Form Event Handlers
        private void btnSave_Click(object sender, EventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            IPAddress ipAddress;
            int port;

            if (string.IsNullOrWhiteSpace(txtUsername.Text.Trim())) {
                errors.Append("Please enter a valid username.");
            } else if (txtUsername.Text.Trim().Length > 15) {
                errors.Append("Usernames must be fewer than 15 characters.");
            }

            if (!IPAddress.TryParse(txtIpAddress.Text, out ipAddress)) {
                errors.Append("Please enter a valid ip address.\n");
            }

            if (!int.TryParse(txtPort.Text, out port)) {
                errors.Append("Please enter a valid port number.\n");
            } else if (port < 0 || port > 35535) {
                errors.Append("Please enter a valid port number.\n");
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
