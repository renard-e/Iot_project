using System;
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
            Console.Write("Id user (ex : 1234) : ");
            String id = Console.ReadLine();
            Console.Write("Name user (ex : tasoeur) : ");
            String name = Console.ReadLine();
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(id) || name.Contains(";") || checkDigit(id))
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
                Console.Write("Welcome to 42sh : tape \"add\" to add a new user or \"remove\" to remove an existing user or \"get\" to get all users infos : ");
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

        public void run()
        {
            Thread goTo42sh = new Thread(new ThreadStart(this.tek42sh));

            goTo42sh.Start();
            while (true)
            {
                //need just de read la sortie et d'envoyer le nom a API
                API.sendEmail(dbAllUser.getUser("17"));
                Thread.Sleep(30000);
            }
        }

        static void Main(string[] args)
        {
            Program mainMaster = new Program();

            mainMaster.run();
        }
    }
}
