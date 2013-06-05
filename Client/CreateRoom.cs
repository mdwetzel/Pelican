using System;
using System.Windows.Forms;

namespace Client
{
    public partial class CreateRoom : Form
    {
        public new string Name
        {
            get { return textBox1.Text; }
        }

        public string Description
        {
            get { return textBox2.Text; }
        }

        public CreateRoom()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
