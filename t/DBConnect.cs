using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace t
{
    class DBConnect
    {
        
            public MySqlConnection connection;
            private string server;
            private string database;
            private string uid;
            private string password;

            //Constructor
            public DBConnect()
            {
                Initialize();
            }

            //Initialize values
            private void Initialize()
            {
                server = "166.62.74.162";
                database = "myinvent";
            uid = "remote";
                password = "ghorar1Dim";
            string connectionString;
                connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

                connection = new MySqlConnection(connectionString);
            }

            //open connection to database
            public  bool OpenConnection()
            {

            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.Write("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.Write("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

            //Close connection
            private bool CloseConnection()
            {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }

            //Insert statement
            public void Insert()
            {
            }

            //Update statement
            public void Update()
            {
            }

            //Delete statement
            public void Delete()
            {
            }

            //Select statement
            //public List<string>[] Select()
            //{
            //}

            //Count statement
            //public int Count()
            //{
            //}

            //Backup
            public void Backup()
            {
            }

            //Restore
            public void Restore()
            {
            }
        }
    
}
