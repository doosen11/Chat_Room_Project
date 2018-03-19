using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Chat_Room {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        //variables for server connection and sending data
        Socket server;
        byte[] bdata = new byte[1024];
        NetworkStream stream;
        StreamWriter writer;

        //Records username
        string username = "";
        private void Login_button_Click(object sender, EventArgs e)
        {
            username = Text_Input.Text;
            Current_User_textbox.Text = "This User: " + username;
        }

        private void Send_button_Click(object sender, EventArgs e)
        {
            if (Text_Input.Text == "") {
                MessageBox.Show("What do you think you're doing? Enter a message.");
                return;
            }
            //string destination = user_list.SelectedItem.ToString();
            string
        }

       
    }
}
