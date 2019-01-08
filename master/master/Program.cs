using System;
using System.IO;
using System.IO.Ports;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace master
{
    class Program
    {
        Db dbAllUser = new Db();
        SendgridApi API = new SendgridApi();
        String GoodBye;
        String Welcome;
        String Alarm;
        String WrongId;
        String Disabled;
        String Intrusion;
        Dictionary<String, String> mapKey = new Dictionary<String, String>();
        Boolean AlarmIdx = false;

        private Boolean checkDigit(String str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return (true);
            }
            return (false);
        }

        private void proccessToAdd()
        {
            Console.WriteLine("--------------- Add User ---------------");
            Console.Write("Id user (ex : 123) : ");
            String id = Console.ReadLine();
            Console.Write("Name user (ex : tasoeur) : ");
            String name = Console.ReadLine();
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(id) || name.Contains(";") || checkDigit(id) || id == "1234")
                Console.WriteLine("Error : Invalid Name or Id.");
            else
            {
                if ((dbAllUser.addUser(name, id)) == false)
                    Console.WriteLine("Error : can't create user");
                else
                    Console.WriteLine("User Name = " + name + " with Id = " + id + " is now created.");
            }
            Console.WriteLine("----------------------------------------");
        }

        private void proccessToRemove()
        {
            Console.WriteLine("--------------- remove User ---------------");
            Console.Write("Id user (ex : 1234) : ");
            String id = Console.ReadLine();
            if (String.IsNullOrEmpty(id) || checkDigit(id))
                Console.WriteLine("Error : Invalid Id.");
            else
            {
                if ((dbAllUser.removeUser(id)) == false)
                    Console.WriteLine("Error : can't remove user");
                else
                    Console.WriteLine("User Id = " + id + " was removed.");
            }
            Console.WriteLine("-------------------------------------------");
        }


        private void proccessToGet()
        {
            Console.WriteLine("--------------- Get all Users infos ---------------");
            Console.WriteLine(dbAllUser.getAllUsersInfos());
            Console.WriteLine("---------------------------------------------------");
        }

        private void tek42sh()
        {
            while (true)
            {
                Console.WriteLine("Welcome to 42sh : tape \"add\" to add a new user or \"remove\" to remove an existing user or \"get\" to get all users infos : ");
                Console.Clear();
                String cmd = Console.ReadLine();
                if (String.IsNullOrEmpty(cmd) || (cmd != "add" && cmd != "remove" && cmd != "get"))
                    Console.WriteLine("Error : Invalid input");
                else if (cmd == "add")
                    this.proccessToAdd();
                else if (cmd == "remove")
                    this.proccessToRemove();
                else if (cmd == "get")
                    this.proccessToGet();
            }
        }

        private void loadText()
        {
            Welcome = dbAllUser.getImg("welcome.txt");
            Alarm = dbAllUser.getImg("alarmReady.txt");
            GoodBye = dbAllUser.getImg("goodBye.txt");
            WrongId = dbAllUser.getImg("wrongId.txt");
            Disabled = dbAllUser.getImg("disabled.txt");
            Intrusion = dbAllUser.getImg("intrusion.txt");
            mapKey["*"] = dbAllUser.getImg("reset.txt");
            mapKey["0"] = dbAllUser.getImg("0.txt");
            mapKey["1"] = dbAllUser.getImg("1.txt");
            mapKey["2"] = dbAllUser.getImg("2.txt");
            mapKey["3"] = dbAllUser.getImg("3.txt");
            mapKey["4"] = dbAllUser.getImg("4.txt");
            mapKey["5"] = dbAllUser.getImg("5.txt");
            mapKey["6"] = dbAllUser.getImg("6.txt");
            mapKey["7"] = dbAllUser.getImg("7.txt");
            mapKey["8"] = dbAllUser.getImg("8.txt");
            mapKey["9"] = dbAllUser.getImg("9.txt");
        }

        public void printKey(String key)
        {
            if (key != "#")
            {
                Console.WriteLine(mapKey[key]);
                Thread.Sleep(1000);
                Console.ResetColor();
                Console.Clear();
            }
        }

        private void comingProccess(String line)
        {
            String idComing;

            if (line.Split('=').Length >= 2)
            {
                idComing = line.Split('=')[1];
                if (AlarmIdx == true && idComing != "1234")
                {
                    Console.WriteLine(Intrusion);
                    Thread.Sleep(3000);
                    Console.ResetColor();
                    Console.Clear();
                    API.sendEmailAlert();
                }
                else if (AlarmIdx == true && idComing == "1234")
                {
                    Console.WriteLine(Disabled);
                    Thread.Sleep(3000);
                    Console.ResetColor();
                    Console.Clear();
                    AlarmIdx = false;
                }
                else if (AlarmIdx == false)
                {
                    if (dbAllUser.getUser(idComing) != "")
                    {
                        Console.WriteLine(Welcome);
                        Thread.Sleep(3000);
                        Console.ResetColor();
                        Console.Clear();
                        API.sendEmail(dbAllUser.getUser(idComing));
                    }
                    else
                    {
                        Console.WriteLine(WrongId);
                        Thread.Sleep(3000);
                        Console.ResetColor();
                        Console.Clear();
                    }
                }
            }
            else
            {
                if (AlarmIdx == true)
                {
                    Console.WriteLine(Intrusion);
                    Thread.Sleep(3000);
                    Console.ResetColor();
                    Console.Clear();
                    API.sendEmailAlert();
                }
                else
                {
                    Console.WriteLine(WrongId);
                    Thread.Sleep(3000);
                    Console.ResetColor();
                    Console.Clear();
                }
            }
        }

        private void exitProccess(String line)
        {
            String idExit;

            if (line.Split('=').Length >= 2)
            {
                idExit = line.Split('=')[1];
                if (AlarmIdx == false && idExit == "1234")
                {
                    Console.WriteLine(Alarm);
                    Thread.Sleep(3000);
                    Console.Clear();
                    AlarmIdx = true;
                    Console.WriteLine(GoodBye);
                    Thread.Sleep(3000);
                    Console.ResetColor();
                    Console.Clear();
                }
                else if (AlarmIdx == false && idExit != "1234")
                {
                    if (dbAllUser.getUser(idExit) != "")
                    {
                        Console.WriteLine(GoodBye);
                        Thread.Sleep(3000);
                        Console.ResetColor();
                        Console.Clear();
                        API.sendEmailExit(dbAllUser.getUser(idExit));
                    }
                    else
                    {
                        Console.WriteLine(WrongId);
                        Thread.Sleep(3000);
                        Console.ResetColor();
                        Console.Clear();
                    }
                }
                else if (AlarmIdx == true)
                {
                    Console.WriteLine(Intrusion);
                    Thread.Sleep(3000);
                    Console.ResetColor();
                    Console.Clear();
                    API.sendEmailAlert();
                }
            }
            else
            {
                if (AlarmIdx == false)
                {
                    Console.WriteLine(WrongId);
                    Thread.Sleep(3000);
                    Console.ResetColor();
                    Console.Clear();
                }
                else if (AlarmIdx == true)
                {
                    Console.WriteLine(Intrusion);
                    Thread.Sleep(3000);
                    Console.ResetColor();
                    Console.Clear();
                    API.sendEmailAlert();
                }
            }
        }

        public void run()
        {
            Thread goTo42sh = new Thread(new ThreadStart(this.tek42sh));
            loadText();
            SerialPort port = new SerialPort("COM3", 9600);
            port.Open();

            goTo42sh.Start();
            Console.Clear();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                String line = port.ReadLine();
                if (line.Contains("key=") == true)
                    printKey(line.Split('=')[1]);
                else if (line.Contains("coming=") == true)
                    comingProccess(line);
                else if (line.Contains("exit=") == true)
                    exitProccess(line);
            }
        }

        static void Main(string[] args)
        {
            Program mainMaster = new Program();

            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            mainMaster.run();
        }
    }
}
