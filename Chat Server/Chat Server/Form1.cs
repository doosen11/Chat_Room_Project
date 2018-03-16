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

        private TcpListener server;

        private void Form1_Shown(object sender, System.EventArgs e) {
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
        private void handle_msgReceived(){

            if (defines.message_arrived == true)
            {
                textBox1.AppendText( defines.client_message);
                string[] msg_fields = defines.client_message.Split('>');
                if (msg_fields[2] == "login")
                {
                    list_box_user.Items.Add(msg_fields[0]); //add login name in the source field
                }
                else if (msg_fields[2] == "logout")
                {
                    list_box_user.Items.Remove(msg_fields[0]);
                }
                else if (msg_fields[2] == "msg")
                {
                    //defines.last_client_msg = defines.clientMsg;
                    defines.list_messages.Add(defines.client_message);

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

                string greeting = "Multithreaded server. \r\n";
                writer.Write(greeting);
                writer.Flush();

                while (true) {
                    bdata = new byte[1024];
                    recv = stream.Read(bdata, 0, bdata.Length);
                    if (recv == 0) break;
                    string str_recv = Encoding.ASCII.GetString(bdata, 0, recv);

                    string[] message_field = str_recv.Split('>');

                    lock (_lock) {
                        defines.client_message = str_recv + "\r\n";
                        defines.message_arrived = true;
                    }
                    if (message_field[2] == "logout") {
                        break;
                    }
                    else if (message_field[2] == "user_list") {
                        string msg;
                        msg = "server>all>user_list>" + defines.user_list;
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
    }
}
