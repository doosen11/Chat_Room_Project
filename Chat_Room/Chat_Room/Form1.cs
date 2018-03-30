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

        bool userispriv = false;
        string privusername;
        
        public Form1() {
            InitializeComponent();
            InitTimer();
        }

        //variables for server connection and sending data
        Socket server;
        byte[] bdata = new byte[1024];
        byte[] bdatatwo = new byte[1024];
        NetworkStream stream;
        StreamWriter writer;
        public List<string> _items = new List<string>();
        private System.Windows.Forms.Timer user_list_timer;

        //Records username
        string username1 = "";
        string username = "";
        string old_msg = "";
        string old_msg2 = "";
        string status = "public";
        int temp;
        static readonly object _lock = new object();
        
        /************************
         * The function below is a timer for updating the user list every 2 seconds 
         ************************ */
        
        public void InitTimer() {
            user_list_timer = new System.Windows.Forms.Timer();
            
            user_list_timer.Tick += new EventHandler(get_stuff);
            user_list_timer.Interval = 2000;
            user_list_timer.Start();
        }
           

        private void get_stuff(object sender, EventArgs e) {
            //get user list
            if (username1 != "") {
                string msg;
                msg = username1 + ">" + "server" + ">user_list>";
                writer.Write(msg);
                writer.Flush();
                Application.DoEvents();
                bdata = new byte[1024];
                int recv;
               
                    recv = server.Receive(bdata);
                
                string received_msg = Encoding.ASCII.GetString(bdata, 0, recv);

                int last_i = received_msg.LastIndexOf('>');

                string[] u_list;
                u_list = received_msg.Substring(last_i + 1).Split('/');

                user_list.Items.Clear();
                for (int i = 0; i < u_list.Length; i++) {
                    user_list.Items.Add(u_list[i]);
                }
            }
            //get public messages
            if (username != "") {
                
                string msg;
                msg = username + ">" + "server" + ">last_msg";
                writer.Write(msg);
                writer.Flush();
                Application.DoEvents();
               // Public_Chat_textbox.AppendText("Client sent: " + msg + "\r\n");
                // wait for server response
                bdata = new byte[1024];
                // Thread.Sleep(500);
                int recv;
               
                    recv = server.Receive(bdata);
                            
                string received_msg = Encoding.ASCII.GetString(bdata, 0, recv);
                Console.Write(recv + "\r\n");
                Console.Write("WRITE WHAT YOU GOT " + received_msg + "\r\n");
                //old_msg = received_msg;
                
                    if ( received_msg != old_msg) {
                        Public_Chat_textbox.AppendText(received_msg + "\r\n");
                        Console.Write("UPDATE THAT STUFF" + received_msg + "\r\n");
                        
                    }
                    Application.DoEvents();
                old_msg = received_msg;
            }

            //get Private messages

            if(username != "" && status == "Private"){
                string msg;
                msg = username + ">" + "server" + ">private_msg";
                writer.Write(msg);
                writer.Flush();
                Application.DoEvents();
               
                // wait for server response
                bdata = new byte[1024];
                
                int recv;
               
                    recv = server.Receive(bdata);
                            
                string received_msg = Encoding.ASCII.GetString(bdata, 0, recv);
               // Console.Write(recv + "\r\n");
                //Console.Write("WRITE WHAT YOU GOT " + received_msg + "\r\n");
                //old_msg = received_msg;
                
                    if ( received_msg != old_msg) {
                        Private_Chat_textbox.AppendText(received_msg + "\r\n");
                        Console.Write("UPDATE THAT STUFF" + received_msg + "\r\n");
                        
                    }
                    Application.DoEvents();
                old_msg2 = received_msg;
            }
            

        }

        private void Login_button_Click(object sender, EventArgs e)
        {
          //  do
           // {
                username1 = Microsoft.VisualBasic.Interaction.InputBox("Enter Username: ", "User Login", "");
                username = username1 + "#" + "public";
                //Application.DoEvents();
           // } while (username == "");
            Current_User_textbox.Text = username1;
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
            temp = recv;
          
             //   Public_Chat_textbox.Text += "Server Response: " + Encoding.ASCII.GetString(bdata, 0, recv) + "\r\n";
                Application.DoEvents();

                string msg = username + ">" + "server" + ">login>";//username+status is sent as username
                writer.Write(msg);
                writer.Flush();
                Console.Write("LOGGED IN " + "\r\n");
            
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
            string msg;
            if (status == "Private") {
                msg = username1 + " " + ">privmsg>" + Text_Input.Text;
            }
            else msg = username1 + " " + ">msg>" + Text_Input.Text;
            
            writer.Write(msg);
            writer.Flush();

           // Public_Chat_textbox.Text += "Client sent: " + Text_Input.Text + "\r\n";
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

           
            
            //Acquires name that user wishes to communicate with
          
            
            privusername = Microsoft.VisualBasic.Interaction.InputBox("Enter requested username: ", "Username", "");
           

            

           //Application.DoEvents();

           if (privusername != "") {
               Request_Private_Chat_button.Enabled = false;

               string msg2 = username + " " + ">5+>" + privusername;
               

               writer.Write(msg2);
               writer.Flush();

               //get the first response from server
               bdata = new byte[1024];
               int recv = server.Receive(bdata);
             
                   string testdata = Encoding.ASCII.GetString(bdata, 0, recv);

                   //If user connect is successfull
                   if (testdata == "+1") {
                       Private_Chat_textbox.Text = "Connected to the private chat with: " + privusername + "\r\n";
                       userispriv = true;
                       status = "Private";


                       //string msg;
                       //username = username1 + "#" + status;
                       //msg = username + ">" + "server" + ">login>";//username+status is sent as username
                       //writer.Write(msg);
                       //writer.Flush();
                       //do_private_chat();
                   }
                  //If user connection fails
                   else if (testdata == "+2") {
                       Private_Chat_textbox.Text = "Failed to connect with requested user, please attempt again.";
                       Request_Private_Chat_button.Enabled = true;
                       Application.DoEvents();


                   }
               

           }
   


        }
        private void do_private_chat() {
            while (status == "Private") { 
            
            
            
            //
            
            }
        }
        private void End_Private_Chat_button_Click(object sender, EventArgs e) {

            string msg = username + " " + ">6+>" + privusername;
            writer.Write(msg);
            writer.Flush();

            Private_Chat_textbox.Text = "You have disconnected from the private chat";
            Request_Private_Chat_button.Enabled = true;
            userispriv = false;
            
            
           

           

         

        }

        private void Logout_button_Click(object sender, EventArgs e)
        {
            string msg;
            msg = username1 + ">" + "server" + ">logout>";
            writer.Write(msg);
            writer.Flush();

            server.Shutdown(SocketShutdown.Both);
            server.Close();
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        
       
    }
}
