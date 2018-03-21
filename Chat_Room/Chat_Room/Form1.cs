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

            //testing code for initial server connection and text sending
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //try to connect to the server
            try {
                server.Connect(ipep);
            }
            catch (SocketException ev) {
                Public_Chat_textbox.Text = "Unable to connect to server. \r\n";
                Public_Chat_textbox.Text += ev.ToString() + "\r\n";
                Application.DoEvents();
                return;
            }
            Public_Chat_textbox.Text = "Connected to the server. \r\n";
            stream = new NetworkStream(server);
            writer = new StreamWriter(stream);
            //get the first response from server
            bdata = new byte[1024];
            int recv = server.Receive(bdata);
            Public_Chat_textbox.Text += "Server Response: " + Encoding.ASCII.GetString(bdata, 0, recv) + "\r\n";
            Application.DoEvents();
            string msg = username + ">" + "server" + ">login>";
            writer.Write(msg);
            writer.Flush();
            Thread.Sleep(100);

            //end testing code
        }

        private void Send_button_Click(object sender, EventArgs e)
        {
            if (Text_Input.Text == "") {
                MessageBox.Show("What do you think you're doing? Enter a message.");
                return;
            }
            //string destination = user_list.SelectedItem.ToString();
            string msg = username + ">" + "Public" + ">msg>" + Text_Input.Text;
            writer.Write(msg);
            writer.Flush();

            Public_Chat_textbox.Text += "Client sent: " + Text_Input.Text + "\r\n";
            Application.DoEvents();
            bdata = new byte[1024];
            int recv = server.Receive(bdata);
            Public_Chat_textbox.Text += "Server replied: " + Encoding.ASCII.GetString(bdata, 0, recv) + "\r\n";
            Application.DoEvents();

        }

        private void Public_Chat_textbox_TextChanged(object sender, EventArgs e) {

        }

        private void user_list_SelectedIndexChanged(object sender, EventArgs e) {

        }

       
    }
}
