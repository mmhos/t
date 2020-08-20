using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

               
              
namespace t

{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class adqr : ContentPage
    {

        public static ObservableCollection<string> items { get; set; }
        public adqr()
        {
            //if (Constants.branch == "Transfer")
            //{
            //    Content.IsVisible = false;
               


            //}
            //else if (Constants.branch == "Add") { Content.IsVisible = true; }
            InitializeComponent();
            ld();
            //if (Constants.branch == "Transfer")
            //{
               
                


            //}

        }
        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
#if __ANDROID__
	// Initialize the scanner first so it can track the current context
	MobileBarcodeScanner.Initialize (Application);
#endif
            Button btn = sender as Button;
            string s = btn.Text;
            
            {
                try
                {


                    var overlay = new ZXingDefaultOverlay
                    {
                        TopText = "Please scan QR code",
                        BottomText = "Align the QR code within the frame"
                    };

                    var QRScanner = new ZXingScannerPage();

                    Navigation.PushModalAsync(QRScanner);

                    QRScanner.OnScanResult += (result) =>
                    {
                    // Stop scanning
                    QRScanner.IsScanning = false;

                    // Pop the page and show the result
                    Device.BeginInvokeOnMainThread(async () =>
                        {
                            Navigation.PopModalAsync(true);
                            await DisplayAlert("Current destination", s, "OK");
                            Constants.currentLocation = s;
                            await DisplayAlert("Scanned Barcode", result.Text, "OK");



                            string phrase = result.Text;
                            string[] words = phrase.Split(new string[] { " ll " }, StringSplitOptions.None); ;
                            int counter = 0;
                            int counter2 = 0;
                            string[] qrdata = { "abra", "cad", "abra" };
                            foreach (var word in words)
                            {

                                if ((counter % 2) == 1)
                                {
                                // DisplayAlert(counter.ToString(), word, "OK");

                                qrdata[counter2] = word;
                                    counter2 = counter2 + 1;
                                }
                                counter = counter + 1;

                            }
                        //items = new ObservableCollection<string>() { "Speaker", "Pen", "Lamp", "Monitor", "Bag", "Book", "Cap", "Tote", "Floss", "Phone" };
                        ////DisplayAlert("Scanned Infos", qrdata[0]+ qrdata[1]+qrdata[2], "OK");
                        //// var listView = new ListView { ... SelectionMode = ListViewSelectionMode.None };
                        //ListView lstView = new ListView();
                        //lstView.ItemsSource = items;
                        ////UniqueItemIdentifier uii = new UniqueItemIdentifier();
                        ////AddItem(uii);
                        ///
                        string[] destination = { "New York", "Jamaica","Warehouse", "Atlantic Terminal", "Newark","Trenton",





            "NJT NY",





            "Secaucus",





            "Grand Central Terminal",





            "The National WWII Museum",







            "Metro North",





            "Metro Park" };

                            var dest = await DisplayActionSheet("Select the transfer destination", "Abort the transfer", "x", destination);
                            DBConnect co = new DBConnect();

                            if (co.OpenConnection() == true)
                            {
                                if (Constants.branch == "Transfer")
                                {
                                    string query = "Update inventory Set flquantity = flquantity-1 where fllocation = '" + s + "' AND flItem ='" + qrdata[1] + "' AND flsolarinumb = " + qrdata[0] + " AND fldesc = '" + qrdata[2] + "'";
                                    var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, co.connection);
                                    await DisplayAlert("Scanned Infos", cmd.CommandText, "OK");
                                    cmd.ExecuteNonQuery();

                                    query = "Update inventory Set flquantity = flquantity+1  where fllocation = '" + dest + "' AND flItem ='" + qrdata[1] + "' AND flsolarinumb = " + qrdata[0] + " AND fldesc = '" + qrdata[2] + "'";
                                    cmd = new MySql.Data.MySqlClient.MySqlCommand(query, co.connection);
                                    await DisplayAlert("Scanned Infos", cmd.CommandText, "OK");
                                    cmd.ExecuteNonQuery();
                                    //string[] values = { "fllocation", "flagency", "flcond", "flsolarinumb", "flmodel", "fldesc" };


                                }
                                else
                                {
                                    
                                    {
                                       
                                        string query = "SELECT * FROM inventory  where fllocation = '" + s + "' AND flItem ='" + qrdata[1] + "' AND flsolarinumb = " + qrdata[0] + " AND fldesc = '" + qrdata[2] + "'";
                                        var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, co.connection);
                                        var reader = cmd.ExecuteReader();
                                        var coun = 0;

                                        while (reader.Read())
                                        {
                                            coun = coun + 1;

                                        }
                                        co.connection.Close();
                                        co = new DBConnect();
                                        co.OpenConnection();
                                        if (coun == 0)
                                        {
                                            string sn = await DisplayPromptAsync("Required info", "Enter the shelf number");
                                            string sc = await DisplayPromptAsync("Required info", "Enter the shelf column number");
                                            string sr = await DisplayPromptAsync("Required info", "Enter the shelf row number");
                                            string sd = await DisplayPromptAsync("Required info", "Enter the shelf depth");
                                            // string emp = await DisplayPromptAsync("Required info", "Type your name?");

                                            query = query = " INSERT INTO inventory (fllocation,flItem,fldesc,flsolarinumb,shelfNumber,shelfColumn,shelfRow,shelfDepth,flquantity) VALUES('" + s + "', '" + qrdata[1] + "', '" + qrdata[2] + "', '" + qrdata[0] + "', " + sn + ", " + sc + ", " + sr + ", " + sd + "," + 1 + ")";
                                            cmd = new MySql.Data.MySqlClient.MySqlCommand(query, co.connection);
                                            await DisplayAlert("Scanned Infos", cmd.CommandText, "OK");
                                            cmd.ExecuteNonQuery();
                                            co.connection.Close();
                                        }
                                        else
                                        {
                                            query = "Update inventory Set flquantity = flquantity+1  where fllocation = '" + s + "' AND flItem ='" + qrdata[1] + "' AND flsolarinumb = " + qrdata[0] + " AND fldesc = '" + qrdata[2] + "'";
                                            cmd = new MySql.Data.MySqlClient.MySqlCommand(query, co.connection);
                                            await DisplayAlert("Scanned Infos", cmd.CommandText, "OK");
                                            cmd.ExecuteNonQuery();
                                            co.connection.Close();


                                        }
                                        await DisplayAlert("Number of items", coun.ToString(), "OK");
                                        Constants.branch = "Transfer";
                                    }


                                }
                            co.connection.Close();

                            }
                        });

                    };

                }
                catch (Exception ex)
                {
                    //GlobalScript.SeptemberDebugMessages("ERROR", "BtnScanQR_Clicked", "Opening ZXing Failed: " + ex);
                }
            }
        }
        public void AddItem(UniqueItemIdentifier ui)
        {

            DBConnect addconn = new DBConnect();
            if (addconn.OpenConnection() == true)
            {
                string query = "SELECT EXISTS (SELECT * FROM inventory  where flcustomer =" + ui.customer + " AND flagency =" + ui.agency + " AND  flsolarinumb = " + ui.solnum + ")";
                //query = "SELECT EXISTS(SELECT * FROM inventory  where  flsolarinumb = "+ui.solnum+")";
                var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, addconn.connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DisplayAlert("Can read", reader[0].ToString(), "ok");
                }
            }
        }
        public void ld() {

            {
                try
                {


                    var overlay = new ZXingDefaultOverlay
                    {
                        TopText = "Please scan QR code",
                        BottomText = "Align the QR code within the frame"
                    };

                    var QRScanner = new ZXingScannerPage();

                    Navigation.PushModalAsync(QRScanner);

                    QRScanner.OnScanResult += (result) =>
                    {
                        // Stop scanning
                        QRScanner.IsScanning = false;

                        // Pop the page and show the result
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            Navigation.PopModalAsync(true);
                           
                            await DisplayAlert("Scanned Barcode", result.Text, "OK");



                            string phrase = result.Text;
                            string[] words = phrase.Split(new string[] { " ll " }, StringSplitOptions.None); ;
                            int counter = 0;
                            int counter2 = 0;
                            string[] qrdata = { "abra", "cad", "abra" };
                            foreach (var word in words)
                            {

                                if ((counter % 2) == 1)
                                {
                                    // DisplayAlert(counter.ToString(), word, "OK");

                                    qrdata[counter2] = word;
                                    counter2 = counter2 + 1;
                                }
                                counter = counter + 1;

                            }
                            List<String> possibleLocations = new List<string> { };
                            DBConnect co = new DBConnect();
                            if (co.OpenConnection() == true)
                            {
                                string query = "SELECT * FROM inventory  where  flItem ='" + qrdata[1] + "' AND flsolarinumb = " + qrdata[0] + " AND fldesc = '" + qrdata[2] + "'";
                                if (Constants.branch == "Add") { query = "SELECT DISTINCT fllocation FROM inventory "; }
                                var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, co.connection);
                                var reader = cmd.ExecuteReader();

                                while (reader.Read())
                                {

                                    possibleLocations.Add("" + reader["fllocation"]);



                                }


                                co.connection.Close();

                            }

                            var s = await DisplayActionSheet("Select the current location of the item ", "Abort the transfer", "x", possibleLocations.ToArray());
                            if (s == "Abort the transfer") { await Navigation.PushAsync(new adit()); return; }
                            string[] destination = { "New York", "Jamaica","Warehouse", "Atlantic Terminal", "Newark","Trenton",





            "NJT NY",





            "Secaucus",





            "Grand Central Terminal",





            "The National WWII Museum",







            "Metro North",





            "Metro Park" };
                            var dest = "";
                            if (Constants.branch == "Transfer") { 
                            dest = await DisplayActionSheet("Select the transfer destination", "Abort the transfer", "x", destination);}
                            if (dest == "Abort the transfer") { await Navigation.PushAsync(new adit()); return; }
                            co = new DBConnect();

                            if (co.OpenConnection() == true)
                            {
                                if (Constants.branch == "Transfer")
                                {
                                    string query = "Update inventory Set flquantity = flquantity-1 where fllocation = '" + s + "' AND flItem ='" + qrdata[1] + "' AND flsolarinumb = " + qrdata[0] + " AND fldesc = '" + qrdata[2] + "'";
                                    var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, co.connection);
                                    await DisplayAlert("Scanned Infos", cmd.CommandText, "OK");
                                    cmd.ExecuteNonQuery();

                                    query = "Update inventory Set flquantity = flquantity+1  where fllocation = '" + dest + "' AND flItem ='" + qrdata[1] + "' AND flsolarinumb = " + qrdata[0] + " AND fldesc = '" + qrdata[2] + "'";
                                    cmd = new MySql.Data.MySqlClient.MySqlCommand(query, co.connection);
                                    await DisplayAlert("Scanned Infos", cmd.CommandText, "OK");
                                    cmd.ExecuteNonQuery();
                                    //string[] values = { "fllocation", "flagency", "flcond", "flsolarinumb", "flmodel", "fldesc" };


                                }
                                else
                                {

                                    {

                                        string query = "SELECT * FROM inventory  where fllocation = '" + s + "' AND flItem ='" + qrdata[1] + "' AND flsolarinumb = " + qrdata[0] + " AND fldesc = '" + qrdata[2] + "'";
                                        var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, co.connection);
                                        var reader = cmd.ExecuteReader();
                                        var coun = 0;

                                        while (reader.Read())
                                        {
                                            coun = coun + 1;

                                        }
                                        co.connection.Close();
                                        co = new DBConnect();
                                        co.OpenConnection();
                                        if (coun == 0)
                                        {
                                            string sn = await DisplayPromptAsync("Required info", "Enter the shelf number","Ok","Cancel");
                                            if (sn == "Cancel") { await Navigation.PushAsync(new adit()); return; }
                                            string sc = await DisplayPromptAsync("Required info", "Enter the shelf column number","Ok", "Cancel");
                                            if (sc == "Cancel") { await Navigation.PushAsync(new adit()); return; }
                                            string sr = await DisplayPromptAsync("Required info", "Enter the shelf row number","Ok", "Cancel");
                                            if (sr == "Cancel") { await Navigation.PushAsync(new adit()); return; }
                                            string sd = await DisplayPromptAsync("Required info", "Enter the shelf depth","Ok", "Cancel");
                                            if (sd == "Cancel") { await Navigation.PushAsync(new adit()); return; }
                                            // string emp = await DisplayPromptAsync("Required info", "Type your name?");

                                            query = query = " INSERT INTO inventory (fllocation,flItem,fldesc,flsolarinumb,shelfNumber,shelfColumn,shelfRow,shelfDepth,flquantity) VALUES('" + s + "', '" + qrdata[1] + "', '" + qrdata[2] + "', '" + qrdata[0] + "', " + sn + ", " + sc + ", " + sr + ", " + sd + "," + 1 + ")";
                                            cmd = new MySql.Data.MySqlClient.MySqlCommand(query, co.connection);
                                            await DisplayAlert("Scanned Infos", cmd.CommandText, "OK");
                                            cmd.ExecuteNonQuery();
                                            co.connection.Close();
                                        }
                                        else
                                        {
                                            query = "Update inventory Set flquantity = flquantity+1  where fllocation = '" + s + "' AND flItem ='" + qrdata[1] + "' AND flsolarinumb = " + qrdata[0] + " AND fldesc = '" + qrdata[2] + "'";
                                            cmd = new MySql.Data.MySqlClient.MySqlCommand(query, co.connection);
                                            await DisplayAlert("Scanned Infos", cmd.CommandText, "OK");
                                            cmd.ExecuteNonQuery();
                                            co.connection.Close();


                                        }
                                        await DisplayAlert("Number of items", coun.ToString(), "OK");
                                        Constants.branch = "Transfer";
                                    }


                                }
                                co.connection.Close();

                            }
                        });

                    };

                }
                catch (Exception ex)
                {
                    //GlobalScript.SeptemberDebugMessages("ERROR", "BtnScanQR_Clicked", "Opening ZXing Failed: " + ex);
                }
            }


        }
    }
     

    //async void OnActionSheetSimpleClicked(object sender, EventArgs e)
    //{
    //    string action = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Email", "Twitter", "Facebook");
    //    Debug.WriteLine("Action: " + action);
    //}


}