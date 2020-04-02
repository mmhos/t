using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace t.cm
{
    class ic
    {
        // Length of a line
        public MySqlConnection conn;
        public  ic()
        {
            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = "166.62.74.162";
            conn_string.Port = 3306;
            conn_string.UserID = "remote";
            conn_string.Password = "ghorar1Dim";
            conn_string.Database = "myinvent";



           conn = new MySqlConnection(conn_string.ToString());
            
        }
        
    }
    }
