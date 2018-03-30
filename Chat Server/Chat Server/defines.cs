using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server {
    static public class defines {
        public class USER {
            public string username;
            public string status;
        }
        static public string client_message;
        static public string private_client_message;
        static public bool message_arrived;
        static public bool private_message_arrived;
        static public string user_list;
        static public List<string> list_messages = new List<string>();
        static public List<string> private_list_messages = new List<string>();
        static public List<USER> USER_LIST = new List<USER>();
    }
}
