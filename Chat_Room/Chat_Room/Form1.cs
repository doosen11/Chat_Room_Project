using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_Room {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        //Records username
        string username;
        private void Login_button_Click(object sender, EventArgs e)
        {
            username = Text_Input.Text;
            Current_User_textbox.Text = "This User: " + username;
        }

        private void Send_button_Click(object sender, EventArgs e)
        {

        }

       
    }
}
