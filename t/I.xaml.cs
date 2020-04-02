using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RowDefinition = Xamarin.Forms.RowDefinition;

namespace t
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class I : ContentPage
    {
        public I()
        {
            InitializeComponent();
            MySqlConnection  c = new cm.ic().conn;


            try
            {
                c.Open();
                lab.Text = "Open";
                
                ;
            }
            catch (Exception ex)
            {
                lab.Text = ex.Message;
            }

            string query = "SELECT * FROM inventory  where id  <"+ Constants.ItemNumberForDisplay;
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, c);
            var reader = cmd.ExecuteReader();
            var layout = new StackLayout();
            var button = new Button
            {
                Text = "StackLayout",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            

            
            
            var h = 0;
            //var sc = new Label { Text = "asfda" };
            while (reader.Read())
            {
                var clab = new Label { Text = "Item " + h+"  " };
                var someValue = reader["flItem"]
                                + " Location : "
                                + reader["fllocation"]
                                + " Agency :  "
                                + reader["flagency"];
                
                //lab.Text = lab.Text + someValue;
                ////// Do something with someValue
                ////var sc = new Label { Text = "lab.Text"};
                ////sc.Text = ""+someValue;
                clab.Text = clab.Text + someValue;

                h += 1;
                layout.Children.Add(clab);
            }
            //grid.Children.Add(sc, 1, 1);
            c.Close(); 
            layout.Spacing = 20;
            Content = layout;
            //    string serverIp = "166.62.74.162";
            //    string username = "remote";
            //    string password = "ghorar1Dim";
            //    string databaseName = "myinvent";

            //    string dbConnectionString = string.Format("server={0};uid={1};pwd={2};database={3};", serverIp, username, password, databaseName);
            //    string query = "SELECT * FROM inventory";

            //    var conn = new MySql.Data.MySqlClient.MySqlConnection(dbConnectionString);
            //    conn.Open();

            //    var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
            //    var reader = cmd.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        var someValue = reader["SomeColumnName"];
            //        lab.Text = lab.Text + someValue;
            //        // Do something with someValue
            //    }
            //}
        }
    }
}