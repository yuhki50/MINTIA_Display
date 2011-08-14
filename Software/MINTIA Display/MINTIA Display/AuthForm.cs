using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MINTIA_Display
{
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();
        }

        public string PIN { get; private set; }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            PIN = TextBox_PIN.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
