﻿#region Using
using System;
using System.Net;
using System.Text;
using System.Windows.Forms; 
#endregion

namespace Client
{
    public partial class OptionsForm : Form
    {
        #region Fields/Properties
        private IPAddress ipAddress;
        private int port;

        public IPAddress IpAddress
        {
            get { return ipAddress; }
        }

        public int Port
        {
            get { return port; }
        }
        #endregion

        #region Constructors
        public OptionsForm()
        {
            InitializeComponent();
        } 
        #endregion

        #region Form Event Handlers
        private void btnSave_Click(object sender, EventArgs e)
        {
            StringBuilder errors = new StringBuilder();

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
