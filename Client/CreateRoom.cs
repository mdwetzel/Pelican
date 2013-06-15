#region Using
using System;
using System.Net;
using System.Text;
using System.Windows.Forms;
#endregion

namespace Client
{
    public partial class CreateRoom : Form
    {
        #region Fields/Properties
        public string RoomName
        {
            get { return txtRoomName.Text.Trim(); }
        }

        public string Description
        {
            get { return txtDescription.Text.Trim(); }
        }

        public string AdminPassword
        {
            get { return txtAdminPassword.Text.Trim(); }
        }

        public string RoomPassword
        {
            get { return txtRoomPassword.Text.Trim(); }
        }
        #endregion

        #region Constructors
        public CreateRoom()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Event Handlers
        private void btnOK_Click(object sender, EventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            // Invalid names.
            if (string.IsNullOrWhiteSpace(RoomName)) {
                errors.Append("Room name must not be blank.");
            } else if (RoomName.Length < 4) {
                errors.Append("Room name must be at least 4 characters.");
            } else if (RoomName.Length > 20) {
                errors.Append("Room name must be fewer than 20 characters.");
            }

            // Invalid descriptions.
            if (string.IsNullOrWhiteSpace(Description)) {
                errors.Append("Room description must not be blank.");
            } else if (Description.Length < 20) {
                errors.Append("Room description must be at least 20 characters.");
            } else if (Description.Length > 100) {
                errors.Append("Room description must be fewer than 100 characters.");
            }

            // Invalid admin password.
            if (string.IsNullOrWhiteSpace(AdminPassword)) {
                errors.Append("Admin Password must not be blank.");
            } else if (AdminPassword.Length < 5) {
                errors.Append("Admin Password must be at least 5 characters.");
            } else if (AdminPassword.Length > 100) {
                errors.Append("Admin Password must be fewer than 100 characters.");
            }

            // Invalid room password.
            if (string.IsNullOrWhiteSpace(RoomPassword)) {
                errors.Append("Room Password must not be blank.");
            } else if (RoomPassword.Length < 5) {
                errors.Append("Room Password must be at least 5 characters.");
            } else if (RoomPassword.Length > 100) {
                errors.Append("Room Password must be fewer than 100 characters.");
            }

            if (errors.Length > 0) {
                // I was too lazy to go through and add a new line to each error.
                MessageBox.Show(string.Join(".\n", errors.ToString().Split('.')));
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
