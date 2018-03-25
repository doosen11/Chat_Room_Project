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

using Microsoft.VisualBasic;




namespace Chat_Room {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            InitTimer();
        }

        //variables for server connection and sending data
        Socket server;
        byte[] bdata = new byte[1024];
        NetworkStream stream;
        StreamWriter writer;
        public List<string> _items = new List<string>();
        private System.Windows.Forms.Timer user_list_timer;
        // user_list = new ListBox();
        
        /************************
         * The function below is a timer for updating the user list every 2 seconds 
         ************************ */
        
        public void InitTimer() {
            user_list_timer = new System.Windows.Forms.Timer();
            user_list_timer.Tick += new EventHandler(update_user_list);       
            user_list_timer.Interval = 2000;
            user_list_timer.Start();
        }
        private void update_user_list(object sender, EventArgs e) {
           
            if (username != "") {
                string msg;
                msg = username + ">" + "server" + ">user_list>";
                writer.Write(msg);
                writer.Flush();
                Application.DoEvents();
                bdata = new byte[1024];
                int recv = server.Receive(bdata);
                string received_msg = Encoding.ASCII.GetString(bdata, 0, recv);

                int last_i = received_msg.LastIndexOf('>');

                string[] u_list;
                u_list = received_msg.Substring(last_i + 1).Split('/');

                user_list.Items.Clear();
                for (int i = 0; i < u_list.Length; i++) {
                    user_list.Items.Add(u_list[i]);
                }
            }

        }

        /** *************************
         * END UPDATE USER LIST STUFF
         **************************** **/
        //Records username
        string username = "";
        
        
       
        
        private void Login_button_Click(object sender, EventArgs e)
        {
           // username = Text_Input.Text;
            //Current_User_textbox.Text = "This User: " + username;

            do
            {
                username = Microsoft.VisualBasic.Interaction.InputBox("Enter Username: ", "User Login", "");
                Application.DoEvents();


            } while (username == "");

            Current_User_textbox.Text = username;
            Application.DoEvents();

            Login_button.Enabled = false;
            
            
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

        private void Request_Private_Chat_button_Click(object sender, EventArgs e) {

        }

        private void End_Private_Chat_button_Click(object sender, EventArgs e) {

        }

        private void Logout_button_Click(object sender, EventArgs e)
        {
            string msg;
            msg = username + ">" + "server" + ">logout>";
            writer.Write(msg);
            writer.Flush();

            server.Shutdown(SocketShutdown.Both);
            server.Close();
            Application.Exit();
        }

       
    }
}
