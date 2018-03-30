using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Chat_Server {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();

            Shown += new EventHandler(Form1_Shown);
            Closed += new System.EventHandler(this.Form1_Closed);
        }

        static bool running = true;
        private TcpListener server;
        public  string will_this_work;
      
      
        private void Form1_Shown(object sender, System.EventArgs e) {
             //List<USER> USER_LIST = new List<USER> { };
            start_server();
        }
        private void Form1_Closed(object sender, System.EventArgs e) {
            Environment.Exit(0);
        }
        private void textBox1_TextChanged(object sender, EventArgs e) {
            //log text
            

        }
        private void start_server() {
            IPEndPoint ipend = new IPEndPoint(IPAddress.Any, 9050);
            server = new TcpListener(ipend);
            server.Start();

            textBox1.Text = "Waiting for client. \r\n";
            Application.DoEvents();

            int threads = 0;
            while (true) {
                while (!server.Pending()) { 
                    //handle recieved messages
                    handle_msgReceived();
                    Application.DoEvents();
                }
                threads++;
                ConnectionThread newconnection = new ConnectionThread();
                newconnection.thread_listener = this.server;
                newconnection.thread_count = threads;
                Thread new_thread = new Thread(new ThreadStart(newconnection.connection_handler));
                new_thread.Start();
                textBox1.AppendText("Client No:" + threads.ToString() + " started! \r\n");
                textBox1.Update();
            }
        }

        private void update_client_msg() { 
            //nothing right now...
        }
        private void handle_msgReceived(){

            if (defines.message_arrived == true)
            {
                defines.USER temp_user = new defines.USER();
                temp_user = null;
                this.textBox1.AppendText( defines.client_message);
                string[] msg_fields = defines.client_message.Split('>');
                string[] msg_fieldstemp = defines.client_message.Split('>');
                string[] user = msg_fieldstemp[0].Split('#'); ;
                
                if (msg_fields[2] == "login")
                {
                    temp_user = new defines.USER();
                    temp_user.username = user[0];
                    temp_user.status = user[1];
                    defines.USER_LIST.Add(temp_user);
                  //  Console.Write(msg_fields[0]);
                     list_box_user.Items.Add(user[0] + " " + user[1]); //add login name in the source field
                }
                else if (msg_fields[2] == "logout")
                {
                    temp_user = defines.USER_LIST.FirstOrDefault(o => o.username == user[0]);//this is sort of hacky. It won't work if there are duplicate usernames.
                    if (temp_user != null)  defines.USER_LIST.Remove(temp_user);
                    list_box_user.Items.Remove(temp_user.username + " " + temp_user.status);//super hacky...
                }
                else if (msg_fields[1] == "msg")
                {
                    //defines.last_client_msg = defines.clientMsg;
                    
                        defines.list_messages.Add(defines.client_message);
                    
                    
                  //  Console.Write("Inside handlemessage()" + defines.client_message);
                  //  Console.Write("ALSO INSIDE handlemessage()" + defines.list_messages.LastOrDefault());

                    // update_client_msg();

                }


                else if (msg_fields[1] == "privmsg")
                {
                    //temp_user.status = "Private";
                    
                    
                    defines.private_list_messages.Add(defines.client_message);
                   
                
                }

                else if (msg_fields[1] == "6+")
                {
                    //end private chat
                    defines.USER temp_user1 = new defines.USER();
                    defines.USER temp_user_copy = new defines.USER();

                    string[] blah = msg_fields[0].Split('*');
                    Console.Write(blah[0] + " " + blah[1]  + "\r\n");
                    temp_user1 = defines.USER_LIST.FirstOrDefault(o => o.username == blah[0]);
                    Console.Write("WHATBLEAHEHSDF JELKJEF " + temp_user1.status + " " + temp_user1.username + "\r\n");
                    temp_user_copy = temp_user1;
                    defines.USER_LIST.Remove(temp_user1);
                    temp_user_copy.status = "public";
                    defines.USER_LIST.Add(temp_user_copy);

                    //temp_user1 = defines.USER_LIST.FirstOrDefault(o => o.username == blah[1]);
                    //temp_user_copy = temp_user;
                    //defines.USER_LIST.Remove(temp_user1);
                    //temp_user_copy.status = "public";
                    //defines.USER_LIST.Add(temp_user_copy);
                }



                else
                {
                    // do nothing here for other requests
                }

                if (this.list_box_user.Items.Count != 0)
                {
                    defines.user_list = "";
                    for (int i = 0; i < this.list_box_user.Items.Count; i++)
                    {
                        defines.user_list += this.list_box_user.Items[i] + "/";

                    }
                    //remove last "/"
                    defines.user_list = defines.user_list.Substring(0, defines.user_list.Length - 1);
                }

                defines.message_arrived = false;
            }
        }
       
        static readonly object _lock = new object();
        public class ConnectionThread {
            public TcpListener thread_listener;
            public int thread_count;

            public void connection_handler() {
               
                int recv;
                byte[] bdata = new byte[1024];
                TcpClient client;
                client = thread_listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                StreamWriter writer = new StreamWriter(stream);

                string greeting = "Welcome to our Multithreaded server. \r\n";
                writer.Write(greeting);
                writer.Flush();

                while (true) {
                   
                    bdata = new byte[1024];
                   
                    recv = stream.Read(bdata, 0, bdata.Length);
                    if (recv == 0) break;
                   
                    string str_recv = Encoding.ASCII.GetString(bdata, 0, recv);
                    
                    string[] message_field = str_recv.Split('>');
                    
                    //Console.Write("before lock " + message_field[2] + "\r\n");
                    
                    //for (int i = 0; i < message_field.Count(); i++) {
                    //    Console.Write(message_field[i] + " ");    
                    //}
                    Console.Write("\r\n");
                    
                    lock (_lock) {
                        defines.client_message = str_recv + "\r\n";
                       
                   //     Console.Write("Inside the lock  "+ str_recv + " " + message_field[2] + "\r\n");
                        defines.message_arrived = true;
                    }
                    if (message_field[2] == "logout") {

                        break;
                    }
                    else if (message_field[2] == "user_list") {
                        string msg;
                        defines.USER dummy_user = new defines.USER();
                        //List<string> temp_list = new List<string>();
                        defines.user_list = "";
                        for (int i = 0; i < defines.USER_LIST.Count; i++) {
                            dummy_user = defines.USER_LIST[i];
                            string name = dummy_user.username;
                            string status = dummy_user.status;
                           // temp_list.Add(name + " " + status);
                            defines.user_list += name + " " + status + "/";


                        }
                        defines.user_list = defines.user_list.Substring(0, defines.user_list.Length - 1);
                        msg = "server>all>user_list>" + defines.user_list;

                            writer.Write(msg);
                            writer.Flush();
                
                    }
                    //
                    //Private Chat

                    else if (message_field[1] == "6+") { 
                        

                    }
                    else if (message_field[1] == "5+") {
                       //start private chat
                        string msg;
                        defines.USER temp_user;
                        string[] temp;


                        temp_user = new defines.USER();
                        temp = message_field[0].Split('#');
                        temp_user = defines.USER_LIST.FirstOrDefault(o => o.username == temp[0]);
                       
                        
                        if (temp_user.status == "public") {
                            msg = "+1";
                            temp_user = defines.USER_LIST.FirstOrDefault(o => o.username == temp[0]);
                            defines.USER_LIST.Remove(temp_user);
                            temp_user.status = "Private";
                            defines.USER_LIST.Add(temp_user);
                        }
                        else { msg = "+2"; }


                        writer.Write(msg);
                        writer.Flush();

                    }

                    else if (message_field[2] == "private_msg") {
                        //get private messages
                        string msg;

                        msg = "server>priv>private_msg>No message available";
                        
                        string[] blah;
                        string[] temp_priv = message_field[0].Split('*','>');


                        
                        for (int i = 0; i < defines.private_list_messages.Count(); i++) {
                            blah = defines.private_list_messages[i].Split('*','>');
                            blah[1] = blah[1].Substring(0, blah[1].Length-1);
                           
                            //Console.Write(blah[0] + " " + blah[1] + " "+ temp_priv[0] + " " + temp_priv[1] + "\r\n");
                            if (((blah[0] == temp_priv[0]) || (blah[0] == temp_priv[1])) ) {
                                //Console.Write("WHAT?" + blah[0] + " " + blah[1] + " " + temp_priv[0] + " " + temp_priv[1] + "\r\n");
                                //Console.Write("THIS IS MAYBE?" + blah[1] == temp_priv[1] + "\r\n");
                                if ((blah[1] == temp_priv[0]) || (blah[1] == temp_priv[1])) {

                                    msg = defines.private_list_messages[i];
                                        if (temp_priv[0] != blah[0]) {
                                            defines.private_list_messages.Remove(msg);
                                    }
                                    break;
                                }
                            }
                            else msg = "server>priv>private_msg>No message available";
                        }
                    
                        writer.Write(msg);
                        writer.Flush();

                    }
                    else if (message_field[2] == "last_msg") {
                        string msg;

                        if (defines.list_messages.Count != 0) msg = defines.list_messages.LastOrDefault();

                        else msg = "server>all>last_msg>No message available";
                      
                        writer.Write(msg);
                        writer.Flush();

                    }

                    else {
                        writer.Write("Okay Client #" + thread_count.ToString() + "; \r\n Received: " + str_recv);
                        writer.Flush();
                    }

  
                }
                writer.Close();
                stream.Close();
                client.Close();
            }
        }

        private void shut_down_button_Click(object sender, EventArgs e)
        {
            string goodbye = "Server is shutting down \r\n Thanks for useing our Multithreaded server. \r\n";
            Console.Write(goodbye);
            //writer.Flush();
            //Socket clientSocket = listener.EndAcceptSocket(ar);
            //server.Shutdown(SocketShutdown.Both);
            //server.Close();
            Application.Exit();
        }
    }
}
