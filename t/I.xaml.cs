
using System;

using MySql.Data.MySqlClient;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace t
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class I : ContentPage
    {
        MySqlConnection con;
        public I()
        {
            InitializeComponent();
            //using (var connection = new MySqlConnection("Server=166.62.74.162;User ID=remote;Password=ghorar1Dim;Database=mydataba"))
            //    //{
            //    //    connection.Open();

            //    //    using (var command = new MySqlCommand("SELECT field FROM table;", connection))
            //    //    using (var reader = command.ExecuteReader())
            //    //        while (reader.Read())
            //    //            Console.WriteLine(reader.GetString(0));
            //    //}
                MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = "166.62.74.162";
            conn_string.Port = 3306;
            conn_string.UserID = "remote";
            conn_string.Password = "ghorar1Dim";
            conn_string.Database = "myinvent";


            //server = "localhost";
            //database = "connectcsharptomysql";
            //uid = "username";
            //password = "password";
            //string connectionString;
            //connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            //database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            //connection = new MySqlConnection(connectionString);
            DBConnect conn = new DBConnect();
            //con = new MySqlConnection(conn_string.ToString());
            
            

            
            if (conn.OpenConnection()== true) 
            {   
                string query = "SELECT * FROM inventory  where id  <" + (Constants.ItemNumberForDisplay + 1);
                var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn.connection);
                var reader = cmd.ExecuteReader();
                con = conn.connection;
                var layout = new StackLayout();
                var lbl = new Label { Text = "Change the number of item to display " };
                var itn = new Entry { Text = Constants.ItemNumberForDisplay.ToString() };
                itn.TextChanged += E_T;
                layout.Children.Add(lbl);
                layout.Children.Add(itn);
                var lbl2 = new Label { Text = "Change the number of attributes  to display " };
                var itn2 = new Entry { Text = Constants.AttributeNumberForDisplay.ToString() };
                itn2.TextChanged += E_Ta;
                layout.Children.Add(lbl2);
                layout.Children.Add(itn2);


                var button = new Button
                {
                    Text = "StackLayout",
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                layout.Children.Add(button);



                var h = 0;
                //var sc = new Label { Text = "asfda" };


                string[] values = { "flItem", "flItem", "fllocation", "flagency", "flcond", "flsolarinumb", "flmodel", "fldesc" };


                while (reader.Read())
                {
                    var clab = new Label { Text = "Item " + h + "  " };
                    var someValue = "";
                    for (int i = 0; i <= Constants.AttributeNumberForDisplay; i = i + 1)
                    {
                        someValue = someValue + values[i] + " " + reader[values[i]] + "  ";
                    }




                    clab.Text +=  someValue;

                    h += 1;
                    layout.Children.Add(clab);
                }
                //grid.Children.Add(sc, 1, 1);
                layout.Spacing = 20;

                var li = new ToolbarItem { Text = "Logout " };
                li.Clicked += Li_Clicked;

                ScrollView scrollView = new ScrollView();
                scrollView.Content = layout;
                Content = scrollView;
                //Di(reader);
                reader.Close();
                //con.Close();

            }
        }

        public void Di(MySqlDataReader r)
        {
            Content = null;
            var reader = r;
            var layout = new StackLayout();
            var lbl = new Label { Text = "Change the number of item to display " };
            var itn = new Entry { Text = Constants.ItemNumberForDisplay.ToString()};
            itn.TextChanged += E_T;
            layout.Children.Add(lbl);
            layout.Children.Add(itn);
            var lbl2 = new Label { Text = "Change the number of attributes  to display " };
            var itn2 = new Entry { Text = Constants.AttributeNumberForDisplay.ToString() };
            itn2.TextChanged += E_Ta;
            layout.Children.Add(lbl2);
            layout.Children.Add(itn2);
            
           
            var button = new Button
            {
                Text = "StackLayout",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            layout.Children.Add(button);



            var h = 0;
            //var sc = new Label { Text = "asfda" };


            string[] values = {"flItem","flItem","fllocation","flagency","flcond","flsolarinumb","flmodel","fldesc" };


            while (reader.Read())
            {
                var clab = new Label { Text = "Item " + h + "  " };
                var someValue = " ";
                for (int i = 0; i <= Constants.AttributeNumberForDisplay; i +=1)
                {
                    someValue = someValue +values[i]+" "+ reader[values[i]]+"  ";
                }

               


                clab.Text = clab.Text + someValue;

                h += 1;
                layout.Children.Add(clab);
            }
            //grid.Children.Add(sc, 1, 1);
            layout.Spacing = 20;
            
            var li = new ToolbarItem { Text = "Logout " };
            li.Clicked += Li_Clicked;

            ScrollView scrollView = new ScrollView();
            scrollView.Content = layout;
            Content = scrollView;
            reader.Close();
        }

        private void Li_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Ld(MySqlConnection c) {
            string query = "SELECT * FROM inventory  where id  <" +( Constants.ItemNumberForDisplay+1);
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, c);
            var reader = cmd.ExecuteReader();
            Di(reader);
            reader.Close();
        }
        void E_T(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == "") { return; }
            Constants.ItemNumberForDisplay = Convert.ToInt32(e.NewTextValue);
            Ld(con);
        }
        void E_Ta(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == "") { return; }
            Constants.AttributeNumberForDisplay = Convert.ToInt32(e.NewTextValue);
            Ld(con);
        }
        void Li_Clicked(object sender, TextChangedEventArgs e)
        {
            App.IsUserLoggedIn = false;
            Navigation.InsertPageBefore(new LoginPage(), this);
        }
        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }
    }
    
}