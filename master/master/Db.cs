using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master
{
    class Db
    {
        private String directory = @"c:\master\";
        private String file = "master.csv";
        private Boolean createdDir = false;
        private Boolean createdFile = false;

        public Db()
        {
            Console.WriteLine("DB CREATION");
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            if (Directory.Exists(directory))
            {
                createdDir = true;
                if (!File.Exists(directory + file))
                    File.Create(directory + file);
                if (File.Exists(directory + file))
                    createdFile = true;
            }
            Console.WriteLine("DB CREATED");
        }

        private Boolean checkUser(String id)
        {
            try
            {
                StreamReader inFile = new StreamReader(directory + file);
                String line;

                while ((line = inFile.ReadLine()) != null)
                {
                    if (line.Split(';')[0] == id)
                    {
                        inFile.Close();
                        return (true);
                    }
                }
                inFile.Close();
                return (false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return (true);
            }
        }

        public String getNameUser(String id)
        {
            try
            {
                StreamReader inFile = new StreamReader(directory + file);
                String line;

                while ((line = inFile.ReadLine()) != null)
                {
                    if (line.Split(';')[0] == id)
                    {
                        inFile.Close();
                        return (line.Split(';')[1]);
                    }
                }
                inFile.Close();
                return ("");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR GET NAME USER = " + ex.ToString());
                return ("");
            }
        }

        public String getUser(String Id)
        {
            if (this.checkUser(Id) == false)
                return ("");
            else
                return (getNameUser(Id));
        }

        public Boolean addUser(String name, String id)
        {
            if (this.checkUser(id) == true)
            {
                Console.WriteLine("Error : user id : " + id + " already exist.");
                return (false);
            }
            try
            {
                StreamWriter outFile = new StreamWriter(directory + file, true);

                outFile.WriteLine(id + ";" + name);
                outFile.Close();
                return (true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return (false);
            }
        }

        public Boolean removeUser(String id)
        {
            if (this.checkUser(id) == false)
            {
                Console.WriteLine("Error : user id : " + id + " doesn't exist.");
                return (false);
            }
            try
            {
                StreamReader reader = new StreamReader(directory + file);

                String[] allFile = reader.ReadToEnd().Split('\n');
                String newAllFile = "";

                foreach (String str in allFile)
                {
                    if (String.IsNullOrEmpty(str) != true && str.Split(';')[0] != id)
                        newAllFile += str + "\n";
                }
                newAllFile = newAllFile.Remove(newAllFile.Length - 1);
                reader.Close();
                StreamWriter writer = new StreamWriter(directory + file, false);

                writer.WriteLine(newAllFile);
                writer.Close();
                return (true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return (false);
            }
        }

        public String getAllUsersInfos()
        {
            try
            {
                StreamReader reader = new StreamReader(directory + file);
                String[] allFile = reader.ReadToEnd().Split('\n');
                String formatedData = "";

                reader.Close();
                foreach (String str in allFile)
                {
                    if (!String.IsNullOrEmpty(str) && str.Contains(";"))
                        formatedData += "Id = " + str.Split(';')[0] + " Name = " + str.Split(';')[1] + "\n";
                }
                formatedData = formatedData.Remove(formatedData.Length - 1);
                return (formatedData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return ("");
            }
        }

        public String getImg(String nameFile)
        {
            try
            {
                StreamReader reader = new StreamReader(directory + nameFile);
                String allFile = reader.ReadToEnd();

                reader.Close();
                return (allFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return ("");
            }
        }
    }
}
