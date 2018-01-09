using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSU_IRC聊天
{
    class Program
    {
        static void Main(string[] args)
        {
            bool a = true;
            Irc Irc = new Irc();
            string User;
            string Password;
            Console.WriteLine("Hello World");
            Console.WriteLine("Loding 成功");
            A: try
            {
                Irc.IRCEncoding();
                Console.WriteLine("编码设置成功");
            }
            catch
            {
                Console.WriteLine("编码设置失败");

            }
            try
            {
                Irc.Connect();
                Console.WriteLine("服务器链接成功 IP:" + "irc.ppy.sh:6667");
            }
            catch
            {

                Console.WriteLine("链接失败");
            }

            while (a)
            {
                Console.WriteLine("设置用户名");
                User = Console.ReadLine();
                Irc.IrcUser = User;
                Console.WriteLine("输入密码");
                Password = Console.ReadLine();
                Irc.Password = Password;
                a = false;

            }
            Console.WriteLine("登录服务器ING");
            try
            {
                Irc.IrcLogin();
                Console.WriteLine("登录成功");
            }
            catch
            {

                Console.WriteLine("登录失败");
            }
            Console.WriteLine("使用模式：1.频道 2.私聊");
            int choese = 0;
            try
            {
                choese = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {


            }

            switch (choese)
            {
                case 1:
                    Console.WriteLine("输入进入频道 无需输入井号 如为MP房间 请输入 mp_房间号");
                    string OSUIRC;
                    string OSUMsg;
                    OSUIRC = Console.ReadLine();
                    Irc.chan = "#" + OSUIRC;
                    Irc.Join();
                    while (true)
                    {

                        OSUMsg = Console.ReadLine();
                        if (OSUMsg == "Exit Msg")
                        {
                            Irc.ExitChanl();
                            Irc.Disco();
                            Console.WriteLine("已经退出频道");
                            break;
                        }
                        Irc.send(OSUMsg, Irc.chanl);
                    }
                    goto A;

                case 2:
                    Console.WriteLine("输入私聊人ID");
                    string ID;
                    ID = Console.ReadLine();
                    Irc.Start();
                    Console.WriteLine("输入消息");
                    string Msg;
                    while (true)
                    {
                        Msg = Console.ReadLine();
                        if (Msg == "Exit Msg")
                        {
                            Console.WriteLine("退出私聊");

                            Irc.Disco();
                            break;
                        }
                        Irc.IRCsend(Msg, ID);
                    }
                    goto A;


                default:
                    Console.WriteLine("帮助:Exit Msg即可退出聊天 私聊信息是会一直接收的 频道信息 只可以在使用频道聊天时接收");
                    goto A;
            }

        }
    }
}
