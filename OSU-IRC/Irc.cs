using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Meebey.SmartIrc4net;

namespace OSU_IRC聊天
{
    class Irc
    {
        public static IrcClient IRC = new IrcClient();
        private static Thread _ListenThread;
        Program program = new Program();
        public string chan { set { chanl = value; } get { return chanl; } }
        public string chanl;
        public string User{set{ IrcUser = value; } get{ return IrcUser; } }
        public string IrcUser;
        public string Password { set{ IrcPassword = value; }get{ return IrcPassword; } }
        string IrcPassword;
  
        public void Disco()
        {
            IRC.Disconnect();
        }
        public Irc()
        {




            IRC.OnChannelMessage += IRC_OnChannelMessage;
            IRC.OnQueryAction += IRC_OnQueryAction;
            IRC.OnQueryMessage += IRC_OnQueryMessage;

           



        }
        public  void IRCEncoding()
        {
            IRC.Encoding = Encoding.UTF8;
        }
        public void Connect()
        {
            IRC.Connect("irc.ppy.sh", 6667);

        }
        public void IrcLogin()
        {
                string id = IrcUser;
            try
            {
                IRC.Login(id, id, 0, id, IrcPassword); 
            }
            catch 
            {

                Console.WriteLine( "IrcLogin Error");
            }
            
           
        }
        public void Start()
        {
            _ListenThread = new Thread(new ThreadStart(IRCThread));
            _ListenThread.Start();
        }
        public void Join()
        {
            IRC.RfcJoin(chanl);
            _ListenThread = new Thread(new ThreadStart(IRCThread));

            try
            {
                _ListenThread.Start();
            }
            catch 
            {

                
            } 
        }
        private void IRC_OnQueryMessage(object sender, IrcEventArgs e)
        {

            Console.WriteLine("[" + System.DateTime.Now + "私聊" + "]" + e.Data.Nick+":" + e.Data.Message);
        

        }

        public void ExitChanl()
        {
            IRC.RfcQuit(chanl);
        }
        private void IRC_OnQueryAction(object sender, ActionEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void IRC_OnChannelMessage(object sender, IrcEventArgs e)
        {


           Console.WriteLine("[" + System.DateTime.Now + "来自" + e.Data.Channel + "]" + e.Data.Nick + ":" + e.Data.Message);

         
        }

        public static void send(string message, string Chanl)
        {
            
            IRC.SendMessage(SendType.Message,Chanl, message);
        }
        public   void IRCsend(string Msg,string id)
        {
            IRC.SendMessage(SendType.Message,id,Msg);
        }
        public void IRCThread()
        {
            try

            {
                IRC.Listen();
            }
            catch { }
        }
    }
}
